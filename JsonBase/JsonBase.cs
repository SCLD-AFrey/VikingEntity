using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
// ReSharper disable MemberCanBePrivate.Global

namespace JsonBase;

public abstract class JsonBase<T> : Collection<T> , IJsonBase
{
    public string StoragePath { get; set; }
    public string StorageFile { get; set; }
    public string Name { get; set; } = typeof(T).Name;

    public override string ToString()
    {
        return $"{Name} JsonBase : {StorageFile} : {Count} item(s)";
    }

    protected JsonBase(string? p_storageFolder = null)
    {
        StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), (string.IsNullOrEmpty(p_storageFolder) ? ".jsonBase" : p_storageFolder));
        StorageFile = Path.Combine(StoragePath, $"_appDataFile{typeof(T).Name}.json");
        if (!Directory.Exists(StoragePath))
        {
            Directory.CreateDirectory(StoragePath);
        }
        Load();
    }
    
    public void Commit()
    {
        JsonSerializerOptions options = 
            new() { 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, 
                WriteIndented = true
            };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(StorageFile, jsonString);
    }
    
    public void Load()
    {
        if (!File.Exists(StorageFile))
        {
            File.WriteAllText(StorageFile, "[]");
        } 
        var jsonString = File.ReadAllText(StorageFile);
        // ReSharper disable once SuggestVarOrType_BuiltInTypes
        foreach (dynamic v in JsonConvert.DeserializeObject<dynamic>(jsonString)!)
        {
            var obj = JsonConvert.DeserializeObject<T>(v.ToString());
            Items.Add(obj);
        }
    }

    public int GetNextOid()
    {
        if (Count == 0)
        {
            return 1;
        }
        return this.Max(p_x => (int)(p_x!.GetType().GetProperty("Oid")?.GetValue(p_x) ?? 0)) + 1;
    }
}