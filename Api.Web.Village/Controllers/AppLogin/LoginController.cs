using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Principal;
using System.ComponentModel;
using Api.Helpers;
using Api.Services;
using Api.Models;

namespace Api.Controllers;

[Description("登入及註冊系統")]
[Route("api/[controller]")]
[ApiController]
public class LoginController(ILogger<LoginController> logger,ILoginService service) : ControllerBase
{
    private readonly ILogger<LoginController> _logger = logger;
    private readonly ILoginService _service = service;
    [Description("檢查團隊代號是否存在")]
    [AllowAnonymous]
    [HttpGet("team/{teamId}/CheckTeamExists")]
    public async Task<bool> CheckTeamExists(string teamId)
    {
        var result = await _service.CheckTeamExistsAsync(teamId);
        return result;
    }

    [Description("用戶註冊")]
    [AllowAnonymous]
    [HttpPost("team/{teamId}/register")]
    public async Task<IActionResult> Register(string teamId, RegisterDto model)
    {
        if (!ModelState.IsValid) {
            Dictionary<string, string[]> ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(RegisterDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }

        if (await _service.CheckExistsAsync(teamId, model)) {
            _logger.LogWarning($"{model.Email},{model.MobileTel} 此註冊資料已經是會員了");
            return BadRequest("此註冊資料已經是會員了");
        }

        ApiUserData apiUserData = await _service.RegisterAsync(teamId, model, model.Password);
        //send email to enable
        string jwtToken = Api.Helpers.AuthJwtHelper.GenerateApiJwtToken(apiUserData);
        return Ok(new {jwtToken});
    }

    [Description("用戶登入")]
    [AllowAnonymous]
    [HttpPost("team/{teamId}/login")]
    public async Task<IActionResult> Login(string teamId, LoginDto model)
    {
        if (!ModelState.IsValid) {
            var ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(RegisterDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }
        if (string.IsNullOrEmpty(model.IpAddress) || model.IpAddress=="undefined") {
            model.IpAddress =  _service.GetClientIp();
        }
        // var windowsIdentity = WindowsIdentity.GetCurrent();
        //檢查登入錯誤次數
        string? errMsg = await _service.CheckOverLoginErrorTimesAsync(teamId, model);
        if (!string.IsNullOrEmpty(errMsg)) {
            return BadRequest(errMsg);
        }
        //系統管理員優先登入
        string jwtToken ="";
        ApiUserData? apiUserData = await _service.LoginAsAdminAsync(teamId, model);
        if (apiUserData != null) {
            jwtToken = Api.Helpers.AuthJwtHelper.GenerateApiJwtToken(apiUserData);
            _logger.LogInformation($"{model.LoginName} Login 系統管理帳號登入成功");
            return Ok(new {jwtToken});
        }

        //檢查用戶登入
        apiUserData = await _service.LoginAsync(teamId, model);
        if (apiUserData == null) {
            _logger.LogInformation($"{model.LoginName} 用戶帳號或密碼錯誤，請重新輸入");
            return BadRequest("用戶帳號或密碼錯誤，請重新輸入");
        }
        jwtToken = Api.Helpers.AuthJwtHelper.GenerateApiJwtToken(apiUserData);
        return Ok(new {jwtToken});
    }

    [Authorize]
    [Description("寫入用戶驗證碼")]
    [HttpPost("team/{teamId}/verify-code/{userId}")]
    public async Task<IActionResult> WriteUserMacIpAddressVerifyCode(string teamId, Guid userId, [FromBody] VerifyCodeDto model)
    {
        if (!ModelState.IsValid) {
            var ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(RegisterDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }
        if (userId != User.GetCurrentUserId()) {
            return BadRequest("使用者代不符合目前用戶代號，請檢查");
        }
        string? errMsg = await _service.WriteUserMacIpAddressVerifyCodeAsync(teamId, model);
        if (!string.IsNullOrEmpty(errMsg)) {
            return BadRequest(errMsg);
        }
        ApiUserData apiUserData = await _service.GetApiUserDataAsync(teamId, model.UserId);
        string jwtToken = Api.Helpers.AuthJwtHelper.GenerateApiJwtToken(apiUserData);
        return Ok(new {jwtToken});
        //返回用戶資料及用戶憑證JWT Token
        //apiUserData.JwtToken = jwtToken;
        //return Ok(apiUserData);
    }

    [Description("檢查使用者密碼是否到期")]
    [Authorize]
    [HttpGet("team/{teamId}/check-password-expired/{userId}")]
    public async Task<IActionResult> CheckPasswordExpired(string teamId, Guid  userId)
    {
        // ApiUserData apiUserData = Api.Helpers.HttpContextHelper.GetCurrentApiUserData();
        ApiUserData apiUserData = User.GetCurrentApiUserData();
        if (userId != apiUserData.UserId) {
            return BadRequest("使用者代不符合目前用戶代號，請檢查");
        }
        var isExpired = await _service.CheckPasswordExpiredAsync(teamId, apiUserData.UserId);
        if (isExpired){
            return BadRequest("使用者密碼已經到期了，請儘快變更密碼，以免被鎖定無法登入了");
        }

        return Ok("");
    }

    [Description("用戶忘記密碼")]
    [AllowAnonymous]
    [HttpPost("team/{teamId}/forgetPassword")]
    public async Task<IActionResult> ForgetPassword(string teamId, [FromBody] ForgetPasswordDto model)
    {
        if (!ModelState.IsValid) {
            var ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(ForgetPasswordDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }
        string errMsg = await _service.ForgetPasswordAsync(teamId, model);
        if (!string.IsNullOrEmpty(errMsg)){
            return BadRequest("取回密碼失敗,"+errMsg);
        }
        return Ok("新密碼已寄出至您的信箱或手機,請至信箱或手機確認");
    }

    [Authorize]
    [Description("用戶變更密碼作業")]
    [HttpPut("team/{teamId}/changePassword/{userId}")]
    public async Task<IActionResult> ChangePassword( string teamId, Guid userId, [FromBody] ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            var ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(ChangePasswordDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }
        if (userId != User.GetCurrentUserId()) {
            return BadRequest("使用者代不符合目前用戶代號，請檢查");
        }
        bool isSuccess = await _service.ChangePasswordAsync(teamId, userId, model);
        if (!isSuccess)
        {
            _logger.LogWarning($"UserId={userId},用戶變更密碼錯誤");
            return BadRequest("變更密碼錯誤,資料比對不符合,請重新輸入");
        }
        return Ok( new {Message="變更密碼成功"} );
    }

    [Authorize]
    [Description("讀取注冊資料")]
    [HttpGet("team/{teamId}/userData/{userId}")]
    public async Task<IActionResult> GetUserData(string teamId, Guid userId)
    {
        if (userId != User.GetCurrentUserId()) {
            return BadRequest("使用者代不符合目前用戶代號，請檢查");
        }
        UserDataDto result = await this._service.GetUserDataAsync(teamId, userId);
        return Ok(result);
    }

    [Authorize]
    [Description("修改注冊資料")]
    [HttpPut("team/{teamId}/userData/{userId}")]
    public async Task<IActionResult> UpdateUserData(string teamId, Guid userId, [FromBody] UserDataDto model)
    {
        if (!ModelState.IsValid)
        {
            var ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError("{0} Add ModelState.IsValid Error {1}", typeof(UserDataDto), ModelStateErrors);
            return UnprocessableEntity(ModelStateErrors);
        }
        UserDataDto result = await this._service.UpdateUserDataAsync(teamId, userId, model);
        return Ok(result);
    }

    [Authorize]
    [Description("讀取登入資料")]
    [HttpGet("team/{teamId}/AppUserLogin/pageList/{userId}")]
    public async Task<IActionResult> GetAppLoginPageList(string teamId, Guid userId, [FromQuery] BaseParas baseParas)
    {
        if (userId != User.GetCurrentUserId()) {
            return BadRequest("使用者代不符合目前用戶代號，請檢查");
        }
        PageListResult<AppUserLogin> result = await this._service.GetAppLoginPageListAsync(teamId, userId, baseParas);
        return Ok(result);
    }

}