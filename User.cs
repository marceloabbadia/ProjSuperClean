namespace ProjSuperClean;

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
        return newUser;

    }

    public static void DisplayInfoUser()
    {
        foreach (var user in users) 
        Console.WriteLine($"Username: {user.Username}, ID: {user.UserId}");
    }


    private static bool UserExists(string username)
    {
        return users.Exists(u => u.Username == username);
    }
}
