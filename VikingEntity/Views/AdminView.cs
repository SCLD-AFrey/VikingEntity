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
            MenuItem.Display('R', "Reset Data Files", Enums.AdminRole.Administrator);
            MenuItem.Display('2', "Do Something Else", Enums.AdminRole.Administrator);
            MenuItem.Display('X', "<= Back", Enums.AdminRole.BasicUser);
            
            input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.R:
                    ResetDataFiles();
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

    private static void ResetDataFiles()
    {
        try
        {
            Program.AppFiles = new AppFiles(true);
            Console.WriteLine("Data files reset");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error creating data files: " + e.Message);
        }

        try
        {
            Program.Settings.Load();
            Console.WriteLine("Settings loaded");
            Program.UserBase.Load();
            Console.WriteLine("User Base loaded");
            Program.CurrentUser = Program.Settings.LastUser;
            Console.WriteLine("Last User loaded");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error creating loading files: " + e.Message);
            throw;
        }
    }
}