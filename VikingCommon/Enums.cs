﻿namespace VikingCommon;

public class Enums
{
    public enum ViewMode
    {  
        Main,
        Admin,
        Settings,
        User,
        Exit,
        Login
    }

    [Flags]
    public enum AdminRole
    {
        BasicUser = 0,
        UserManagement = 1,
        SettingsManagement = 2,
        Admin = BasicUser | UserManagement | SettingsManagement
    }
}