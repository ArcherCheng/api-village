using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Api.Controllers;

public class FallbackController : Controller
{
    public IActionResult Index()
    {
        /*
            //class startup.Configure()
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index","Fallback");
            });
        */            
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","index.html"), "text/HTML");
        //return PhysicalFileResult(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","index.html"), "text/HTML");
    }
}
 