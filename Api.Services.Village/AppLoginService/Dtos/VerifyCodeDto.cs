using System.ComponentModel.DataAnnotations;

public class VerifyCodeDto
{
    public Guid UserId { get; set; }
    public required string MacAddress { get; set; }
    public required string IpAddress { get; set; }
    public required string VerifyCode { get; set; }
}
 