using VikingEntityConsole.Helpers.Menu;

namespace VikingEntityConsole.Display;

public class FileMenu : AbstractMenu
{
    public FileMenu() : base("Users Menu") {}
    
    protected override void Init()
    {
        AddMenuItem(new MenuItem(0, "Display Files", ListFiles));
        AddMenuItem(new MenuItem(1, "Import File", ImportFile));
        AddMenuItem(new MenuItem(2, "Back to Main").SetAsExitOption());
    }

    private void ImportFile()
    {

    }

    private void ListFiles()
    {
        



    }
}