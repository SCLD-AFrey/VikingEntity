using VikingCommon.Models;
using VikingEntityConsole.Helpers;
using VikingEntityConsole.Models;
// ReSharper disable InconsistentNaming

namespace VikingEntityConsole;

static class Program
{
    internal static Settings _settings = new Settings();
    internal static UserBase _userBase = new UserBase();
    internal static User _currentUser = new User();
    static void Main(string[] args)
    {
        AppFiles.Create();
        // if (_settings.LastUser.Oid > 0)
        // {
        //     _currentUser = _settings.LastUser;
        // }

        DoLogin();

        if (_currentUser.RequirePasswordChange)
        {
            DoChangePassword();
        }

        var mainMenu = new Display.Main();
        mainMenu.Display();

        Console.WriteLine("Press any key to exit.");    
        Console.ReadKey();
    }

    private static void DoLogin()
    {
        while (_currentUser.Oid == 0 || (_currentUser.Oid > 0 && _settings.RequireLogin == true))
        {
            string? user = string.Empty;
            string pass = string.Empty;
            while (user!.Trim().Equals(""))
            {
                Console.Write("Username: ");
                user = Console.ReadLine();
            }
            while (pass!.Trim().Equals(""))
            {
                Console.Write("Password: ");
                pass = Console.ReadLine()!;
            }
            _currentUser = _userBase.Login(user, pass);
            //_settings.LastUser = _currentUser;
            _settings.Commit();
        }
    }
    
    private static void DoChangePassword()
    {
        var user = _userBase.Users!.FirstOrDefault(u => u.Oid == _currentUser.Oid);

        string pass = "";
        while (pass == "")
        {
            Console.Write("New Password: ");
            pass = Console.ReadLine();
        }
        _currentUser = _userBase.ChangePassword(user!.UserName, pass);
    }
}