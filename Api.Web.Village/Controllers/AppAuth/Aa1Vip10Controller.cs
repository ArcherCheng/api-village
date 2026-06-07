// using System.ComponentModel;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using Dapper;
// using Api.Services;
// using Api.Models;

// namespace Api.Controllers;

// [Description("聯誼活動設定作業")]
// // [TypeFilter(typeof(RbacAuthorizeFilter))]
// [ApiController]
// [Route("api/[controller]")]
// public class Au1Team10Controller( ILogger<Au1Team10Controller> logger, AppDbContext db): ControllerBase   //, IConfiguration configuration, IAu1Team10Service service
// {
//     // private readonly ILogger<Au1Team10Controller> _logger = logger;
//     // private readonly IConfiguration _configuration = configuration;
//     // private readonly IAu1Team10Service _service = service;

//     [Description("查詢VIP表格鍵值的資料")]
//     [HttpGet("team/{teamId}")]
//     [AllowAnonymous]
//     public async Task<IActionResult> GetByTeamId(string teamId)
//     {
//         //var result = await AppDbHelper.GetByIdAsync<Au1Team10>(teamId);
//         //var result = await this._service.GetByIdAsync<Au1Team10>(teamId);
//         var result = await db.Au1Team10.AsNoTracking().FirstOrDefaultAsync(x => x.TeamId == teamId);
//         if (result == null)
//         {
//             logger.LogWarning($"{typeof(Au1Team10Controller)} GetByIdAsync not found TeamId = {teamId}");
//             return NotFound();
//         }
//         var result2 = AgileObjects.AgileMapper.Mapper.Map(result).ToANew<DtoAu1Team10>();
//         return Ok(result2);

//         //// Dapper Error Invalid cast from 'System.DateTime' to 'System.DateOnly'
//         // string sql = "select * from Au1Team10 where TeamId = @TeamId";
//         // string defaultConnection = _configuration.GetConnectionString("DefaultConnection")??"";
//         // using var conn = new Microsoft.Data.SqlClient.SqlConnection(defaultConnection);
//         // var result3 = await conn.QueryFirstOrDefaultAsync<Au1Team10>(sql, new { TeamId = teamId });
//         // return Ok(result3);
//     }

// }

