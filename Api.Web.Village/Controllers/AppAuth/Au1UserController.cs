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
// public class Au1UserController(ILogger<Au1UserController> logger, IAu1UserService service)
//     : ApiControllerBase<Au1User, Au1User, Au1User, IAu1UserService>(logger,service)
// {
//     private readonly IAu1UserService _service = service;
// }
