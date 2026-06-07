using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Helpers;

public class AuthJwtHelper
{
    public static string GenerateApiJwtToken(ApiUserData apiUserData)
    {
        // 建立一組對稱式加密的金鑰,主要用於 JWT 簽章之用
        SigningCredentials jwtTokenKeySigningCredentials = GetJwtTokenKeySigningCredentials();
        List<Claim> claims = CreateClaims(apiUserData);
        SecurityTokenDescriptor tokenDescripter = GenerateTokenDescripter(jwtTokenKeySigningCredentials, claims, apiUserData);

        // 產出所需要的 JWT securityToken 物件,並取得序列化後的 Token 結果(字串格式)
        JwtSecurityTokenHandler jwtTokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        SecurityToken securityToken = jwtTokenHandler.CreateToken(tokenDescripter);
        string jwtTokenString = jwtTokenHandler.WriteToken(securityToken);
        return jwtTokenString;
    }


    private static SigningCredentials GetJwtTokenKeySigningCredentials()
    {
        // var tokenSaltKey1 = AppSettingsHelpers.Configuration.GetSection("JwtSettings").GetSection("TokenKey").Value;
        // var tokenSaltKey2 = AppSettingsHelpers.Configuration.GetSection("JwtSettings:TokenKey").Value;
        // var tokenSaltKey3 = AppSettingsHelpers.Configuration.GetSection("JwtSettings")["TokenKey"];
        //var tokenSaltKey = Api.Helpers.AppSettingsHelper.Configuration["JwtSettings:TokenKey"];
        IConfigurationSection jwtSettings = Api.Helpers.AppSettingsHelper.Configuration.GetSection("JwtSettings");
        string? tokenSaltKey = jwtSettings.GetSection("TokenKey")?.Value;
        if (string.IsNullOrEmpty(tokenSaltKey))
        {
            tokenSaltKey = "justdo.tw@webapi for JwtBearer Security Salt Key use JwtSettings:TokenKey and SymmetricSecurityKey";
        }

        // 建立一組對稱式加密的金鑰,主要用於 JWT 簽章之用
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenSaltKey!));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        return credentials;
    }

    private static List<Claim> CreateClaims(ApiUserData user)
    {
        // 設定要加入到 JWT Token 中的聲明資訊(Claims)
        // JWT 註冊聲明參數 (建議但不強制使用)
        // sub (Subject) - jwt主體 (使用者ID)
        // jti (JWT ID) - jwt的唯一身份標識，主要用來作為一次性token,從而迴避重放攻擊
        // iss (Issuer) - jwt發行者
        // aud (Audience) - jwt接收者 (意味著這個令牌是為了哪一個App而生成的)
        // exp (Expiration Time) - jwt的過期時間，這個過期時間必須要大於簽發時間
        // nbf (Not Before) - jwt定義在什麼時間之前，該簽證都是不可用的
        // iat (Issued At) - jwt的簽發時間

        var Claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, user.UserId.ToString() ?? ""),
            new(JwtRegisteredClaimNames.Name, user.UserName ?? ""),
            new(JwtRegisteredClaimNames.Sid, user.TeamId ?? ""),
            new(JwtRegisteredClaimNames.Iss, user.UserRole ?? "Users"),
            new(JwtRegisteredClaimNames.Aud, user.UserData ?? ""),
            new(JwtRegisteredClaimNames.Picture, user.PhotoUrl ?? ""),
            new(JwtRegisteredClaimNames.Profile, user.UserType.ToString() ?? "0"),
            new(JwtRegisteredClaimNames.ZoneInfo, user.UserCode.ToString() ?? "0"),
            // //以下這些也可以用，先保留
            // new(JwtRegisteredClaimNames.Address, user.UserRole ?? "Users"),
            // new(JwtRegisteredClaimNames.ZoneInfo, user.UserData ?? ""),
            // new(JwtRegisteredClaimNames.Prn, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Locale, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.PreferredUsername, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Profile, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Nickname, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Typ, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Alg, user.UserName ?? ""),

            //以下這些不要用，claim type 太長了，http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier
            // new(JwtRegisteredClaimNames.Sub, user.UserData ?? ""),  //subject
            // new(JwtRegisteredClaimNames.NameId, user.UserId.ToString() ?? ""),
            // new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.GivenName, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.FamilyName, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Gender, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Email, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Birthdate, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Actort, user.PhotoUrl ?? ""),
            // new(JwtRegisteredClaimNames.Acr, user.UserType.ToString()),
            // new(JwtRegisteredClaimNames.Website, user.UserName ?? ""),
            // new(JwtRegisteredClaimNames.Amr, user.UserCode.ToString()),

            //以下是MS定義的ClaimTypes，不是JWT註冊聲明參數，但也可以用，先保留
            // new(ClaimTypes.PrimarySid, user.TeamId ?? "NoTeamId"),
            // new(ClaimTypes.NameIdentifier, user.UserId.ToString() ?? ""),
            // new(ClaimTypes.Name, user.UserName ?? ""),
            // new(ClaimTypes.Actor, user.PhotoUrl ?? ""),
            // new(ClaimTypes.Role, user.UserRole ?? "NoRole"),
            // new(ClaimTypes.UserData, user.UserData ?? ""),
            // new(ClaimTypes.Sid, user.UserType.ToString()),
            // new(ClaimTypes.Spn, user.UserCode.ToString())
            // new(ClaimTypes.PrimarySid, user.IsOperator.ToString()),
            // new(ClaimTypes.Webpage, user.CompanyName ?? "NoCompany"),
            // new(ClaimTypes.Surname, user.DbName ?? "NoDatabase"),
        };
        return Claims;
    }

    private static SecurityTokenDescriptor GenerateTokenDescripter(SigningCredentials signingCredentials, List<Claim> claims, ApiUserData user)
    {
        IConfigurationSection jwtSettings = Api.Helpers.AppSettingsHelper.Configuration.GetSection("JwtSettings");
        int tokenDays=365;
        if (user.UserRole == "Admin") {
            string adminDays = jwtSettings.GetSection("AdminDays").Value??"365";
            tokenDays = int.TryParse(adminDays, out int result) ? result : 365;
        } else {
            string userDays = jwtSettings.GetSection("UserDays").Value??"365";
            tokenDays = int.TryParse(userDays, out int result) ? result : 365;
        }

        SecurityTokenDescriptor tokenDescripter = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = System.DateTime.Now.AddDays(tokenDays),
            SigningCredentials = signingCredentials
        };
        return tokenDescripter;
    }
}
