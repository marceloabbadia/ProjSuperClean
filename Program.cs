using System.Security.Cryptography.X509Certificates;
using ProjSuperClean.Class;
using static ProjSuperClean.Utils.Utils;
using static ProjSuperClean.Class.User;
using static ProjSuperClean.Class.Help;
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

        Title("SEJA BEM VINDO AO SUPER CLEAN");
        Console.WriteLine();
        Console.WriteLine("Por favor, informe o nome do seu utilizador:");
        PrintSucessMessage("- Caso não tenha cadastro, será criado um novo para você.");
        Console.WriteLine("- Lembre-se: Deve ter no máximo 8 caracteres.");
        Console.WriteLine("- Lembre-se: Use apenas letras e números ");
        Console.WriteLine("- Exemplo de nome válido: joao123).");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Vamos começar! Fique à vontade.");
        Console.WriteLine();
        Console.Write("O nome do seu UTILIZADOR: ");


        string utilizador = Console.ReadLine()?.ToLower();

        UserStart(utilizador);


    }


    public static void FastMenu(Guid userId, string utilizador)
    {
        while (true)
        {
            Console.Clear();
            Title("ATALHOS SUPER CLEAN");
            Console.WriteLine();
            Console.WriteLine($"Escolha uma opção abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Marcar Limpeza da Área");
            Console.WriteLine("2 - Remover Última Limpeza");
            Console.WriteLine("3 - Voltar ao Menu Principal");
            Console.WriteLine();

            switch (GetOption(1, 3))
            {
                case 1:
                    Room.CleanDone(userId, utilizador);
                    WaitForUser();
                    break;

                case 2:
                    Room.DeleteLastClean(userId, utilizador);
                    WaitForUser();
                    break;

                case 3:
                    return;
            }
        }
    }


    public static void CleaningMenu(Guid userId, string utilizador)
    {
        while (true)
        {
            Console.Clear();
            Title("GERENCIAR LIMPEZAS");
            Console.WriteLine();
            Console.WriteLine($"Escolha uma opção abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Consultar Limpezas");
            Console.WriteLine("2 - Simulador de Datas");
            Console.WriteLine("3 - Voltar ao Menu Principal");
            Console.WriteLine();

            switch (GetOption(1, 3))
            {
                case 1:
                    Floor.RoomsList(userId, utilizador);
                    WaitForUser();
                    break;

                case 2:
                    User.DisplayInfoUser(userId, utilizador);
                    WaitForUser();
                    break;

                case 3:
                    return;
            }
        }
    }


    public static void MainMenuUser(Guid userId, string utilizador)
    {


        while (true)
        {
            Console.Clear();

            utilizador = RefreshUserName(userId, utilizador);

            Title("MENU PRINCIPAL SUPER CLEAN");
            Console.WriteLine();
            Console.WriteLine($"Escolha uma opção abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Atalhos Rápidos");
            Console.WriteLine("2 - Gerenciar Limpezas");
            Console.WriteLine("3 - Configuracoes do Utilizador");
            Console.WriteLine("4 - Configuracoes da Residência");
            Console.WriteLine("5 - Sair");
            Console.WriteLine("6 - Ajuda");

            Console.WriteLine();



            switch (GetOption(1, 6))
            {

                case 1:
                    FastMenu(userId, utilizador);
                    break;

                case 2:
                    CleaningMenu(userId, utilizador);
                    break;

                case 3:
                    UserMenu(userId, utilizador);
                    break;

                case 4:
                    ResidenceMenu(userId, utilizador);
                    break;

                case 5:
                    Help.HelpMenu(userId, utilizador);
                    WaitForUser();
                    break;

                case 6:
                    SaveUsersToFile();
                    Environment.Exit(0);
                    break;
            }
        }
    }


    public static void UserMenu(Guid userId, string utilizador)
    {

        while (true)
        {
            Console.Clear();
            Title(" MENU DO UTILIZADOR SUPER CLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Atalhos Rápidos");
            Console.WriteLine("2 - Gerenciar Limpezas");
            Console.WriteLine("3 - Alterar nome do Utilizador");
            Console.WriteLine("4 - Apagar Utilizador");
            Console.WriteLine("5 - Criar novo Utilizador");
            Console.WriteLine("6 - Voltar Menu Principal");



            switch (GetOption(1, 6))
            {
                case 1:
                    FastMenu(userId, utilizador);
                    break;

                case 2:
                    CleaningMenu(userId, utilizador);
                    break;

                case 3:

                    ChangeUsername(userId, utilizador);
                    break;

                case 4:

                    DeleteUser(userId);
                    Console.WriteLine();
                    WaitForUser();
                    HeaderProgramUserStart();
                    break;

                case 5:

                    HeaderProgramUserStart();
                    break;

                case 6:
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
            Title("MENU RESIDÊNCIA SUPER CLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opção abaixo, {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Marcar Limpeza da Área");
            Console.WriteLine("2 - Remover Última Limpeza da Área");
            Console.WriteLine("3 - Editar nome da residência");
            Console.WriteLine("4 - Editar pisos");
            Console.WriteLine("5 - Editar área");
            Console.WriteLine("6 - Voltar ao menu principal");

            switch (GetOption(1, 6))
            {
                case 1:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("        *** Marcar Limpeza da Área ***        ");
                    Console.WriteLine();
                    Room.CleanDone(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("     *** Remover Última Limpeza da Área ***   ");
                    Console.WriteLine();
                    DisplayInfoUser(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 3:
                    Console.Clear();
                    Title("MENU RESIDÊNCIA SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("        *** Editar nome da residência ***     ");
                    Console.WriteLine();
                    Residence.ChangeNameResidence(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    FloorMenu(userId, utilizador);
                    break;

                case 5:
                    Console.Clear();
                    RoomMenu(userId, utilizador);
                    break;

                case 6:
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
            Title("MENU PISO SUPER CLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Marcar Limpeza da Área");
            Console.WriteLine("2 - Remover Última Limpeza da Área");
            Console.WriteLine("3 - Editar Numero do Piso");
            Console.WriteLine("4 - Incluir Piso");
            Console.WriteLine("5 - Excluir Piso");
            Console.WriteLine("6 - Voltar Menu Residencia");


            switch (GetOption(1, 6))
            {
                case 1:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("        *** Marcar Limpeza da Área ***        ");
                    Console.WriteLine();
                    Room.CleanDone(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("     *** Remover Última Limpeza da Área ***   ");
                    Console.WriteLine();
                    DisplayInfoUser(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 3:
                    Console.Clear();
                    Title("MENU PISO SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("   *** Listagem dos pisos do utilizador ***   ");
                    Console.WriteLine();
                    Residence.ChangeNumberFloor(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    Title("MENU PISO SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Adicionar pisos à residência do utilizador ***");
                    Console.WriteLine();
                    Residence.AddFloorUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 5:
                    Console.Clear();
                    Title("MENU PISO SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Remover pisos da residência do utilizador ***");
                    Console.WriteLine();
                    Residence.DeleteFloorUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 6:
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
            Title("MENU ÁREA SUPER CLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");
            Console.WriteLine();


            Console.WriteLine("3 - Editar nome da área");
            Console.WriteLine("4 - Incluir área");
            Console.WriteLine("5 - Excluir área");
            Console.WriteLine("6 - Voltar ao menu residência");


            switch (GetOption(1, 6))
            {
                case 1:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("        *** Marcar Limpeza da Área ***        ");
                    Console.WriteLine();
                    Room.CleanDone(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 2:
                    Console.Clear();
                    Title("MENU PRINCIPAL SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("     *** Remover Última Limpeza da Área ***   ");
                    Console.WriteLine();
                    DisplayInfoUser(userId, utilizador);
                    Console.WriteLine();
                    break;

                case 3:
                    Console.Clear();
                    Title("MENU ÁREA SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("    *** Editar nome da área do utilizador *** ");
                    Console.WriteLine();
                    Floor.ChangeNameRoom(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    Title("MENU ÁREA SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Adicionar área à residência do utilizador ***");
                    Console.WriteLine();
                    Floor.AddRoomUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 5:
                    Console.Clear();
                    Title("MENU ÁREA SUPER CLEAN");
                    Console.WriteLine();
                    Console.WriteLine("*** Remover área da residência do utilizador ***");
                    Console.WriteLine();
                    Floor.DeleteRoomUser(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 6:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;
            }
        }
    }


    public static void MainMenuAdmin()
    {
        Title("MENU ADMIN SUPER CLEAN");
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












