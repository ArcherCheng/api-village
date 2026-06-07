using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface IAuthBaseService: IApiBaseService
{
    // public string GenerateInsertSql(string tableName,string dbName);
    // int GetCheckSumValue(string companyName);
    // Task<string> GetAndCheckSumCompanyNameAsync();
    // Task AddAppLogRequestAsync(AppLogRequest model);
    // void AddAppLogRequest(AppLogRequest model);
    // Task<bool> CheckControllerActionAuthorizeAsync(Guid userId, string contollerId, string actionId);
    // Task<bool> CheckComponentAuthorizeAsync(Guid userId, string componentId);
    // //Task SetTitleToAuthDescriptionAsync(string controllerId, string actionId, string title);

    #region RouterInfo & ControllerInfo
    // // 2023.10.14 取消 Au1Ctrller 檔案
    // //Task ImportControllerByCtrlInfosAsync(IEnumerable<ControllerInfo> controllerInfos);
    // Task ImportActionByRouteInfosAsync(IEnumerable<RouteInfo> routeInfos);
    // Task ImportComponentBySpaSystemAsync(IList<SystemMenu> systemMenus);
    #endregion

}
