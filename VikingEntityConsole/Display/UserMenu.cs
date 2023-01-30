using VikingEntityConsole.Helpers;
using VikingCommon;
using VikingEntityConsole.Models;
using static VikingEntityConsole.Program;

namespace VikingEntityConsole.Display;

public class UserMenu : AbstractMenu
{
    public UserMenu() : base("Users Menu") {}
    
    protected override void Init()
    {
        AddMenuItem(new MenuItem(0, "Display Users", DisplayUsers));
        AddMenuItem(new MenuItem(1, "Add User"));
        AddMenuItem(new MenuItem(2, "Edit User"));
        AddMenuItem(new MenuItem(3, "Remove User"));
        AddMenuItem(new MenuItem(4, "Change Password", ChangePassword));
        AddMenuItem(new MenuItem(5, "Back to Main").SetAsExitOption());
    }

    private void DisplayUsers()
    {
        Messages.Results($"{_userBase.Users!.Count} users found");
        foreach (var user in _userBase.Users!)
        {
            Messages.Results($"{user.Oid, 3}. {user.UserName, 20} {user.FullName}");
        }
    }

    private void ChangePassword()
    {
        string oid = string.Empty;
        string pass = string.Empty;
        User? user = new User();
        while (oid == String.Empty)
        {
            Console.Write("User ID: ");
            oid = Console.ReadLine();
            if (int.TryParse(oid, out int id))
            {
                user = _userBase.Users!.FirstOrDefault(u => u.Oid == id);
            }
            else
            {
                oid = string.Empty;
            }
        }

        while (pass == string.Empty)
        {
            Console.Write("New Password: ");
            pass = Console.ReadLine();
        }
        
        _userBase.ChangePassword(user!.UserName, pass);
        
        
    }
}