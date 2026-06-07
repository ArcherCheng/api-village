using System;
using System.Threading.Tasks;
using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface ILoginService : IApiBaseService
{
    Task<bool> CheckExistsAsync(string teamId, RegisterDto model);
    Task<ApiUserData> RegisterAsync(string teamId, RegisterDto model, string password);
    Task<ApiUserData?> LoginAsync(string teamId, LoginDto model);
    Task<ApiUserData?> LoginAsAdminAsync(string teamId, LoginDto model);
    Task<string?> CheckOverLoginErrorTimesAsync(string teamId, LoginDto model);
    Task<string> ForgetPasswordAsync(string teamId, ForgetPasswordDto model);
    Task<bool> ChangePasswordAsync(string teamId, Guid userId, ChangePasswordDto model);
    Task<bool> CheckPasswordExpiredAsync(string teamId, Guid userId);
    Task<UserDataDto> GetUserDataAsync(string teamId, Guid userId);
    Task<UserDataDto> UpdateUserDataAsync(string teamId, Guid userId, UserDataDto model);
    Task<ApiUserData> GetApiUserDataAsync(string teamId, Guid userId);
    Task<PageListResult<AppUserLogin>> GetAppLoginPageListAsync(string teamId, Guid userId, BaseParas baseParas);

    //int GetCheckSumValue(string companyName);
    //string GetAndCheckSumCompanyName();

    Task<bool> IsValidUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress);
    Task<SendMessageResult> SendVerifyCodeByUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress);
    Task<string?> WriteUserMacIpAddressVerifyCodeAsync(string teamId, VerifyCodeDto verifyCodeDto);
    Task<int> ResetAllPasswordAsync(string teamId, string password, string userId1, string userId2 );

}