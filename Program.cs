using System.Security.Cryptography.X509Certificates;
using ProjSuperClean.Class;
using static ProjSuperClean.Utils.Utils;
using static ProjSuperClean.Class.User;
using static ProjSuperClean.Class.Room;
using static ProjSuperClean.Class.Floor;
using static ProjSuperClean.Class.Residence;
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

        Title("SEJA BEM-VINDO(A) AO SUPER CLEAN");
        Console.WriteLine();
        Console.WriteLine("Por favor, nos informe o nome do seu utilizador.");
        PrintSucessMessage("- Caso não tenha cadastro, será criado um novo para você.");
        Console.WriteLine("- Lembre-se: Deve ter no máximo 8 caracteres.");
        Console.WriteLine("- Lembre-se: Use apenas letras e números ");
        Console.WriteLine("- Exemplo de nome válido: 'joao123'.");
        Console.WriteLine();
        Console.WriteLine("Vamos começar! Fique à vontade.");
        Console.WriteLine();
        Console.Write("Digite aqui o nome do seu UTILIZADOR: ");


        string utilizador = Console.ReadLine()?.Trim();

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
                    CleanDone(userId, utilizador);
                    WaitForUser();
                    break;

                case 2:
                    DeleteLastClean(userId, utilizador);
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
                    RoomsList(userId, utilizador);
                    WaitForUser();
                    break;

                case 2:
                    DisplayInfoUser(userId, utilizador);
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
            Console.WriteLine("3 - Configurações do Utilizador");
            Console.WriteLine("4 - Configurações da Residência");
            Console.WriteLine("5 - Ajuda");
            Console.WriteLine("6 - Sair");

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
                    HelpMenu(userId, utilizador);
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

            utilizador = RefreshUserName(userId, utilizador);

            Title("MENU DO UTILIZADOR SUPER CLEAN");
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
                    Console.Clear();
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

            Console.WriteLine("1 - Atalhos Rápidos");
            Console.WriteLine("2 - Gerenciar Limpezas");
            Console.WriteLine("3 - Editar nome da residência");
            Console.WriteLine("4 - Editar pisos");
            Console.WriteLine("5 - Editar área");
            Console.WriteLine("6 - Voltar ao menu principal");

            switch (GetOption(1, 6))
            {
                case 1:
                    FastMenu(userId, utilizador);
                    break;

                case 2:
                    CleaningMenu(userId, utilizador);
                    break;

                case 3:
                    ChangeNameResidence(userId, utilizador);
                    break;

                case 4:
                    FloorMenu(userId, utilizador);
                    break;

                case 5:
                    RoomMenu(userId, utilizador);
                    break;

                case 6:
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
            Console.WriteLine("1 - Atalhos Rápidos");
            Console.WriteLine("2 - Gerenciar Limpezas");
            Console.WriteLine("3 - Editar número do piso");
            Console.WriteLine("4 - Incluir Piso");
            Console.WriteLine("5 - Excluir Piso");
            Console.WriteLine("6 - Voltar Menu Residencia");


            switch (GetOption(1, 6))
            {
                case 1:
                    FastMenu(userId, utilizador);
                    break;

                case 2:
                    CleaningMenu(userId, utilizador);
                    break;

                case 3:
                    ChangeNumberFloor(userId, utilizador);
                    break;

                case 4:
                    AddFloorUser(userId, utilizador);
                    break;

                case 5:
                    DeleteFloorUser(userId, utilizador);
                    break;

                case 6:
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

            Console.WriteLine("1 - Atalhos Rápidos");
            Console.WriteLine("2 - Gerenciar Limpezas");
            Console.WriteLine("3 - Editar nome da área");
            Console.WriteLine("4 - Incluir área");
            Console.WriteLine("5 - Excluir área");
            Console.WriteLine("6 - Voltar ao menu residência");


            switch (GetOption(1, 6))
            {
                case 1:
                    FastMenu(userId, utilizador);
                    break;

                case 2:
                    CleaningMenu(userId, utilizador);
                    break;

                case 3:
                    ChangeNameRoom(userId, utilizador);
                    Console.WriteLine();
                    WaitForUser();
                    break;

                case 4:
                   AddRoomUser(userId, utilizador);
                    break;

                case 5:
                    DeleteRoomUser(userId, utilizador);
                    break;

                case 6:
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












