namespace VikingCommon;

public static class SafeInput
{
    private static void ShowPrompt(string p_prompt)
    {
        Console.Write(p_prompt + " : ");
    }
    
    public static bool Bool(string p_prompt = "")
    {
        char[] inputsTrue  = new char[] {'y','Y','t','T','1'};
        char[] inputsFalse = new char[] {'n','N','f','F','0'};
        char? input = ' ';
        while (!inputsTrue.Contains(input.Value) && !inputsFalse.Contains(input.Value))   
        {
            ShowPrompt(p_prompt);
            input = Console.ReadKey().KeyChar;
            Console.WriteLine();
        }

        if (inputsTrue.Contains(input.Value))
        {
            return true;
        }
        return false;
    }
    
    public static char Char(string p_prompt = "")
    {
        char? input = null;
        while (input == null || input == ' ')
        {
            ShowPrompt(p_prompt);
            input = Console.ReadKey().KeyChar;
            Console.WriteLine();
        }
        return input.Value;
    }
    
    
    public static string? String(string p_prompt = "")
    {
        string? input = "";
        while (string.IsNullOrEmpty(input))
        {
            ShowPrompt(p_prompt);
            input = Console.ReadLine();
        }
        return input;
    } 
    public static void String(out string p_output, string p_prompt = "")
    {
        p_output = "";
        while (string.IsNullOrEmpty(p_output))
        {
            ShowPrompt(p_prompt);
            p_output = Console.ReadLine();
        }
    } 
    public static int? Int(string p_prompt = "")
    {
        string? input = "";
        while (string.IsNullOrEmpty(input))
        {
            ShowPrompt(p_prompt);
            input = Console.ReadLine();
            if (int.TryParse(input, out var output))
            {
                return output;
            }
            input = "";
        }
        return null;
    } 
}