using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace JsonBase;

public abstract class JsonBase<T> : List<T> , IJsonBase
{
    private readonly string _storageFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), $"_appFile{typeof(T).Name}.json");

    protected JsonBase(bool p_load = true)
    {
        if (p_load)
        {
            Load();
        }
    }
    
    public void Commit()
    {
        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(_storageFileName, jsonString);
    }
    
    public void Load()
    {
        T? objs;
        if (!File.Exists(_storageFileName))
        {
            File.WriteAllText(_storageFileName, "[]");
        } 
        var jsonString = File.ReadAllText(_storageFileName);
        foreach (dynamic v in JsonConvert.DeserializeObject<dynamic>(jsonString)!)
        {
            var obj = JsonConvert.DeserializeObject<T>(v.ToString());
            this.Add(obj);
        }
       

    }
    
    
    
}