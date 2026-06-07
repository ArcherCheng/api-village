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
// public class Au1RoleController(ILogger<Au1RoleController> logger, IAu1RoleService service)
//     : ApiControllerBase<Au1Role, Au1Role, Au1Role, IAu1RoleService>(logger,service)
// {
//     private readonly IAu1RoleService _service = service;
// }
