namespace VikingEntityConsole.Helpers;

public static class Messages
{
    public static void Welcome(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
    
    public static void Success(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
    
    public static void Error(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
    
    public static void Information(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
    
    public static void Menu(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
    public static void Results(string pMessage)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
}