using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Security.Cryptography.X509Certificates;

namespace ProjSuperClean.Class;

public class Room
{

    public string RoomName { get; set; }
    public int CleanTime { get; set; }

    public DateTime DayClean { get; set; }
    public int CleanInterval { get; set; }

    public Guid RoomId { get; set; }
    public DateTime NextClean { get; set; }

    public static List<Room> rooms = new List<Room>();

    public Room(string name, int cleanTime, int cleanInterval)
    {


        RoomName = name;
        CleanTime = cleanTime;
        CleanInterval = cleanInterval > 0 ? cleanInterval : throw new ArgumentException("O intervalo de limpeza deve ser positivo.");
        RoomId = Guid.NewGuid();

        DayClean = DateTime.Today;

        NextClean = DayClean.AddDays(CleanInterval);

    }

    public Room() { }

    public static void CleanDone(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId && u.Username == utilizador);
        if (user == null)
        {
            Console.WriteLine("Utilizador não encontrado.");
            return;
        }

        if (user.Residence == null || !user.Residence.ResidenceFloors.Any())
        {
            Console.WriteLine("Este utilizador não possui uma residência.");
            return;
        }

        Console.WriteLine("Áreas disponíveis para limpeza ordenadas por vencimento:");
        Console.WriteLine();

        var rooms = user.Residence.ResidenceFloors.SelectMany(f => f.Rooms).OrderBy(r => r.NextClean).ToList();
        for (int i = 0; i < rooms.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {rooms[i].RoomName} - vencimento dia: {rooms[i].NextClean:dd/MM/yyyy}");
        }

        Console.WriteLine();

        Console.WriteLine("Informe o número da área que deseja limpar (ou digite 'fim' para retornar):");
        string codeClean = Console.ReadLine();

        if (codeClean.ToLower() == "fim")
        {
            return;
        }

        if (!int.TryParse(codeClean, out int roomIndex) || roomIndex < 1 || roomIndex > rooms.Count)
        {
            Console.WriteLine("Entrada inválida.");
            return;
        }

        var selectedRoom = rooms[roomIndex - 1];

        selectedRoom.DayClean = DateTime.Now;
        selectedRoom.NextClean = selectedRoom.DayClean.AddDays(selectedRoom.CleanInterval);

        Console.WriteLine($"Limpeza concluída na sala: {selectedRoom.RoomName}");
        Console.WriteLine($"Próxima limpeza agendada para: {selectedRoom.NextClean:dd/MM/yyyy}");
    }


    public static void DeleteLastClean(Guid userId, string utilizador)
    {

        var user = User.users.FirstOrDefault(u => u.UserId == userId && u.Username == utilizador);
        if (user == null)
        {
            Console.WriteLine("Utilizador não encontrado.");
            return;
        }

        if (user.Residence == null || !user.Residence.ResidenceFloors.Any())
        {
            Console.WriteLine("Este utilizador não possui uma residência.");
            return;
        }

        Console.WriteLine("Áreas disponíveis para excluir a última limpeza, ordenadas por vencimento:");
        Console.WriteLine();

        var rooms = user.Residence.ResidenceFloors.SelectMany(f => f.Rooms).OrderBy(r => r.NextClean).ToList();
        for (int i = 0; i < rooms.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {rooms[i].RoomName} - vencimento dia: {rooms[i].NextClean:dd/MM/yyyy}");
        }

        Console.WriteLine();

        Console.WriteLine("Informe o número da área que deseja excluir a última limpeza (ou digite 'fim' para retornar):");
        string codeClean = Console.ReadLine();

        if (codeClean.ToLower() == "fim")
        {
            return;
        }

        if (!int.TryParse(codeClean, out int roomIndex) || roomIndex < 1 || roomIndex > rooms.Count)
        {
            Console.WriteLine("Entrada inválida.");
            return;
        }


        var selectedRoom = rooms[roomIndex - 1];

        if (selectedRoom != null)
        {
            DateTime previousClean = selectedRoom.DayClean.AddDays(-selectedRoom.CleanInterval);

            selectedRoom.DayClean = previousClean;
            selectedRoom.NextClean = previousClean.AddDays(selectedRoom.CleanInterval);

            Console.WriteLine($"Removida a última limpeza da sala: {selectedRoom.RoomName}");
            Console.WriteLine($"Data da última limpeza restaurada para: {selectedRoom.DayClean:dd/MM/yyyy}");
            Console.WriteLine($"Próxima limpeza ajustada para: {selectedRoom.NextClean:dd/MM/yyyy}");
        }
        else
        {
            Console.WriteLine("Sala não encontrada.");
        }

    }


    public static int[] GenerationPypes(Room room, DateTime today)
    {
        int greenPipe = 0, yellowPipe = 0, redPipe = 0;

        DateTime dayClean = room.DayClean;
        DateTime nextClean = dayClean.AddDays(room.CleanInterval);

        int daysSinceClean = (today - dayClean).Days;
        int totalBars = Math.Min(Math.Abs(daysSinceClean), 20);

        if (daysSinceClean <= room.CleanInterval - 1)
        {
            greenPipe = totalBars;
        }
        else if (daysSinceClean <= room.CleanInterval + 2)
        {
            greenPipe = room.CleanInterval - 1;
            yellowPipe = Math.Min(daysSinceClean - greenPipe, totalBars - greenPipe);
        }
        else
        {
            greenPipe = room.CleanInterval - 1;
            yellowPipe = 2;
            redPipe = totalBars - (greenPipe + yellowPipe);
        }

        greenPipe = Math.Abs(greenPipe);
        yellowPipe = Math.Abs(yellowPipe);
        redPipe = Math.Abs(redPipe);


        return new int[] { greenPipe, yellowPipe, redPipe };
    }


}


