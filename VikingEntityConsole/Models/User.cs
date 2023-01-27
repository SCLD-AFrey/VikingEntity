namespace VikingEntityConsole.Models;

public class User
{
    public int Oid { get; set; } = 0;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool RequirePasswordChange { get; set; } = false;
}