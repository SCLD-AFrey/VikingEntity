using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class Settings : ISettings
{
    public string AppName { get; set; } = "The Viking Entity";
    public string AppVersion { get; set; } = "0.1.0";
    public string AppDescription { get; set; } = "Viking Tools is a collection of tools for the Viking Age.";
    public string AppAuthor { get; set; } = "Viking Tools Utd.";
    public User LastUser { get; set; } = new User();
    
    public int LoginClaimHours { get; set; } = 24;

    public Settings()
    {
        //Load();
    }

    public void Display()
    {
        Console.WriteLine($"AppName: {AppName}");
        Console.WriteLine($"AppVersion: {AppVersion}");
        Console.WriteLine($"AppDescription: {AppDescription}");
        Console.WriteLine($"AppAuthor: {AppAuthor}");
        Console.WriteLine($"LoginClaim: {LoginClaimHours} hours");
        Console.WriteLine($"LastUser: {LastUser.UserName}");
        Console.WriteLine($"         : {LastUser.FirstName} {LastUser.LastName}");
        Console.WriteLine($"         : {LastUser.LastLogin}");
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

    public void Update(string p_settingName, string p_settingValue)
    {
        var settings = new Settings();
        settings.Load();
        var setting = settings.GetType().GetProperty(p_settingName);
        if (setting == null)
        {
            throw new Exception("Invalid Setting");
        }
        
        try
        {
            switch (setting?.PropertyType.Name)
            {
                case "Int32":
                    settings.GetType().GetProperty(p_settingName)?.SetValue(settings, int.Parse(p_settingValue));
                    break;
                case "Boolean":
                    settings.GetType().GetProperty(p_settingName)?.SetValue(settings, bool.Parse(p_settingValue));
                    break;
                case "String":
                    settings.GetType().GetProperty(p_settingName)?.SetValue(settings, p_settingValue);
                    break;
                case "DateTime":
                    settings.GetType().GetProperty(p_settingName)?.SetValue(settings, DateTime.Parse(p_settingValue));
                    break;
                default:
                    throw new Exception("Invalid Setting Type");
            }
            settings.Commit();
        } catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}