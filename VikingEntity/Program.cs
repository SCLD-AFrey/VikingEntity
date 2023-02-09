using System.Diagnostics;
using VikingCommon;
using VikingCommon.Models;
using VikingEntity.Views;

namespace VikingEntity;

internal static class Program
{
    private static Enums.ViewMode _viewMode = Enums.ViewMode.Main;
    public static Settings Settings = new Settings();
    public static AppFiles AppFiles = new AppFiles();
    public static User CurrentUser = new User();
    public static UserBase UserBase = new UserBase();
    static void Main(string[] p_args)
    {
        Settings.Load();
        UserBase.Load();
        CurrentUser = Settings.LastUser;

        if ((Settings.LastUser.Oid > 0 && DateTime.UtcNow.Subtract(Settings.LastUser.LastLogin).TotalHours > Settings.LoginClaimHours))
        {
            Console.WriteLine($"Last '{Settings.LastUser.UserName}' login was more than 24 hours ago. Please login again.");
            CurrentUser = new User();
            Settings.LastUser = CurrentUser;
            Settings.Commit();
            _viewMode = Enums.ViewMode.Login;
        }
        Settings.Load();
        if (CurrentUser.Oid == 0)
        {
            _viewMode = Enums.ViewMode.Login;
        }
        else
        {
            CurrentUser.LastLogin = DateTime.UtcNow;
            CurrentUser.Save();
            Settings.LastUser = CurrentUser;
            Settings.Commit();
        }

        Console.WriteLine($"Main Menu {Program.Settings.AppName} - {CurrentUser.FullName}");
        while (_viewMode != Enums.ViewMode.Exit)
        {
            _viewMode = _viewMode switch
            {
                Enums.ViewMode.Login => LoginView.Display(),
                Enums.ViewMode.Main => MainView.Display(),
                Enums.ViewMode.Admin => AdminView.Display(),
                Enums.ViewMode.Settings => SettingsView.Display(),
                Enums.ViewMode.User => UserMgntView.Display(),
                Enums.ViewMode.Exit => Enums.ViewMode.Exit,
                _ => MainView.Display()
            };
        }
        Console.WriteLine("Goodbye!");
    }
}