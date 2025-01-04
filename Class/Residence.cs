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


    //Adicionar Floor
    public void ResidenceAddFloor(Floor floor)
    {
        ResidenceFloors.Add(floor);
    }

    //Remover Floor
    public void ResidenceDeleteFloor(Floor floor)
    {
        ResidenceFloors.Remove(floor);
    }
    
    //Mudar nome da Residencia
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

   

}
