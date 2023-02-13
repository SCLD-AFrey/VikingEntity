namespace VikingCommon;

public static class SafeInput
{
    public static char Char(string p_prompt = "")
    {
        char? input = null;
        while (input == null)
        {
            Console.Write(p_prompt);
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
            Console.Write(p_prompt);
            input = Console.ReadLine();
        }
        return input;
    } 
    public static void String(out string p_output, string p_prompt = "")
    {
        p_output = "";
        while (string.IsNullOrEmpty(p_output))
        {
            Console.Write(p_prompt);
            p_output = Console.ReadLine();
        }
    } 
    public static int? Int(string p_prompt = "")
    {
        string? input = "";
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(p_prompt);
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