namespace ProjSuperClean.Class;

using System.Drawing;
using System.Security.Policy;
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


    public static User CreateUserAdmin(string username)
    {
        if (UserExists(username))
        {
            Console.WriteLine($"O nome do usuario {username} ja esta em uso!");
            return null;
        }

        User newUser = new User(username);
        users.Add(newUser);
        SaveUsersToFile();
        return newUser;
    }

    public static User CreateUser(string username)
    {
        Console.WriteLine();
        Console.WriteLine($"Informe o nome da morada para o utilizador '{username}':");
        string residenceName = Console.ReadLine();

        User newUser = new User(username);
        Residence residence = new Residence(residenceName);

        bool addFloors = true;

        while (addFloors)
        {
            Console.WriteLine();
            Console.WriteLine("Por favor, informe o número do piso usando sempre 2 dígitos (ou digite 'fim' para encerrar):");
            Console.WriteLine("*** Exemplo: '01' para o primeiro piso, '02' para o segundo, e assim por diante. ***");

            string floorName = Console.ReadLine()?.Trim();


            if (floorName.ToLower() == "fim")
            {
                Utils.PrintSucessMessage("Encerrando a entrada de pisos.");
                addFloors = false;
            }
            else if (!string.IsNullOrEmpty(floorName) && floorName.Length == 2)
            {
                Floor floor = new Floor(floorName);
                bool addRooms = true;

                while (addRooms)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Informe o nome da divisão no piso {floorName} (ou 'fim' para voltar ao piso):");
                    Console.WriteLine("*** Exemplo: Quarto ou Sala de Jantar ***");

                    string roomName = Console.ReadLine();

                    if (roomName.ToLower() == "fim")
                    {
                        addRooms = false;
                    }
                    else
                    {
                        bool isCleanTime = false;
                        int cleanTime = 0;


                        while (!isCleanTime)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Informe o tempo de limpeza (em minutos):");

                            if (int.TryParse(Console.ReadLine(), out cleanTime))
                            {
                                Utils.PrintSucessMessage($"Tempo de limpeza definido: {cleanTime} minutos.");
                                Console.WriteLine();
                                isCleanTime = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Utils.PrintErrorMessage("Entrada inválida! Por favor, insira um número inteiro.");
                            }
                        }

                        bool isCleanIntervalValid = false;
                        int cleanInterval = 0;


                        while (!isCleanIntervalValid)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Informe o intervalo de limpeza (em dias):");

                            if (int.TryParse(Console.ReadLine(), out cleanInterval))
                            {
                                Utils.PrintSucessMessage($"Intervalo de limpeza definido: {cleanInterval} dias.");
                                isCleanIntervalValid = true;
                            }
                            else
                            {
                                Console.WriteLine();
                                Utils.PrintErrorMessage("Entrada inválida! Por favor, insira um número inteiro.");
                            }
                        }


                        Room room = new Room(roomName, cleanTime, cleanInterval);
                        floor.AddRoom(room);
                    }
                }


                residence.AddFloor(floor);
            }
        }


        newUser.Residence = residence;


        users.Add(newUser);
        SaveUsersToFile();

        Console.WriteLine();
        Console.WriteLine("DETALHAMENTO DO CADASTRO DO UTILIZADOR");
        Console.WriteLine();
        DisplayInfoUser(newUser.Username);

        return newUser;
    }

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

    public static List<User> LoadUsersFromFile()
    {
        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        try
        {
            if (File.Exists(filePath))
            {
                string usersJson = File.ReadAllText(filePath);

                return users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson) ?? new List<User>();
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado. Criando lista vazia de usuários.");
                return users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar usuários do arquivo: {ex.Message}");
            return users = new List<User>();
        }
    }



    public static void DisplayInfoUser(string username)
    {
        var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.Ordinal));


        if (user != null)
        {
            Console.WriteLine($"Username: {user.Username}");
            Console.WriteLine($"ID: {user.UserId}");
            Console.WriteLine($"Residence: {user.Residence?.ResidenceName}");

            int counter = 1;

            if (user.Residence?.ResidenceFloors != null && user.Residence.ResidenceFloors.Count > 0)
            {
                foreach (var floor in user.Residence.ResidenceFloors)
                {
                    Console.WriteLine($"\t - Floor: {floor.FloorName}");

                    if (floor.Rooms != null && floor.Rooms.Count > 0)
                    {
                        foreach (var room in floor.Rooms)
                        {
                            string counterFormatted = counter.ToString("D2");
                            string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);
                            counter++;


                            int[] pipes = Room.GenerationPypes(room);
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
                }
            }
            Console.WriteLine();
            Utils.PrintSucessMessage("Listagem Realizada com Sucesso.");

        }
    }

    public static void DisplayInfoCompleteAdmin()
    {
        bool found = false;
        int count = 0;

        Console.WriteLine($"Total utilizadores carregados:{count}");
        Console.WriteLine();

        foreach (var user in users)
        {
            if (user != null)
            {
                count++;

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
                            foreach (var room in floor.Rooms)
                            {
                                string counterFormatted = counter.ToString("D2");
                                string roomInfo = $" - {counterFormatted} {room.RoomName}".PadRight(20);
                                counter++;


                                int[] pipes = Room.GenerationPypes(room);
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
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                found = true;
            }
        }

        if (!found)
        {

            Console.WriteLine("Nenhum Utilizador foi encontrado!");
        }
    }



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
        Console.WriteLine("Digite o novo nome do seu utilizador (máximo 8 caracteres):");
        Console.WriteLine();
        string newName = Console.ReadLine()?.Trim();

        try
        {
            if (ValidationNameUser(newName))
            {
                User.UpdateUserName(userId, newName);
                Utils.PrintSucessMessage("O nome do utilizador foi alterado com sucesso!");
            }
            else
            {
                Utils.PrintErrorMessage($"username {newName} vazio ou com mais que 8 caracteres!");
            }
        }

        catch (ArgumentException ex)
        {
            Utils.PrintErrorMessage($"Erro ao alterar o nome do utilizador: {ex.Message}");
        }
        catch (Exception ex)
        {
            Utils.PrintErrorMessage($"Erro inesperado: {ex.Message}");
        }

        Utils.WaitForUser();
    }


    public static void UpdateUserName(Guid userId, string newName)
    {
        var user = users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
            throw new ArgumentException("Utilizador não encontrado.");

        user.Username = newName;
        SaveUsersToFile();

    }

    public static bool ValidationNameUser(string name)
    {

        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        else if (name.Length > 8)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void DeleteUser(Guid userId)
    {
        try
        {
            var user = users.FirstOrDefault(u => u.UserId == userId) ?? throw new ArgumentException("Utilizador não encontrado.");

            Console.WriteLine($"Usurario = {user.Username} and {user.UserId}");

            users.Remove(user);
            SaveUsersToFile();

            Utils.PrintSucessMessage("Utilizador deletado com sucesso!");
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

    public static bool UserExists(string username)
    {
        List<User> users = LoadUsersFromFile();

        return users.Any(u => u.Username.Equals(username));


    }

    public static Guid GetUserId(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("O nome do utilizador não pode ser vazio ou nulo.");

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

