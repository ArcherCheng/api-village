namespace Api.Controllers;

public class ApiUploadResult
{
    public ApiUploadResult(string filePath, string fileName)
    {
        this.FilePath = filePath;
        this.FileName = fileName;
    }
    public string FilePath { get; set;}
    public string FileName { get; set;}
}
 
public class ApiUploadPhotoResult<T> where T : class
{
    public ApiUploadPhotoResult(string filePath, T result)
    {
        this.FilePath = filePath;
        this.Result = result;
    }
    public string FilePath { get; set;}
    public T Result { get; set;}
}