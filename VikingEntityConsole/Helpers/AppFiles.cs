using System.Runtime.InteropServices;
using VikingEntityConsole.Models;

namespace VikingEntityConsole.Helpers;

public static class AppFiles
{
    //Folders
    public static readonly string LocalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".vikingentity");
    public static readonly string ImportsPath = Path.Combine(LocalPath, "imports");
    
    //Files
    public static readonly string SettingsFile = Path.Combine(LocalPath, "settings.json");
    public static readonly string UsersFile = Path.Combine(LocalPath, "users.json");
    
    public static void Create(bool force = false)
    {
        if (force)
        {
            DeleteFolders(ImportsPath);
            DeleteFolders(LocalPath);

            void DeleteFolders(string path)
            {
                var di = new DirectoryInfo(path);
                foreach (var f in di.GetFiles())
                {
                    f.Delete();
                }
                Directory.Delete(path);
            }

        }
        
        
        //Folders
        if(Directory.Exists(LocalPath).Equals(false)) Directory.CreateDirectory(LocalPath);
        if(Directory.Exists(ImportsPath).Equals(false)) Directory.CreateDirectory(ImportsPath);

        //Files
        if (!File.Exists(SettingsFile))
        {
            var obj = new Settings();
            obj.Commit();
        }
        if (!File.Exists(UsersFile))
        {
            var obj = new UserBase();
            obj.Users = new List<User>();
            obj.Users.Add(new User()
            {
                Oid = obj.GetNextOid() + 1,
                UserName = "admin",
                Password = "password",
                FirstName = "Administrator",
                LastName = "User",
                RequirePasswordChange = true
            });
            obj.Commit();
        }
        
        Program._settings.Load();
        Program._userBase.Load();
    }
}