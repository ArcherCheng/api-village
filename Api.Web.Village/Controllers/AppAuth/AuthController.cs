// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ActionConstraints;
// using Microsoft.AspNetCore.Mvc.Controllers;
// using Microsoft.AspNetCore.Mvc.Infrastructure;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.Extensions.Configuration;
// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Linq;
// using System.Reflection;
// using System.Threading.Tasks;
// using Api.Helpers;
// using Microsoft.Extensions.Logging;
// using System.Security.Claims;
// using Api.Services;
// using Api.Models;
// using System.Text.Json.Nodes;
// using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

// namespace Api.Controllers;

// [Description("權限系統-使用者權限管理系統")]
// [Authorize]
// //[TypeFilter(typeof(RbacAuthorizeFilter))]
// [ApiController]
// [Route("api/[controller]")]
// public partial class AuthController : ControllerBase
// {
//     private readonly IAuthService _service;
//     private readonly ILogger<AuthController> _logger;
//     private readonly IConfiguration _configuration;
//     private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
//     // private readonly IRouteAnalyzer _routeAnalyzer;

//     public AuthController(
//         IAuthService service,
//         ILogger<AuthController> logger,
//         IConfiguration configuration,
//         IActionDescriptorCollectionProvider actionDescriptorCollectionProvider
//         // IRouteAnalyzer routeAnalyzer,
//     )
//     {
//         this._service = service;
//         this._logger = logger;
//         this._configuration = configuration;
//         this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
//         // this._routeAnalyzer = routeAnalyzer;
//     }

//     #region check user has authorize
//     [Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("檢查用戶程式權限")]
//     [HttpGet("checkControllerActionAuthorize/{controllerId}/{actionId}")]
//     public async Task<IActionResult> CheckAuthorize(string controllerId, string actionId)
//     {
//         ApiUserData apiUserData = User.GetCurrentApiUserData();
//         if (apiUserData.UserType>0 && apiUserData.UserRole.Contains("Admin")) {
//             return Ok(true);
//         }
//         if (string.IsNullOrWhiteSpace(controllerId) || string.IsNullOrWhiteSpace(actionId)) {
//             return Ok(true);
//         }
//         if (controllerId == "Auth" || controllerId == "Login") {
//             return Ok(true);
//         }
//         var result = await this._service.CheckControllerActionAuthorizeAsync(User.GetCurrentUserId(), controllerId.ToLower(),actionId.ToLower());
//         return Ok(result);
//     }

//     [Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("檢查用戶首頁主選單權限")]
//     [HttpGet("checkComponentAuthorize/{componentId}")]
//     public async Task<IActionResult> CheckComponentAuthorize(string componentId)
//     {
//         if (string.IsNullOrWhiteSpace(componentId)) {
//             return Ok(true);
//         }

//         var result = await this._service.CheckComponentAuthorizeAsync(User.GetCurrentUserId(), componentId);
//         return Ok(result);
//     }

//     //[Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("設定程式名稱")]
//     // [HttpGet("setTitleToAuthDescription/{controllerId}/{actionId}/{title}")]
//     // public async Task<IActionResult> SetTitleToAuthDescription(string controllerId, string actionId, string title)
//     // {
//     //     if (string.IsNullOrWhiteSpace(controllerId) || string.IsNullOrWhiteSpace(actionId)) {
//     //         return Ok(true);
//     //     }
//     //     if (controllerId == "Auth" || controllerId == "Login") {
//     //         return Ok(true);
//     //     }
//     //     var baseActionList = new List<string>
//     //     {
//     //         "Add",
//     //         "Update",
//     //         "Delete",
//     //         "GetById",
//     //         "GetAll",
//     //         "GetViewList",
//     //         "CreateRdlcReport",
//     //         "GetViewPageList",
//     //     };
//     //     if (baseActionList.Any(x => x.Contains(actionId)) ) {
//     //         return Ok(true);
//     //     }
//     //     await this._service.SetTitleToAuthDescriptionAsync(controllerId.ToLower(),actionId.ToLower(),title);
//     //     return Ok(true);
//     // }

//     #region Auto Add Controller-Action and Http Method-Router
//     // 2023.10.14 取消 Au1Ctrller 檔案
//     // [Authorize]
//     // //[AllowAnonymous]
//     // //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("自動滙入系統代號(Controllers)")]
//     // [HttpGet("importControllerInfos")]
//     // public async Task<ActionResult> ImportControllerInfos()
//     // {
//     //     var result = Api.Helpers.ControllersHelper.ImportControllerInfos();
//     //     await _service.ImportControllerByCtrlInfosAsync(result);
//     //     return Ok(result);
//     // }

//     [Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("自動滙入程式代號(Actions)")]
//     // [HttpGet("importRouteInfos")]
//     // public async Task<IActionResult> ImportRouteInfos()
//     // {
//     //     var result = _routeAnalyzer.ImportRouteInfos();
//     //     await _service.ImportActionByRouteInfosAsync(result);
//     //     return Ok(result);
//     // }

