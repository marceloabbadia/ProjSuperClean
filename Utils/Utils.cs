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
    //Atualiza Username do utilizador apos troca
    public static string RefreshUserName(Guid userId, string currentUsername)
    {
        var updatedUser = User.users.FirstOrDefault(u => u.UserId == userId);

        if (updatedUser == null)
        {
            Console.WriteLine("Utilizador não encontrado.");
            return currentUsername;
        }

        return updatedUser.Username != currentUsername ? updatedUser.Username : currentUsername;

    }

    //Valida limites inferior e superior para os menus
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

    //Aguarda informacao em tela ate usuario digitar algo
    public static void WaitForUser()
    {
        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    //Mensagem de erro na cor vermelha
    public static void PrintErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Erro: {message}");
        Console.ResetColor();
    }

    //Mensagem de sucesso na cor verde
    public static void PrintSucessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    //Mascara dos cabecalhos do programa
    public static void Title(string title)
    {
        Console.WriteLine("=====================================================");
        Console.WriteLine($"<<< {title}");
        Console.WriteLine("=====================================================");
    }

    //Check do Fim e reiniciar Com Ignore Case sensitive
    public static string CheckSpecialCommandsEndAndRestart(string input)
    {
        if (string.IsNullOrEmpty(input)) return null;

        if (input.Equals("fim", StringComparison.OrdinalIgnoreCase))
            return "fim";
        if (input.StartsWith("reiniciar", StringComparison.OrdinalIgnoreCase))
            return "reiniciar";

        return null; 
    }

    //Validacao do input de numeros inteiros para Clear e Interval
    public static int GetValidClearAndIntervalInput()
    {
        while (true)
        {

            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            else
            {
                Utils.PrintErrorMessage("Entrada inválida! Por favor, insira um número inteiro válido.");
            }
        }
    }

    //Validacao do input para Floor
    public static string GetValidFloorInput()
    {
        while (true)
        {
            string input = Console.ReadLine()?.Trim();

            if (input?.ToLower() == "fim" || input?.ToLower() == "reiniciar") return input;

            if (!string.IsNullOrEmpty(input) && input.Length == 2 && int.TryParse(input, out _))
            {
                return input;
            }
            else
            {
                PrintErrorMessage("O número da área (piso) deve ter exatamente 2 dígitos.");
            }
        }
    }

    //Valida eventos de repeticao em loop
    public static bool AskWantContinue()
    {
        Console.WriteLine("Gostaria de alterar novamente? (sim/não): ");
        string response = Console.ReadLine()?.Trim().ToLower();

        return response == "sim" || response == "s" || response =="sin";
    }

    //Valida o input do Room do utilizador 

    //public static string GetValidRoomInput()
    //{
    //    while (true)
    //    {
    //        string input = Console.ReadLine()?.Trim();

    //        if (input?.ToLower() == "fim" || input?.ToLower() == "reiniciar") return input;

    //        if (!string.IsNullOrEmpty(input) && input.Length <= 10)
    //        {
    //            return input;
    //        }
    //        else
    //        {
    //            Utils.PrintErrorMessage("O nome da divisão deve ter no máximo 10 caracteres e não pode estar vazio.");
    //        }
    //    }
    //}


}