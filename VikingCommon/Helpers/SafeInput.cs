namespace VikingCommon;

public static class SafeInput
{
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