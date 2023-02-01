using VikingCommon;
using VikingCommon.Models;
using System;

namespace VikingEntityConsole;

static class Program
{
    public static Settings _settings;
    public static UserBase _userBase;
    public static User _currentUser = new User();
    

    static void Main(string[] p_args)
    {
        AppFiles.Create();
        _settings = new Settings();
        _settings.Load();
        _userBase = new UserBase();

        if (_settings.LastUser.Oid > 0)
        {
            _currentUser = _settings.LastUser;
        }

        while(_currentUser.Oid == 0)
        {
            DoLogin();
        }

        var mainMenu = new Views.Main();
        mainMenu.Display();
        
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
    
    
    
    private static void DoLogin()
    {        
        string user = "";
        string pass = "";
        while (user == "" || pass == "")
        {
            Console.Write("Username: ");
            user = Console.ReadLine();
            Console.Write("Password: ");
            pass = Console.ReadLine();
        }
        
        _currentUser = _userBase.Login(user, pass, out string message);
        _settings.LastUser = _currentUser;
        _settings.LastUser.LastLogin = DateTime.UtcNow;
        _settings.Commit();

        if (message != "")
        {
            Messages.Error($"Login Error: {message}");
        }
        
    }
}