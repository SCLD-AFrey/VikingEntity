
using JsonBase.Models.Logging;
using JsonBase.Models.Settings;
using VikingCommon;
using VikingEntity.Models;
using VikingEntity.Views;

namespace VikingEntity;

internal static class Program
{
    private static Enums.ViewMode _viewMode;
    public static Enums.ViewMode ViewModePrev;
    public static readonly UserBase UserBase = new UserBase();
    public static readonly SettingBase Settings = new SettingBase();
    public static readonly LoggerBase LoggerBase = new LoggerBase();
    public static User? CurrentUser;
    
    static Task Main()
    {
        if (Settings.Count < 1)
            InitSettings();
        if(UserBase.Count < 0)
            InitUsers();
        
        
        
        
        //Helpers.InitJsonBase.Init();
        if (Settings.Count < 0)
            CurrentUser = UserBase.FirstOrDefault(p_x => p_x.Oid == (int)(Settings.GetValue("LastUserOid") ?? 0));
        else
            CurrentUser = new User();
        
        
        
        LoggerBase.Log("Starting Viking Entity", LogEntry.Severity.Info, CurrentUser!.Oid);
        LoggerBase.Log($"JsonBase {Settings.ToString()}", LogEntry.Severity.Info, CurrentUser!.Oid);
        LoggerBase.Log($"JsonBase {LoggerBase.ToString()}", LogEntry.Severity.Info, CurrentUser!.Oid);
        LoggerBase.Log($"JsonBase {UserBase.ToString()}", LogEntry.Severity.Info, CurrentUser!.Oid);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Welcome to {Settings.GetValue("AppName")}");
        Console.ResetColor();

        _viewMode = CurrentUser.Oid == 0 ? Enums.ViewMode.Login : Enums.ViewMode.Main;
        
        while (_viewMode != Enums.ViewMode.Exit)
        {
            ViewModePrev = _viewMode;
            LoggerBase.Log($"View switched to [{_viewMode}]", LogEntry.Severity.Info, CurrentUser.Oid);
            _viewMode = _viewMode switch
            {
                Enums.ViewMode.Login => LoginView.Login().Result,
                Enums.ViewMode.Main => MainView.Display().Result,
                Enums.ViewMode.Admin => AdminView.Display(),
                Enums.ViewMode.Settings => SettingsView.Display(),
                Enums.ViewMode.User => UserManageView.Display(),
                Enums.ViewMode.ChatGpt => ChatGptView.Display().Result,
                Enums.ViewMode.Exit => Enums.ViewMode.Exit,
                Enums.ViewMode.Notes => NotesView.Display(),
                _ => MainView.Display().Result
            };
        }
        Console.WriteLine("Goodbye!");
        LoggerBase.Log("Quit Viking Entity", LogEntry.Severity.Info, CurrentUser!.Oid);
        return Task.CompletedTask;
    }

    private static void InitUsers()
    {
        PasswordHash ph = new();
        User admin = UserBase.DefaultUser();
        admin.Oid = UserBase.GetNextOid();
        UserBase.Add(admin);
        UserBase.Commit();
    }

    private static void InitSettings()
    {
        
        try
        {
            Settings.Clear();
            TimeSpan ts = TimeSpan.FromHours(24);
            Settings.Add("AppName", "Viking Entity");
            Settings.Add("AppVersion", "0.1.0");
            Settings.Add("AppDescription", "Do Viking Stuff");
            Settings.Add("AppAuthor", "Viking Enterprise");
            Settings.Add("LoginClaimTimeSpan", ts);
            Settings.Add("LastUserOid", UserBase.GetRootUser()?.Oid);
            Settings.Add("InstallDate", DateTime.UtcNow);
            Settings.Commit();
        }
        catch (Exception e)
        {
            LoggerBase.Log(e.Message, LogEntry.Severity.Error);
        }
    }
}