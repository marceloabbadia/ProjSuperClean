using System.Security.Cryptography.X509Certificates;
using ProjSuperClean.Class;
using static ProjSuperClean.Utils.Utils;
using static ProjSuperClean.Class.User;
using System.Diagnostics.Metrics;

namespace ProjSuperClean;

public class Program
{
    static void Main(string[] args)
    {
        LoadUsersFromFile();
        HeaderProgramUserStart();

    }

    public static void MainMenuUser(Guid userId, string utilizador)
    {


        while (true)
        {
            Console.WriteLine("MENU PRINCIPAL SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Menu Utilizador");
            Console.WriteLine("2 - Menu Residencia");
            Console.WriteLine("3 - Listagem completa do seu Utilizador");
            Console.WriteLine("4 - Sair");
            Console.WriteLine("5 - Ajuda");



            switch (GetOption(1, 5))
            {
                case 1:
                    Console.Clear();
                    UserMenu(userId, utilizador);
                    break;

                case 2:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("MENU PRINCIPAL SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Listagem do Utilizador ***");
                    DisplayInfoUser(utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    SaveUsersToFile();
                    Console.WriteLine("Fechando o programa...");
                    Console.WriteLine("Clique em qualquer tecla para fechar a tela!");
                    Environment.Exit(0);
                    break;
                    
                case 5:
                    Console.Clear();
                    Help.ExibirManualDoUtilizador();
                    break;  

            }
        }
    }

    public static void UserMenu(Guid userId, string utilizador)
    {


        while (true)
        {
            Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Alterar nome do Utilizador");
            Console.WriteLine("2 - Apagar Utilizador");
            Console.WriteLine("3 - Criar novo Utilizador");
            Console.WriteLine("4 - Voltar Menu Principal");



            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Alterar nome do Utilizador ***");
                    ChangeUsername(userId, utilizador);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Apagar Utilizador ***");
                    DeleteUser(userId);
                    Console.WriteLine();
                    WaitForUser();
                    HeaderProgramUserStart();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
                    Console.Clear();
                    Console.WriteLine("*** Criar novo Utilizador ***");
                    HeaderProgramUserStart();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    MainMenuUser(userId, utilizador);
                    break;

            }
        }
    }

    public static void ResidenceMenu(Guid userId, string utilizador)
    {
        while (true)
        {
            Console.WriteLine("MENU RESIDÊNCIA SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opção abaixo, {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Editar nome da residência");
            Console.WriteLine("2 - Editar pisos");
            Console.WriteLine("3 - Editar área");
            Console.WriteLine("4 - Voltar ao menu principal");

            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU RESIDÊNCIA SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Editar nome da residência ***");
                    Console.WriteLine();
                    Residence.ChangeNameResidence(userId, utilizador);
                    break;

                case 2:
                    Console.Clear();
                    FloorMenu(userId, utilizador);
                    break;

                case 3:
                    Console.Clear();
                    RoomMenu(userId, utilizador);
                    break;

                case 4:
                    Console.Clear();
                    MainMenuUser(userId, utilizador);
                    break;
            }
        }
    }

    public static void FloorMenu(Guid userId, string utilizador)
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("MENU PISO SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Editar Numero do Piso");
            Console.WriteLine("2 - Incluir Piso");
            Console.WriteLine("3 - Excluir Piso");
            Console.WriteLine("4 - Voltar Menu Residencia");


            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU PISO SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Listagem dos pisos do utilizador");
                    Console.WriteLine();
                    Residence.ChangeNumberFloor(userId, utilizador);
                    WaitForUser();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("MENU PISO SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Adicionar pisos à residência do utilizador");
                    Console.WriteLine();
                    Residence.AddFloorUser(userId, utilizador);
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("MENU PISO SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Remover pisos da residência do utilizador");
                    Console.WriteLine();
                    Residence.DeleteFloorUser(userId, utilizador);
                    break;

                case 4:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;
            }
        }
    }

    public static void RoomMenu(Guid userId, string utilizador)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("MENU ÁREA SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Editar nome da área");
            Console.WriteLine("2 - Incluir área");
            Console.WriteLine("3 - Excluir área");
            Console.WriteLine("4 - Voltar ao menu residência");


            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU ÁREA SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Editar nome da área");
                    Console.WriteLine();
                    Floor.ChangeNameRoom(userId, utilizador);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("MENU ÁREA SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Adicionar área à residência do utilizador");
                    Console.WriteLine();
                    Floor.AddRoomUser(userId, utilizador);
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("MENU ÁREA SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Remover área da residência do utilizador");
                    Console.WriteLine();
                    Floor.DeleteRoomUser(userId,utilizador);
                    break;

                case 4:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;
            }
        }
    }


    // -----------------------------------/-----------------------------//

    public static void MainMenuAdmin()
    {
        Console.WriteLine("MENU PRINCIPAL ADMIN SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine("Escolha uma opcao abaixo:");

            Console.WriteLine("1 - Listagem completa");
            Console.WriteLine("2 - Sair");



            switch (GetOption(1, 2))
            {
                case 1:
                    Console.Clear();
                    HeaderProgramUserAdmin(5);
                    DisplayInfoCompleteAdmin();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem de Utilizadores criada com sucesso!");
                    WaitForUser();
                    break;

                case 2:
                    Console.Clear();
                    Console.Write("Saindo...");
                    Thread.Sleep(1000);
                    Console.Write(".....");
                    Thread.Sleep(1000);
                    Console.Write("........");
                    Thread.Sleep(1000);
                    Console.Clear();
                    HeaderProgramUserStart();
                    break;  
            }
        }
    }

}












