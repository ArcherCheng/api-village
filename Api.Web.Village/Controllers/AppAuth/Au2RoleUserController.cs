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
// public class Au2RoleUserController(ILogger<Au2RoleUserController> logger, IAu2RoleUserService service)
//     : ApiControllerBase<Au2RoleUser, Au2RoleUser, Au2RoleUser, IAu2RoleUserService>(logger,service)
// {
//     private readonly IAu2RoleUserService _service = service;
// }
