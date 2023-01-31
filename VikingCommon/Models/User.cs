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

    public void Save()
    {
        UserBase userBase = new UserBase();
        userBase.Add(this);
        userBase.Commit();
    }
}