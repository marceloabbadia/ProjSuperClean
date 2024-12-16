namespace ProjSuperClean;

public class User
{

    public string Username { get; set; }
    public Residence Residence { get; set; }

    public User(string username)
    {
        if (username.Length > 8)
        {
            throw new ArgumentException("O username deve ter no máximo 8 caracteres.");
        }

        Username = username;
    }
}


