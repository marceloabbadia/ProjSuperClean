namespace ProjSuperClean.Class;

using System.Diagnostics.Metrics;
using System.Drawing;
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

        RoomsList(userId, utilizador);

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


    public static void AddRoomUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        RoomsList(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe o número do andar (piso) da área a ser incluída (ex: '03'):");
        Console.WriteLine("⚠️ ATENÇÃO: O piso deve existir. Se necessário, crie o piso primeiro no menu principal.");
        string floorNumber = Console.ReadLine()?.Trim();

        var selectedFloor = user.Residence.ResidenceFloors
            .FirstOrDefault(f => f.FloorName.Equals(floorNumber, StringComparison.Ordinal));

        if (selectedFloor == null)
        {
            Utils.PrintErrorMessage("Número do andar (piso) inválido ou inexistente.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o nome da área que gostaria de incluir (ex: 'Sala de Jantar'): ");
        string addRoomName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(addRoomName))
        {
            Utils.PrintErrorMessage("O nome da área não pode estar vazio.");
            return;
        }

        if (selectedFloor.Rooms.Any(room => room.RoomName.Equals(addRoomName, StringComparison.Ordinal)))
        {
            Utils.PrintErrorMessage($"A área '{addRoomName}' já existe neste andar (piso).");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o tempo de limpeza (em minutos):");
        int cleanTime = User.GetValidIntInput();

        Console.WriteLine();
        Console.WriteLine("Informe o intervalo de limpeza (em dias):");
        int cleanInterval = User.GetValidIntInput();



        Room newRoom = new Room (addRoomName, cleanTime, cleanInterval);

        selectedFloor.Rooms.Add(newRoom);


        User.SaveUsersToFile();
        Utils.PrintSucessMessage("Nova área incluída com sucesso!");
    }


    public static void DeleteRoomUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        RoomsList(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe em qual piso está a área que deseja excluir (ex: '03'): ");
        Console.WriteLine("ATENÇÃO! Deve ser escolhido um piso já existente.");
        string floorNumber = Console.ReadLine()?.Trim();

        var selectedFloor = user.Residence.ResidenceFloors
            .FirstOrDefault(f => f.FloorName.Equals(floorNumber, StringComparison.Ordinal));

        if (selectedFloor == null)
        {
            Utils.PrintErrorMessage("Número do piso inválido ou não encontrado!");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Informe o nome da área que gostaria de excluir (ex: 'Sala de Jantar'): ");
        string roomNameToDelete = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(roomNameToDelete))
        {
            Utils.PrintErrorMessage("Nome da área inválido!");
            return;
        }


        var roomToDelete = selectedFloor.Rooms.FirstOrDefault(room => room.RoomName.Equals(roomNameToDelete, StringComparison.Ordinal));

        if (roomToDelete == null)
        {
            Utils.PrintErrorMessage($"A área '{roomNameToDelete}' não foi encontrada no piso {floorNumber}.");
            return;
        }

        selectedFloor.Rooms.Remove(roomToDelete);

        User.SaveUsersToFile();
        Utils.PrintSucessMessage($"A área '{roomNameToDelete}' foi excluída com sucesso!");
    }


    public static void RoomsList(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Console.Clear();
        
        Utils.Title($"Listagem de Áreas - Utilizador: {utilizador}");
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

    }


    public static void CleaningRoomsList(Guid userId, string utilizador)
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
        Console.WriteLine($"<<<  Lista de Áreas para efetuar a limpeza  >>>  ");
        Console.WriteLine("==================================================");
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

            Utils.PrintSucessMessage("Fim da listagem das áreas.");
            Console.WriteLine();
        }
        else
        {
            Utils.PrintErrorMessage("Nenhum piso ou área cadastrada na residência.");
        }

        Console.WriteLine("==================================================");
        
    }

    public static void AutoSortFloors()
    {
        foreach (var user in User.users)
        {
            if (user.Residence != null && user.Residence.ResidenceFloors != null)
            {
                user.Residence.ResidenceFloors = user.Residence.ResidenceFloors
                    .OrderBy(f => f.FloorName, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
        }
    }



}
