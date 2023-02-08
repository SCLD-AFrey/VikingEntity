using VikingCommon;

namespace VikingEntity.Views;

public class MenuItem
{
    private static string Text { get; set; }
    private static char Key { get; set; }
    private static Enums.AdminRole DisplayRole { get; set; } = Enums.AdminRole.BasicUser;

    public static void Display(char p_key, string p_text, Enums.AdminRole p_displayRole = Enums.AdminRole.BasicUser)
    {
        Text = p_text;
        Key = p_key;
        DisplayRole = p_displayRole;
        if (Program.CurrentUser.Roles.Contains(DisplayRole))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Key.ToString()}. {p_text}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"?. {p_text}");
            Console.ResetColor();
        }
    }
}