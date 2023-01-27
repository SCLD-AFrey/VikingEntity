﻿using System.Text.Json;
using System.Text.Json.Serialization;
using VikingEntityConsole.Helpers;

namespace VikingEntityConsole.Models;

public class UserBase
{
    public List<User>? Users { get; private set; }

    public UserBase()
    {
        if (File.Exists(AppFiles.UsersFile))
        {
            Load();
        }
        else
        {
            Users = new List<User>();
        }
    }

    public int GetNextOid()
    {
        if (Users!.Count > 0)
        {
            return Users.Max(x => x.Oid) + 1;
        }
        else
        {
            return 1;
        }
    }

    private void Load()
    {
        if (File.Exists(AppFiles.SettingsFile))
        {
            var jsonString = File.ReadAllText(AppFiles.SettingsFile);
            Users =  JsonSerializer.Deserialize<List<User>>(jsonString);
        }
        else
        {
            Users = new List<User>();
        }
    }

    public void Commit()
    {
        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(Users, options);
        File.WriteAllText(AppFiles.UsersFile, jsonString);
    }
}
