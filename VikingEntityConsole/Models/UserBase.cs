using System.Text.Json;
using System.Text.Json.Serialization;
using VikingEntityConsole.Helpers;

namespace VikingEntityConsole.Models;

public class UserBase
{
    public List<User>? Users { get; set; }

    public User Login(string username, string password)
    {
        var user = Users?.FirstOrDefault(u => u.UserName == username);
        if (user != null)
        {
            PasswordCrypt crypt = new PasswordCrypt();
            if (crypt.VerifyPassword(password, user.Password, user.Salt))
            {
                return user;
            }
            else
            {
                return new User();
            }
        }
        return new User();
    }

    public int GetNextOid()
    {
        return Users?.Max(u => u.Oid) + 1 ?? 1;
    }

    public User ChangePassword(string username, string newPassword)
    {
        PasswordCrypt crypt = new PasswordCrypt();
        var user = Users.FirstOrDefault(x => x.UserName == username);
        if (user != null)
        {
            user.Password = crypt.GeneratePasswordHash(newPassword, out var salt);
            user.Salt = salt;
            user.RequirePasswordChange = false;
        }
        Commit();
        return user;
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

    public User GetById(int oid)
    {
        var user = Users?.FirstOrDefault(x => x.Oid == oid);
        if (user == null)
        {
            return new User();
        }
        return user;
    }

    public User GetByUsername(string username)
    {
        var user = Users?.FirstOrDefault(x => x.UserName == username);
        if (user == null)
        {
            return new User();
        }
        return user;
    }
}
