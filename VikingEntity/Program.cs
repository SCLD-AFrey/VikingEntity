using System.Diagnostics;
using VikingCommon;
using VikingCommon.Models;
using VikingEntity.Models;
using VikingEntity.Views;

namespace VikingEntity;

internal static class Program
{
    private static Enums.ViewMode _viewMode;
    public static Enums.ViewMode _viewModePrev;
    public static readonly UserBase UserBase = new UserBase();
    public static NotesBase NotesBase = new NotesBase();
    public static SettingBase SettingBase = new SettingBase();
    public static User Currentuser;
    
    static Task Main(string[] p_args)
    {
        Helpers.InitJsonBase.Init();
        Currentuser = UserBase.FirstOrDefault(p_x => p_x.Oid == int.Parse(SettingBase.Get("lastuseroid") ?? string.Empty))  ?? new User();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Welcome to {SettingBase.Get("appname")}");
        Console.ResetColor();

        _viewMode = Currentuser.Oid == 0 ? Enums.ViewMode.Login : Enums.ViewMode.Main;
        
        while (_viewMode != Enums.ViewMode.Exit)
        {
            _viewModePrev = _viewMode;
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
        return Task.CompletedTask;
    }
}