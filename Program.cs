using System.Security.Cryptography.X509Certificates;
using ProjSuperClean.Class;
using static ProjSuperClean.Utils.Utils;
using static ProjSuperClean.Class.User;

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
        Console.WriteLine("MENU PRINCIPAL SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Menu Utilizador");
            Console.WriteLine("2 - Menu Residencia");
            Console.WriteLine("3 - Listagem completa do seu Utilizador");
            Console.WriteLine("4 - Sair");



            switch (GetOption(1, 4))
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
                    HeaderProgramUserAdmin(5);
                    DisplayInfoCompleteAdmin();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem Realizada com Sucesso.");
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    return;

            }
        }
    }

    public static void UserMenu(Guid userId, string utilizador)
    {

        Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Alterar nome do seu Utilizador");
            Console.WriteLine("2 - Apagar seu Utilizador");
            Console.WriteLine("3 - Voltar Menu Principal");



            switch (GetOption(1, 3))
            {
                case 1:
                    HeaderProgramUserAdmin(1);
                    Console.WriteLine("Digite o novo nome do seu utilizador (máximo 8 caracteres):");
                    string newName = Console.ReadLine();
                    try
                    {

                        ValidationNameUser(newName);
                        User.UpdateUserName(userId, newName);
                        PrintSucessMessage("O Nome do seu utilizador foi alterado com sucesso!");
                        utilizador = newName;
                        WaitForUser();
                        continue;

                    }
                    catch (ArgumentException ex)
                    {
                        PrintErrorMessage(ex.Message);
                        WaitForUser();
                    }
                    break;

                case 2:
                    HeaderProgramUserAdmin(2);
                    DisplayInfoUser();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem de Utilizadores criada com sucesso!");
                    WaitForUser();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Retornando...");
                    return;

            }
        }
    }

    public static void ResidenceMenu(Guid userId, string utilizador)
    {

        Console.WriteLine("MENU RESIDENCIA SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");

            Console.WriteLine("1 - Editar Nome da Residencia");
            Console.WriteLine("2 - Editar Pisos");
            Console.WriteLine("3 - Editar Area");
            Console.WriteLine("4 - Voltar Menu Principal");



            switch (GetOption(1, 4))
            {
                case 1:
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine();
                    break;

                case 3:
                    Console.WriteLine();
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("Retornando...");
                    return;
            }
        }
    }

    // -------------------------------------------//

    public static void MainMenuAdmin()
    {
        Console.WriteLine("MENU PRINCIPAL ADMIN SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine("Escolha uma opcao abaixo:");

            Console.WriteLine("1 - Menu Utilizador");
            Console.WriteLine("2 - Menu Residencia");
            Console.WriteLine("3 - Listagem completa");
            Console.WriteLine("4 - Sair");



            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    UserMenuAdmin();
                    break;

                case 2:
                    Console.Clear();
                    ResidenceMenuAdmin();
                    break;

                case 3:
                    Console.Clear();
                    HeaderProgramUserAdmin(5);
                    DisplayInfoCompleteAdmin();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem de Utilizadores criada com sucesso!");
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    return;

            }
        }
    }

    public static void UserMenuAdmin()
    {

        Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine("Escolha uma opcao abaixo:");

            Console.WriteLine("1 - Criar Utilizador");
            Console.WriteLine("2 - Listar Utilizador(es)");
            Console.WriteLine("3 - Editar Utilizador");
            Console.WriteLine("4 - Apagar Utilizador");
            Console.WriteLine("5 - Voltar Menu Principal");



            switch (GetOption(1, 5))
            {
                case 1:
                    HeaderProgramUserAdmin(1);
                    Console.WriteLine("Digite o nome do seu novo usuário (máximo 8 caracteres):");
                    string name = Console.ReadLine();
                    try
                    {
                        User newUser = User.CreateUser(name);
                        PrintSucessMessage("Utilizador criado com sucesso!");
                        WaitForUser();

                    }
                    catch (ArgumentException ex)
                    {
                        PrintErrorMessage(ex.Message);
                        WaitForUser();
                    }

                    break;

                case 2:
                    HeaderProgramUserAdmin(2);
                    DisplayInfoUser();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem de Utilizadores criada com sucesso!");
                    WaitForUser();
                    break;

                case 3:
                    HeaderProgramUserAdmin(3);
                    Console.WriteLine();
                    break;

                case 4:
                    HeaderProgramUserAdmin(4);
                    Console.WriteLine();
                    break;

                case 5:
                    Console.Clear();
                    Console.WriteLine("Retornando...");
                    return;

            }
        }
    }

    public static void ResidenceMenuAdmin()
    {

        Console.WriteLine("MENU ADMIN RESIDENCIA SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine("Escolha uma opcao abaixo:");

            Console.WriteLine("1 - Criar Residencia");
            Console.WriteLine("2 - Editar Residencia");
            Console.WriteLine("3 - Apagar Residencia");
            Console.WriteLine("4 - Voltar Menu Principal");



            switch (GetOption(1, 4))
            {
                case 1:
                    Console.WriteLine();
                    break;

                case 2:
                    Console.WriteLine();
                    //Room.CleanDone();
                    break;

                case 3:
                    Console.WriteLine();
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine("Retornando...");
                    return;
            }
        }
    }







}




