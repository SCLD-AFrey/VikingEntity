using System.Text.Json;
using System.Text.Json.Serialization;
using VikingEntityConsole.Helpers;

namespace VikingEntityConsole.Models;

public class UserBase
{
    public List<User>? Users { get; set; }

    public int GetNextOid()
    {
        try{
            return Users.Max(x => x.Oid) + 1;
        } catch (Exception) {
            return 1;
        }
    }

    public void ChangePassword(string username, string newPassword)
    {
        PasswordCrypt crypt = new PasswordCrypt();
        var user = Users.FirstOrDefault(x => x.UserName == username);
        if (user != null)
        {
            user.Password = crypt.GeneratePasswordHash(newPassword, out var salt);
            user.Salt = salt;
        }
        Commit();
        
    }
    
    
    public void Load()
    {
        if (File.Exists(AppFiles.UsersFile))
        {
            var jsonString = File.ReadAllText(AppFiles.UsersFile);
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
