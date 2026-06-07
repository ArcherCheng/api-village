using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Services;

public class UserDataDto
{
    public string? UserId { get; set; }

    public string? TeamId { get; set; }

    public string? UserName { get; set; }

    public string? MobileTel { get; set; }

    public string? Email { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? PhotoPath { get; set; }

    public string? UserData { get; set; }

    public string? UserRole { get; set; }

    public DateTime? LoginDate { get; set; }

    public DateTime? LastDate { get; set; }

    public bool? IsNeedChangePassword { get; set; }

    public DateTime? ChangePasswordDate { get; set; }

}
