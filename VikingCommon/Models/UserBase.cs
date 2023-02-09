using System.Text.Json;
using System.Text.Json.Serialization;

namespace VikingCommon.Models;

public class UserBase : List<User>
{
    public UserBase()
    {
        //Load();
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
            UserBase users = new UserBase();
            users.Load();
            return users.Max(p_u => p_u.Oid) + 1;
        } catch (Exception) {
            return 1;
        }
    }

    public User? GetUserByUsername(string p_userName)
    {
        return this.FirstOrDefault(u => u.UserName == p_userName);
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

        if (user.RequirePasswordChange)
        {
            string? newPassword = null;
            string? chkPassword = null;
            while(string.IsNullOrEmpty(newPassword) || newPassword != chkPassword)
            {
                Console.Write("New Password: ");
                newPassword = Console.ReadLine();
                Console.Write("Confirm Password: ");
                chkPassword = Console.ReadLine();
                if (newPassword != chkPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                    newPassword = null;
                    chkPassword = null;
                }
            }
            user.Password = hash.GeneratePasswordHash(newPassword, out byte[] salt);
            user.Salt = salt;
            user.RequirePasswordChange = false;
        }
        user.LastLogin = DateTime.UtcNow;
        return user;
    }
    
    

    
    
}