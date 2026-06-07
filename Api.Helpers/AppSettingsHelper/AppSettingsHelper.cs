using System;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Api.Helpers;

public static class AppSettingsHelper
{
   /// <summary>
   /// string sqlString1 = Api.Helpers.AppSettingsHelper.Configuration.GetConnectionString("TestConnection");
   /// string sqlString2 = Api.Helpers.AppSettingsHelper.Configuration["Logging:LogLevel:Default"];
   /// </summary>
   public static IConfiguration Configuration { get; set; } = AppSettingsHelper.BuildConfiguration();

   public static IConfiguration BuildConfiguration()
   {
      return new ConfigurationBuilder()
         .Add(new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource
         {
            //沒有辦法辨別開發環境，
            Path = "appsettings.json",
            Optional = true,
            ReloadOnChange = true,
         })
         .Build();
   }

   public static string ConnectionStr()
   {
      AppSettingsHelper.Configuration ??= BuildConfiguration();

      // var resultStr = AppSettingsHelper.Configuration.GetConnectionString("DefaultConnection") ?? "not set connection string";

      var resultStr = AppSettingsHelper.Configuration.GetConnectionString("DefaultConnection")
          ?? "server=.\\SQLEXPRESS01; database=Village2026; User ID=SqlDbUser; Password=sql@123456; Integrated Security=true;TrustServerCertificate=true;Persist Security Info=False;";

      // var appSettings = System.IO.Path.Combine(AppSettingsHelper.ApplicationRootDirectory(), "appsettings.json");

      // var result = AppSettingsHelper.Configuration.GetSection("ConnectionStrings:DefaultConnection")?.Value
      //    ?? "server=.\\SQLEXPRESS01; database=Party2027; User ID=SqlDbUser; Password=sql@123456; Integrated Security=true;TrustServerCertificate=true;Persist Security Info=False;";

      return resultStr;
   }

   public static string ResourcesFolder()
   {
      AppSettingsHelper.Configuration ??= BuildConfiguration();
      var result = AppSettingsHelper.Configuration.GetSection("AppSettings:Resources").Value??"Resources";
      return result;
   }

   public static string ReportTemplateFolder()
   {
      AppSettingsHelper.Configuration ??= BuildConfiguration();
      var result = AppSettingsHelper.Configuration.GetSection("AppSettings:ReportTemplate").Value??"ReportTemplate";
      return result;
   }

   public static string JwtValidIssuer()
   {
      AppSettingsHelper.Configuration ??= BuildConfiguration();
      var result = AppSettingsHelper.Configuration.GetSection("JwtSettings:ValidIssuer").Value?? "https://*.justdo.tw";
      return result;
   }

   public static string JwtValidAudience()
   {
      AppSettingsHelper.Configuration ??= BuildConfiguration();
      var result = AppSettingsHelper.Configuration.GetSection("JwtSettings:ValidAudience").Value?? "https://localhost:5001";
      return result;
   }

   public static string GetReportTemplateFilePath(string reportPath, string reportName)
   {
      string resourcesFolder = Api.Helpers.AppSettingsHelper.ResourcesFolder();
      string reportsFolder = Api.Helpers.AppSettingsHelper.ReportTemplateFolder();
      var templateFile = System.IO.Path.Combine(resourcesFolder, reportsFolder, reportPath, reportName+".rdlc");
      if (!System.IO.File.Exists(templateFile)) {
         throw new Exception("File not find");
      }
      return templateFile;
   }

   public static bool IsDevelopment()
   {
      var result = AppSettingsHelper.Configuration?.GetSection("AppSettings:IsDevelopment").Value;
      if (result=="true") {
         return true;
      }
      return false;
   }

   //use example
   //var appSettings = AppSettingsHelper.GetAppSettings();
   //appSettings["keyName"]
   //https://stackoverflow.com/questions/39231951/how-do-i-access-configuration-in-any-class-in-asp-net-core
   public static IConfigurationRoot GetAppSettings()
   {
      string appRootDirectory = ApplicationRootDirectory();
      var builder = new ConfigurationBuilder().SetBasePath(appRootDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
      return builder.Build();

   }

   /// <summary>
   /// 取得應用程式的 Root Directory
   /// </summary>
   /// <returns>應用程式的 Root Directory</returns>
   /// <remarks>
   /// 例如 D:\Git\Party2026\Api\Api.Web
   /// </remarks>
   public static string ApplicationRootDirectory()
   {
      var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
      var appRoot = System.IO.Path.GetDirectoryName(location);
      return appRoot??".";
   }
}