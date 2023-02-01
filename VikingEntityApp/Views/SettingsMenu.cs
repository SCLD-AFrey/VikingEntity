using System.ComponentModel;
using VikingCommon;
using VikingCommon.Models;
using WindowsFirewallHelper;

namespace VikingEntityConsole.Views;

public class SettingsMenu : AbstractMenu
{
    public SettingsMenu() : base("Settings Menu") {}
    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "View Settings", ViewSettings));
        AddMenuItem(new MenuItem(2, "Change Settings", ChangeSettings));
        AddMenuItem(new MenuItem(3, "View Firewall Rules", ViewFirewallRules));
        AddMenuItem(new MenuItem(4, "Return").SetAsExitOption());
    }

    private void ViewFirewallRules()
    {
        var allRules = FirewallManager.Instance.Rules;
        int i = 0;
        foreach (var rule in allRules)
        {
            i++;
            Messages.Results($"{i, 3}. {rule.FriendlyName} {rule.Direction} {rule.Action.ToString()} {rule.Protocol.ToString()} {rule.LocalPorts} {rule.RemotePorts} {rule.LocalAddresses} {rule.RemoteAddresses}");

            if (i % 10 == 0)
            {
                Messages.Information($"Press 'X' to exit or any other key to continue");
                var inp = Console.ReadKey();
                Console.WriteLine("");
                if (inp.Key == ConsoleKey.X)
                {
                    break;
                }
            }
            
        }
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