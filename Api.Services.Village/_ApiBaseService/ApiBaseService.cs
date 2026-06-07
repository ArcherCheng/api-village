using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;

namespace Api.Services;

public class ApiBaseService<T>(ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
    : BaseService<T>(logger, httpContextAccessor), IApiBaseService where T : IBaseService
{
    public async Task<bool> CheckTeamExistsAsync(string teamId)
    {
        using AppDbContext db = NewDb();
        var result = await db.Au1Team.AsNoTracking().FirstOrDefaultAsync(x => x.TeamId == teamId);
        if (result == null)
            return false;
        return true;
    }
    #region Log Requests and Db tables
    public async Task<PageListResult<AppUserRequest>> GetAppUserRequestPageListAsync(BaseParas baseParas)
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<AppUserRequest>(baseParas.WhereConditionList);
        var result = db.AppUserRequest.Where(filterPredicate).OrderByDescending(x => x.Id).AsQueryable();
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy))
        {
            result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy))
        {
            result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
        }
        return await PageListResult<AppUserRequest>.CreateAsync(result, baseParas.Pagination!);
    }


    public void AddAppUserRequest(AppUserRequest model)
    {
        using var db = NewDb();
        db.AppUserRequest.Add(model);
        db.SaveChanges();
    }

    public async Task AddAppUserRequestAsync(AppUserRequest model)
    {
        using var db = NewDb();
        db.AppUserRequest.Add(model);
        await db.SaveChangesAsync();
    }

    public async Task<PageListResult<AppDataLog>> GetAppDataLogPageListAsync(BaseParas baseParas)
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<AppDataLog>(baseParas.WhereConditionList);
        var result = db.AppDataLog.Where(filterPredicate).OrderByDescending(x => x.Id).AsQueryable();
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy))
        {
            result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy))
        {
            result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
        }
        return await PageListResult<AppDataLog>.CreateAsync(result, baseParas.Pagination!);
    }

    public async Task<PageListResult<AppUserLogin>> GetAppUserLoginPageListAsync(BaseParas baseParas)
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<AppUserLogin>(baseParas.WhereConditionList);
        var result = db.AppUserLogin.Where(filterPredicate).OrderByDescending(x => x.Id).AsQueryable();
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy))
        {
            result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy))
        {
            result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
        }
        return await PageListResult<AppUserLogin>.CreateAsync(result, baseParas.Pagination!);
    }



    // public async Task<PageListResult<AppUserLogMessage>> GetAppLogMessagePageListAsync(BaseParas baseParas)
    // {
    //     using var db = NewDb();
    //     var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<AppLogMessage>(baseParas.WhereConditionList);
    //     var result = db.AppLogMessage.Where(filterPredicate).OrderByDescending(x => x.Id).AsQueryable();
    //     if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy))
    //     {
    //         result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
    //     }
    //     if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy))
    //     {
    //         result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
    //     }
    //     return await PageListResult<AppLogMessage>.CreateAsync(result, baseParas.Pagination!);
    // }
    #endregion


}


    // public string GetAndCheckSumCompanyName()
    // {
    //     try
    //     {
    //         using (var db = NewDb())
    //         {
    //             //公司編號抬頭 AA2X0020
    //             int comId=0;
    //             string companyName = "新軟資訊企業社";
    //             string comIdOrName = db.AppKeyRules.FirstOrDefault(x => x.RuleId=="AA2X0020").RuleValue;
    //             if (int.TryParse(comIdOrName, out comId)) {
    //                 var viewAu1Company = db.ViewAu1Companies.FirstOrDefault(x => x.ComId == comIdOrName);
    //                 if (viewAu1Company != null) {
    //                     companyName = viewAu1Company.ComName.Trim();
    //                     //throw new Exception($"公司代號未建檔{comId}");
    //                 }
    //             } else {
    //                 companyName = comIdOrName;
    //             }
    //             //系統檢查碼 AA2X0026
    //             int checksumValue = this.GetCheckSumValue(companyName);
    //             var keySystem = db.AppKeyRules.FirstOrDefault(x => x.RuleId == "AA2X0026");
    //             int checkValue = int.Parse(keySystem.RuleValue);
    //             if (checksumValue != checkValue)
    //             {
    //                 companyName += "(盜版用戶，請洽開發者鄭先生0970922888購買正版)";
    //             }
    //             return companyName;
    //         }
    //     }
    //     catch (System.Exception)
    //     {
    //         return "新軟資訊企業社";
    //         //throw;
    //     }
    // }

    // public async Task<bool> CheckAuthorizeAsync(ApiJwtUserData userData, string controllerName, string actionName)
    // {
    //     if (controllerName == "Auth" && actionName == "CheckAuthorize") {
    //         return true;
    //     }

    //     string adminName = Api.Helpers.AppSettingsHelper.Configuration.GetSection("AppSettings:AdminName").Value ;
    //     if (userData.UserId == adminName) {
    //         _logger.LogInformation($"Au1Users not found {userData.UserId} at {controllerName}.{actionName}");
    //         return true;
    //     }

    //     var user = await GetUserByIdAsync(userData.UserId);
    //     if (user == null){
    //         var viewUser = db.ViewAu1Users.FirstOrDefault(x => x.UserId == userData.UserId);
    //         if (viewUser == null) {
    //             _logger.LogWarning($"Au1Users not found {userData.UserId} at {controllerName}.{actionName}");
    //             return false;
    //         }
    //         user = AgileObjects.AgileMapper.Mapper.Map(viewUser).ToANew<Au1User>();
    //     }

    //     if (user.IsOnOff != true) {
    //         _logger.LogWarning($"Au1Users user IsOff {userData.UserId} at {controllerName}.{actionName}");
    //         return false;
    //     } else if (user.AdminType == 1) {
    //         return true;
    //     } else if (user.AdminType == 2) {
    //         if (actionName=="Add" || actionName == "Update" || actionName=="Delete") {
    //             return false;
    //         } else {
    //             return true;
    //         }
    //     }

    //     var Action = await db.Au1Actions.FirstOrDefaultAsync(x => x.CtrllerId.ToLower() == controllerName && x.ActionId.ToLower() == actionName);
    //     if (Action == null ){
    //         _logger.LogWarning($"Au1Actions not found {controllerName}.{actionName}");
    //         return false;
    //     } else if (Action.IsRbacAuthorize == false) {
    //         return true;  //改成true,因為不設定權限的action 不會有資料在 RoleAction 檔中。
    //     }

    //     var roles = await db.Au2RoleUsers
    //         .Include(x => x.Role)
    //         .Where(x => x.UserId == userData.UserId && x.IsOnOff && x.Role.IsOnOff)
    //         .Select(x => x.Role.RoleId)
    //         .ToListAsync();
    //     if (!userData.UserRoles.IsNullOrEmpty()) {
    //         var list = userData.UserRoles.Split(",");
    //         roles.AddRange(list);
    //     }

    //     if (roles.Count() == 0){
    //         _logger.LogWarning($"Au2RoleUsers not found user {userData.UserId}");
    //         return false;
    //     }

    //     var isPermitted = await db.Au2RoleActions.AnyAsync(x =>
    //         x.CtrlActnId == Action.CtrlActnId
    //         && x.IsOnOff
    //         && roles.Contains(x.RoleId)
    //         //&& roles.Any(y => y == x.RoleId)
    //     );

    //     // if (!isPermitted) {
    //     //     _logger.LogWarning($"Au2RoleActions not authorize {userId} {Action.ActionId}");
    //     // }

    //     return isPermitted;
    // }







