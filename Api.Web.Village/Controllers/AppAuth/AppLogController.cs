// using System.ComponentModel;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Api.Services;
// using Api.Models;
// using Api.Services.Village;
// using Api.Models;

// using Microsoft.Extensions.Logging;

// namespace Api.Controllers;

// [Description("權限系統--Action代號設定作業")]
// // [TypeFilter(typeof(RbacAuthorizeFilter))]
// [Authorize]
// [ApiController]
// [Route("api/[controller]")]
// public class AppLogController(IAppLogService service) : ControllerBase
// {
//     private readonly IAppLogService _service = service;

//     #region Log User Requests and Database Tables
//     //[Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("查詢用戶網頁使用記錄")]
//     [HttpGet("team/{teamId}/UserRequest/pageList")]
//     public async Task<IActionResult> GetAppLogRequestPageList(string teamId, [FromQuery] BaseParas baseParas)
//     {
//         var result = await this._service.GetAppLogRequestPageListAsync(baseParas);
//         if (result == null)
//             return NotFound();
//         return Ok(result);
//     }

//     //[Authorize]
//     //[AllowAnonymous]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("查詢資料表異動記錄")]
//     [HttpGet("team/{teamId}/UpdateTable/pageList")]
//     public async Task<IActionResult> GetAppLogTablePageList( string teamId,[FromQuery] BaseParas baseParas)
//     {
//         var result = await this._service.GetAppLogTablePageListAsync(baseParas);
//         if (result == null)
//             return NotFound();
//         return Ok(result);
//     }

//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [System.ComponentModel.Description("查詢使用者登入記錄")]
//     [HttpGet("team/{teamId}/UserLogin/pageList")]
//     public async Task<IActionResult> GetAppLogSinginPageList(string teamId, [FromQuery] BaseParas baseParas)
//     {
//         var result = await this._service.GetAppLogLoginPageListAsync(baseParas);
//         if (result == null)
//             return NotFound();
//         return Ok(result);
//     }

//     [System.ComponentModel.Description("查詢資料表異動記錄")]
//     [HttpGet("team/{teamId}/sms-message/pageList")]
//     public async Task<IActionResult> GetAppLogMessagePageList(string teamId, [FromQuery] BaseParas baseParas)
//     {
//         var result = await this._service.GetAppLogMessagePageListAsync(baseParas);
//         if (result == null)
//             return NotFound();
//         return Ok(result);
//     }

//     #endregion
// }
