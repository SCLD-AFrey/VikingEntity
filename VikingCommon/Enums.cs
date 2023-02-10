namespace VikingCommon;

public class Enums
{
    public enum ViewMode
    {  
        Main,
        Admin,
        Settings,
        User,
        Exit,
        Login,
        ChatGpt
    }

    [Flags]
    public enum AdminRole
    {
        BasicUser = 0,
        UserManagement = 1,
        SettingsManagement = 2,
        Administrator = 4,
    }
}