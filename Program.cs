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

    public static void HeaderProgramUserStart()
    {
        Console.Clear();
        Console.WriteLine("==============================================");
        Console.WriteLine("          SEJA BEM VINDO AO SUPER CLEAN       ");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine("Por favor, informe o nome do seu utilizador:");
        PrintSucessMessage("- Caso não tenha cadastro, não se preocupe! Vamos criar um novo para você.");
        Console.WriteLine("- Lembre-se: o nome deve ter no máximo 8 caracteres e não pode estar vazio.");
        Console.WriteLine("- O nome deve conter apenas letras e números (exemplo de nome válido: joao123).");
        Console.WriteLine();
        PrintSucessMessage("Digite 'AJUDA' para acessar o manual.");
        Console.WriteLine();
        Console.WriteLine("Vamos começar! Fique à vontade.");
        Console.WriteLine();
        Console.Write("O nome do seu UTILIZADOR: ");


        string utilizador = Console.ReadLine()?.ToLower();

        UserStart(utilizador);

    }

    public static void MainMenuUser(Guid userId, string utilizador)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("          MENU PRINCIPAL SUPER CLEAN          ");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opção abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Marcar Limpeza da Área");
            Console.WriteLine("2 - Remover Última Limpeza da Área");
            Console.WriteLine("3 - Consulta Limpeza - Simulador De Datas");
            Console.WriteLine("4 - Menu Utilizador");
            Console.WriteLine("5 - Menu Residência");
            Console.WriteLine("6 - Sair");
            Console.WriteLine("7 - Ajuda");
            Console.WriteLine();



            switch (GetOption(1, 7))
            {

                case 1:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PRINCIPAL SUPER CLEAN          ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("        *** Marcar Limpeza da Área ***        ");
                    Console.WriteLine();
                    Room.CleanDone(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PRINCIPAL SUPER CLEAN          ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("     *** Remover Última Limpeza da Área ***   ");
                    Console.WriteLine();
                    DisplayInfoUser(utilizador);
                    Console.WriteLine();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PRINCIPAL SUPER CLEAN          ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine(" *** Consulta Limpeza - Simulador De Datas ***");
                    Console.WriteLine();
                    DisplayInfoUser(utilizador);
                    Console.WriteLine();
                    break;

                case 4:
                    Console.Clear();
                    UserMenu(userId, utilizador);
                    break;

                case 5:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;

                case 6:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PRINCIPAL SUPER CLEAN          ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    SaveUsersToFile();
                    Thread.Sleep(1000);
                    PrintSucessMessage("Clique em qualquer tecla para fechar a tela!");
                    Environment.Exit(0);
                    break;

                case 7:
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
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("        MENU DO UTILIZADOR SUPER CLEAN        ");
            Console.WriteLine("==============================================");
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
                    Console.WriteLine("==============================================");
                    Console.WriteLine("        MENU DO UTILIZADOR SUPER CLEAN        ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("        *** Alterar nome do Utilizador ***    ");
                    Console.WriteLine();
                    ChangeUsername(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("        MENU DO UTILIZADOR SUPER CLEAN        ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("        *** Apagar Utilizador ***             ");
                    Console.WriteLine();
                    DeleteUser(userId);
                    Console.WriteLine();
                    WaitForUser();
                    HeaderProgramUserStart();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("        MENU DO UTILIZADOR SUPER CLEAN        ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("        *** Criar novo Utilizador ***         ");
                    Console.WriteLine();
                    HeaderProgramUserStart();
                    Console.WriteLine();
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
            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("          MENU RESIDÊNCIA SUPER CLEAN         ");
            Console.WriteLine("==============================================");
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
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU RESIDÊNCIA SUPER CLEAN         ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("        *** Editar nome da residência ***     ");
                    Console.WriteLine();
                    Residence.ChangeNameResidence(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
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
            Console.WriteLine("==============================================");
            Console.WriteLine("          MENU PISO SUPER CLEAN               ");
            Console.WriteLine("==============================================");
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
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PISO SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("   *** Listagem dos pisos do utilizador ***   ");
                    Console.WriteLine();
                    Residence.ChangeNumberFloor(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PISO SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("*** Adicionar pisos à residência do utilizador ***");
                    Console.WriteLine();
                    Residence.AddFloorUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU PISO SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("*** Remover pisos da residência do utilizador ***");
                    Console.WriteLine();
                    Residence.DeleteFloorUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
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
            Console.WriteLine("==============================================");
            Console.WriteLine("          MENU ÁREA SUPER CLEAN               ");
            Console.WriteLine("==============================================");
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
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU ÁREA SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("    *** Editar nome da área do utilizador *** ");
                    Console.WriteLine();
                    Floor.ChangeNameRoom(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU ÁREA SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("*** Adicionar área à residência do utilizador ***");
                    Console.WriteLine();
                    Floor.AddRoomUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          MENU ÁREA SUPER CLEAN               ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine("*** Remover área da residência do utilizador ***");
                    Console.WriteLine();
                    Floor.DeleteRoomUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;
            }
        }
    }



    public static void MainMenuAdmin()
    {
        Console.Clear();
        Console.WriteLine("==============================================");
        Console.WriteLine("          MENU ADMIN SUPER CLEAN               ");
        Console.WriteLine("==============================================");
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












