using VikingCommon;
using VikingCommon.Models;

namespace VikingEntityConsole.Views;

public class Main : AbstractMenu
{
    public Main() : base("Main Menu") {}
    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "Random Stuff", () => new RandomMenu().Display()));
        AddMenuItem(new MenuItem(2, "Manage Users"));
        AddMenuItem(new MenuItem(3, "Manage Files"));
        AddMenuItem(new MenuItem(4, "Manage Settings", () => new SettingsMenu().Display()));
        AddMenuItem(new MenuItem(5, "Logout", LogoutCurrentUser).SetAsExitOption());
        AddMenuItem(new MenuItem(6, "Exit menu").SetAsExitOption());
    }

    private void LogoutCurrentUser()
    { 
        Program._settings.LastUser = new User();
        Program._settings.Commit();
    }
}