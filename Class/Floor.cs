namespace ProjSuperClean.Class;

using System.Diagnostics.Metrics;
using ProjSuperClean.Utils;

public class Floor
{
    public string FloorName { get; set; }
    public List<Room> Rooms { get; set; }

    public Floor(string floorName)
    {
        FloorName = floorName;
        Rooms = new List<Room>();
    }

    public Floor() { }



    public void AddRoom(Room room)
    {
        Rooms.Add(room);
    }

    public void RemoveRoom(Room room)
    {
        Rooms.Remove(room);
    }

    public static void ChangeNameRoom(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        ListRoom(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe o nome da área que gostaria de alterar (ex: 'Quarto casal'): ");
        string roomName = Console.ReadLine()?.Trim();

        Room roomToEdit = null;

        foreach (var floor in user.Residence.ResidenceFloors)
        {
            roomToEdit = floor.Rooms.FirstOrDefault(r => r.RoomName.Equals(roomName, StringComparison.Ordinal));
            if (roomToEdit != null)
                break;
        }

        if (roomToEdit == null)
        {
            Utils.PrintErrorMessage("Área não encontrada. Verifique o nome e tente novamente.");
            Utils.WaitForUser();
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o novo nome da área (ex: 'Suite Casal'): ");
        string newRoomName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newRoomName))
        {
            Utils.PrintErrorMessage("Nome da área inválido!");
            return;
        }

        if (user.Residence.ResidenceFloors.Any(floor =>
            floor.Rooms.Any(room => room.RoomName.Equals(newRoomName, StringComparison.Ordinal))))
        {
            Utils.PrintErrorMessage($"O nome '{newRoomName}' já está atribuído a outra área.");
            return;
        }

        roomToEdit.RoomName = newRoomName;
        User.SaveUsersToFile();
        Utils.PrintSucessMessage("Nome da área alterado com sucesso!");
    }


    public static void ListRoom(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Console.Clear();
        Console.WriteLine("==================================================");
        Console.WriteLine($"<<< Listagem de Áreas - Utilizador: {utilizador} >>>");
        Console.WriteLine("==================================================");
        Console.WriteLine();

        int counter = 1;

        if (user.Residence.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
        {
            foreach (var floor in user.Residence.ResidenceFloors)
            {
                Console.WriteLine($"> Piso: {floor.FloorName}");
                Console.WriteLine();

                if (floor.Rooms != null && floor.Rooms.Count > 0)
                {
                    foreach (var room in floor.Rooms)
                    {
                        string counterFormatted = counter.ToString("D2");
                        Console.WriteLine($"   {counterFormatted} - {room.RoomName}");
                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine("   Nenhuma área cadastrada neste piso.");
                }

                Console.WriteLine();
            }

            Utils.PrintSucessMessage("Fim da listagem das áreas.");
            Console.WriteLine();
        }
        else
        {
            Utils.PrintErrorMessage("Nenhum piso ou área cadastrada na residência.");
        }

        Console.WriteLine("==================================================");
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

}
