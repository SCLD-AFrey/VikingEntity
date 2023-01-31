using VikingCommon;
using VikingCommon.Models;

namespace VikingEntityConsole;

static class Program
{
    public static Settings _settings;
    public static UserBase _userBase;
    static void Main(string[] p_args)
    {
        AppFiles.Create();
        _settings = new Settings();
        _userBase = new UserBase();


        var mainMenu = new Views.Main();
        mainMenu.Display();


        
        
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}