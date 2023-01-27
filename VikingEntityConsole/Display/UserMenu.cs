using VikingEntityConsole.Helpers;
using VikingEntityConsole.Helpers.Menu;
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
        AddMenuItem(new MenuItem(4, "Back to Main").SetAsExitOption());
    }

    private void DisplayUsers()
    {
        Messages.Results($"{_userBase.Users!.Count} users found");
        foreach (var user in _userBase.Users!)
        {
            Messages.Results($"{user.Oid, 3}. {user.UserName, 20} {user.FullName}");
        }
    }
}