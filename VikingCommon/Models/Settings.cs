using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class Settings : ISettings
{
    public string AppName { get; set; } = "The Viking Entity";
    public string AppVersion { get; set; } = "0.1.0";
    public string AppDescription { get; set; } = "Viking Tools is a collection of tools for the Viking Age.";
    public string AppAuthor { get; set; } = "Viking Tools";
    public User LastUser { get; set; } = new User();

    public Settings()
    {
        //Load();
    }

    public void Commit()
    {
        JsonSerializerOptions _options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(this, _options);
        File.WriteAllText(AppFiles._appFileSettings, jsonString);
    }

    public void Load()
    {
        Settings? sets;
        if (File.Exists(AppFiles._appFileSettings))
        {
            var jsonString = File.ReadAllText(AppFiles._appFileSettings);
            sets =  JsonSerializer.Deserialize<Settings>(jsonString);
        }
        else
        {
            sets = new Settings();
        }
        
        AppName = sets!.AppName;
        AppVersion = sets.AppVersion;
        AppDescription = sets.AppDescription;
        AppAuthor = sets.AppAuthor;
        LastUser = sets.LastUser;
    }
}