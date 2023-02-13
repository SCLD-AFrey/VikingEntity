using JsonBase;
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

    public User? GetRootUser()
    {
        return Program.UserBase.FirstOrDefault(p_x => p_x.IsRootUser);
    }
}