using System.Runtime.InteropServices.ComTypes;
using VikingCommon;

namespace VikingEntity.Views;

public static class UserMgntView
{
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('1', "Do Something", Enums.AdminRole.UserManagement);
            MenuItem.Display('2', "Do Something Else", Enums.AdminRole.UserManagement);
            MenuItem.Display('X', "<= Back", Enums.AdminRole.BasicUser);
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.U:
                    DisplayUsers();
                    break;
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
    }

    private static void DisplayUsers()
    {
        Console.WriteLine("Users:");
        foreach (var user in Program.UserBase)
        {
            Console.WriteLine($"{user.Oid, 5} {user.UserName, 20} {user.FullName, 20} {user.Roles, 20}");
        }
    }
}