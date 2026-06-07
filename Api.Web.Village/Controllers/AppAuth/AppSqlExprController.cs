// using System.Linq;
// using System.ComponentModel;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using Microsoft.Data.SqlClient;
// using Newtonsoft.Json.Linq;
// using System.Security.Claims;
// using System.Collections.Generic;
// using Dapper;
// using System.Text.Json;
// using System.Reflection;
// using Newtonsoft.Json;
// using Api.Helpers;
// using Api.Services;
// using Api.Models;

// namespace Api.Controllers;

// [Description("Sql語法執行器")]
// [ApiController]
// [Route("api/[controller]")]
// [Authorize]
// public class AppSqlExprController : ControllerBase
// {
//     private readonly AppDbContext db;
//     private readonly ILogger _logger;
//     private readonly IConfiguration _Config;

//     public AppSqlExprController(
//         AppDbContext db,
//         ILogger<AppSqlExprController> logger,
//         IConfiguration configuration)
//     {
//         this.db = db;
//         this._logger = logger;
//         this._Config = configuration;
//     }

//     #region Dapper Sql query
//     [Description("查詢資料庫資料")]
//     [Authorize]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     [HttpPost("select")]
//     public async Task<IActionResult> QuerySql([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec sql: {model.SqlExpr}");

//         if (model.SqlExpr.Trim().ToSubstring(0,6).ToLower() != "select")
//             return BadRequest("select sql 語法不對");

//         string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//         using var conn = new SqlConnection(connectionStr);
//         string sqlStr= ParseSql(model.SqlExpr);
//         var result = await conn.QueryAsync(sqlStr);
//         return Ok(result);
//     }

//     [Description("修改資料庫資料")]
//     //[Authorize]
//     [HttpPost("update")]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     public async Task<IActionResult> UpdateSql([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec sql: {model.SqlExpr}");

//         if (model.SqlExpr.Trim().ToSubstring(0,6).ToLower() != "update")
//             return BadRequest("update sql 語法不對");

//         string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//         using var conn = new SqlConnection(connectionStr);
//         string sqlStr= ParseSql(model.SqlExpr);
//         var result = await conn.ExecuteAsync(sqlStr);
//         return Ok(result);
//     }

//     [Description("新增資料庫資料")]
//     //[Authorize]
//     [HttpPost("insert")]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     public async Task<IActionResult> InsertSql([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec sql: {model.SqlExpr}");

//         if (model.SqlExpr.Trim().ToSubstring(0,6).ToLower() != "insert")
//             return BadRequest("insert sql 語法不對");

//         string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//         using var conn = new SqlConnection(connectionStr);
//         string sqlStr= ParseSql(model.SqlExpr);
//         var result = await conn.ExecuteAsync(sqlStr);
//         return Ok(result);
//     }

//     [Description("刪除資料庫資料")]
//     //[Authorize]
//     [HttpPost("delete")]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     public async Task<IActionResult> DeleteSql([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec sql: {model.SqlExpr}");

//         if (model.SqlExpr.Trim().ToSubstring(0,6).ToLower() != "delete")
//             return BadRequest("delete sql 語法不對");

//         string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//         using var conn = new SqlConnection(connectionStr);
//         string sqlStr= ParseSql(model.SqlExpr);
//         var result = await conn.ExecuteAsync(sqlStr);
//         return Ok(result);
//     }

//     [Description("建立或刪除概觀檔資料")]
//     //[Authorize]
//     [HttpPost("view")]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     public async Task<IActionResult> ViewSql([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec sql: {model.SqlExpr}");
//         var matchSql = new List<string>
//         {
//             "create view",
//             "drop view",
//             "view"
//         };

