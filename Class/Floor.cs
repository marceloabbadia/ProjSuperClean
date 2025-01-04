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



    //Adicionar Room
    public void FloorAddRoom(Room room)
    {
        Rooms.Add(room);
    }

    //Remover Room
    public void FloorRemoveRoom(Room room)
    {
        Rooms.Remove(room);
    }


    //Altera o numero do piso (floor)
    public static void ChangeNumberFloor(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        while (true)
        {

            ListFloor(userId, utilizador);

            Console.WriteLine();
            Console.WriteLine("Informe o número do piso que gostaria de alterar (ex: '01') ou 'fim' para retornar: ");
            string floorNumber = Console.ReadLine()?.Trim();
            Console.WriteLine();

            if (floorNumber.Equals("fim", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Retornando ao Menu...");
                return;
            }

            if (string.IsNullOrWhiteSpace(floorNumber))
            {
                Utils.PrintErrorMessage("Número do piso não pode ser vazio.");
                continue;

            }

            var floorToEdit = user.Residence.ResidenceFloors.FirstOrDefault(f => f.FloorName == floorNumber);

            if (floorToEdit == null)
            {
                Utils.PrintErrorMessage($"Piso '{floorNumber}' não encontrado!");
                continue;
            }

            Console.WriteLine("Qual o novo número do piso que deseja (ex: '02')? ");
            string newFloorNumber = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newFloorNumber))
            {
                Utils.PrintErrorMessage("Número inválido! Certifique-se de inserir APENAS dois dígitos.");
                continue;
            }

            if (user.Residence.ResidenceFloors.Any(f => f.FloorName == newFloorNumber))
            {
                Utils.PrintErrorMessage($"O novo número informado '{newFloorNumber}' já está atribuído a outro piso.");
                continue;
            }

            floorToEdit.FloorName = newFloorNumber;
            User.SaveUsersToFile();
            Utils.PrintSucessMessage("Número do piso alterado com sucesso!");

            if (!Utils.AskWantContinue())
            {
                break;  
            }
        }
    }



    //Ordenar os Pisos (Floors) cresc de todos utilizadores (ADM)
    public static void GlobalAutoSortFloors()
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


    //Ordenar os pisos(Floors) cresc de 1 utilizador
    public static void AutoSortFloors(User user)
    {
        if (user.Residence?.ResidenceFloors != null)
        {
            user.Residence.ResidenceFloors = user.Residence.ResidenceFloors
                .OrderBy(f => f.FloorName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }


    //Listagem de todos os Pisos do utilizador cresc
    public static void ListFloor(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Console.WriteLine("Listagem dos Pisos do utilizador");
        Console.WriteLine();

        AutoSortFloors(user);


        foreach (var floor in user.Residence.ResidenceFloors)
        {
            Console.WriteLine($"\t - Piso: {floor.FloorName}");
        }

        Console.WriteLine();
        Utils.PrintSucessMessage("Fim da listagem dos pisos.");
        Console.WriteLine();
    }


    // Adiciona um ou mais pisos à residência do utilizador
    public static void AddFloorUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            return;
        }

        ListFloor(userId, utilizador);


        bool addFloors = true;

        while (addFloors)
        {
            Console.WriteLine();
            Console.WriteLine("Por favor, informe o número do piso usando sempre 2 dígitos (ou digite 'fim' para encerrar):");
            Console.WriteLine("*** Exemplo: '01' para o primeiro piso, '02' para o segundo, e assim por diante. ***");

            string floorName = Console.ReadLine()?.Trim();


            if (floorName.ToLower() == "fim")
            {
                Utils.PrintSucessMessage("Encerrando a entrada de pisos.");
                addFloors = false;
            }
            else if (user.Residence.ResidenceFloors.Any(f => f.FloorName == floorName))
            {
                Utils.PrintErrorMessage("Esse piso já existe. Tente outro número.");
            }
            else if (!string.IsNullOrEmpty(floorName) && floorName.Length == 2)
            {
                var newFloor = new Floor(floorName);
                user.Residence.ResidenceAddFloor(newFloor);
                Utils.PrintSucessMessage($"Piso '{floorName}' adicionado com sucesso!");
                User.SaveUsersToFile();
            }
            else
            {
                Utils.PrintErrorMessage("Entrada inválida. Certifique-se de informar exatamente 2 dígitos para o piso!");
            }

        }
    }


    // Deleta um ou mais pisos à residência do utilizador
    public static void DeleteFloorUser(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            return;
        }

        ListFloor(userId, utilizador);


        bool deleteFloors = true;

        while (deleteFloors)
        {
            Console.WriteLine();
            Console.WriteLine("Por favor, informe o número do piso que deseja EXCLUIR usando sempre 2 dígitos (ou digite 'fim' para encerrar):");
            Console.WriteLine("*** Exemplo: '01' para o primeiro piso, '02' para o segundo, e assim por diante. ***");

            string deleteFloorName = Console.ReadLine()?.Trim();


            if (deleteFloorName.ToLower() == "fim")
            {
                Utils.PrintSucessMessage("Encerrando o processo de exclusão de pisos."); deleteFloors = false;
            }
            else if (string.IsNullOrEmpty(deleteFloorName) || deleteFloorName.Length != 2)
            {
                Utils.PrintErrorMessage("Entrada inválida. Certifique-se de informar exatamente 2 dígitos para o piso!");

            }
            else if (user.Residence.ResidenceFloors.Any(f => f.FloorName == deleteFloorName))
            {

                var floorToRemove = user.Residence.ResidenceFloors.FirstOrDefault(f => f.FloorName == deleteFloorName);
                user.Residence.ResidenceDeleteFloor(floorToRemove);

                Utils.PrintSucessMessage($"Piso '{deleteFloorName}' excluído com sucesso!");
                User.SaveUsersToFile();
            }
        }



    }


    //Verifica se o utilizador possui pelo menos 1 piso em sua residência
    public static bool HasFloors(Guid userId)
    {
        User user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user?.Residence?.ResidenceFloors == null || user.Residence.ResidenceFloors.Count == 0)
        {
            return false;
        }

        return true;
    }


}
