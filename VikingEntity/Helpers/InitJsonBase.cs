using JsonBase.Models.Settings;
using VikingEntity.Models;
using VikingCommon;

namespace VikingEntity.Helpers;

public static class InitJsonBase
{
    public static void Init()
    {
        UserBase userBase = Program.UserBase;
        // NotesBase notesBase = Program.NotesBase;
        SettingBase settingBase = Program.Settings;

        if(userBase.Count == 0)
        {
            PasswordHash ph = new();
            User admin = UserBase.DefaultUser();
            admin.Oid = userBase.GetNextOid();
            userBase.Add(admin);
            userBase.Commit();
        }

        
        
    }
}