//     // [Authorize]
//     // //[AllowAnonymous]
//     // //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("自動滙入報表代號(Reports)")]
//     // [HttpGet("importReportInfos")]
//     // public async Task<IActionResult> ImportRdlcNameInfos()
//     // {
//     //     var resourcesDir = Api.Helpers.AppSettingsHelper.ResourcesFolder(); //_configuration.GetSection("AppSettings:Resources").Value;
//     //     var reportsDir = Api.Helpers.AppSettingsHelper.ReportTemplateFolder(); //_configuration.GetSection("AppSettings:ReportTemplate").Value;
//     //     //var reportsFile = System.IO.Path.Combine(resourcesDir, reportsDir);
//     //     var result = _routeAnalyzer.ImportReportInfos(resourcesDir, reportsDir);
//     //     await _service.ImportActionByRouteInfosAsync(result);
//     //     return Ok(result);
//     // }

//     // [Authorize]
//     // //[AllowAnonymous]
//     // //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("設定前端程式系統")]
//     // [HttpPost("ImportComponent")]
//     // public async Task<IActionResult> ImportComponent([FromBody] IList<SystemMenu> spaMenus)
//     // {
//     //     if (!ModelState.IsValid)
//     //     {
//     //         var ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray());
//     //         this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(SystemMenu), ModelStateErrors);
//     //         return UnprocessableEntity(ModelStateErrors);
//     //     }
//     //     await this._service.ImportComponentBySpaSystemAsync(spaMenus);
//     //     return Ok(true);
//     // }

//     // https://joonasw.net/view/discovering-actions-and-razor-pages
//     // https://github.com/juunas11/AspNetCoreActionDiscovery/blob/master/AspNetCoreActionDiscovery/Controllers/TestController.cs
//     [Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("查詢所有的 Controllers and Actions")]
//     [HttpGet("all/controllerActions")]
//     public IActionResult GetControllerActions()
//     {
//         var result = _actionDescriptorCollectionProvider
//             .ActionDescriptors
//             .Items
//             .OfType<ControllerActionDescriptor>()
//             .Select(x => new
//             {
//                 x.DisplayName,
//                 x.ControllerName,
//                 x.ActionName,
//                 AttributeRouteTemplate = x.AttributeRouteInfo?.Template,
//                 HttpMethod = string.Join(", ", x.ActionConstraints?.OfType<HttpMethodActionConstraint>().SingleOrDefault()?.HttpMethods ?? new string[] { "any" }),
//                 Parameters = x.Parameters?.Select(p => new
//                 {
//                     type = p.ParameterType.Name,
//                     p.Name
//                 }),
//                 ControllerClassName = x.ControllerTypeInfo.FullName,
//                 ActionMethodName = x.MethodInfo.Name,
//                 filters = x.FilterDescriptors?.Select(f => new
//                 {
//                     ClassName = f.Filter.GetType().FullName,
//                     f.Scope
//                 }),
//                 Constraints = x.ActionConstraints?.Select(c => new
//                 {
//                     Type = c.GetType().Name
//                 }),
//                 RouteValues = x.RouteValues.Select(r => new
//                 {
//                     r.Key,
//                     r.Value
//                 }),
//                 MethodAttributes = x.MethodInfo.CustomAttributes?.Select(c => new
//                 {
//                     c.AttributeType,
//                     c.NamedArguments
//                 })
//             });
//         return Ok(result);
//     }

//     // https://joonasw.net/view/discovering-actions-and-razor-pages
//     // https://github.com/juunas11/AspNetCoreActionDiscovery/blob/master/AspNetCoreActionDiscovery/Controllers/TestController.cs
//     //[Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     //[HttpGet("allPages")]
//     // public IActionResult GetAppPages()
//     // {
//     //     return Ok(_actionDescriptorCollectionProvider
//     //         .ActionDescriptors
//     //         .Items
//     //         .OfType<Microsoft.AspNetCore.Mvc.RazorPages.PageActionDescriptor>()
//     //         .Select(a => new
//     //         {
//     //             a.DisplayName,
//     //             a.ViewEnginePath,
//     //             a.RelativePath,
//     //         }));
//     // }
//     #endregion
//     // [Authorize]
//     // //[AllowAnonymous]
//     // //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("設定程式歸屬系統")]
//     // [HttpPost("setAu1CtrllerSpaSystem")]
//     // public async Task<IActionResult> SetAu1CtrllerSpaSystem([FromBody] IList<SystemMenu> menus)
//     // {
//     //     if (!ModelState.IsValid)
//     //     {
//     //         var ModelStateErrors = ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray());
//     //         this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(SystemMenu), ModelStateErrors);
//     //         return UnprocessableEntity(ModelStateErrors);
//     //     }
//     //     await this._service.SetAu1CtrllerSpaSystemAsync(menus);
//     //     return Ok(true);
//     // }


//     // [Authorize]
//     // //[AllowAnonymous]
//     // //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     // [System.ComponentModel.Description("設定程式歸屬系統")]
//     // [HttpGet("getAu2RoleActionsBySpaSystem/{spaSystem}")]
//     // public async Task<IActionResult> GetAu2RoleActionsBySpaSystem(string spaSystem)
//     // {
//     //     var result = await this._service.GetAu2RoleActionsBySpaSystemAsync(this._userData.UserId, spaSystem);
//     //     return Ok(result);
//     // }
//     #endregion



// }

// // public class ForceChangePassword
// // {
// //     public string UserId { get; set; }
// //     public string NewPassword { get; set; }
// // }