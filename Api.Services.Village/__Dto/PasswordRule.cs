// using System.ComponentModel;
// using System.Linq;
// using System.Threading.Tasks;
// using Api.Services;
// using Api.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;


namespace Api.Services;

public class PasswordRule
{
    /// <summary>
    /// AU1X0010=是否有強制密碼變更作業(6)
    /// </summary>
    public bool IsForceChangePassword { get; set; } = false;

    
    /// <summary>
    /// AU1X0011=強制幾天須密碼變更一次(6)
    /// </summary>
    public int ForceChangeDays { get; set; } = 30;

    
    /// <summary>
    /// AU1X0012=密碼變更最少字元(6)
    /// </summary>
    public int MinLenth { get; set; } = 6;

    /// <summary>
    /// AU1X0013=密碼是否強制大寫字元(1=是)
    /// </summary>
    public bool IsUpperWord { get; set; } = false;

    /// <summary>
    /// AU1X0014=密碼是否強制小寫字元(1=是)
    /// </summary>
    public bool IsLowerWord { get; set; } = false;

    /// <summary>
    /// AU1X0015=密碼是否強制數字字元(1=是) 
    /// </summary>
    public bool IsNumberWord { get; set; } = false;

    /// <summary>
    /// AU1X0016=密碼是否強制特殊字元(1=是)
    /// </summary>
    public bool IsSpecialWord { get; set; } = false;

    /// <summary>
    /// AU1X0017=密碼是否強制英數字字元(1=是) 
    /// </summary>
    public bool IsWordAndNumber { get; set; } = false;        
    
    /// <summary>
    /// AU1X0018=密碼是否允許重使用(1=是) 
    /// </summary>
    public bool IsAllowRepeat { get; set; } = true;

    /// <summary>
    /// AU1X0019=是否啟用郵件通知用戶登入訊息(1=是/0=否) 
    /// </summary>
    public bool IsEmailToLoginUser { get; set; } = false;
    
    /// <summary>
    /// AU1X0020=是否啟用郵件通知用戶登入訊息(1=是/0=否) 
    /// </summary>
    public bool IsVerifyCode { get; set; } = false;

    /// <summary>
    /// password regular expression
    /// </summary>
    public string RegularEx { get; set; } = "";
}