using VikingCommon;

namespace VikingEntity.Views;

public static class AdminView
{
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('1', "Do Something", Enums.AdminRole.Administrator);
            MenuItem.Display('2', "Do Something Else", Enums.AdminRole.Administrator);
            MenuItem.Display('X', "<= Back", Enums.AdminRole.BasicUser);
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.D1:
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
}