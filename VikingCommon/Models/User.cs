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
    public DateTime LastLogin { get; set; }
    public List<Enums.AdminRole> Roles { get; set; } = new List<Enums.AdminRole>() { Enums.AdminRole.BasicUser };

    public string FullName => $"{FirstName} {LastName}";

    public void Save()
    {
        
    }
}