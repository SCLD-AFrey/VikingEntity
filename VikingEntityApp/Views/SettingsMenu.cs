using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
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
        AddMenuItem(new MenuItem(3, "View Firewall Rules", () => ViewFirewallRules()));
        AddMenuItem(new MenuItem(4, "Return").SetAsExitOption());
    }

    private void ViewFirewallRules()
    {
        var allRules = FirewallManager.Instance.Rules;
        int i = 0;
        Messages.Results($"{" ", 4} {"FriendlyName", 60} {"Enabled", 7} {"Direction", 10} {"Action", 10} {"Proto", 5}");
        Messages.Results($"{" ", 4} {"------------", 60} {"-------", 7} {"---------", 10} {"------", 10} {"-----", 5}");

        foreach (var rule in allRules.OrderBy(r => r.Name))
        {
            i++;
            var fn = rule.FriendlyName.Length > 60 ? rule.FriendlyName.Substring(0, 60) : rule.FriendlyName;
            Messages.Results($"{i, 3}. {fn, 60} {rule.IsEnable.ToString(), 7} {rule.Direction, 10} {rule.Action.ToString(), 10} {rule.Protocol.ToString(), 5}", (i%2==0));
            
            if (i % 30 == 0)
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