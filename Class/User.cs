namespace ProjSuperClean.Class;

public class User
{
    private string _username = string.Empty;

    private static List<User> users = new List<User>();
    public string Username
    {
        get => _username;
        set
        {
            if (value.Length > 8)
                throw new ArgumentException("O username deve ter no máximo 8 caracteres.");
            _username = value;
        }
    }

    public Residence? Residence { get; set; }
    public Guid UserId { get; private set; }

    public User(string username)
    {
        Username = username;
        UserId = Guid.NewGuid();
    }

    public static User CreateUser(string username)
    {
        if (UserExists(username))
        {
            throw new ArgumentException($"O nome do usuario {username} ja esta em uso!");
        }

        User newUser = new User(username);
        users.Add(newUser);  
        SaveUsersToFile();   
        return newUser;
    }

    public static void SaveUsersToFile()
    {
        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        
        string updatedUsersJson = System.Text.Json.JsonSerializer.Serialize(users, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, updatedUsersJson);
    }

    public static void LoadUsersFromFile()
    {
        var dirPath = @"C:\Users\marce\OneDrive\Área de Trabalho\cegid\c#\Restart-24\ProjSuperClean";
        var filePath = Path.Combine(dirPath, "users.json");

        
        if (File.Exists(filePath))
        {
            string usersJson = File.ReadAllText(filePath);
            users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson) ?? new List<User>();
        }
        else
        {
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



    private static bool UserExists(string username)
    {
        return users.Exists(u => u.Username == username);
    }
}
