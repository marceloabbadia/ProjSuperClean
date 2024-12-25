namespace ProjSuperClean.Class;

using System.Drawing;
using System.Security.Policy;
using ProjSuperClean.Utils;


public class User
{
    public string Username { get; set; }
    public Residence? Residence { get; set; }
    public Guid UserId { get; set; }

    private static List<User> users = new List<User>();

    public User(string username)
    {
        Username = username;
        if (UserId == Guid.Empty)
        {
            UserId = Guid.NewGuid();
        }
    }
    public User() { }


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
        Console.WriteLine($"Informe o nome da morada para o utilizador '{username}':");
        string residenceName = Console.ReadLine();

        User newUser = new User(username);
        Residence residence = new Residence(residenceName);

        bool addFloors = true;

        while (addFloors)
        {
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
                    Console.WriteLine($"Informe o nome da divisão no piso {floorName} (ou 'fim' para voltar ao piso):");
                    Console.WriteLine("*** Exemplo: Quarto ou Sala de Jantar ***");
                    Console.WriteLine();

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
                            Console.WriteLine("Informe o tempo de limpeza (em minutos):");
                            Console.WriteLine();

                            if (int.TryParse(Console.ReadLine(), out cleanTime))
                            {
                                Utils.PrintSucessMessage($"Tempo de limpeza definido: {cleanTime} minutos.");
                                Console.WriteLine();
                                isCleanTime = true;
                            }
                            else
                            {
                                Utils.PrintErrorMessage("Entrada inválida! Por favor, insira um número inteiro.");
                            }
                        }

                        bool isCleanIntervalValid = false;
                        int cleanInterval = 0;


                        while (!isCleanIntervalValid)
                        {
                            Console.WriteLine("Informe o intervalo de limpeza (em dias):");

                            if (int.TryParse(Console.ReadLine(), out cleanInterval))
                            {
                                Utils.PrintSucessMessage($"Intervalo de limpeza definido: {cleanInterval} dias.");
                                isCleanIntervalValid = true;
                            }
                            else
                            {
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
        Utils.PrintSucessMessage("Utilizador adicionado com sucesso!");

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
            Console.WriteLine("Usuários salvos com sucesso no arquivo.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar usuários no arquivo: {ex.Message}");
        }
    }

    public static void LoadUsersFromFile()
    {
        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        try
        {
            if (File.Exists(filePath))
            {
                string usersJson = File.ReadAllText(filePath);
                Console.WriteLine("Arquivo encontrado. Carregando usuários...");

                users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson) ?? new List<User>();

                //Console.WriteLine($"Usuários carregados com sucesso. Total: {users.Count}");


            }
            else
            {
                Console.WriteLine("Arquivo não encontrado. Criando lista vazia de usuários.");
                users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar usuários do arquivo: {ex.Message}");
            users = new List<User>();
        }
    }


    public static void DisplayInfoUser()
    {
        bool found = false;

        foreach (var user in users)
        {
            if (user != null)
            {
                Console.WriteLine($"Username: {user.Username}, ID: {user.UserId}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Nenhum usuário válido encontrado.");
        }
    }


    public static void DisplayInfoCompleteAdmin()
    {
        bool found = false;
        int counter = 1;

        foreach (var user in users)
        {
            if (user != null)
            {
                Console.WriteLine($"Username: {user.Username}, ID: {user.UserId}");
                Console.WriteLine($"Residence: {user.Residence?.ResidenceName}");

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
                                string roomInfo = $" - {counterFormatted} {room.RoomName} - {room.RoomId}".PadRight(20);
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
                found = true;
            }
        }

        if (!found)
        {

            Console.WriteLine("Nenhum Utilizador foi encontrado!");
        }
    }



    public static bool UserExists(string username)
    {
        return users.Exists(u => string.Equals(u.Username, username, StringComparison.Ordinal));

    }

    public static Guid GetUserId(string username)
    {
        var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.Ordinal));
        return user?.UserId ?? Guid.Empty;
    }



}

