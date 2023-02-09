using VikingCommon;
using VikingCommon.Models;

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
        User user = new User();

        while (string.IsNullOrEmpty(user.UserName))
        {
            user.UserName = SafeInput.String("User Name: ")!;
            var chkUser = Program.UserBase.FirstOrDefault(p_x => p_x.UserName == user.UserName);

            if (chkUser != null)
            {
                Console.WriteLine($"Username '{user.UserName}' already exists. Please try again.");
                user.UserName = "";
            }
        }
        
        user.FirstName = SafeInput.String("First Name: ") ?? string.Empty;
        user.LastName = SafeInput.String("Last Name: ")!;
        var password = SafeInput.String("Password: ")!;
        PasswordHash hash = new PasswordHash();
        user.Password = hash.GeneratePasswordHash(password, out var salt);
        user.Salt = salt;
        user.Roles.Add(Enums.AdminRole.BasicUser);
        
        ConsoleKeyInfo input = new ConsoleKeyInfo();

        while (input.Key != ConsoleKey.X)
        {
            var charList = new char[Enum.GetValues(typeof(Enums.AdminRole)).Length];
            string instr = "Roles: (";
            int i = 0;
            foreach (var role in Enum.GetValues(typeof(Enums.AdminRole)))
            {
                instr += $"{role.ToString()!.Substring(0,1).ToUpper()}-{role}, ";
                charList[i] = char.Parse(role.ToString()!.ToUpper().Substring(0,1));
                i++;
            }
            instr += "X-Quit)";
            Console.WriteLine(instr);
            
            while (input.Key != ConsoleKey.X)
            {
                Console.WriteLine("Current Roles: " + string.Join(", ", user.Roles));
                input = Console.ReadKey();
                Console.WriteLine();

                var thisChar = Char.Parse(input.KeyChar.ToString().ToUpper());

                if (charList.Contains(thisChar))
                { 
                   switch (thisChar)
                   {
                       case 'A':
                           if (user.Roles.Contains(Enums.AdminRole.Administrator))
                           {
                               user.Roles.Remove(Enums.AdminRole.Administrator);
                               Console.WriteLine($"'{Enums.AdminRole.Administrator.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(Enums.AdminRole.Administrator);
                               Console.WriteLine($"'{Enums.AdminRole.Administrator.ToString()} added'");
                           }
                           break;
                       case 'U':
                           if (user.Roles.Contains(Enums.AdminRole.UserManagement))
                           {
                               user.Roles.Remove(Enums.AdminRole.UserManagement);
                               Console.WriteLine($"'{Enums.AdminRole.UserManagement.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(Enums.AdminRole.UserManagement);
                               Console.WriteLine($"'{Enums.AdminRole.UserManagement.ToString()} added'");
                           }
                           break;
                       case 'S':
                           if (user.Roles.Contains(Enums.AdminRole.SettingsManagement))
                           {
                               user.Roles.Remove(Enums.AdminRole.SettingsManagement);
                               Console.WriteLine($"'{Enums.AdminRole.SettingsManagement.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(Enums.AdminRole.SettingsManagement);
                               Console.WriteLine($"'{Enums.AdminRole.SettingsManagement.ToString()} added'");
                           }
                           break;
                       case 'B':
                           if (user.Roles.Contains(Enums.AdminRole.BasicUser))
                           {
                               user.Roles.Remove(Enums.AdminRole.BasicUser);
                               Console.WriteLine($"'{Enums.AdminRole.BasicUser.ToString()} removed'");
                           }
                           else
                           {
                               user.Roles.Add(Enums.AdminRole.BasicUser);
                               Console.WriteLine($"'{Enums.AdminRole.BasicUser.ToString()} added'");
                           }
                           break;
                   }
                } 
            }


        }

        user.Oid = Program.UserBase.GetNextOid();
        user.Save();
        Program.UserBase.Load();
    }

    private static void DisplayUsers()
    {
        Console.WriteLine("Users:");
        Console.WriteLine($"{"Oid", -5} {"FullName", -20} {"UserName", -15} {"Login", -8} {"Created", -8}");
        foreach (var user in Program.UserBase)
        {
            if (user.IsRootUser)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            Console.WriteLine($"{user.Oid + ".", -5} {user.FullName, -20} {user.UserName, -15} {user.LastLogin:d} {user.DateCreated:d}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"      Roles: {string.Join(", ",user.Roles.ToList())}");
            Console.ResetColor();
        }
    }
}