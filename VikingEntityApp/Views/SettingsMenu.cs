using VikingCommon;
using VikingCommon.Models;

namespace VikingEntityConsole.Views;

public class SettingsMenu : AbstractMenu
{
    public SettingsMenu() : base("Settings Menu") {}
    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "View Settings", ViewSettings));
        AddMenuItem(new MenuItem(2, "Change Settings", ChangeSettings));
        AddMenuItem(new MenuItem(3, "Return").SetAsExitOption());
    }

    private void ChangeSettings()
    {
        string setting = "";
        string value = "";
        while (setting.Equals(""))
        {
            Console.Write("Setting: ");
            setting = Console.ReadLine();
        }
        while (value.Equals(""))
        {
            Console.Write("Value: ");
            value = Console.ReadLine();
        }
        
        var prop = Program._settings.GetType().GetProperty(setting);
        if (prop == null)
        {
            Messages.Error($"Setting '{setting}' is not found" );
            return;
        }
        
        Program._settings.GetType().GetProperty(setting)?.SetValue(Program._settings, value);
        Messages.Success($"Setting '{setting}' updated" );
        Program._settings.Commit();
        
    }

    private static void ViewSettings()
    {
        int i = 0;
        foreach (var setting in Program._settings.GetType().GetProperties())
        {
            i++;
            Messages.Results($"{i, 5} {setting.Name}: {setting.GetValue(Program._settings)}");
        }
    }
    
    
}