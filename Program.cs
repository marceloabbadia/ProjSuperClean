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

    public static void MainMenuUser(Guid userId, string utilizador)
    {


        while (true)
        {
            Console.WriteLine("MENU PRINCIPAL SUPERCLEAN");
            Console.WriteLine();

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
                    Console.WriteLine();
                    Console.WriteLine("LISTAGEM COMPLETA DO UTILIZADOR");
                    Console.WriteLine();
                    DisplayInfoUser(utilizador);
                    Console.WriteLine();
                    PrintSucessMessage("Listagem Realizada com Sucesso.");
                    WaitForUser();
                    break;

                case 4:
                    Console.Clear();
                    SaveUsersToFile();
                    Console.WriteLine("Fechando o programa...");
                    Console.WriteLine("Clique em qualquer tecla para janela fehcar!");
                    Environment.Exit(0);
                    break;


            }
        }
    }

    public static void UserMenu(Guid userId, string utilizador)
    {


        while (true)
        {
            Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, Utilizador {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Alterar nome do seu Utilizador");
            Console.WriteLine("2 - Apagar seu Utilizador");
            Console.WriteLine("3 - Criar novo Utilizador");
            Console.WriteLine("4 - Voltar Menu Principal");

            var user = users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                PrintErrorMessage("Utilizador não encontrado.");
                WaitForUser();
                return;
            }

            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine($"Atual nome do utilizador: {user.Username}");
                    Console.WriteLine("Digite o novo nome do seu utilizador (máximo 8 caracteres):");
                    Console.WriteLine();
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
                    Console.Clear();
                    Console.WriteLine("MENU DO UTILIZADOR SUPERCLEAN");
                    Console.WriteLine();
                    DeleteUser(userId);
                    Console.WriteLine();
                    WaitForUser();
                    HeaderProgramUserStart();
                    break;

                case 3:
                    Console.Clear();
                    CreateUser(utilizador);
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
            Console.WriteLine("MENU RESIDÊNCIA SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opção abaixo, {utilizador}:");
            Console.WriteLine();

            Console.WriteLine("1 - Editar nome da residência");
            Console.WriteLine("2 - Editar pisos");
            Console.WriteLine("3 - Editar área");
            Console.WriteLine("4 - Voltar ao menu principal");

            var user = users.FirstOrDefault(u => u.UserId == userId);

            if (user == null || user.Residence == null)
            {
                PrintErrorMessage("Utilizador ou residência não encontrado.");
                WaitForUser();
                return;
            }


            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU RESIDÊNCIA SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine($"Atual nome da residência: {user.Residence.ResidenceName}");
                    Console.WriteLine("Digite o novo nome da residência do seu utilizador: ");
                    Console.WriteLine();
                    string newNameResidence = Console.ReadLine();

                    try
                    {
                        if (newNameResidence != null)
                        {
                            user.Residence.ResidenceName = newNameResidence;
                            SaveUsersToFile();
                            PrintSucessMessage("O Nome da sua residência foi alterada com sucesso!");
                        }
                        else
                        {
                            PrintErrorMessage("Nome da residência inválido (vazia)!");

                        }

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
                    Console.WriteLine();
                    FloorMenu(userId, utilizador);
                    break;

                case 3:
                    Console.WriteLine();
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
            Console.WriteLine("MENU PISO SUPERCLEAN");
            Console.WriteLine();

            Console.WriteLine($"Escolha uma opcao abaixo, utilizador {utilizador}:");
            Console.WriteLine();
            Console.WriteLine("1 - Editar Numero do Piso");
            Console.WriteLine("2 - Incluir Piso");
            Console.WriteLine("3 - Excluir Piso");
            Console.WriteLine("4 - Voltar Menu Residencia");

            var user = users.FirstOrDefault(u => u.UserId == userId);

            if (user == null || user.Residence == null)
            {
                PrintErrorMessage("Utilizador ou residência não encontrado.");
                WaitForUser();
                return;
            }

            switch (GetOption(1, 4))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("MENU PISO SUPERCLEAN");
                    Console.WriteLine();
                    Console.WriteLine("Listagem dos Pisos do utilizador");
                    Console.WriteLine();

                    int counter = 1;

                    foreach (var floor in user.Residence.ResidenceFloors)
                    {
                        Console.WriteLine($"\t - Piso: {floor.FloorName}");

                        if (floor.Rooms != null && floor.Rooms.Count > 0)
                        {
                            foreach (var room in floor.Rooms)
                            {
                                string counterFormatted = counter.ToString("D2");
                                string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);
                                counter++;
                            }
                        }
                    }
                    Console.WriteLine();
                    PrintSucessMessage("Fim da listagem dos pisos.");
                    Console.WriteLine();

                    Console.WriteLine("Informe o número do piso que gostaria de alterar: ");
                    string floorNumber = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Qual o novo número do piso que deseja? ");
                    string newFloorNumber = Console.ReadLine();

                    var floorToEdit = user.Residence.ResidenceFloors.FirstOrDefault(f => f.FloorName == floorNumber);

                    if (floorToEdit != null)
                    {
                        
                        floorToEdit.FloorName = newFloorNumber;
                        SaveUsersToFile(); 
                        PrintSucessMessage("Número do piso alterado com sucesso!");
                    }
                    else
                    {
                        PrintErrorMessage("Piso não encontrado!");
                    }

                    WaitForUser();
                    break;

               
                case 2:
                    Console.WriteLine();
                    break;

                case 3:
                    Console.WriteLine();
                    break;

                case 4:
                    Console.Clear();
                    ResidenceMenu(userId, utilizador);
                    break;
            }
        }
    }
    
    
    // -------------------------------------------/-----------------------------//

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
                    //DisplayInfoUserAdmin();
                    Console.WriteLine();
                    PrintSucessMessage("Listagem do Utilizador criada com sucesso!");
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




