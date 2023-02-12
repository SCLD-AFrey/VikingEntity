using System.Diagnostics;
using VikingCommon;
using VikingCommon.Models;
using VikingEntity.Views;

namespace VikingEntity;

internal static class Program
{
    private static Enums.ViewMode _viewMode = Enums.ViewMode.Main;
    public static Settings Settings = new Settings();
    public static UserBase UserBase = new UserBase();
    public static User CurrentUser = new User();
    public static AppFiles AppFiles = new AppFiles();
    static Task Main(string[] p_args)
    {
        Settings.Load();
        UserBase.Load();

        CurrentUser = Settings.LastUser;


        var rems = new ReminderBase();



        Console.ReadLine();
        // ReviewCrawler crawler = new ReviewCrawler();
        // await crawler.StartCrawlerAsync();

        if ((Settings.LastUser.Oid > 0 && DateTime.UtcNow.Subtract(Settings.LastUser.LastLogin).TotalHours > Settings.LoginClaimHours))
        {
            Console.WriteLine($"Last '{Settings.LastUser.UserName}' login was more than 24 hours ago. Please login again.");
            CurrentUser = new User();
            Settings.LastUser = CurrentUser;
            Settings.Commit();
            _viewMode = Enums.ViewMode.Login;
        }
        if (CurrentUser.Oid == 0)
        {
            _viewMode = Enums.ViewMode.Login;
        }
        else
        {
            CurrentUser.LastLogin = DateTime.UtcNow;
            Settings.LastUser = CurrentUser;
            Settings.Commit();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Welcome to {Program.Settings.AppName} - {CurrentUser.FullName}");
        Console.ResetColor();
        while (_viewMode != Enums.ViewMode.Exit)
        {
            _viewMode = _viewMode switch
            {
                Enums.ViewMode.Login => LoginView.Display(),
                Enums.ViewMode.Main => MainView.Display().Result,
                Enums.ViewMode.Admin => AdminView.Display(),
                Enums.ViewMode.Settings => SettingsView.Display(),
                Enums.ViewMode.User => UserManageView.Display(),
                Enums.ViewMode.ChatGpt => ChatGptView.Display().Result,
                Enums.ViewMode.Exit => Enums.ViewMode.Exit,
                _ => MainView.Display().Result
            };
        }
        Console.WriteLine("Goodbye!");
        return Task.CompletedTask;
    }
}