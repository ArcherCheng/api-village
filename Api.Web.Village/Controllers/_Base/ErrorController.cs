using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Helpers;
using Api.Models;

namespace Api.Controllers;

// https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0
// https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error() 
    {
        var exception = HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        return Problem(
            detail: exception!.Error.StackTrace,
            title: exception!.Error.Message
        );
    }
}
 
