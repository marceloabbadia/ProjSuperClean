using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjSuperClean.Utils;

public static class Utils
{
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

    public static void PrintSucessMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Realizado com Sucesso");
        Console.ResetColor();
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

    public static void HeaderProgramUser(int opcao)
    {
        switch (opcao)
        {
            case 1:
                Console.WriteLine("MENU UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Criacao de Utilizador");
                Console.WriteLine();
                break;

            case 2:
                Console.WriteLine("MENU UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Listar Utilizador(es)");
                Console.WriteLine();
                break;

            case 3:
                Console.WriteLine("MENU UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Editar Utilizador");
                Console.WriteLine();
                break;

            case 4:
                Console.WriteLine("MENU UTILIZADOR SUPERCLEAN");
                Console.WriteLine();
                Console.WriteLine("Apagar Utilizador");
                Console.WriteLine();
                break;



        }



    }

}
