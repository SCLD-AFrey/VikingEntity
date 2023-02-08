namespace VikingCommon.Models;

public interface ISettings
{
    public void Commit();
    public void Load();
    public void Display();
}