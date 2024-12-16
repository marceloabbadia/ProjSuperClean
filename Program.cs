namespace ProjSuperClean;

public class Program
{
    static void Main(string[] args)
    {
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
            Console.WriteLine("2 - Editar Utilizador");
            Console.WriteLine("3 - Apagar Utilizador");
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
}

