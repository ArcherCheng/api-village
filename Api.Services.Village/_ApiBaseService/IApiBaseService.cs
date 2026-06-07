using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface IApiBaseService: IBaseService
{
    Task<bool> CheckTeamExistsAsync(string teamId);

    #region Log User Web Requests and Database change logs

    Task<PageListResult<AppDataLog>> GetAppDataLogPageListAsync(BaseParas baseParas);
    void AddAppUserRequest(AppUserRequest model);
    Task AddAppUserRequestAsync(AppUserRequest model);
    Task<PageListResult<AppUserRequest>> GetAppUserRequestPageListAsync(BaseParas baseParas);
    Task<PageListResult<AppUserLogin>> GetAppUserLoginPageListAsync(BaseParas baseParas);
    //Task<PageListResult<AppUserMessage>> GetAppLogMessagePageListAsync(BaseParas baseParas);
    
    #endregion
}