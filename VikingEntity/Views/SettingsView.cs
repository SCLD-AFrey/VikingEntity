using System.Diagnostics;
using System.Runtime.CompilerServices;
using VikingCommon;

namespace VikingEntity.Views;

public static class SettingsView
{
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            Console.WriteLine("Settings Menu");
            MenuItem.Display('V', "View Current Settings", Enums.AdminRole.BasicUser);
            MenuItem.Display('C', "Change Settings", Enums.AdminRole.SettingsManagement);
            MenuItem.Display('X', "<= Back", Enums.AdminRole.BasicUser);
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.V:
                    Program.Settings.Display();
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

    private static void UpdateSettings()
    {
        string _setting = "";
        string _value = "";
        while (string.IsNullOrEmpty(_setting) || string.IsNullOrEmpty(_value))
        {
            Console.Write("Setting Name: ");
            _setting = Console.ReadLine();
            Console.Write("Setting Value: ");
            _value = Console.ReadLine();
        }
        try
        {
            Program.Settings.Update(_setting, _value);
            Program.Settings.Commit();
            Program.Settings.Load();
            Console.WriteLine($"Setting '{_setting}' set to '{_value}'");
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}