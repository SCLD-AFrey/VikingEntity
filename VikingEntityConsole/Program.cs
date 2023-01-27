using VikingEntityConsole.Helpers;
using VikingEntityConsole.Models;

namespace VikingEntityConsole;

static class Program
{
    internal static Settings _settings = new Settings();
    internal static UserBase _userBase = new UserBase();
    static void Main(string[] args)
    {
        AppFiles.Create();
        
        var mainMenu = new Display.Main();
        mainMenu.Display();

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}