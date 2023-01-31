using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class UserBase : List<User>
{
    public UserBase()
    {
        Load();
    }
    
    public void Load()
    {
        List<User>? users = new List<User>();
        if (File.Exists(AppFiles._appFileUsers))
        {
            var jsonString = File.ReadAllText(AppFiles._appFileUsers);
            users =  JsonSerializer.Deserialize<List<User>>(jsonString);
        }
        AddRange(users!);
    }

    public void Commit()
    {
        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText(AppFiles._appFileUsers, jsonString);
    }
    
    public int GetNextOid()
    {
        try{
            return this.Max(u => u.Oid) + 1;
        } catch (Exception) {
            return 1;
        }
    }
    
    public User Login(string p_userName, string p_password, out string p_message)
    {
        p_message = "";
        var user = this.FirstOrDefault(u => u.UserName == p_userName);
        if (user == null)
        {
            p_message = $"User '{p_userName}' not found.";
            return new User();
        }
        PasswordHash hash = new PasswordHash();
        if (!hash.VerifyPassword(p_password, user.Password, user.Salt))
        {
            p_message = $"User '{p_userName}' used an invalid password.";
            return new User();
        }
        return user;
    }
    
    

    
    
}