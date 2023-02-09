﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VikingCommon.Models;

namespace VikingCommon;

public class AppFiles
{
    private static bool _force { get; set; }
    
    private static string _appName = ".vikingentity";
    //Folders
    public static readonly string _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".vikingentity");
    public static string _appDataPathImports = Path.Combine(_appDataPath, "imports");
    public static string _appDataPathLogs = Path.Combine(_appDataPath, "logs");
    
    //Files
    public static string _appFileSettings = Path.Combine(_appDataPath, "settings.json");
    public static string _appFileUsers = Path.Combine(_appDataPath, "users.json");
    public static string _appFileReviews = Path.Combine(_appDataPath, "ap-reviews.json");
    
    //API Keys
    public static string _appChatGptApiKey = @"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";
    

    public AppFiles(bool p_force = false)
    {
        _force = p_force;
        Create(_force);
    }

    private static void Create(bool p_force = false)
    {
        if (p_force)
        {
            Helpers.FileDir.SmartDeleteFolder(_appDataPath);
        }

        Helpers.FileDir.SmartCreateFolder(_appDataPath);
        Helpers.FileDir.SmartCreateFolder(_appDataPathImports);
        Helpers.FileDir.SmartCreateFolder(_appDataPathLogs);
        if (!File.Exists(_appFileReviews))
        {
            var obj = new List<Review>();
            JsonTools.SaveOptions(_appFileReviews, JsonSerializer.Serialize(obj));
        }

        User? adminUser = User.DefaultAdminUser(1);
        if (!File.Exists(_appFileUsers))
        {
            UserBase obj = new UserBase();
            PasswordHash hash = new PasswordHash();
            obj.Add(adminUser);
            obj.Commit();
        }
        if (!File.Exists(_appFileSettings))
        {
            var obj = new Settings();
            obj.LastUser = adminUser;
            obj.Commit();
        }
    }
}