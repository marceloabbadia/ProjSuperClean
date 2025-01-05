namespace ProjSuperClean.Class;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Security.Policy;
using System.Text.RegularExpressions;
using ProjSuperClean.Utils;


public class User
{
    public string Username { get; set; }
    public Residence? Residence { get; set; }
    public Guid UserId { get; set; }

    public static List<User> users = new List<User>();

    public User(string username)
    {
        Username = username;
        if (UserId == Guid.Empty)
        {
            UserId = Guid.NewGuid();
        }
    }

    // Determina o tipo de login a ser realizado: adm, navegação com utilizador existente ou criação de novo utilizador.
    public static void UserStart(string utilizador)
    {
        utilizador = utilizador.Trim();

        if (utilizador.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            Console.Clear();
            Program.MainMenuAdmin();
            return;
        }

        Guid userId = Guid.Empty;


        while (true)
        {

            if (!IsValidUsername(utilizador, out string errorMessage))
            {
                Utils.PrintErrorMessage(errorMessage);
                Console.WriteLine("Informe um nome válido para o utilizador:");
                utilizador = Console.ReadLine()?.Trim();
                continue;
            }

            if (UserExists(utilizador))
            {
                userId = GetUserId(utilizador);
                Console.Clear();

                // Caso o utilizador não tenha concluído o registo da residência, será redirecionado para finalizar o processo.
                if (!Floor.HasFloors(userId))
                {
                    Console.WriteLine("Parece que a sua residência ainda não está concluída. Vamos ajudá-lo a configurar os pisos agora...");
                    Floor.AddFloorUser(userId, utilizador);
                    break;

                }
                else
                {
                    Program.MainMenuUser(userId, utilizador);
                    break;
                }

            }

            if (CreateUser(utilizador) != null)
            {
                userId = GetUserId(utilizador);
                Utils.WaitForUser();
                Console.Clear();
                Program.MainMenuUser(userId, utilizador);
                break;
            }
        }
    }


    // Cria um novo utilizador, incluindo residência, pisos e divisões.
    public static User CreateUser(string username)
    {
        while (true)
        {
            string residenceName = UserGetResidenceName(username);
            if (residenceName == null) return null;

            User newUser = new(username);
            Residence residence = new(residenceName);

            if (!UserAddFloorsToResidence(residence)) return null;

            newUser.Residence = residence;
            users.Add(newUser);
            SaveUsersToFile();

            Utils.PrintSucessMessage($"Usuário '{username}' e residência '{residenceName}' cadastrados com sucesso!");
            return newUser;
        }
    }

