using JsonBase.Models.Settings;
using VikingCommon;

namespace VikingEntity.Views;

public static class SettingsView
{
    public static Enums.ViewMode Display()
    {
        // ReSharper disable once TooWideLocalVariableScope
        ConsoleKey input;
        while (true)
        {
            Console.WriteLine("Settings Menu");
            MenuItem.Display('V', "View Current Settings");
            MenuItem.Display('C', "Change Settings");
            MenuItem.Display('A', "Add Setting");
            MenuItem.Display('X', "<= Back");
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.V:
                    DisplaySettings();
                    break;
                case ConsoleKey.C:
                    UpdateSettings();
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
    }

    private static void DisplaySettings()
    {
        foreach (var setting in Program.Settings)
        {
            Console.WriteLine(setting.ToString());
        }
    }

    private static void UpdateSettings()
    {
        string settingName = SafeInput.String("Setting Name")!;
        Setting? setting = Program.Settings.FirstOrDefault(p_x => p_x.Name!.ToLower().Equals(settingName.ToLower()));
        if (setting == null)
        {
            Console.WriteLine("Setting not found");
            return;
        }
        string settingValue = SafeInput.String("Setting Value")!;
        setting.Value = settingValue;
        Program.Settings.Commit();
    }
}