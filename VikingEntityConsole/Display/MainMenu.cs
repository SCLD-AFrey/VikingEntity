using VikingEntityConsole.Helpers.Menu;

namespace VikingEntityConsole.Display;

public class Main : AbstractMenu
{
    public Main() : base("Main Menu") {}
    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "Manage Users", new UserMenu()));
        AddMenuItem(new MenuItem(2, "Manage Files", new FileMenu()));
        AddMenuItem(new MenuItem(3, "Manage Settings", new SettingsMenu()));
        AddMenuItem(new MenuItem(4, "Logout"));
        AddMenuItem(new MenuItem(5, "Exit menu").SetAsExitOption());
    }
}