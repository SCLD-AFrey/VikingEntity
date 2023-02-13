using VikingCommon;
using VikingEntity.Models;

namespace VikingEntity.Views;

public static class LoginView
{
    public static Task<Enums.ViewMode> Quit()
    {
        return Task.FromResult(Enums.ViewMode.Exit); 
    }
    public static Task<Enums.ViewMode> Logout()
    {
        var opt = SafeInput.Char("Are you sure you want to logout? (y/n): ");        
        if (opt == 'n' || opt == 'N')
        {
            return Task.FromResult(Program._viewModePrev);
        }
        Program.Currentuser = new User();
        Program.Settings.Set("lastuseroid", "0");
        Program.Settings.Commit();
        opt = SafeInput.Char("Are you sure you want to quit? (y/n): ");
        if (opt == 'y' || opt == 'Y')
        {
            return Task.FromResult(Enums.ViewMode.Exit);
        }
        return Task.FromResult(Quit().Result);
    }

    public static Task<Enums.ViewMode> Login()
    {
        SafeInput.String(out var username, "Username: ");
        SafeInput.String(out var password, "Password: ");
        
        var user = Program.UserBase.FirstOrDefault(p_x => p_x.UserName.ToLower() == username.ToLower());
        if (user == null)
        {
            Console.WriteLine("User not found");
            return Task.FromResult(Enums.ViewMode.Login);
        }

        PasswordHash hash = new PasswordHash();
        if (!hash.VerifyPassword(password, user.Password, user.Salt))
        {
            Console.WriteLine("Password Incorrect");
            return Task.FromResult(Enums.ViewMode.Login);
        }
        
        Program.Currentuser = user;
        Program.Settings.Set("lastuseroid", user.Oid.ToString());
        
        
        return Task.FromResult(Enums.ViewMode.Main);
    }
}