using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Helpers;

// 設定網路HTTP存取Service，給 Entity.DoWriteUser()，services.AddDbContext<AppDbContext> 用
// 1.設定 HttpContext
// startup.cs
// public void ConfigureServices(IServiceCollection services)
//   //for asp.net core 2.2
//   services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//   //for asp.net core 3.0
//   services.AddHttpContextAccessor();
// public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
//   //not need
//   //HttpContextHelpers.Configure(services.GetRequiredService<IHttpContextAccessor>());

public static class HttpContextHelper
{
    // 不要用，使用DI注入會更安全
    // #region use case:1  不要用，使用DI注入會更安全
    // private static IHttpContextAccessor? _contextAccessor;
    // private static Microsoft.AspNetCore.Http.HttpContext? _currentHttpContext => _contextAccessor?.HttpContext;
    // public static void SetHttpContext(IHttpContextAccessor contextAccessor)
    // {
    //     _contextAccessor = contextAccessor;
    // }

    // public static Guid GetCurrentUserId()
    // {
    //     var userId = _currentHttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    //     if (userId == null) {
    //         return Guid.Empty;
    //     }
    //     return Guid.Parse(userId);
    // }

    // public static bool CheckIsCurrentUserId(Guid userId)
    // {
    //     var claimUserId = HttpContextHelper.GetCurrentUserId();
    //     if (claimUserId == userId)
    //         return true;
    //     return false;
    // }

    // public static ApiUserData? GetCurrentApiUserData()
    // {
    //     var claims = _currentHttpContext?.User.Claims.ToList();
    //     if (claims?.Count == 0) {
    //         return null;
    //     }
    //     var apiUserData = new ApiUserData();
    //     var value = _currentHttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    //     apiUserData.UserId = Guid.Parse(value??String.Empty);
    //     apiUserData.UserName = _currentHttpContext?.User.FindFirstValue(ClaimTypes.Name)??"NoUserName";
    //     apiUserData.UserRole = _currentHttpContext?.User.FindFirstValue(ClaimTypes.Role)??"NoUserRole";
    //     apiUserData.UserData = _currentHttpContext?.User.FindFirstValue(ClaimTypes.UserData)??"NoUserData";
    //     apiUserData.PhotoUrl = _currentHttpContext?.User.FindFirstValue(ClaimTypes.Actor)??"NoPhotoUrl";
    //     value = _currentHttpContext?.User.FindFirstValue(ClaimTypes.Sid)??"0";
    //     apiUserData.UserType = int.Parse(value);
    //     value = _currentHttpContext?.User.FindFirstValue(ClaimTypes.Spn)??"0";
    //     apiUserData.UserCode = int.Parse(value);
    //     return apiUserData;
    // }
    // #endregion

    #region use case:2
    public static bool CheckIsCurrentUserId(this ClaimsPrincipal claimsPrincipal, Guid userId)
    {
        var tempUserId = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Jti);
        _ = Guid.TryParse(tempUserId, out Guid claimUserId);
        if (claimUserId == userId){
            return true;
        }
        return false;
    }

    public static Guid GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var claimUserId = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Jti);
        if (claimUserId == null) {
            return Guid.Empty;
        }
        return Guid.Parse(claimUserId);
    }

    public static string GetCurrentUserIdName(this ClaimsPrincipal claimsPrincipal)
    {
        //var claimUserId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)??"UnkownUserId";
        var claimUserId = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Jti)??"UnkownUserId";
        var claimUserName = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Name)??"UnkownUserName";
        var userIdName = $"{claimUserId}_{claimUserName}";
        return userIdName;
    }

    public static ApiUserData GetCurrentApiUserData(this ClaimsPrincipal claimsPrincipal)
    {
        // claimsPrincipal.Claims.ToList().ForEach(c => {
        //     Console.WriteLine($"claim type: {c.Type}, claim value: {c.Value}");
        // });
        var apiUserData = new ApiUserData();
        var value = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Jti);
        if (Guid.TryParse(value, out Guid userId)) {
            apiUserData.UserId = userId;
        } else {
            apiUserData.UserId = Guid.Empty;
        }
        apiUserData.UserName = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Name);
        apiUserData.TeamId = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Sid);
        apiUserData.UserRole = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Iss);
        apiUserData.UserData = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Aud);
        apiUserData.PhotoUrl = claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Picture);
        apiUserData.UserType = int.Parse(claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Profile)??"0");
        apiUserData.UserCode = int.Parse(claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.ZoneInfo)??"0");
        return apiUserData;

        // var value = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        // if (Guid.TryParse(value, out var userId)) {
        //     apiUserData.UserId = userId;
        // } else {
        //     apiUserData.UserId = Guid.Parse(value!);
        // }
        // apiUserData.TeamId = claimsPrincipal.FindFirstValue(ClaimTypes.PrimarySid)??"";
        // apiUserData.UserName = claimsPrincipal.FindFirstValue(ClaimTypes.Name)??"";
        // apiUserData.UserRole = claimsPrincipal.FindFirstValue(ClaimTypes.Role)??"";
        // apiUserData.UserData = claimsPrincipal.FindFirstValue(ClaimTypes.UserData)??"";
        // apiUserData.PhotoUrl = claimsPrincipal.FindFirstValue(ClaimTypes.Actor)??"";
        // value = claimsPrincipal.FindFirstValue(ClaimTypes.Sid)??"0";
        // apiUserData.UserType = int.Parse(value);
        // value = claimsPrincipal.FindFirstValue(ClaimTypes.Spn)??"0";
        // apiUserData.UserCode = int.Parse(value);
        // return apiUserData;
    }
    #endregion
}