    //Complemento do CreateUser para Residencia
    private static string UserGetResidenceName(string username)
    {
        while (true)
        {
            Console.WriteLine($@"
Informe o nome da residência para o utilizador '{username}':
- Digite o nome desejado para a residência.
- Digite 'fim' para encerrar e retornar ao login.
- Digite 'reiniciar' para reiniciar o processo de cadastro do nome.
");
            string residenceName = Console.ReadLine()?.Trim();

            string command = Utils.CheckSpecialCommandsEndAndRestart(residenceName);

            if (command == "reiniciar")
            {
                Console.WriteLine("Reiniciando o cadastro...");
                continue;
            }

            if (command == "fim")
            {
                Console.WriteLine("Encerrando o cadastro. Retornando ao login...");
                Console.Clear();
                Program.HeaderProgramUserStart();
                return null;
            }

            if (string.IsNullOrEmpty(residenceName))
            {
                Utils.PrintErrorMessage("O nome da residência não pode estar vazio.");
                continue;
            }

            return residenceName;
        }
    }

    //Complemento do CreateUser para Pisos(Floor)
    private static bool UserAddFloorsToResidence(Residence residence)
    {
        while (true)
        {
            Console.WriteLine($@"
Adicione um novo andar à residência:
- Digite o número do andar (exemplo: '00' para térreo, '01' para o primeiro andar...).
- Digite 'fim' para concluir a construção da residência.
- Digite 'reiniciar' para reiniciar o processo de cadastro.
");

            string floorName = Console.ReadLine()?.Trim();

            string command = Utils.CheckSpecialCommandsEndAndRestart(floorName);

            if (command == "fim")
            {
                Console.WriteLine("Finalizando o cadastro de Pisos.");
                return true;
            }
            if (command == "reiniciar")
            {
                Console.WriteLine("Reiniciando o processo de cadastro...");
                return false;
            }

            string errorMessage;
            string validFloorInput = Utils.GetValidFloorInput(floorName, out errorMessage);

            if (validFloorInput == null)
            {
                Utils.PrintErrorMessage(errorMessage);
                continue;
            }

            Floor floor = new(validFloorInput);

            if (!UserAddRoomsToFloor(floor, validFloorInput)) return false;

            residence.ResidenceAddFloor(floor);
            Utils.PrintSucessMessage($"Andar '{validFloorInput}' adicionado à residência.");
        }
    }

    //Complemento do CreateUser para Rooms (divisoes)
    private static bool UserAddRoomsToFloor(Floor floor, string floorName)
    {
        while (true)
        {
            Console.WriteLine($@"
Informe o nome da divisão no piso {floorName}:
- Exemplo: 'Sala'
- Digite 'fim' para voltar ao cadastro de pisos.
- Digite 'reiniciar' para recomeçar o cadastro.
");
            string roomName = Console.ReadLine()?.Trim();

            string command = Utils.CheckSpecialCommandsEndAndRestart(roomName);

            if (command == "fim") return true;
            if (command == "reiniciar") return false;

            if (string.IsNullOrEmpty(roomName))
            {
                Utils.PrintErrorMessage("O nome da divisão não pode estar vazio.");
                continue;
            }

            Console.WriteLine($"Informe o tempo de limpeza da(o) {roomName} (em minutos):");
            int cleanTime = Utils.GetValidClearAndIntervalInput();

            Console.WriteLine($"Informe o intervalo de limpeza da(o) {roomName} (em dias):");
            int cleanInterval = Utils.GetValidClearAndIntervalInput();

            Console.WriteLine($"Deseja realizar a primeira limpeza da(o) {roomName} (sim/não):");
            string decisionCleaning = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(decisionCleaning))
            {
                Utils.PrintErrorMessage("A resposta da limpeza precisa ser 'sim' ou 'não'.");
                continue;
            }

            DateTime? firstCleaning = null;

            if (decisionCleaning.Equals("sim", StringComparison.OrdinalIgnoreCase) ||
               decisionCleaning.Equals("s", StringComparison.OrdinalIgnoreCase) ||
               decisionCleaning.Equals("sin", StringComparison.OrdinalIgnoreCase))
            {
                firstCleaning = DateTime.Now;
            }
            else if (decisionCleaning.Equals("não", StringComparison.OrdinalIgnoreCase) ||
                     decisionCleaning.Equals("nao", StringComparison.OrdinalIgnoreCase) ||
                    decisionCleaning.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                firstCleaning = null;
            }
            else
            {
                Utils.PrintErrorMessage("Resposta inválida. Digite 'sim' ou 'não'.");
                continue;
            }


            Room room = new Room(roomName, cleanTime, cleanInterval, firstCleaning);
            floor.FloorAddRoom(room);

            Utils.PrintSucessMessage($"Divisão '{roomName}' adicionada ao piso {floorName}.");
        }
    }


    //Salva alteracoes, inclusoes, exclusoes em arquivo
    public static void SaveUsersToFile()
    {

        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        try
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }


            string updatedUsersJson = System.Text.Json.JsonSerializer.Serialize(users, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, updatedUsersJson);
            Utils.PrintSucessMessage("A atualização foi salva no arquivo com sucesso!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar usuários no arquivo: {ex.Message}");
        }
    }


    //Carrega arquivos salvos 
    public static List<User> LoadUsersFromFile()
    {
        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        try
        {
            if (File.Exists(filePath))
            {
                string usersJson = File.ReadAllText(filePath);
                var loadedUsers = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson) ?? new List<User>();

                users = loadedUsers;

                foreach (var user in users)
                {
                    if (user.Residence != null && user.Residence.ResidenceFloors != null)
                    {
                        Floor.GlobalAutoSortFloors();
                    }
                }
                return users;
            }
            else
            {
                users = new List<User>();
                return users;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar usuários do arquivo: {ex.Message}");
            users = new List<User>();
            return users;
        }
    }


