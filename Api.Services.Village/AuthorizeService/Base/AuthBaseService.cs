using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Models;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Reflection;
using System.CodeDom;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class AuthBaseService<T>(ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<T>(logger,httpContextAccessor), IAuthBaseService where T : IApiBaseService
{
    //protected readonly AppDbContext db = new AppDbContext();
    //protected PasswordRule _passwordRule;

    // public async Task<bool> CheckControllerActionAuthorizeAsync(Guid userId, string controllerId, string actionId)
    // {
    //     await Task.Delay(0);
    //     return true;
    //     // using var db = NewDb();
    //     // if (this._apiUserData.UserId != userId) {
    //     //     _logger.LogInformation($"Jwt UserId != {userId} at {controllerId}.{actionId}");
    //     //     return false;
    //     // }
    //     // if (controllerId == "Auth" && actionId == "CheckAuthorize") {
    //     //     return true;
    //     // }
    //     // string adminName = Api.Helpers.AppSettingsHelper.Configuration.GetSection("AppSettings:AdminName").Value;
    //     // if (userName == adminName || userId == "archer") {
    //     //     _logger.LogInformation($"Admin User {userId} at {controllerId}.{actionId}");
    //     //     return true;
    //     // }
    //     // var user = await db.Au1Users.FirstOrDefaultAsync(x => x.UserId == userId);
    //     // if (user == null) {
    //     //     var viewUser = db.ViewAu1Users.FirstOrDefault(x => x.UserId == userId);
    //     //     if (viewUser == null) {
    //     //         _logger.LogWarning($"Au1Users not found {userId} at {controllerId}.{actionId}");
    //     //         return false;
    //     //     }
    //     //     user = AgileObjects.AgileMapper.Mapper.Map(viewUser).ToANew<Au1User>();
    //     // }
    //     // if (user.IsOnOff != true) {
    //     //     _logger.LogWarning($"Au1Users user IsOff {userId} at {controllerId}.{actionId}");
    //     //     return false;
    //     // } else if (user.AdminType == 1) {
    //     //     if (actionId.Contains("Add") || actionId.Contains("Update") || actionId.Contains("Delete")) {
    //     //         return false;
    //     //     } else {
    //     //         return true;
    //     //     }
    //     // } else if (user.AdminType == 2) {
    //     //     return true;
    //     // }
    //     // var Action = await db.Au1Actions.FirstOrDefaultAsync(x => x.CtrllerId == controllerId && x.ActionId == actionId);
    //     // if (Action == null) {
    //     //     _logger.LogWarning($"Au1Actions not found {controllerId}.{actionId}");
    //     //     return false;
    //     // } else if (Action.IsRbacAuthorize == false) {
    //     //     return true;  //改成true,因為不設定權限的action 不會有資料在 RoleAction 檔中。
    //     // }
    //     // var roles = await db.Au2RoleUsers
    //     //     .Include(x => x.Role)
    //     //     .Where(x => x.UserId == userId && x.IsOnOff && x.Role.IsOnOff)
    //     //     .Select(x => x.Role.RoleId)
    //     //     .ToListAsync();
    //     // if (!roles.Any()) {
    //     //     roles.Add("Users");
    //     // }
    //     // var isPermitted = await db.Au2RoleActions.AnyAsync(x =>
    //     //     x.CtrlActnId == Action.CtrlActnId
    //     //     && x.IsOnOff
    //     //     //&& roles.Contains(x.RoleId)
    //     //     && roles.Any(y => y == x.RoleId)
    //     // );
    //     // // if (!isPermitted) {
    //     // //     _logger.LogWarning($"Au2RoleActions not authorize {userId} {Action.ActionId}");
    //     // // }
    //     // return isPermitted;
    // }


    // public async Task<bool> CheckComponentAuthorizeAsync(Guid userId, string componentId)
    // {
    //     await Task.Delay(0);
    //     return true;
    //     // using var db = NewDb();
    //     // if (this._userData.UserId != userId) {
    //     //     _logger.LogWarning($"Jwt UserId != {userId} at {componentId}");
    //     //     return false;
    //     // }

    //     // string adminName = Api.Helpers.AppSettingsHelper.Configuration.GetSection("AppSettings:AdminName").Value;
    //     // if (userId == adminName || userId == "archer") {
    //     //     _logger.LogWarning($"Admin User {userId} at {componentId}");
    //     //     return true;
    //     // }

    //     // var user = await db.Au1Users.FirstOrDefaultAsync(x => x.UserId == userId);
    //     // if (user == null) {
    //     //     var viewUser = db.ViewAu1Users.FirstOrDefault(x => x.UserId == userId);
    //     //     if (viewUser == null) {
    //     //         _logger.LogWarning($"Au1Users not found {userId} at {componentId}");
    //     //         return false;
    //     //     }
    //     //     user = AgileObjects.AgileMapper.Mapper.Map(viewUser).ToANew<Au1User>();
    //     // }
    //     // if (user.IsOnOff != true) {
    //     //     _logger.LogWarning($"Au1Users user IsOff {userId} at {componentId}");
    //     //     return false;
    //     // } else if (user.AdminType >= 1) {
    //     //     return true;
    //     // }

    //     // var roles = await db.Au2RoleUsers
    //     //     .Include(x => x.Role)
    //     //     .Where(x => x.UserId == userId && x.IsOnOff && x.Role.IsOnOff)
    //     //     .Select(x => x.Role.RoleId)
    //     //     .ToListAsync();
    //     // if (!roles.Any()) {
    //     //     roles.Add("Users");
    //     // }

    //     // var isPermitted = await db.Au2RoleComponents.AnyAsync(x =>
    //     //     x.ComponentId == componentId
    //     //     && x.IsOnOff
    //     //     //&& roles.Contains(x.RoleId)
    //     //     && roles.Any(y => y == x.RoleId)
    //     // );
    //     // if (!isPermitted) {
    //     //     _logger.LogWarning($"Au2RoleActions not authorize {userId} {Action.ActionId}");
    //     // }
    //     // return isPermitted;
    // }


    #region get company name abd check sum
    // public int GetCheckSumValue(string companyName)
    // {
    //     byte[] bytes = System.Text.Encoding.Default.GetBytes(companyName);
    //     int checksumValue = 0;
    //     int i=1;
    //     foreach (var item in bytes)
    //     {
    //         if (i==1) {
    //             checksumValue += item * 17;
    //         } else if (i==2){
    //             checksumValue += item * 4;
    //         } else if (i==3){
    //             checksumValue += item * 13;
    //         } else {
    //             checksumValue += item * i;
    //         }
    //         i++;
    //     }
    //     return checksumValue;
    // }

    // public string GenerateInsertSql(string tableName,string dbName)
    // {
    //     List<string> nameList = new List<string>();
    //     var instance = GetInstance("Api.Models."+tableName);
    //     Type type = instance.GetType();
    //     PropertyInfo[] props = type.GetProperties();
    //     foreach (var prp in props)
    //     {
    //         if (prp.PropertyType.IsSealed){
    //             nameList.Add(prp.Name);
    //         }
    //     }
    //     var listString = string.Join(",",nameList);
    //     var result = $"Insert Into {tableName} ({listString}) select {listString} From {dbName}.dbo.{tableName}";
    //     return result;
    // }

    // public static object GetInstance(string strFullyQualifiedName)
    // {
    //     Type type = Type.GetType(strFullyQualifiedName)!;
    //     if (type != null){
    //         return Activator.CreateInstance(type)!;
    //     }

    //     foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
    //     {
    //         type = asm.GetType(strFullyQualifiedName)!;
    //         if (type != null){
    //             return Activator.CreateInstance(type)!;
    //         }
    //     }
    //     return null!;
    // }

    // public async Task<string> GetAndCheckSumCompanyNameAsync()
    // {
    //     using var db = NewDb();
    //     try
    //     {
    //         string comName = "新軟資訊企業社";
    //         //公司編號抬頭 AA2X0020
    //         var result = await db.Ak1KeyRule.FirstOrDefaultAsync(x => x.RuleId == "AA2X0020");
    //         // if (result == null) {
    //         //     var company = await db.ViewAu1Companies.OrderBy(x => x.ComId).FirstOrDefaultAsync();
    //         //     comName = company.ComName;
    //         // } else {
    //         //     var company = db.ViewAu1Companies.FirstOrDefault(x => x.ComId == result.RuleValue);
    //         //     if (company == null) {
    //         //         comName = result.RuleValue;
    //         //     } else {
    //         //         comName = company.ComName;
    //         //     }
    //         // }

    //         //系統檢查碼 AA2X0026
    //         int checksumValue = this.GetCheckSumValue(comName);
    //         var keySystem = db.Ak1KeyRule.FirstOrDefault(x => x.RuleId == "AA2X0026");
    //         int checkValue = int.Parse(keySystem?.RuleValue??"0");
    //         if (comName.Contains("新軟"))
    //         {
    //             return comName;
    //         }
    //         if (checksumValue != checkValue)
    //         {
    //             comName += "(盜版用戶，請洽開發者鄭先生0970922888購買正版)";
    //         }
    //         return comName;
    //     }
    //     catch (System.Exception)
    //     {
    //         return "新軟資訊企業社@版權所有";
    //         //throw;
    //     }
    // }

    #endregion

    #region AddAppLogRequest
    // public async Task AddAppLogRequestAsync(AppLogRequest model)
    // {
    //     using var db = NewDb();
    //     // db.AppLogRequest.Add(model);
    //     await db.SaveChangesAsync();
    // }

    // public void AddAppLogRequest(AppLogRequest model)
    // {
    //     using var db = NewDb();
    //     // db.AppLogRequest.Add(model);
    //     db.SaveChanges();
    // }
    #endregion

    public async Task ImportComponentBySpaSystemAsync(IList<SystemMenu> systemMenus)
    {
        await Task.Delay(0);
        // var resourcesDir = Api.Helpers.AppSettingsHelper.ResourcesFolder();
        // var i18nFileName = Path.Combine(resourcesDir,"i18n","tw.json.txt");
        // JsonNode node;
        // using (var fs = File.OpenRead(i18nFileName)) {
        //     node = JsonNode.Parse(fs);
        // }
        //var addbutton = node["global"]["button.add"].GetValue<string>();

        // using var db = NewDb();
        // foreach (var group in systemMenus)
        // {
        //     int order = 1;
        //     var spaSystem = group.System;
        //     var spaSubMenu = group.Group;
        //     var pgms = group.TabPgms;
        //     foreach (var pgm in pgms)
        //     {
        //         var au1SpaComponent = await db.Au1Component.FirstOrDefaultAsync(x => x.ComponentId == pgm.Component);
        //         if (au1SpaComponent == null) {
        //             au1SpaComponent = new()
        //             {
        //                 ComponentId = pgm.Component,
        //                 SubGroup = spaSubMenu,
        //                 ComponentDesc = pgm.ComponentDesc,
        //                 SystemId = spaSystem,
        //                 SystemDesc = spaSystem,
        //                 SortOrder = order
        //             };
        //             db.Au1Component.Add(au1SpaComponent);
        //             await db.SaveChangesAsync();
        //         } else {
        //             if (au1SpaComponent.ComponentDesc.IsNullOrEmpty() || au1SpaComponent.ComponentDesc != pgm.ComponentDesc) {
        //                 au1SpaComponent.SortOrder = order++;
        //                 au1SpaComponent.SystemId = spaSystem;
        //                 au1SpaComponent.SubGroup = spaSubMenu;
        //                 au1SpaComponent.ComponentDesc = pgm.ComponentDesc;
        //                 au1SpaComponent.SystemDesc = pgm.System;
        //                 db.Au1Component.Update(au1SpaComponent);
        //                 await db.SaveChangesAsync();
        //             }
        //         }
        //     }
        // }
    }

    #region RouterInfo & controllerInfo
    // 2023.10.14 取消 Au1Ctrller 檔案
    // public async Task ImportControllerByCtrlInfosAsync(IEnumerable<ControllerInfo> controllerInfos)
    // {
    //     using var db = NewDb();
    //     foreach (var item in controllerInfos)
    //     {
    //         var controller = await db.Au1Ctrllers.FirstOrDefaultAsync(x => x.CtrllerId == item.Controller);
    //         if (controller == null)
    //         {
    //             controller = new Au1Ctrller();
    //             controller.CtrllerId = item.Controller;
    //             controller.CtrllerDesc = item.Description;
    //             controller.SortOrder = 1000;
    //             controller.WriteCreateUser(_userData);
    //             db.Au1Ctrllers.Add(controller);
    //             await db.SaveChangesAsync();
    //         }
    //     }
    // }

    public async Task ImportActionByRouteInfosAsync(IEnumerable<RouteInfo> routeInfos)
    {
        await Task.Delay(0);
        // using var db = NewDb();
        // foreach (var item in routeInfos)
        // {
        //     Api.Helpers.GlobalVar.CurrentId = item.CtrllerActionId;
        //     if (item.ActionId == "GetKeyRuleValue")
        //     {
        //         Api.Helpers.GlobalVar.CurrentId = item.CtrllerActionId;
        //     }

        //     bool isNew = false;
        //     var Action = await db.Au1Action.FirstOrDefaultAsync(x => x.CtrlActnId == item.CtrllerActionId);
        //     if (Action == null)
        //     {
        //         Action = new Au1Action
        //         {
        //             CtrlActnId = item.CtrllerActionId  //$"{item.Controller}-{item.Action}";
        //         };
        //         isNew = true;
        //     }
        //     Action.CtrllerId = item.CtrllerId;
        //     Action.ActionId = item.ActionId;
        //     Action.ActionDesc = item.ActDescription;
        //     Action.CtrllerDesc = item.ConDescription;
        //     Action.SortOrder = item.SortOrder;
        //     Action.IsRbacAuthorize = item.IsRbacAuthorize;
        //     Action.HttpMethod = item.HttpMethod;
        //     Action.HttpRoute = item.HttpRoute;
        //     Action.CreateUser = System.DateTime.Now.ToString();
        //     //Action.DoWriteUser(1, _userData);
        //     if (isNew) {
        //         db.Au1Action.Add(Action);
        //     } else {
        //         db.Au1Action.Update(Action);
        //     }
        //     await db.SaveChangesAsync();
        // }
    }

    #endregion

    // 2023.10.14 取消 Au1Ctrller 檔案
    // public async Task SetTitleToAuthDescriptionAsync(string controllerId, string actionId, string title)
    // {
    //     await Task.Delay(0);
    //     using var db = NewDb();
    //     if (actionId == "getviewpagelist") {
    //         //update controller
    //         var controller = await db.Au1Ctrllers.FirstOrDefaultAsync(x => x.CtrllerId.ToLower() == controllerId);
    //         if (controller != null && controller.CtrllerDesc != title) {
    //             controller.CtrllerDesc = title;
    //             db.Au1Ctrllers.Update(controller);
    //             await db.SaveChangesAsync();
    //         }
    //     } else {
    //         //update action
    //         var action = await db.Au1Actions.FirstOrDefaultAsync(x => x.CtrllerId.ToLower() == controllerId && x.ActionId.ToLower() == actionId);
    //         if (action != null && action.ActionDesc != title) {
    //             action.ActionDesc = title;
    //             db.Au1Actions.Update(action);
    //             await db.SaveChangesAsync();
    //         }
    //     }
    // }

    // 2023.10.14 取消 Au1Ctrller 檔案
    // public async Task SetAu1CtrllerSpaSystemAsync(IList<SystemMenu> systemMenus)
    // {
    //     await Task.Delay(0);
    //     using var db = NewDb();
    //     foreach (var group in systemMenus)
    //     {
    //         var system = group.System;
    //         var pgms = group.TabPgms;
    //         string oldController = "";
    //         var au1Controller = db.Au1Ctrllers.Find(system);
    //         if (au1Controller == null) {
    //             au1Controller = new()
    //             {
    //                 CtrllerId=system,
    //                 CtrllerDesc=system
    //             };
    //             db.Au1Ctrllers.Add(au1Controller);
    //             await db.SaveChangesAsync();
    //         }
    //         // system include many controllers
    //         foreach (var pgm in pgms)
    //         {
    //             string controller = ComponentParsing(pgm.Component);
    //             if (!controller.IsNullOrEmpty() && controller != oldController) {
    //                 au1Controller = db.Au1Ctrllers.Find(controller);
    //                 if (au1Controller != null && au1Controller.SpaSystem != system) {
    //                     au1Controller.SpaSystem = system;
    //                     db.Au1Ctrllers.Update(au1Controller);
    //                     await db.SaveChangesAsync();
    //                     oldController = controller;
    //                 }
    //             }
    //         }
    //     }
    // }
}


