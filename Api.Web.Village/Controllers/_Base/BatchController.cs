// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace Api.Controllers;

// [Description("批次過帳")]
// [AllowAnonymous]
// [ApiController]
// [Route("api/[controller]")]
// public class BatchController : ControllerBase
// {
//     [AllowAnonymous]
//     [Description("批次過帳訊息")]
//     [HttpGet("Status")]
//     public IActionResult GetCurrentStatus()
//     {
//         return Ok(Api.Helpers.GlobalVar.CurrentStatus());
//     }

//     [AllowAnonymous]
//     [Description("批次過帳訊息")]
//     [HttpGet]
//     public IActionResult GetCurrentId()
//     {
//         return Ok(Api.Helpers.GlobalVar.CurrentStatus());
//     }

// }

