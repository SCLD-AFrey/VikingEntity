using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class Settings
{
    public string AppName { get; set; } = "Viking Tools";
    public string AppVersion { get; set; } = "1.0.0";
    public string AppDescription { get; set; } = "Viking Tools is a collection of tools for the Viking Age.";
    public string AppAuthor { get; set; } = "Viking Tools";
    public bool RequireLogin { get; set; } = true;
    //public User LastUser { get; set; } = new User();

    public void Commit()
    {
        JsonSerializerOptions _options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(this, _options);
        File.WriteAllText(AppFiles._appSettingsFile, jsonString);
    }

    public void Load()
    {
        Settings? sets;
        if (File.Exists(AppFiles._appSettingsFile))
        {
            var jsonString = File.ReadAllText(AppFiles._appSettingsFile);
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
        RequireLogin = sets.RequireLogin;
        //LastUser = sets.LastUser;
    }
}