using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Services;

public class ChangePasswordDto
{
    public required string OldPassword { get; set; }

    public required string NewPassword { get; set; }

    public required string TeamId { get; set; }

    public required string MacAddress { get; set; }

    public required string IpAddress { get; set; }
}