//         if (matchSql.Any(x => model.SqlExpr.ToLower().Contains(x)))
//         {
//             string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//             using var conn = new SqlConnection(connectionStr);
//             string errMsg = "";
//             string sqlStr = "";
//             int result = 0;
//             bool isMark = false;
//             char[] delimiterChars = ['\n'];  //,'\r'
//             var array = model.SqlExpr.Split(delimiterChars);  //new string[] {Environment.NewLine},StringSplitOptions.RemoveEmptyEntries
//             foreach (var line in array)
//             {
//                 string temp = line.Trim();
//                 if (temp.Length == 0) continue;
//                 if (temp.StartsWith("--")) continue;
//                 if (temp.StartsWith("/*"))
//                 {
//                     isMark= true;
//                     continue;
//                 }
//                 if (temp.StartsWith("*/"))
//                 {
//                     isMark= false;
//                     continue;
//                 }
//                 if (isMark)
//                 {
//                     _logger.LogInformation($"sql ignore mark block /* */: {temp}");
//                     continue;
//                 }
//                 if (!temp.StartsWith("go", StringComparison.CurrentCultureIgnoreCase))
//                 {
//                     var sqltemp = StringExtensions.ToParsing(ref temp,"--");  //消除字尾有註解行的指令
//                     sqlStr += (" "+sqltemp);
//                 }
//                 if (temp.StartsWith("go", StringComparison.CurrentCultureIgnoreCase) || sqlStr.EndsWith(';'))
//                 {
//                     try
//                     {
//                         if (sqlStr.Trim().Length == 0) continue;
//                         if (sqlStr.Trim().ToLower() == "go") continue;
//                         if (sqlStr.ToLower().IndexOf("view")>=0){
//                             result += await conn.ExecuteAsync(sqlStr);
//                         } else {
//                             _logger.LogWarning($"sql view not found: {sqlStr}");
//                             errMsg  += $"sql view not found: {sqlStr}" + Environment.NewLine;
//                         }
//                         sqlStr = "";
//                         continue;
//                     }
//                     catch (System.Exception ex)
//                     {
//                         _logger.LogError($"sql error line: {sqlStr}");
//                         _logger.LogError($"sql error message: {ex.Message}");
//                         errMsg  += ex.Message + Environment.NewLine;
//                         //throw;
//                     }
//                     finally
//                     {
//                         sqlStr = ""; //reset
//                     }
//                 }
//             }
//             if (errMsg.Length>0)
//             {
//                 return BadRequest(errMsg);
//             }
//             return Ok(Math.Abs(result));
//         }
//         return BadRequest("Sql View 語法不對:");
//     }

//     [Description("執行其他SQL語法指令")]
//     //[Authorize]
//     [HttpPost("OtherTemp")]
//     // [TypeFilter(typeof(RbacAuthorizeFilter))]
//     public async Task<IActionResult> DropTempTable([FromBody] AppTempSql model)
//     {
//         var _userData = User.GetCurrentApiUserData();
//         _logger.LogInformation($"{_userData.UserId} {_userData.UserName} exec other sql: {model.SqlExpr}");
//         var matchSql = new List<string>
//         {
//             "drop table temp",
//             "temp",
//             "apptempexcel",
//             "applogtable",
//             "applogrequest",
//             "Alter Table ",
//             "create trigger",
//             "drop trigger",
//             "create function",
//             "drop function",
//             "alter function",
//             "create index",
//             "drop index",
//             "create",
//             "exec",
//             "alter"
//         };
//         if (matchSql.Any(x => model.SqlExpr.ToLower().Contains(x)) )
//         {
//             string connectionStr = _Config.GetSection("ConnectionStrings:DefaultConnection").Value!;
//             using var conn = new SqlConnection(connectionStr);
//             string sqlStr= ParseSql(model.SqlExpr);
//             var result = await conn.ExecuteAsync(sqlStr);
//             return Ok(result);
//         }
//         return BadRequest("sql 語法不對");
//     }

//     private string ParseSql(string sqlExpr)
//     {
//         if (sqlExpr.IsNullOrEmpty()) return "";
//         char[] delimiterChars = ['\n'];
//         var array = sqlExpr.Split(delimiterChars);
//         string sqlStr="";
//         bool isMark = false;
//         foreach (var line in array)
//         {
//             string temp = line.Trim();
//             if (temp.IsNullOrEmpty()) continue;
//             if (temp.StartsWith("--")) continue;
//             if (temp.StartsWith("/*"))
//             {
//                 isMark= true;
//                 continue;
//             }
//             if (temp.StartsWith("*/"))
//             {
//                 isMark= false;
//                 continue;
//             }
//             if (isMark)
//             {
//                 _logger.LogInformation($"sql ignore mark block /* */: {temp}");
//                 continue;
//             }
//             sqlStr += (StringExtensions.ToParsing(ref temp,"--")+" ");//消除字尾有註解行的指令
//         }
//         return sqlStr;
//     }
//     #endregion

//     #region AppTempSql CRUD維護作業 use EntityFrameworkCore
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [Description("查詢資料")]
//     [Authorize]
//     [HttpGet("all/appTempSql")]
//     public async Task<IActionResult> GetAllAsync()
//     {
//         var list = await db.AppTempSql.OrderByDescending(x =>x.Id).ToListAsync();
//         return Ok(list);
//     }

//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [Description("新增資料")]
//     [Authorize]
//     [HttpPost]
//     public async Task<IActionResult> AddAsync([FromBody] AppTempSql model)
//     {
//         db.AppTempSql.Add(model);
//         await db.SaveChangesAsync();
//         return Ok(model);
//     }

