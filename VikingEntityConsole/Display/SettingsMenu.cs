using VikingEntityConsole.Helpers.Menu;

namespace VikingEntityConsole.Display;

public class SettingsMenu : AbstractMenu
{
    public SettingsMenu() : base("settings Menu") {}
    
    protected override void Init()
    {
        AddMenuItem(new MenuItem(0, "Display Settings", DisplaySettings));
        AddMenuItem(new MenuItem(1, "Change Settings"));
        AddMenuItem(new MenuItem(2, "Back to Main").SetAsExitOption());
    }

    private void DisplaySettings()
    {
        var settings = Program._settings;
        
    }
    private void ChangeSettings()
    {
    }


    

}