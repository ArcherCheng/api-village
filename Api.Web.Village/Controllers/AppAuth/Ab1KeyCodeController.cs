// using System.ComponentModel;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using Api.Services;
// using Api.Models;

// namespace Api.Controllers;

// [Description("聯誼活動設定作業")]
// // [TypeFilter(typeof(RbacAuthorizeFilter))]
// [ApiController]
// [Route("api/[controller]")]
// public class Ab1KeyCodeController(ILogger<Ab1KeyCodeController> logger, IAb1KeyCodeService service)
//     : ApiControllerBase<Ab1KeyCode, Ab1KeyCode, Ab1KeyCode, IAb1KeyCodeService>(logger,service)
// {
//     [Description("查詢類別鍵值資料")]
//     [HttpGet("team/{teamId}/group-all/{group}")]
//     public async Task<IActionResult> GetKeyCodeIdLabelList(string teamId, string group)
//     {
//         var result = await service.GetKeyCodeIdLabelListAsync(group);
//         return Ok(result);
//     }
// }
