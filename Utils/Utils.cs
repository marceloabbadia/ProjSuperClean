using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProjSuperClean.Class;



namespace ProjSuperClean.Utils;

public static class Utils
{
    public static void HeaderProgramUserAdmin(int opcao)
    {
        switch (opcao)
        {
            case 1:
                Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Criacao de Utilizador");
                Console.WriteLine();
                break;

            case 2:
                Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Listar Utilizador(es)");
                Console.WriteLine();
                break;

            case 3:
                Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Editar Utilizador");
                Console.WriteLine();
                break;

            case 4:
                Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Apagar Utilizador");
                Console.WriteLine();
                break;

            case 5:
                Console.WriteLine("MENU ADMIN UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Listagem Completa");
                Console.WriteLine();
                break;





        }



    }

    public static void HeaderProgramUserStart()
    {
        Console.WriteLine("BEM VINDO AO SUPERCLEAN");
        Console.WriteLine();
        Console.WriteLine("Informe o nome de seu Utilizador:");
        Console.WriteLine("- Para criar um novo, digite um nome.");
        Console.WriteLine("- O nome deve ter no máximo 8 caracteres e não pode estar vazio.");
        Console.WriteLine("- Exemplo: joao123.");
        Console.WriteLine();


        string utilizador = string.Empty;

        while (true)
        {
            utilizador = Console.ReadLine()?.ToLower();

            string validationResult = ValidationNameUser(utilizador);

            if (validationResult == null)
            {
                PrintErrorMessage("O nome do utilizador não é válido. Tente novamente.");
            }
            else if (utilizador == "admin")
            {
                Console.Clear();
                Program.MainMenuAdmin();
                return;
            }
            else if (User.UserExists(utilizador))
            {
                Guid userId = User.GetUserId(utilizador);
                Console.Clear();
                Program.MainMenuUser(userId, utilizador);
                return;
            }
            else
            {
                User.CreateUser(utilizador);
                return;
            }
        }

    }

    public static string ValidationNameUser(string name)
    {
        try
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("O nome do utilizador não pode ser vazio.");
            if (name.Length > 8)
                throw new ArgumentException("O nome do utilizador não pode ter mais de 8 caracteres.");

            return name;

        }
        catch (ArgumentException ex)
        {
            PrintErrorMessage(ex.Message);
            WaitForUser();
            return null;
        }
    }

    public static int GetOption(int min, int max)
    {
        int opcao;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out opcao) && opcao >= min && opcao <= max)
                return opcao;

            Console.WriteLine($"Opção inválida. Por favor, escolha uma opção entre {min} e {max}.");
        }


    }
   
    public static void WaitForUser()
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
        Console.Clear();
    }
    
    public static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Erro: {message}");
        Console.ResetColor();
    }
    
    public static void PrintSucessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

}