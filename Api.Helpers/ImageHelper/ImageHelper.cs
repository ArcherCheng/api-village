using System;
using System.Collections;
using Microsoft.Extensions.Configuration;
namespace Api.Helpers;

public static class ImageHelper
{
    /// <summary> 
    /// convert resource\report\logo.png to Base64String
    /// </summary>
    /// <returns></returns>
    //[System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static string GetLogoImageBase64String()
    {
        try
        {
            string base64String="";
            //var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var resourcesFolder = AppSettingsHelper.ResourcesFolder();
            var reportsFolder = AppSettingsHelper.ReportTemplateFolder();
            var logoFile = System.IO.Path.Combine(resourcesFolder, reportsFolder, "logo.png");    
            if (!System.IO.File.Exists(logoFile)) {
                //throw new Exception("Logo.png file not find");
                logoFile = System.IO.Path.Combine(resourcesFolder, reportsFolder, "logo1.png");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(logoFile);
            base64String = System.Convert.ToBase64String(imageBytes);

            // using (var bm = new System.Drawing.Bitmap(logoFile))
            // {
            //     using var ms = new System.IO.MemoryStream();
            //     bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //     base64String = Convert.ToBase64String(ms.ToArray());
            // }  

            //// https://github.com/iron-software/IronSoftware.System.Drawing
            //// https://ironsoftware.com/open-source/csharp/drawing/docs/#trial-license-after-download
            //// use IronSoftware.Drawing
            // var bitmap = AnyBitmap.FromFile(logoFile);
            // //bitmap.SaveAs("result.png");
            // //var bytes = bitmap.ExportBytes();
            // var resultExport = new System.IO.MemoryStream();
            // bitmap.ExportStream(resultExport, AnyBitmap.ImageFormat.Png,100);
            // base64String = Convert.ToBase64String(resultExport.ToArray());            
            return base64String;            
        }
        catch (System.Exception)
        {
            return "";
            //throw;
        }

    }     

    /// <summary>
    /// convert image-file to base64String
    /// </summary>
    /// <param name="imageFilePath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    //[System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static string ConvertImageToBase64String(string imageFilePath)
    {
        try
        {
            string base64String="";
            if (!System.IO.File.Exists(imageFilePath)) {
                //throw new Exception($"{imageFilePath} file not find");
                return "";
            }
            byte[] imageBytes = System.IO.File.ReadAllBytes(imageFilePath);
            base64String = System.Convert.ToBase64String(imageBytes);

            // using (var bm = new System.Drawing.Bitmap(logoFile))
            // {
            //     using var ms = new System.IO.MemoryStream();
            //     bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //     base64String = Convert.ToBase64String(ms.ToArray());
            // }

            //// https://ironsoftware.com/open-source/csharp/drawing/docs/#trial-license-after-download
            //// use IronSoftware.Drawing        
            // var bitmap = AnyBitmap.FromFile(imageFilePath);
            // //bitmap.SaveAs("result.png");
            // //var bytes = bitmap.ExportBytes();
            // var resultExport = new System.IO.MemoryStream();
            // bitmap.ExportStream(resultExport, AnyBitmap.ImageFormat.Png,100);
            // base64String = Convert.ToBase64String(resultExport.ToArray());                    
            return base64String;            
        }
        catch (System.Exception)
        {
            return "";
            //throw;
        }

    }     

}