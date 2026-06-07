using Microsoft.Extensions.Logging;

namespace Api.Helpers;

public static class MyFileLoggerFactory
{
    public static ILoggerFactory LoggerFactory { get; set; }

    static MyFileLoggerFactory()
    {
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).AddLogFilePath("c:\\logs"); 
    }
    
    public static ILoggerFactory AddLogFilePath(this ILoggerFactory factory, string filePath="c:\\logs")
    {
        factory.AddProvider(new MyFileLoggerProvider(filePath));
        return factory;
    }
    
    public static Microsoft.Extensions.Logging.ILogger<T> CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    public static Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
}

public class MyFileLoggerProvider : ILoggerProvider
{
    private string _path;
    private static object _lock = new object();
    public MyFileLoggerProvider(string path)
    {
        this._path = path;
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new MyFileLogger(this._path, categoryName);
    }
    
    public void Dispose()
    {
    }
}

public class MyFileLogger : ILogger
{
    private readonly string _path;
    private readonly string _category;
    private static object _lock = new object();
    // private static ApiUserData _currUser;

    public MyFileLogger(string path, string category)
    {
        this._path = path;
        this._category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;
    // public IDisposable BeginScope<TState>(TState state)
    // {
    //     return null;
    // }
    
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if(formatter != null){   // && exception != null
            lock(_lock) 
            {
                var logFilepath = Path.Combine(this._path,DateTime.Now.ToString("yyyy-MM-dd")+"_log.txt");
                var nl = Environment.NewLine;
                string exc = "";
                if (exception != null) {
                    exc = nl + exception.GetType()+ ": " + exception.Message + nl + exception.StackTrace + nl;
                }
                
                File.AppendAllText(logFilepath, nl+logLevel.ToString()+": "+DateTime.Now.ToString()+": "+nl+this._category+nl+formatter(state,exception!)+nl+exc+nl);
            }
        }
    }
}
