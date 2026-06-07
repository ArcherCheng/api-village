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
// public class Au2RoleComponentController(ILogger<Au2RoleComponentController> logger, IAu2RoleComponentService service)
//     : ApiControllerBase<Au2RoleComponent,Au2RoleComponent, Au2RoleComponent, IAu2RoleComponentService>(logger,service)
// {
//     private readonly IAu2RoleComponentService _service = service;
// }
