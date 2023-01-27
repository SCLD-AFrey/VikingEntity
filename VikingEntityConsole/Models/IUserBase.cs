namespace VikingEntityConsole.Models;

public interface IUserBase
{
    public int GetNextOid();
    public void Load();
    public void Commit();
}