using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace VikingCommon;

public static class JsonTools
{
    public static void SaveOptions(string p_fileName, string p_text)
    {
        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(p_text, options);
        File.WriteAllText(p_fileName, jsonString);
    }
    
    public static string LoadOptions(string p_fileName)
    {
        var jsonString = File.ReadAllText(p_fileName);
        return JsonSerializer.Deserialize<string>(jsonString);
    }
}