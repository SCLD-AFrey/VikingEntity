using VikingCommon;

namespace VikingEntityConsole.Views;

public class Main : AbstractMenu
{
    public Main() : base("Main Menu") {}
    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "Manage Users"));
        AddMenuItem(new MenuItem(2, "Manage Files"));
        AddMenuItem(new MenuItem(3, "Manage Settings", () => new SettingsMenu().Display()));
        AddMenuItem(new MenuItem(4, "Logout"));
        AddMenuItem(new MenuItem(5, "Exit menu").SetAsExitOption());
    }
    
    
    
    
    
}