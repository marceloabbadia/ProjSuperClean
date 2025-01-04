using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using static ProjSuperClean.Utils.Utils;

namespace ProjSuperClean.Class;

public class Room
{

    public string RoomName { get; set; }
    public int CleanTime { get; set; }

    public DateTime DayClean { get; set; }
    public int CleanInterval { get; set; }

    public Guid RoomId { get; set; }
    public DateTime NextClean { get; set; }

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


    //Realizar limpeza do Room
    public static void CleanDone(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId && u.Username == utilizador);
        if (user == null)
        {
            Console.WriteLine($"Utilizador '{utilizador}' com ID '{userId}' não encontrado.");
            return;
        }

        if (user.Residence == null || user.Residence.ResidenceFloors == null || !user.Residence.ResidenceFloors.Any())
        {
            Console.WriteLine("Este utilizador não possui uma residência ou andares cadastrados.");
            return;
        }

        Console.WriteLine("Áreas disponíveis para limpeza ordenadas por vencimento:");
        Console.WriteLine();

        var rooms = user.Residence.ResidenceFloors
                     .Where(f => f.Rooms != null)
                     .SelectMany(f => f.Rooms)
                     .Where(r => r != null)
                     .OrderBy(r => r.NextClean)
                     .ToList();

        for (int i = 0; i < rooms.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {rooms[i].RoomName} - vencimento dia: {rooms[i].NextClean:dd/MM/yyyy}");
        }

        Console.WriteLine();

        if (!rooms.Any())
        {
            Console.WriteLine("Não há áreas disponíveis para limpeza.");
            return;
        }

        string codeClean;
        int roomIndex;

        do
        {
            Console.WriteLine("Informe o número da área que deseja limpar (ou digite 'fim' para retornar):");
            codeClean = Console.ReadLine();

            if (codeClean.ToLower() == "fim")
                return;

        } while (!int.TryParse(codeClean, out roomIndex) || roomIndex < 1 || roomIndex > rooms.Count);

        var selectedRoom = rooms[roomIndex - 1];

        selectedRoom.DayClean = DateTime.Now;
        selectedRoom.NextClean = selectedRoom.DayClean.AddDays(selectedRoom.CleanInterval);

