using VikingCommon;
using VikingCommon.Models;

namespace VikingEntity.Views;

public static class MainView
{
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            MenuItem.Display('1', "Operation 1", Enums.AdminRole.BasicUser);
            MenuItem.Display('2', "Operation 2", Enums.AdminRole.BasicUser);
            MenuItem.Display('S', "Settings", Enums.AdminRole.BasicUser);
            MenuItem.Display('U', "Users", Enums.AdminRole.BasicUser);
            MenuItem.Display('A', "Admin", Enums.AdminRole.BasicUser);
            MenuItem.Display('Q', "<= Quit", Enums.AdminRole.BasicUser);
            MenuItem.Display('L', "<= Logout and Quit", Enums.AdminRole.BasicUser);
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.A:
                    return Enums.ViewMode.Admin;
                case ConsoleKey.S:
                    return Enums.ViewMode.Settings;
                case ConsoleKey.U:
                    return Enums.ViewMode.User;
                case ConsoleKey.L:
                    DoLogout();
                    return Enums.ViewMode.Exit;
                case ConsoleKey.Q:
                    return Enums.ViewMode.Exit;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private static void DoLogout()
    {
        Program.CurrentUser = new User();
        Program.Settings.LastUser = Program.CurrentUser;
        Program.Settings.Commit();
        Console.WriteLine("Logging out...");
    }
}