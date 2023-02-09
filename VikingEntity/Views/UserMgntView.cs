using System.Runtime.InteropServices.ComTypes;
using System.Speech.Synthesis;
using VikingCommon;
using VikingCommon.Models;
using static VikingCommon.Enums.AdminRole;

namespace VikingEntity.Views;

public static class UserMgntView
{
    public static Enums.ViewMode Display()
    {
        ConsoleKey input = ConsoleKey.NoName;
        while (true)
        {
            Console.WriteLine("Admin Menu");
            MenuItem.Display('D', "Display Users", BasicUser);
            MenuItem.Display('A', "Add New User", UserManagement);
            MenuItem.Display('X', "<= Back", BasicUser);
            
            input = Console.ReadKey().Key;
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
    
    public static string SafeInput(string prompt = "")
    {
        string input = "";
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        }
        return input;
    }

    private static void DisplayAddUser()
    {
        User user = new User();
        string _password = "";
        user.UserName = SafeInput("User Name: ");
        
        user.FirstName = SafeInput("First Name: ");
        user.LastName = SafeInput("Last Name: ");
        _password = SafeInput("Password: ");
        PasswordHash hash = new PasswordHash();
        user.Password = hash.GeneratePasswordHash(_password, out var salt);
        user.Salt = salt;
        user.Roles.Add(BasicUser);
        _password = String.Empty;
        
        ConsoleKeyInfo input = new ConsoleKeyInfo();

        while (input.Key != ConsoleKey.X)
        {
            string instr = "Roles: ";
            foreach (var role in Enums.AdminRole.GetValues(typeof(Enums.AdminRole)))
            {
                instr += $"{role.ToString().Substring(0,1).ToUpper()}-{role.ToString()}, ";
            }
            instr += "X-Quit)";
            Console.WriteLine(instr);
            
            while (input.Key != ConsoleKey.X)
            {
                input = Console.ReadKey();
                var charList = new char[Enum.GetValues(typeof(Enums.AdminRole)).Length];
                int i = 0;
                foreach (var role in Enum.GetValues(typeof(Enums.AdminRole)))
                {
                    charList[i] = Char.Parse(role.ToString().ToUpper().Substring(0,1));
                    i++;
                }

                var thisChar = Char.Parse(input.KeyChar.ToString().ToUpper());

                if (charList.Contains(thisChar)) ;
               {
                   switch (thisChar)
                   {
                       case 'A':
                           if (user.Roles.Contains(Enums.AdminRole.Administrator))
                           {
                               user.Roles.Remove(Administrator);
                               Console.WriteLine($"'{Administrator.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(Administrator);
                               Console.WriteLine($"'{Administrator.ToString()} added'");
                           }
                           break;
                       case 'U':
                           if (user.Roles.Contains(Enums.AdminRole.UserManagement))
                           {
                               user.Roles.Remove(UserManagement);
                               Console.WriteLine($"'{UserManagement.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(UserManagement);
                               Console.WriteLine($"'{UserManagement.ToString()} added'");
                           }
                           break;
                       case 'S':
                           if (user.Roles.Contains(Enums.AdminRole.SettingsManagement))
                           {
                               user.Roles.Remove(SettingsManagement);
                               Console.WriteLine($"'{SettingsManagement.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(SettingsManagement);
                               Console.WriteLine($"'{SettingsManagement.ToString()} added'");
                           }
                           break;
                       case 'B':
                           if (user.Roles.Contains(Enums.AdminRole.BasicUser))
                           {
                               user.Roles.Remove(BasicUser);
                               Console.WriteLine($"'{BasicUser.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(BasicUser);
                               Console.WriteLine($"'{BasicUser.ToString()} added'");
                           }
                           break;
                       default:
                           Console.WriteLine("Invalid input");
                           break;
                   }
                   Console.WriteLine(string.Join(", ", user.Roles));
               } 
            }


        }
        
        
        
        
        
        
        UserBase userBase = new UserBase();
        userBase.Add(user);
        userBase.Commit();
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