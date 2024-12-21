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
        MainMenu();
    }

    public static void MainMenu()
    {
        Console.WriteLine("MENU PRINCIPAL SUPERCLEAN");
        Console.WriteLine();

        while (true)
        {

            Console.WriteLine("Escolha uma opcao abaixo:");

            Console.WriteLine("1 - Menu Utilizador");
            Console.WriteLine("2 - Menu Residencia");
            Console.WriteLine("3 - Sair");



            switch (GetOption(1, 3))
            {
                case 1:
                    Console.Clear();
                    UserMenu();
                    break;

                case 2:
                    Console.Clear();
                    ResidenceMenu();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    return;

            }
        }
    }


    public static void UserMenu()
    {

        Console.WriteLine("MENU UTILIZADOR SUPERCLEAN");
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
                    HeaderProgramUser(1);
                    Console.WriteLine("Digite o nome do seu novo usuário (máximo 8 caracteres):");
                    string name = Console.ReadLine();
                    try
                    {
                        User newUser = User.CreateUser(name);
                        PrintSucessMessage();
                        WaitForUser();

                    }
                    catch (ArgumentException ex)
                    {
                        PrintErrorMessage(ex.Message);
                        WaitForUser();
                    }

                    break;

                case 2:
                    HeaderProgramUser(2);
                    User.DisplayInfoUser();
                    Console.WriteLine();
                    PrintSucessMessage();
                    WaitForUser();
                    break;

                case 3:
                    HeaderProgramUser(3);
                    Console.WriteLine();
                    break;

                case 4:
                    HeaderProgramUser(4);
                    Console.WriteLine();
                    break;

                case 5:
                    Console.Clear();
                    Console.WriteLine("Retornando...");
                    return;

            }
        }
    }


    public static void ResidenceMenu()
    {

        Console.WriteLine("MENU RESIDENCIA SUPERCLEAN");
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




