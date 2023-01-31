using System.Net.Http.Headers;
using VikingCommon.Models;

namespace VikingCommon;

public class AppFiles
{
    private static string _appName = ".vikingentity";
    //Folders
    public static readonly string _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".vikingentity");
    public static string _appDataPathUser = Path.Combine(_appDataPath, "imports");
    
    //Files
    public static string _appFileSettings = Path.Combine(_appDataPath, "settings.json");
    public static string _appFileUsers = Path.Combine(_appDataPath, "users.json");

    public static void Create(bool force = false)
    {
        if (!Directory.Exists(_appDataPath) || force)
        {
            if(force) SmartDeleteDirectory(_appDataPath);
            Directory.CreateDirectory(_appDataPath);
        }
        if (!Directory.Exists(_appDataPathUser) || force)
        {
            if(force) SmartDeleteFile(_appDataPathUser);
            Directory.CreateDirectory(_appDataPathUser);
        }

        if (!File.Exists(_appFileSettings))
        {
            var obj = new Settings();
            obj.Commit();
        }

        if (!File.Exists(_appFileUsers))
        {
            UserBase obj = new UserBase();
            PasswordHash hash = new PasswordHash();
            obj.Add(new User()
            {
                Oid = obj.GetNextOid(),
                UserName = "admin",
                FirstName = "Admin", 
                LastName = "User",
                Password = hash.GeneratePasswordHash("password", out var salt),
                Salt = salt
            });
            obj.Commit();
        }
    }

    public static void SmartDeleteDirectory(string directory)
    {
        if (Directory.Exists(directory))
        {
            var di = new DirectoryInfo(directory);
            foreach (var f in di.GetFiles())
            {
                f.Delete();
            }
            Directory.Delete(directory);
        }
    }

    public static void SmartDeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }
    
}