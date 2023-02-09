using VikingCommon;
using VikingCommon.Models;

namespace VikingEntity.Views;

public class LoginView
{
    public static Enums.ViewMode Display()
    {
        string? username = null;
        string? password = null;
        while (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Login");
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
        }
        
        var userBase = Program.UserBase;
        var user = userBase.GetUserByUsername(username);
        if (user == null)
        {
            Console.WriteLine("Invalid username or password");
            return Enums.ViewMode.Login;
        }

        PasswordHash hash = new PasswordHash();
        if (!hash.VerifyPassword(password, user.Password, user.Salt))
        {
            Console.WriteLine("Invalid username or password");
            Program.Settings.LastUser = new User
            {
                LastLogin = DateTime.UtcNow
            };
            Program.Settings.Commit();
            return Enums.ViewMode.Login;
        }

        Program.Settings.LastUser = user;
        Program.Settings.LastUser.LastLogin = DateTime.UtcNow;
        Program.CurrentUser = Program.Settings.LastUser;
        Program.Settings.Commit();
        
        return Enums.ViewMode.Main;
    }
}