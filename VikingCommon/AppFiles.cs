using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VikingCommon.Models;

namespace VikingCommon;

public class AppFiles
{
    private static readonly string _adminUsername = "admin";
    private static string _appName = ".vikingentity";
    //Folders
    public static readonly string _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".vikingentity");
    public static string _appDataPathImports = Path.Combine(_appDataPath, "imports");
    public static string _appDataPathLogs = Path.Combine(_appDataPath, "logs");
    
    //Files
    public static string _appFileSettings = Path.Combine(_appDataPath, "settings.json");
    public static string _appFileUsers = Path.Combine(_appDataPath, "users.json");
    public static string _appFileReviews = Path.Combine(_appDataPath, "ap-reviews.json");

    public AppFiles()
    {
        Create();
    }

    public static void Create(bool force = false)
    {
        if (!Directory.Exists(_appDataPath) || force)
        {
            if(force) SmartDeleteDirectory(_appDataPath);
            Directory.CreateDirectory(_appDataPath);
        }
        if (!Directory.Exists(_appDataPathImports) || force)
        {
            if(force) SmartDeleteFile(_appDataPathImports);
            Directory.CreateDirectory(_appDataPathImports);
        }
        if (!Directory.Exists(_appDataPathLogs) || force)
        {
            if(force) SmartDeleteFile(_appDataPathLogs);
            Directory.CreateDirectory(_appDataPathLogs);
        }
        if (!File.Exists(_appFileSettings))
        {
            var obj = new List<Review>();
            JsonTools.SaveOptions(_appFileReviews, JsonSerializer.Serialize(obj));
        }
        bool createUsers = false;
        if (!File.Exists(_appFileUsers))
        {
            createUsers = true;
            UserBase obj = new UserBase();
            PasswordHash hash = new PasswordHash();
            obj.Add(User.DefaultAdminUser());
            obj.Commit();
        }
        if (!File.Exists(_appFileSettings))
        {
            var obj = new Settings();
            if (createUsers)
            {
                UserBase users = new UserBase();
                users.Load();
                obj.LastUser = users.GetUserByUsername(_adminUsername) ?? new User();
            }
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