using System;
using System.Net;
using System.Net.Mail;

namespace Api.Helpers;

public class EmailModel
{
    public string? FromEmailAddress { get; set; }
    public string? HostName { get; set; }
    public int HostPort { get; set; } = 25;
    public string? LoginName { get; set; }
    public string? LoginPassword { get; set; }
    public bool  EnableSsl { get; set; } = true;
    public bool  UseDefaultCredentials { get; set; } = true;
    public string? TestReciveEmail { get; set; }

}
