using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Services;

public class RegisterDto
{
    public Guid? UserId { get; set; }

    [Required(ErrorMessage = "姓名必填")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "生日必填")]
    public DateOnly Birthday { get; set; }

    [EmailAddress(ErrorMessage = "Email格式不正確")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "手機號碼必填")]
    public required string MobileTel { get; set; }

    [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼最少要6個字,最多20個字")]
    public required string Password { get; set; }

    public string? PhotoUrl { get; set; }

    public required string TeamId { get; set; }

    public required string MacAddress { get; set; }

    public required string IpAddress { get; set; }

}
