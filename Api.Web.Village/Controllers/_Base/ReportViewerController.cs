// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.IO;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Api.Helpers;
// //using Microsoft.Extensions.Caching.Memory;
// using Microsoft.AspNetCore.Hosting;
// using Api.Services;
// using Api.Models; 

// namespace Api.Controllers
// {
//     [Description("報表列印作業")]
//     [AllowAnonymous]
//     //[Authorize]
//     [ApiController] // 屬性會讓模型驗證錯誤 ModelState.IsValid 自動觸發 HTTP 400 回應。
//     [Route("api/[controller]/[action]")]
//     public class ReportViewerController: ControllerBase
//     {
//         // Report viewer requires a memory cache to store the information of consecutive client request and
//         // have the rendered report viewer information in server.
//         //private IMemoryCache _cache;

//         // IHostingEnvironment used with sample to get the application data from wwwroot.
//         private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;
//         private IHm1Emp10Service _service;
//         public ReportViewerController(IWebHostEnvironment hostingEnvironment
//             ,IHm1Emp10Service service
//         )
//         {
//             _hostingEnvironment = hostingEnvironment;            
//             //_cache =  memoryCache;
//             _service = service;
//         }

//     }
// }