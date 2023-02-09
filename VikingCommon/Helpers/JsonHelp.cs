using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VikingCommon.Helpers;

public static class JsonHelp
{
    public static bool IsValidJson(string p_input)
    {
        if (string.IsNullOrWhiteSpace(p_input)) { return false;}
        p_input = p_input.Trim();
        if ((p_input.StartsWith("{") && p_input.EndsWith("}")) || //For object
            (p_input.StartsWith("[") && p_input.EndsWith("]"))) //For array
        {
            try
            {
                var obj = JToken.Parse(p_input);
                return true;
            }
            catch (Exception ex) //some other exception
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}