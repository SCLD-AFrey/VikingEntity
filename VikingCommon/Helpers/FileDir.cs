namespace VikingCommon.Helpers;

public static class FileDir
{
    public static void SmartDeleteFolder(string p_directory)
    {
        if (Directory.Exists(p_directory))
        {
            foreach (string file in Directory.GetFiles(p_directory))
            {
                File.Delete(file);
            }
            foreach (string subDirectory in Directory.GetDirectories(p_directory))
            {
                SmartDeleteFolder(subDirectory);
            }
            Directory.Delete(p_directory);
        }
    }
    public static void SmartCreateFolder(string p_directory)
    {
        if (!Directory.Exists(p_directory))
        {
            Directory.CreateDirectory(p_directory);
        }
    }
}