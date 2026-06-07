using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Api.Models;

public class BaseDbContext : DbContext
{
    public BaseDbContext()
    {
    }

    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connString = Api.Helpers.AppSettingsHelper.ConnectionStr();
        if (!optionsBuilder.IsConfigured)
        {
            //SqlServer connection
            optionsBuilder.UseSqlServer(connString);
            ///顯示SQL語法指令
            optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
            //optionsBuilder.LogTo(System.Console.WriteLine, LogLevel.Information);
            optionsBuilder.LogTo(System.Console.WriteLine, LogLevel.Warning);
            // ////顯示SQL語法指令
            optionsBuilder.UseLoggerFactory(MyDbLoggerFactory);
        }
        base.OnConfiguring(optionsBuilder);
    }

    public static readonly ILoggerFactory MyDbLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddProvider(new Api.Helpers.MyFileLoggerProvider("c:\\logs\\sql\\"));
    });

    public string GetConnectionString()
    {
        // var configuration2 = new ConfigurationBuilder()
        // .Add(new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource
        // {
        //     Path = "appsettings.json",
        //     Optional = true,
        //     ReloadOnChange = true,
        // })
        // .Build();

        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        var connString = configuration.GetConnectionString("DefaultConnection")??"";
        return connString;
    }
}

