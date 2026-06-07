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
// public class Au1ActionController(ILogger<Au1ActionController> logger, IAu1ActionService service)
//     : ApiControllerBase<Au1Action, Au1Action, Au1Action, IAu1ActionService>(logger,service)
// {
//     private readonly IAu1ActionService _service = service;
// }
