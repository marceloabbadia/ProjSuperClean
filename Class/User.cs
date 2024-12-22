namespace ProjSuperClean.Class;

using System.Drawing;
using System.Security.Policy;
using ProjSuperClean.Utils;


public class User
{
    public string Username { get; set; }
    public Residence? Residence { get; set; }
    public Guid UserId { get;  set; }

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
        if (UserExists(username))
        {
            Utils.PrintErrorMessage($"O nome do usuario {username} ja esta em uso!");
            return null;
        }

        Console.WriteLine($"Informe o nome da residência para o utilizador '{username}':");
        string residenceName = Console.ReadLine();

        User newUser = new User(username);

        if (newUser.Residence != null)
        {
            Utils.PrintErrorMessage($"O utilizador '{username}' já possui uma residência!");
            return null;
        }
        else
        {
            Residence residence = new Residence(residenceName);

            bool addFloors = true;

            while (addFloors)
            {
                Console.WriteLine("Informe o nome do piso (ou 'fim' para terminar):");
                string floorName = Console.ReadLine();

                if (floorName.ToLower() == "fim")
                {
                    addFloors = false;
                }
                else
                {
                    Floor floor = new Floor(floorName);

                    bool addRooms = true;

                    while (addRooms)
                    {
                        Console.WriteLine($"Informe o nome da divisão no piso {floorName} (ou 'fim' para terminar):");
                        string roomName = Console.ReadLine();
                        if (roomName.ToLower() == "fim")
                        {
                            addRooms = false;
                        }
                        else
                        {
                            Console.WriteLine("Informe o tempo de limpeza (em minutos):");
                            int cleanTime = int.Parse(Console.ReadLine());

                            Console.WriteLine("Informe o intervalo de limpeza (em dias):");
                            int cleanInterval = int.Parse(Console.ReadLine());

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
            Utils.PrintSucessMessage();

            return newUser;


        }

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

                Console.WriteLine($"Usuários carregados com sucesso. Total: {users.Count}");
           

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
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No valid users found.");
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

