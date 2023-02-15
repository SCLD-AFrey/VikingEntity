using JsonBase;
using JsonBase.Models.Logging;
using VikingCommon;

namespace VikingEntity.Models;

public class UserBase : JsonBase<User>, IUserBase
{

    public UserBase()
    {

    }

    public static User DefaultUser()
    {
        PasswordHash ph = new();
        return new User()
        {
            Oid = 0,
            UserName = "admin",
            FirstName = "Administator",
            LastName = "User",
            Password = ph.GeneratePasswordHash("Password", out var salt),
            Salt = salt,
            DateCreated = DateTime.UtcNow,
            IsRootUser = true,
            Roles = new List<Enums.AdminRole>()
            {
                Enums.AdminRole.Administrator
            }
        };
    }

    public static User? GetRootUser()
    {
        try
        {
            return Program.UserBase.FirstOrDefault(p_x => p_x.IsRootUser);
        }
        catch (Exception e)
        {
            Program.LoggerBase.Log(e.Message, LogEntry.Severity.Error, Program.CurrentUser!.Oid);
            return null;
        }
    }
}