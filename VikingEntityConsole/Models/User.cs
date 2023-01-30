namespace VikingEntityConsole.Models;

public class User
{
    public int Oid { get; set; } = 0;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public bool RequirePasswordChange { get; set; } = false;
    
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public User Get()
    {
        var userBase = new UserBase();
        userBase.Load();
        User user;
        if (int.TryParse(Oid.ToString(), out int oid))
        {
            user = userBase.GetById(oid);
        }
        else
        {
            user = userBase.GetByUsername(UserName);
        }

        if (user == null)
        {
            return new User();
        }

        return user;
    }

}