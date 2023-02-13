using VikingCommon;

namespace VikingEntity.Views;

public static class UserManageView
{
    public static Enums.ViewMode Display()
    {
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('D', "Display Users");
            MenuItem.Display('A', "Add New User", Enums.AdminRole.UserManagement);
            MenuItem.Display('X', "<= Back");
            
            var input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.D:
                    DisplayUsers();
                    break;
                case ConsoleKey.A:
                    DisplayAddUser();
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
    }

    private static void DisplayAddUser()
    {

        
    }

    private static void DisplayUsers()
    {

    }
}