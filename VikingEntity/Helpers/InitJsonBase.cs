using VikingEntity.Models;
using VikingCommon;

namespace VikingEntity.Helpers;

public static class InitJsonBase
{
    public static void Init()
    {
        UserBase userBase = Program.UserBase;
        NotesBase notesBase = Program.NotesBase;
        SettingBase settingBase = Program.SettingBase;

        if(userBase.Count == 0)
        {
            PasswordHash ph = new();
            User admin = UserBase.DefaultUser();
            admin.Oid = userBase.GetNextOid();
            userBase.Add(admin);
            userBase.Commit();
        }
        
        if (settingBase.Count == 0)
        {
            TimeSpan ts = TimeSpan.FromHours(24);
            settingBase.Add(new Setting() { Name = "AppName", Value = "Viking Entity" });
            settingBase.Add(new Setting() { Name = "AppVersion", Value = "0.1.0" });
            settingBase.Add(new Setting() { Name = "AppDescription", Value = "Do Viking Stuff" });
            settingBase.Add(new Setting() { Name = "AppAuthor", Value = "Viking Enterprise" });
            settingBase.Add(new Setting() { Name = "LoginClaimTimeSpan", Value = ts.ToString() });
            settingBase.Add(new Setting() { Name = "LastUserOid", Value = userBase.GetRootUser()?.Oid.ToString() });
            settingBase.Commit();
        }
        
        
    }
}