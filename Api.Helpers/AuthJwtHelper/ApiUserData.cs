using System;

namespace Api.Helpers;

#nullable disable
public class ApiUserData
{
    //ClaimTypes.NameIdentifier
    public Guid UserId { get; set; }

    //ClaimTypes.Sub
    public string TeamId { get; set; }

    //ClaimTypes.Name
    public string UserName { get; set; }

    //ClaimTypes.Role
    public string UserRole { get; set; }  //admin,user

    //ClaimTypes.GroupSid
    public string UserData { get; set; }  //1=depId

    //ClaimTypes.sid
    public int UserType { get; set; }  //0=front user, 1=admin,user

    //ClaimTypes.spn
    public int UserCode { get; set; }  //1=resetPassword, 2=verifyCode, 3=expiredPa

    //ClaimTypes.Actor
    public string PhotoUrl { get; set; }

    public string JwtToken { get; set; }

    // public bool IsAdmin { get; set; }=false;
    // public string Issuer { get; set; }="";
    // public string Audience { get; set; }="";
    //public string Email { get; set; }
    //public string MobileTel { get; set; }
    public string GetUserIdName() => $"{UserId}__{UserName}";
}
