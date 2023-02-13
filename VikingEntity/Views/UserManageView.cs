using VikingCommon;
using VikingEntity.Models;

namespace VikingEntity.Views;

public static class UserManageView
{
    public static Enums.ViewMode Display()
    {
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('V', "Display Users");
            MenuItem.Display('A', "Add New User", Enums.AdminRole.UserManagement);
            MenuItem.Display('R', "Remove User", Enums.AdminRole.UserManagement);
            MenuItem.Display('X', "<= Back");
            
            var input = Console.ReadKey().Key;
            Console.WriteLine();

            switch (input)
            {
                case ConsoleKey.V:
                    DisplayUsers();
                    break;
                case ConsoleKey.A:
                    DisplayAddUser();
                    break;
                case ConsoleKey.R:
                    RemoveUser();
                    break;
                case ConsoleKey.X:
                    return Enums.ViewMode.Main;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
        
    }

    private static void RemoveUser()
    {
        string input = SafeInput.String("User ID/UserName")!;
        User? user;
        if(int.TryParse(input, out var oid))
        {
            user = Program.UserBase.FirstOrDefault(p_x => p_x.Oid == oid);
        }
        else
        {
            user = Program.UserBase.FirstOrDefault(p_x => string.Equals(p_x.UserName, input, StringComparison.CurrentCultureIgnoreCase));
        }

        if (user == null)
        {
            Console.WriteLine("User not found");
        }

        if (Program.UserBase.Remove(user))
        {
            Program.UserBase.Commit();
            Console.WriteLine($"User '{user.UserName}' [{user.Oid}] removed");
        }
        else
        {
            Console.WriteLine($"User '{input}' not found");
        }
    }

    private static void DisplayAddUser()
    {
        PasswordHash hash = new PasswordHash();
        User user = new User();
        user.Oid = Program.UserBase.GetNextOid();
        user.UserName = SafeInput.String("Username")!;
        user.FirstName = SafeInput.String("FirstName")!;
        user.LastName = SafeInput.String("LastName")!;
        user.Password = hash.GeneratePasswordHash(SafeInput.String("Password")!, out var salt);
        user.Salt = salt;
        user.RequirePasswordChange = SafeInput.Bool("Require Password Change");
        Program.UserBase.Add(user);
        Program.UserBase.Commit();
        Console.WriteLine(user.ToString());
    }

    private static void DisplayUsers()
    {
        int i = 0;
        foreach (var user in Program.UserBase)
        {
            i++;
            Console.WriteLine($"{i +".", -3} {user.ToString()}");
        }
    }
}