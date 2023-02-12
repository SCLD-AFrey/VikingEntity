namespace VikingCommon;

public static class SafeInput
{
    public static string? String(string p_prompt = "", bool p_allowEmpty = false)
    {
        string? input = "";
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(p_prompt);
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && p_allowEmpty)
            {
                break;
            }
        }
        return input;
    } 
    public static DateTime? Date(string p_prompt = "", bool p_allowEmpty = false)
    {
        string? input = "";
        DateTime dt;
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(p_prompt);
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && p_allowEmpty)
            {
                break;
            }
        }
        if (DateTime.TryParse(input, out dt))
        {
            return dt;
        }
        return new DateTime();
    } 
    public static int? Int(string p_prompt = "", bool p_allowEmpty = false)
    {
        string? input = "";
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(p_prompt);
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && p_allowEmpty)
            {
                break;
            }
        }
        if (int.TryParse(input, out var output))
        {
            return output;
        }
        else
        {
            throw new Exception("Invalid input");
        }

    } 
}