//     [Description("修改資料")]
//     [Authorize]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [HttpPut("{id}")]
//     public async Task<IActionResult> UpdateAsync(int id, [FromBody] AppTempSql model)
//     {
//         var data = await this.db.AppTempSql.FindAsync(id);
//         if (data == null)
//         {
//             var _userData = User.GetCurrentApiUserData();
//             _logger.LogError($"{_userData.UserId} {_userData.UserName} updateAsync not found id {id}");
//             return NotFound();
//         }
//         else
//         {
//             data.SqlExpr = model.SqlExpr;
//             data.SqlDesc = model.SqlDesc;
//             db.AppTempSql.Update(data);
//             await db.SaveChangesAsync();
//             return Ok(data);
//         }
//     }

//     [Description("刪除資料")]
//     [Authorize]
//     //[TypeFilter(typeof(RbacAuthorizeFilter))]
//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteAsync(int id)
//     {
//         var data = await this.db.AppTempSql.FindAsync(id);
//         if (data == null)
//         {
//             var _userData = User.GetCurrentApiUserData();
//             _logger.LogError($"{_userData.UserId} {_userData.UserName} DeleteAsync not found id {id}");
//             return NotFound();
//         }
//         else
//         {
//             db.AppTempSql.Remove(data);
//             await db.SaveChangesAsync();
//             return Ok();
//         }
//     }
//     #endregion

//     #region AppTempExcel
//     [Description("新增AppTempExcel資料")]
//     [Authorize]
//     [HttpPost("AppTempExcel")]
//     public async Task<IActionResult> AddAppTempExcelAsync([FromBody] IEnumerable<dynamic> modelList)
//     {
//         var beginTime = System.DateTime.Now;
//         string temp = @"A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
//             ,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ
//             ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ
//             ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ
//             ,DA,DB,DC,DD,DE,DF,DG,DH,DI,DJ,DK,DL,DM,DN,DO,DP,DQ,DR,DS,DT,DU,DV,DW,DX,DY,DZ
//             ,EA,EB,EC,ED,EE,EF,EG,EH,EI,EJ,EK,EL,EM,EN,EO,EP,EQ,ER,ES,ET,EU,EV,EW,EX,EY,EZ
//             ,FA,FB,FC,FD,FE,FF,FG,FH,FI,FJ,FK,FL,FM,FN,FO,FP,FQ,FR,FS,FT,FU,FV,FW,FX,FY,FZ
//             ,GA,GB,GC,GD,GE,GF,GG,GH,GI,GJ,GK,GL,GM,GN,GO,GP,GQ,GR,GS,GT,GU,GV,GW,GX,GY,GZ
//             ,HA,HB,HC,HD,HE,HF,HG,HH,HI,HJ,HK,HL,HM,HN,HO,HP,HQ,HR,HS,HT,HU,HV,HW,HX,HY,HZ";
//         int maxCols= 234;
//         string[] colArray = temp.Split(",");
//         string delSql = "truncate table AppTempExcel";
//         await db.Database.ExecuteSqlRawAsync(delSql);
//         await db.SaveChangesAsync();
//         GlobalVar.CurrentCount = 0;
//         GlobalVar.TotalCount = modelList.Count();
//         foreach (var record in modelList)
//         {
//             GlobalVar.CurrentCount ++;
//             int j = 0;
//             int k = 0;
//             var appTempExcel = new AppTempExcel();
//             // PropertyInfo[] properties = record.GetType().GetProperties();
//             // Type myType = record.GetType();
//             // IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
//             foreach (var col in record)
//             {
//                 try
//                 {
//                     if (col.Value == null || string.IsNullOrWhiteSpace(col.Value.ToString())){
//                         //NoneDo
//                     } else {
//                         var colname = colArray[j].Trim();
//                         string value = col.Value.ToString().Trim();
//                         appTempExcel.SetValue(colname,value);
//                         k++;
//                     }
//                     j++;
//                     if (j >= maxCols) break;
//                 }
//                 catch (System.Exception)
//                 {
//                     j--;
//                     string message = $"col:{j} error value: {record}";
//                     _logger.LogError(message);
//                     k=0;
//                     break;
//                     //throw new Exception(message);
//                 }
//             }
//             if (k > 0){
//                 appTempExcel.Id = 0;
//                 await db.AppTempExcel.AddAsync(appTempExcel);
//                 await db.SaveChangesAsync();
//             }
//         }
//         //await db.SaveChangesAsync();
//         var list = await db.AppTempExcel.ToListAsync();
//         var endTime = System.DateTime.Now;
//         var times = endTime - beginTime;
//         _logger.LogError($"begin:{beginTime} end:{endTime} time seconds {times}");
//         return Ok(list);
//     }

//     [Description("查詢AppTempExcel資料")]
//     [Authorize]
//     [HttpGet("all/appTempExcel")]
//     public async Task<IActionResult> GetAppTempExcelAsync()
//     {
//         var list = await db.AppTempExcel.ToListAsync();
//         return Ok(list);
//     }
//     #endregion

// }