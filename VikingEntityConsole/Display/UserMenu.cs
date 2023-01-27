using VikingEntityConsole.Helpers.Menu;

namespace VikingEntityConsole.Display;

public class UserMenu : AbstractMenu
{
    public UserMenu() : base("Users Menu") {}
    
    protected override void Init()
    {
        AddMenuItem(new MenuItem(0, "Display Users"));
        AddMenuItem(new MenuItem(1, "Add User"));
        AddMenuItem(new MenuItem(2, "Edit User"));
        AddMenuItem(new MenuItem(3, "Remove User"));
        AddMenuItem(new MenuItem(4, "Back to Main").SetAsExitOption());
    }
}