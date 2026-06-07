using System;

namespace Api.Services; 

public class Au1UserEditDto : BaseDto
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? MobileTel { get; set; }
    public DateTime Birthday { get; set; }
    public string? UserPhoto { get; set; }
    public string? UserData { get; set; }
    public bool IsOnOff { get; set; }
    public int UserType { get; set; }
    public int AdminType { get; set; }
    public string? Notes { get; set; }
    // public DateTime? LoginDate { get; set; }
    // public int? LoginErrors { get; set; }
    // public DateTime? LastLoginDate { get; set; }       
}
