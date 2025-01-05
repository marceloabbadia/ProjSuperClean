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

    public DateTime? DayClean { get; set; }

    public DateTime CreatedRoom_at { get; set; }

    public DateTime? FirstCleaning { get; set; }

    public int CleanInterval { get; set; }

    public Guid RoomId { get; set; }
    public DateTime? NextClean { get; set; }

    public Room(string name, int cleanTime, int cleanInterval, DateTime? firstCleaning)
    {
        RoomName = name;
        CleanTime = cleanTime;
        CleanInterval = cleanInterval > 0 ? cleanInterval : throw new ArgumentException("O intervalo de limpeza deve ser positivo.");
        RoomId = Guid.NewGuid();
        CreatedRoom_at = DateTime.Now;

        if (firstCleaning.HasValue)
        {
            DayClean = firstCleaning.Value;
            NextClean = DayClean.Value.AddDays(cleanInterval);
        }
        else
        {
            DayClean = null;
            NextClean = null;
        }
    }





    public Room() { }


    //Realizar limpeza do Room
    public static void CleanDone(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);
        if (user == null || user.Residence == null)
        {
            PrintErrorMessage("Utilizador ou residência não encontrado.");
            WaitForUser();
            return;
        }

        List<string> roomDetails = RoomsList(userId, utilizador);
        if (roomDetails.Count == 0)
        {
            Console.WriteLine("Nenhuma sala disponível para limpeza.");
            return;
        }

        Console.Clear();
        Title($"Áreas de Limpeza - Utilizador: {utilizador}");
        Console.WriteLine();
        for (int i = 0; i < roomDetails.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {roomDetails[i]}");
        }

        string codeClean;
        int roomIndex;
        do
        {
            Console.WriteLine("\nInforme o número da área que deseja limpar (ou digite 'fim' para retornar):");
            codeClean = Console.ReadLine()?.Trim();

            if (codeClean.Equals("fim", StringComparison.OrdinalIgnoreCase))
                return;

        } while (!int.TryParse(codeClean, out roomIndex) || roomIndex < 1 || roomIndex > roomDetails.Count);

        var selectedRoom = user.Residence.ResidenceFloors
                                .SelectMany(f => f.Rooms)
                                .ElementAt(roomIndex - 1);

        DateTime currentDate = DateTime.Now;
        if (selectedRoom.DayClean.HasValue)
        {
            DateTime nextCleanDate = selectedRoom.DayClean.Value.AddDays(selectedRoom.CleanInterval);
            if (nextCleanDate > currentDate)
            {
                Console.WriteLine($"Aviso: A limpeza será realizada antes do intervalo previsto. A próxima limpeza seria em {nextCleanDate:dd/MM/yyyy}.");
                Console.WriteLine("Deseja continuar com a limpeza? (sim/não)");

                string decision = Console.ReadLine()?.Trim().ToLower();
                if (decision.Equals("sim", StringComparison.OrdinalIgnoreCase) || (decision.Equals("s", StringComparison.OrdinalIgnoreCase)) || (decision.Equals("sin", StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Realizando limpeza antecipada na sala: {selectedRoom.RoomName}...");
                    selectedRoom.DayClean = DateTime.Now;
                    selectedRoom.NextClean = selectedRoom.DayClean.Value.AddDays(selectedRoom.CleanInterval);
                    Console.WriteLine($"Próxima limpeza agendada para: {selectedRoom.NextClean:dd/MM/yyyy}");
                }
                else
                {
                    Console.WriteLine("Operação de limpeza cancelada.");
                }
            }
            else
            {
                Console.WriteLine($"Realizando a limpeza na sala: {selectedRoom.RoomName}...");
                selectedRoom.DayClean = DateTime.Now;
                selectedRoom.NextClean = selectedRoom.DayClean.Value.AddDays(selectedRoom.CleanInterval);
                Console.WriteLine($"Próxima limpeza agendada para: {selectedRoom.NextClean:dd/MM/yyyy}");
            }
        }
        else
        {
            Console.WriteLine($"Primeira limpeza realizada na sala: {selectedRoom.RoomName}");
            selectedRoom.DayClean = DateTime.Now;
            selectedRoom.NextClean = selectedRoom.DayClean.Value.AddDays(selectedRoom.CleanInterval);
            Console.WriteLine($"Próxima limpeza agendada para: {selectedRoom.NextClean:dd/MM/yyyy}");
        }

        User.SaveUsersToFile();
        Console.WriteLine("\nDeseja realizar a limpeza de outra divisão? (sim/não)");

        string continueCleaning = Console.ReadLine()?.Trim().ToLower();
        if (continueCleaning.Equals("sim", StringComparison.OrdinalIgnoreCase) || (continueCleaning.Equals("s", StringComparison.OrdinalIgnoreCase)) || (continueCleaning.Equals("sin", StringComparison.OrdinalIgnoreCase)))
        {
            CleanDone(userId, utilizador);
        }
        else
        {
            Console.WriteLine("Voltando ao menu principal...");
        }
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
            if (rooms[i].FirstCleaning != null)
                Console.WriteLine($"{i + 1}: {rooms[i].RoomName} - vencimento dia: {rooms[i].NextClean:dd/MM/yyyy}");
        }

        Console.WriteLine();
        Console.WriteLine("Informe o número da área que deseja excluir a última limpeza (ou digite 'fim' para retornar):");

        while (true)
        {
            string codeClean = Console.ReadLine()?.Trim();

            if (codeClean.Equals("fim", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!int.TryParse(codeClean, out int roomIndex) || roomIndex < 1 || roomIndex > rooms.Count)
            {
                Console.WriteLine("Entrada inválida. Tente novamente.");
                continue;
            }

            var selectedRoom = rooms[roomIndex - 1];

            if (selectedRoom.DayClean == null)
            {
                Console.WriteLine($"A sala '{selectedRoom.RoomName}' ainda não foi limpa, portanto não há última limpeza para excluir.");
                return;
            }

            DateTime previousClean = selectedRoom.DayClean.Value.AddDays(-selectedRoom.CleanInterval);

            selectedRoom.DayClean = previousClean;
            selectedRoom.NextClean = previousClean.AddDays(selectedRoom.CleanInterval);

            User.SaveUsersToFile();

            Console.WriteLine($"Removida a última limpeza da sala: {selectedRoom.RoomName}");
            Console.WriteLine($"Data da última limpeza restaurada para: {selectedRoom.DayClean:dd/MM/yyyy}");
            Console.WriteLine($"Próxima limpeza ajustada para: {selectedRoom.NextClean:dd/MM/yyyy}");
            break;
        }
    }



    // Gerar barras de status da limpeza
    public static int[] GenerationPypes(Room room, DateTime today)
    {
        int greenPipe = 0, yellowPipe = 0, redPipe = 0;

        if (room.DayClean.HasValue)
        {
            DateTime dayClean = room.DayClean.Value;
            int daysSinceClean = (today - dayClean).Days;

            
            int totalBars = Math.Min(Math.Abs(daysSinceClean), 20);

           
            if (daysSinceClean <= room.CleanInterval - 1)
            {
                greenPipe = totalBars;  
            }
            
            else if (daysSinceClean >= room.CleanInterval - 1 && daysSinceClean <= room.CleanInterval + 2)
            {
                greenPipe = room.CleanInterval - 1;  
                yellowPipe = daysSinceClean - (room.CleanInterval - 1); 
            }
           
            else
            {
                greenPipe = room.CleanInterval - 1;  
                yellowPipe = 4; 

             
                redPipe = totalBars - (greenPipe + yellowPipe);
            }

            greenPipe = Math.Max(0, greenPipe);
            yellowPipe = Math.Max(0, yellowPipe);
            redPipe = Math.Max(0, redPipe);
        }

        return new int[] { greenPipe, yellowPipe, redPipe };
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


        while (true)
        {
            Floor floor = Floor.SearchFloor(userId);

            if (floor == null)
            {
                Console.WriteLine("Retornando ao Menu..");
                return;
            }

            Console.WriteLine($"Por favor, informe o nome da divisão que deseja adicionar ao piso {floor.FloorName} (ou digite 'fim' para encerrar):");

            string roomName = Console.ReadLine()?.Trim();


            if (roomName.Equals("fim", StringComparison.OrdinalIgnoreCase))
                return;


            if (string.IsNullOrEmpty(roomName))
            {
                PrintErrorMessage("O nome da divisão não pode estar vazia.");
                continue;
            }

            string errorMessage;

            string validRoomName = GetValidRoomInput(roomName, out errorMessage);

            Console.WriteLine($"Informe o tempo de limpeza da(o) {validRoomName} (em minutos):");
            int cleanTime = GetValidClearAndIntervalInput();

            Console.WriteLine($"Informe o intervalo de limpeza da(o) {validRoomName} (em dias):");
            int cleanInterval = GetValidClearAndIntervalInput();



            Console.WriteLine($"Deseja realizar a primeira limpeza da(o) {validRoomName} (sim/não):");
            string decisionCleaning = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(decisionCleaning))
            {
                PrintErrorMessage("A resposta da limpeza precisa ser 'sim' ou 'não'.");
                continue;
            }

            DateTime? firstCleaning = null;

            if (decisionCleaning.Equals("sim", StringComparison.OrdinalIgnoreCase) ||
                decisionCleaning.Equals("s", StringComparison.OrdinalIgnoreCase) ||
                decisionCleaning.Equals("sin", StringComparison.OrdinalIgnoreCase))
            {
                firstCleaning = DateTime.Now;
            }
            else if (decisionCleaning.Equals("não", StringComparison.OrdinalIgnoreCase) ||
                     decisionCleaning.Equals("nao", StringComparison.OrdinalIgnoreCase) ||
                    decisionCleaning.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                firstCleaning = null;
            }
            else
            {
                PrintErrorMessage("Resposta inválida. Digite 'sim' ou 'não'.");
                continue;
            }

            Room room = new Room(roomName, cleanTime, cleanInterval, firstCleaning);

            floor.FloorAddRoom(room);

            User.SaveUsersToFile();

            PrintSucessMessage($"Divisão '{roomName}' adicionada ao piso .");
        }
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

        selectedFloor.FloorRemoveRoom(roomToDelete);

        User.SaveUsersToFile();
        PrintSucessMessage($"A área '{roomNameToDelete}' foi excluída com sucesso!");
    }


    //Consulta listagem de Rooms ordenada por vencimento e sem primeira limpeza, separa por piso 
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

        List<Room> allRooms = new List<Room>();

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
                        allRooms.Add(room);
                    }

                    var sortedRooms = allRooms.OrderBy(room =>
                    {
                        if (room.DayClean != null)
                        {
                            DateTime lastCleanDate = room.DayClean.Value;
                            int cleaningFrequency = room.CleanInterval;

                            DateTime nextCleaningDate = lastCleanDate.AddDays(cleaningFrequency);
                            return (nextCleaningDate - currentDate).Days;
                        }
                        else
                        {
                            return int.MaxValue;
                        }
                    }).ToList();

                    foreach (var room in sortedRooms)
                    {
                        string counterFormatted = counter.ToString("D2");

                        if (room.DayClean != null)
                        {
                            DateTime nextCleaningDate = room.DayClean.Value.AddDays(room.CleanInterval);
                            Console.WriteLine($"   {counterFormatted} - {room.RoomName} - Vencimento próxima limpeza: {nextCleaningDate.ToShortDateString()}");
                        }
                        else
                        {
                            Console.WriteLine($"   {counterFormatted} - {room.RoomName} - A primeira limpeza ainda não foi realizada.");
                        }

                        counter++;
                    }
                }
                else
                {
                    Console.WriteLine("   Nenhuma área cadastrada neste piso.");
                }

                allRooms.Clear();

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
            return new List<string>();
        }

        List<string> roomDetails = new List<string>();
        DateTime currentDate = DateTime.Now;

        if (user.Residence.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
        {
            foreach (var floor in user.Residence.ResidenceFloors)
            {
                if (floor.Rooms != null && floor.Rooms.Count > 0)
                {
                    foreach (var room in floor.Rooms)
                    {
                        if (room.DayClean != null)
                        {
                            DateTime nextCleanDate = room.DayClean.Value.AddDays(room.CleanInterval);
                            roomDetails.Add($"{room.RoomName} - Vencimento próxima limpeza: {nextCleanDate:dd/MM/yyyy}");
                        }
                        else
                        {
                            roomDetails.Add($"{room.RoomName} - A primeira limpeza ainda não foi realizada.");
                        }
                    }
                }
            }
        }

        return roomDetails;
    }


}


