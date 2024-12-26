namespace ProjSuperClean.Class;
using ProjSuperClean.Utils;

public class Residence
{
    public string ResidenceName { get; set; }

    public List<Floor> ResidenceFloors { get; set; }

    public Residence(string residenceName)
    {
        ResidenceName = residenceName;
        ResidenceFloors = new List<Floor>();
    }

    public Residence() { }

    public static void ChangeNameResidence(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Console.WriteLine($"Atual nome da residência: {user.Residence.ResidenceName}");
        Console.WriteLine("Digite o novo nome da residência do seu utilizador: ");
        Console.WriteLine();

        string newNameResidence = Console.ReadLine()?.Trim(); 

        if (string.IsNullOrEmpty(newNameResidence))
        {
            Utils.PrintErrorMessage("Nome da residência inválido (vazio ou somente espaços)!");
            Utils.WaitForUser();
            return;
        }

        user.Residence.ResidenceName = newNameResidence;
        User.SaveUsersToFile();
        Utils.PrintSucessMessage("O nome da residência foi alterado com sucesso!");

        Utils.WaitForUser();
    }

    public static void ChangeNumberFloor(Guid userId, string utilizador)
    {
        var user = User.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null || user.Residence == null)
        {
            Utils.PrintErrorMessage("Utilizador ou residência não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Residence.ListFloor(userId, utilizador);

        Console.WriteLine();
        Console.WriteLine("Informe o número do piso que gostaria de alterar (ex: '01'): ");
        string floorNumber = Console.ReadLine()?.Trim();
        Console.WriteLine();
        Console.WriteLine("Qual o novo número do piso que deseja (ex: '02')? ");
        string newFloorNumber = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(floorNumber) || string.IsNullOrWhiteSpace(newFloorNumber))
        {
            Utils.PrintErrorMessage("Números inválidos! Certifique-se de inserir APENAS dois dígitos.");
        }
        else if (user.Residence.ResidenceFloors.Any(f => f.FloorName == newFloorNumber))
        {
            Utils.PrintErrorMessage($"O número '{newFloorNumber}' já está atribuído a outro piso.");
        }
        else
        {
            var floorToEdit = user.Residence.ResidenceFloors.FirstOrDefault(f => f.FloorName == floorNumber);

            if (floorToEdit != null)
            {
                floorToEdit.FloorName = newFloorNumber;
                User.SaveUsersToFile();
                Utils.PrintSucessMessage("Número do piso alterado com sucesso!");
            }
            else
            {
                Utils.PrintErrorMessage($"Piso '{floorNumber}' não encontrado!");
            }
        }
    }

    public void AddFloor(Floor floor)
    {
        ResidenceFloors.Add(floor);
    }

    public void DeleteFloor(Floor floor)
    {
        ResidenceFloors.Remove(floor);
    }

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


        foreach (var floor in user.Residence.ResidenceFloors)
        {
            Console.WriteLine($"\t - Piso: {floor.FloorName}");
        }
        Console.WriteLine();
        Utils.PrintSucessMessage("Fim da listagem dos pisos.");
        Console.WriteLine();
    }

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
                user.Residence.AddFloor(newFloor);
                Utils.PrintSucessMessage($"Piso '{floorName}' adicionado com sucesso!");
                User.SaveUsersToFile();
            }
            else
            {
                Utils.PrintErrorMessage("Entrada inválida. Certifique-se de informar exatamente 2 dígitos para o piso!");
            }
        }
    }

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
                user.Residence.ResidenceFloors.Remove(floorToRemove);

                Utils.PrintSucessMessage($"Piso '{deleteFloorName}' excluído com sucesso!");
                User.SaveUsersToFile();
            }
        }



    }



}
