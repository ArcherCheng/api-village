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
// public class Au1ComponentController(ILogger<Au1ComponentController> logger, IAu1ComponentService service)
//     : ApiControllerBase<Au1Component, Au1Component, Au1Component, IAu1ComponentService>(logger,service)
// {
//     private readonly IAu1ComponentService _service = service;
// }