    //Simulador de datas de limpeza de todos utilizadores - consolida com os UserDisplayRooms
    public static void DisplayInfoUser(Guid userId, string username)
    {
        var user = users.FirstOrDefault(u =>
            (userId != Guid.Empty && u.UserId == userId) ||
            (userId == Guid.Empty && string.Equals(u.Username, username, StringComparison.Ordinal))
        );

        if (user != null)
        {
            DateTime today = DateTime.Today;
            Console.Clear();

            Utils.Title("SIMULADOR DE DATAS SUPER CLEAN");
            Console.WriteLine();

            Console.WriteLine($"Utilizador: {user.Username}, Id: {user.UserId}");
            Console.WriteLine();
            Console.WriteLine($"Nome da residência: {user.Residence?.ResidenceName}");
            Console.WriteLine();

            var roomsWithCleaning = new List<(string FloorName, Room Room)>();
            var roomsWithoutCleaning = new List<(string FloorName, Room Room)>();

            foreach (var floor in user.Residence.ResidenceFloors)
            {
                foreach (var room in floor.Rooms)
                {
                    if (room.FirstCleaning.HasValue)
                    {
                        roomsWithCleaning.Add((floor.FloorName, room));
                    }
                    else
                    {
                        roomsWithoutCleaning.Add((floor.FloorName, room));
                    }
                }
            }

            if (roomsWithCleaning.Any())
            {
                foreach (var (floorName, room) in roomsWithCleaning)
                {
                    Console.WriteLine($"\t- Piso: {floorName}, Quarto: {room.RoomName}, Primeira Limpeza: {room.FirstCleaning.Value.ToShortDateString()}");
                }
                Console.WriteLine();
            }

            int counter = 1;
            foreach (var floor in user.Residence.ResidenceFloors)
            {
                Console.WriteLine($"\t- Piso: {floor.FloorName}");

                foreach (var room in floor.Rooms)
                {
                    string counterFormatted = counter.ToString("D2");
                    string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);

                    int[] pipes = Room.GenerationPypes(room, today);
                    int greenPipe = pipes[0];
                    int yellowPipe = pipes[1];
                    int redPipe = pipes[2];

                    string greenBars = new string('|', greenPipe);
                    string yellowBars = new string('|', yellowPipe);
                    string redBars = new string('|', redPipe);

                    Console.Write($"\t\t{roomInfo}\t");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(greenBars);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(yellowBars);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(redBars);

                    Console.ResetColor();
                    counter++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Pressione as setas do teclado (<- ou ->) para avançar ou retroceder um dia.");
            Console.WriteLine("Pressione 'Enter' para finalizar o simulador.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    today = today.AddDays(1);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    today = today.AddDays(-1);
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }

                Console.Clear();
                Utils.Title("SIMULADOR DE DATAS SUPER CLEAN");
                Console.WriteLine();
                Console.WriteLine($"Dia atual: {today.ToShortDateString()}");
                Console.WriteLine($"Utilizador: {user.Username}, Id: {user.UserId}");
                Console.WriteLine();
                Console.WriteLine($"Nome da residência: {user.Residence?.ResidenceName}");

                Console.WriteLine();

                foreach (var (floorName, room) in roomsWithCleaning)
                {
                    Console.WriteLine($"\t- Piso: {floorName}, Quarto: {room.RoomName}, Primeira Limpeza: {room.FirstCleaning.Value.ToShortDateString()}");
                }

                Console.WriteLine();
                counter = 1;
                foreach (var floor in user.Residence.ResidenceFloors)
                {
                    Console.WriteLine($"\t- Piso: {floor.FloorName}");
                    foreach (var room in floor.Rooms)
                    {
                        string counterFormatted = counter.ToString("D2");
                        string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);

                        int[] pipes = Room.GenerationPypes(room, today);
                        int greenPipe = pipes[0];
                        int yellowPipe = pipes[1];
                        int redPipe = pipes[2];

                        string greenBars = new string('|', greenPipe);
                        string yellowBars = new string('|', yellowPipe);
                        string redBars = new string('|', redPipe);

                        Console.Write($"\t\t{roomInfo}\t");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(greenBars);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(yellowBars);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(redBars);

                        Console.ResetColor();
                        counter++;
                    }
                }
            }
        }
    }



    public static void DisplayInfoCompleteAdmin()
    {
        DateTime today = DateTime.Today;


        while (true)
        {
            bool found = false;
            Console.Clear();
            Console.WriteLine($"Dia atual: {today.ToShortDateString()}");

            foreach (var user in users)
            {
                if (user != null)
                {
                    Console.WriteLine($"Username: {user.Username}, Id: {user.UserId}");
                    Console.WriteLine();
                    Console.WriteLine($"Residence: {user.Residence?.ResidenceName}");
                    int counter = 1;

                    if (user.Residence?.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
                    {
                        foreach (var floor in user.Residence.ResidenceFloors)
                        {
                            Console.WriteLine($"\t - Floor: {floor.FloorName}");

                            if (floor.Rooms != null && floor.Rooms.Count > 0)
                            {
                                DisplayRoomsInfoAdmin(floor.Rooms, today, ref counter);
                                found = true;
                            }
                        }
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Nenhum Utilizador foi encontrado!");
            }


            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.RightArrow)
            {
                today = today.AddDays(1);
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                today = today.AddDays(-1);
            }
            else if (key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }


    //Listagem de todas Rooms com Pipes
    public static void DisplayRoomsInfoAdmin(List<Room> rooms, DateTime today, ref int counter)
    {
        foreach (var room in rooms)
        {
            string counterFormatted = counter.ToString("D2");
            string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);
            counter++;

            int[] pipes = Room.GenerationPypes(room, today);
            int greenPipe = pipes[0];
            int yellowPipe = pipes[1];
            int redPipe = pipes[2];

            string greenBars = new string('|', greenPipe);
            string yellowBars = new string('|', yellowPipe);
            string redBars = new string('|', redPipe);

            Console.Write($"\t\t{roomInfo}\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(greenBars);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(yellowBars);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(redBars);

            Console.ResetColor();
        }
    }


    //Altera o username do utilizador 
    public static void ChangeUsername(Guid userId, string utilizador)
    {
        var user = users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            Utils.PrintErrorMessage("Utilizador não encontrado.");
            Utils.WaitForUser();
            return;
        }

        Console.WriteLine($"Atual nome do utilizador: {user.Username}");
        Console.WriteLine("Digite o novo nome do seu utilizador (máximo 8 caracteres) ou 'fim' para retornar ao Menu:");
        Console.WriteLine();
        string newName = Console.ReadLine()?.Trim();

        if (newName.Equals("fim", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Retornando ao Menu...");
            return;
        }


        if (IsValidUsername(newName, out string errorMessage))
        {
            Utils.PrintSucessMessage(errorMessage);
            Utils.WaitForUser();
            return;
        }

        try
        {

            UpdateUserName(user, newName);
            Utils.PrintSucessMessage("O nome do utilizador foi alterado com sucesso!");
        }

        catch (Exception ex)
        {
            Utils.PrintErrorMessage($"Erro ao alterar o nome do utilizador: {ex.Message}");
        }

        Utils.WaitForUser();
    }

    //Atualiza o username do utilizador
    public static void UpdateUserName(User user, string newName)
    {
        if (user == null)
            throw new ArgumentException("Utilizador não encontrado.");

        user.Username = newName;
        SaveUsersToFile();

    }


    //Valida se o nome do utilizador atende os requisitos
    public static bool IsValidUsername(string username, out string errorMessage)
    {
        if (string.IsNullOrEmpty(username))
        {
            errorMessage = "O nome de utilizador não pode estar vazio.";
            return false;
        }

        if (username.Length > 8)
        {
            errorMessage = "O nome de utilizador deve ter no máximo 8 caracteres.";
            return false;
        }

        foreach (char c in username)
        {
            if (!char.IsLetterOrDigit(c))
            {
                errorMessage = "O nome de utilizador só pode conter letras e números.";
                return false;
            }
        }

        errorMessage = string.Empty;
        return true;
    }


    //Deleta utilizador da base de dados
    public static void DeleteUser(Guid userId)
    {
        try
        {
            var user = users.FirstOrDefault(u => u.UserId == userId) ?? throw new ArgumentException("Utilizador não encontrado.");

            Console.WriteLine($"Utilizador: '{user.Username}', userId: {user.UserId}");

            users.Remove(user);
            Utils.PrintSucessMessage("Utilizador deletado com sucesso!");

            SaveUsersToFile();

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }
    }


    //Veirifca se existe o username do utilizador na base de dados
    public static bool UserExists(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("O nome do utilizador não pode ser nulo ou vazio.", nameof(username));

        List<User> users = LoadUsersFromFile();

        return users.Any(u => u.Username.Equals(username));
    }


    //Localiza o ID do utilizador pela pesquisa do username
    public static Guid GetUserId(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("O nome do utilizador não pode ser vazio ou nulo.", nameof(username));

        var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.Ordinal));

        if (user == null)
        {

            Console.WriteLine($"Utilizador com o nome '{username}' não encontrado.");

            return Guid.Empty;
        }
        else
        {
            return user.UserId;
        }
    }



}

//Simulador de datas de limpeza do utilizador- consolida com os PIPES
// public static void DisplayInfoUser(Guid userId, string username)
// {
//     var user = users.FirstOrDefault(u =>
//    (userId != Guid.Empty && u.UserId == userId) ||
//    (userId == Guid.Empty && string.Equals(u.Username, username, StringComparison.Ordinal))
//);
//     if (user != null)
//     {
//         DateTime today = DateTime.Today;
//         Console.Clear();

//         Utils.Title("SIMULADOR DE DATAS SUPER CLEAN");
//         Console.WriteLine();

//         Console.WriteLine($"Utilizador: {user.Username}, Id: {user.UserId}");
//         Console.WriteLine();
//         Console.WriteLine($"Nome da residência: {user.Residence?.ResidenceName}");
//         int counter = 1;


//         if (user.Residence?.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
//         {

//             foreach (var floor in user.Residence.ResidenceFloors)
//             {
//                 Console.WriteLine($"\t - Piso: {floor.FloorName}");

//                 if (floor.Rooms != null && floor.Rooms.Count > 0)
//                 {
//                     foreach (var room in floor.Rooms)
//                     {
//                         if (room.FirstCleaning != null)
//                         {
//                             string counterFormatted = counter.ToString("D2");
//                             string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);

//                             int[] pipes = Room.GenerationPypes(room, today);
//                             int greenPipe = pipes[0];
//                             int yellowPipe = pipes[1];
//                             int redPipe = pipes[2];

//                             string greenBars = new string('|', greenPipe);
//                             string yellowBars = new string('|', yellowPipe);
//                             string redBars = new string('|', redPipe);

//                             Console.Write($"\t\t{roomInfo}\t");

//                             Console.ForegroundColor = ConsoleColor.Green;
//                             Console.Write(greenBars);

//                             Console.ForegroundColor = ConsoleColor.Yellow;
//                             Console.Write(yellowBars);

//                             Console.ForegroundColor = ConsoleColor.Red;
//                             Console.WriteLine(redBars);

//                             Console.ResetColor();
//                         }
//                         else
//                         {
//                             string counterFormatted = counter.ToString("D2");
//                             string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);


//                         }
//                         counter++;

//                     }
//                 }
//             }
//         }

//         Console.WriteLine();
//         Console.WriteLine("Pressione qualquer uma das setas do teclado (<- ou ->) para iniciar o simulador da datas!");
//         Console.WriteLine("Pressione 'Enter' para finalizar.");

//         while (true)
//         {
//             var key = Console.ReadKey(true).Key;

//             if (key == ConsoleKey.RightArrow)
//             {
//                 today = today.AddDays(1);
//             }
//             else if (key == ConsoleKey.LeftArrow)
//             {
//                 today = today.AddDays(-1);
//             }
//             else if (key == ConsoleKey.Enter)
//             {
//                 break;
//             }

//             Console.Clear();
//             Utils.Title("SIMULADOR DE DATAS SUPER CLEAN");
//             Console.WriteLine();
//             Console.WriteLine($"Dia atual: {today.ToShortDateString()}");
//             Console.WriteLine($"Utilizador: {user.Username}, Id: {user.UserId}");
//             Console.WriteLine();
//             Console.WriteLine($"Nome da residência: {user.Residence?.ResidenceName}");

//             counter = 1;

//             foreach (var floor in user.Residence.ResidenceFloors)
//             {
//                 Console.WriteLine($"\t - Piso: {floor.FloorName}");

//                 if (floor.Rooms != null && floor.Rooms.Count > 0)
//                 {
//                     foreach (var room in floor.Rooms)
//                     {
//                         string counterFormatted = counter.ToString("D2");
//                         string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);
//                         counter++;

//                         int[] pipes = Room.GenerationPypes(room, today);
//                         int greenPipe = pipes[0];
//                         int yellowPipe = pipes[1];
//                         int redPipe = pipes[2];

//                         string greenBars = new string('|', greenPipe);
//                         string yellowBars = new string('|', yellowPipe);
//                         string redBars = new string('|', redPipe);

//                         Console.Write($"\t\t{roomInfo}\t");

//                         Console.ForegroundColor = ConsoleColor.Green;
//                         Console.Write(greenBars);

//                         Console.ForegroundColor = ConsoleColor.Yellow;
//                         Console.Write(yellowBars);

//                         Console.ForegroundColor = ConsoleColor.Red;
//                         Console.WriteLine(redBars);

//                         Console.ResetColor();
//                     }
//                 }
//             }

//             Console.WriteLine();
//             Console.WriteLine("Pressione a seta para a | direita  (->) | para avançar um dia");
//             Console.WriteLine("Pressione a seta para a | esquerda (<-) | para retroceder um dia.");
//             Console.WriteLine("Pressione 'Enter' para finalizar o simulador.");
//         }

//     }
// }

