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
// public class Au2RoleActionController(ILogger<Au2RoleActionController> logger, IAu2RoleActionService service)
//     : ApiControllerBase<Au2RoleAction, Au2RoleAction, Au2RoleAction, IAu2RoleActionService>(logger,service)
// {
//     private readonly IAu2RoleActionService _service = service;
// }
