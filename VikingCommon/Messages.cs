namespace VikingCommon;

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
    public static void Results(string pMessage, bool p_isAlt = false)
    {
        Console.ForegroundColor = p_isAlt ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
        Console.WriteLine(pMessage);
        Console.ResetColor();
    }
}