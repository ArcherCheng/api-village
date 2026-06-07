
namespace Api.Helpers;

public static class FileExtensions
{
    public static void ClearDirectory(string dir)
    {
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        } 
        else
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(dir);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }      
            foreach (DirectoryInfo folder in di.GetDirectories())
            {
                folder.Delete(true); 
            }                   
        }
    }
}
