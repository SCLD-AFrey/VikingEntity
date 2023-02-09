namespace VikingCommon.Models;

public class User : IUser
{
    public int Oid { get; set; } = 0;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; } = Array.Empty<byte>();
    public bool RequirePasswordChange { get; set; } = false;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime LastLogin { get; set; }
    public List<Enums.AdminRole> Roles { get; set; } = new();

    public string FullName => $"{FirstName} {LastName}";
    public bool IsRootUser { get; set; } = false;

    public void Save()
    {
        DateCreated = DateTime.UtcNow;
        UserBase users = new UserBase();
        users.Load();
        users.Add(this);
        users.Commit();
    }
    
    public static User DefaultAdminUser(int p_oid)
    {
        PasswordHash hash = new PasswordHash();
        return new User()
        {
            Oid = p_oid,
            IsRootUser = true,
            UserName = "admin",
            FirstName = "Admin",
            LastName = "User",
            Password = hash.GeneratePasswordHash("password", out var salt),
            Salt = salt,
            LastLogin = DateTime.UtcNow,
            Roles = { Enums.AdminRole.BasicUser, Enums.AdminRole.SettingsManagement, Enums.AdminRole.UserManagement, Enums.AdminRole.Administrator }
        };
    }
}