using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Services;

public class ForgetPasswordDto
{
    public required string UserName { get; set; }

    public DateOnly Birthday { get; set; }

    public required string MobileOrEmail { get; set; }

    public required string TeamId { get; set; }

    public required string MacAddress { get; set; }

    public required string IpAddress { get; set; }

}