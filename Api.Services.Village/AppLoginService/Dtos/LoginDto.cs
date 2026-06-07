

using System.ComponentModel.DataAnnotations;

namespace Api.Services;

public class LoginDto
{
    public required string LoginName { get; set; }

    public required string Password { get; set; }

    public required string TeamId { get; set; }

    public required string MacAddress { get; set; }

    public required string IpAddress { get; set; }

    // HttpContext.Connection.RemoteIpAddress.ToString();
    // public string ClientIp { get; set; }
}