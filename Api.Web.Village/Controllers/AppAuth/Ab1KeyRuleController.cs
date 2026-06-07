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
// public class Ab1KeyRuleController(ILogger<Ab1KeyRuleController> logger,IAb1KeyRuleService service )
//     : ApiControllerBase<Ab1KeyRule, Ab1KeyRule, Ab1KeyRule, IAb1KeyRuleService>(logger,service)
// {
//     private readonly IAb1KeyRuleService _service = service;

//     [HttpGet("team/{teamId}/first/{ruleId}")]
//     public async Task<IActionResult> GetKeyRuleValue(string teamId, string ruleId)
//     {
//         var result = await _service.GetKeyRuleValueAsync(ruleId);
//         return Ok(result);
//     }
// }
