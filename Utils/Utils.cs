using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ProjSuperClean.Class;



namespace ProjSuperClean.Utils;

public static class Utils
{
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