        Console.WriteLine($"Limpeza concluída na sala: {selectedRoom.RoomName}");
        Console.WriteLine($"Próxima limpeza agendada para: {selectedRoom.NextClean:dd/MM/yyyy}");
    }

    //Remover ultima limpeza do room
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


    // Gerar barras de status da limpeza
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


        return [greenPipe, yellowPipe, redPipe];
    }



    // Mudar nome Room
    public static void ChangeNameRoom(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        if (RoomsList(userId, utilizador) == null || user.Residence.ResidenceFloors.All(floor => floor.Rooms.Count == 0))
        {
            Console.WriteLine("Sua residência não possui nenhuma área cadastrada. Você será direcionado para criar uma área.");
            AddRoomUser(userId, utilizador);
            return;
        }

        while (true)
        {
            RoomsListConsole(userId, utilizador);

            Console.WriteLine();
            Console.WriteLine("Informe o nome da área que gostaria de alterar (ex: 'Quarto casal') ou 'fim' para retornar ao Menu: ");
            string roomName = Console.ReadLine()?.Trim();

            if (roomName.Equals("fim", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Retornando ao Menu...");
                return;
            }

            if (string.IsNullOrWhiteSpace(roomName))
            {
                PrintErrorMessage("Nome da área não pode estar vazio.");
                continue;
            }


            Room roomToEdit = null;

            foreach (var floor in user.Residence.ResidenceFloors)
            {
                roomToEdit = floor.Rooms.FirstOrDefault(r => r.RoomName.Equals(roomName, StringComparison.Ordinal));
                if (roomToEdit != null)
                {
                    break;
                }
            }

            if (roomToEdit == null)
            {
                PrintErrorMessage($"Área '{roomName}' não encontrada. Verifique o nome e tente novamente.");
                WaitForUser();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Informe o novo nome da área (ex: 'Suite Casal'): ");
            string newRoomName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newRoomName))
            {
                PrintErrorMessage("Nome da área está inválido!");
                continue;
            }

            if (user.Residence.ResidenceFloors.Any(floor =>
                floor.Rooms.Any(room => room.RoomName.Equals(newRoomName, StringComparison.Ordinal))))
            {
                PrintErrorMessage($"O nome '{newRoomName}' já está atribuído a outra área.");
                continue;
            }

            roomToEdit.RoomName = newRoomName;
            User.SaveUsersToFile();
            PrintSucessMessage("Nome da área alterado com sucesso!");

            if (!AskWantContinue())
            {
                break;
            }
        }
    }


    //Adicionar Room
    public static void AddRoomUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        RoomsListConsole(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe o número do andar (piso) da área a ser incluída (ex: '03'):");
        Console.WriteLine("ATENÇÃO: O piso deve existir. Se necessário, crie o piso primeiro no menu principal.");
        string floorNumber = Console.ReadLine()?.Trim();

        var selectedFloor = user.Residence.ResidenceFloors
            .FirstOrDefault(f => f.FloorName.Equals(floorNumber, StringComparison.Ordinal));

        if (selectedFloor == null)
        {
            PrintErrorMessage("Número do andar (piso) inválido ou inexistente.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o nome da área que gostaria de incluir (ex: 'Sala de Jantar'): ");
        string addRoomName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(addRoomName))
        {
            PrintErrorMessage("O nome da área não pode estar vazio.");
            return;
        }

        if (selectedFloor.Rooms.Any(room => room.RoomName.Equals(addRoomName, StringComparison.Ordinal)))
        {
            PrintErrorMessage($"A área '{addRoomName}' já existe neste andar (piso).");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o tempo de limpeza (em minutos):");
        int cleanTime = GetValidClearAndIntervalInput();

        Console.WriteLine();
        Console.WriteLine("Informe o intervalo de limpeza (em dias):");
        int cleanInterval = GetValidClearAndIntervalInput();



        Room newRoom = new Room(addRoomName, cleanTime, cleanInterval);

        selectedFloor.Rooms.Add(newRoom);


        User.SaveUsersToFile();
        PrintSucessMessage("Nova área incluída com sucesso!");
    }


    //Deletar Room
    public static void DeleteRoomUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        RoomsListConsole(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe em qual piso está a área que deseja excluir (ex: '03'): ");
        Console.WriteLine("ATENÇÃO! Deve ser escolhido um piso já existente.");
        string floorNumber = Console.ReadLine()?.Trim();

        var selectedFloor = user.Residence.ResidenceFloors
            .FirstOrDefault(f => f.FloorName.Equals(floorNumber, StringComparison.Ordinal));

        if (selectedFloor == null)
        {
            PrintErrorMessage("Número do piso inválido ou não encontrado!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o nome da área que gostaria de excluir (ex: 'Sala de Jantar'): ");
        string roomNameToDelete = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(roomNameToDelete))
        {
            PrintErrorMessage("Nome da área inválido!");
            return;
        }


        var roomToDelete = selectedFloor.Rooms.FirstOrDefault(room => room.RoomName.Equals(roomNameToDelete, StringComparison.Ordinal));

        if (roomToDelete == null)
        {
            PrintErrorMessage($"A área '{roomNameToDelete}' não foi encontrada no piso {floorNumber}.");
            return;
        }

        //selectedFloor.Rooms.Remove(roomToDelete);
        selectedFloor.FloorRemoveRoom(roomToDelete);

        User.SaveUsersToFile();
        PrintSucessMessage($"A área '{roomNameToDelete}' foi excluída com sucesso!");
    }


    //Consulta listagem de Rooms ordenada por vencimento separa por piso 
    public static void RoomsListConsole(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        Console.Clear();

        Title($"Listagem de Áreas - Utilizador: {utilizador}");
        Console.WriteLine();

        DateTime currentDate = DateTime.Now;

        int counter = 1;

        if (user.Residence.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
        {
            foreach (var floor in user.Residence.ResidenceFloors)
            {
                Console.WriteLine($"> Piso: {floor.FloorName}");
                Console.WriteLine();

                if (floor.Rooms != null && floor.Rooms.Count > 0)
                {
                    var sortedRooms = floor.Rooms.OrderBy(room =>
                    {
                        DateTime lastCleanDate = room.DayClean;
                        int cleaningFrequency = room.CleanInterval;

                        DateTime nextCleaningDate = lastCleanDate.AddDays(cleaningFrequency);

                        return (nextCleaningDate - currentDate).Days;
                    }).ToList();

                    foreach (var room in sortedRooms)
                    {
                        string counterFormatted = counter.ToString("D2");
                        DateTime nextCleaningDate = room.DayClean.AddDays(room.CleanInterval);
                        Console.WriteLine($"   {counterFormatted} - {room.RoomName} - Vencimento próxima limpeza: {nextCleaningDate.ToShortDateString()}");
                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine("   Nenhuma área cadastrada neste piso.");
                }

                Console.WriteLine();
            }

            PrintSucessMessage("Fim da listagem das áreas.");
            Console.WriteLine();
        }
        else
        {
            PrintErrorMessage("Nenhum piso ou área cadastrada na residência.");
        }
    }


    public static List<string> RoomsList(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return null;
        }

        List<string> roomListUser = new List<string>();

        Console.Clear();

        Title($"Listagem de Áreas - Utilizador: {utilizador}");
        Console.WriteLine();

        DateTime currentDate = DateTime.Now;
        int counter = 1;

        if (user.Residence.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
        {
            foreach (var floor in user.Residence.ResidenceFloors)
            {
                Console.WriteLine($"> Piso: {floor.FloorName}");
                Console.WriteLine();

                if (floor.Rooms != null && floor.Rooms.Count > 0)
                {
                    var sortedRooms = floor.Rooms.OrderBy(room =>
                    {
                        DateTime lastCleanDate = room.DayClean;
                        int cleaningFrequency = room.CleanInterval;
                        DateTime nextCleaningDate = lastCleanDate.AddDays(cleaningFrequency);
                        return (nextCleaningDate - currentDate).Days;
                    }).ToList();

                    foreach (var room in sortedRooms)
                    {
                        string counterFormatted = counter.ToString("D2");
                        DateTime nextCleaningDate = room.DayClean.AddDays(room.CleanInterval);
                        roomListUser.Add($"   {counterFormatted} - {room.RoomName} - Vencimento próxima limpeza: {nextCleaningDate.ToShortDateString()}");
                        counter++;
                    }
                }
                else
                {
                    roomListUser.Add("   Nenhuma área cadastrada neste piso.");
                }

                Console.WriteLine();
            }

            PrintSucessMessage("Fim da listagem das áreas.");
            Console.WriteLine();
            return roomListUser;
        }
        else
        {
            PrintErrorMessage("Nenhum piso ou área cadastrada na residência.");
            return null;
        }
    }




    // Realizar a listagem limpeza 
    public static void CleaningRoomsList(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        Console.Clear();
        Title("Lista de Áreas para efetuar a limpeza");
        Console.WriteLine();
        Console.WriteLine($"Utilizador: {utilizador}");

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
                        Console.WriteLine($"   {counterFormatted} - {room.RoomName} - Codigo para limpar: L-{counterFormatted}");
                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine("   Nenhuma área cadastrada neste piso.");
                }

                Console.WriteLine();
            }

            PrintSucessMessage("Fim da listagem das áreas.");
            Console.WriteLine();
        }
        else
        {
            PrintErrorMessage("Nenhum piso ou área cadastrada na residência.");
        }
    }



}


