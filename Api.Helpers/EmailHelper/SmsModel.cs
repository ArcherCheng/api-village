using System;
using System.Net;
using System.Net.Mail;

namespace Api.Helpers;

public class SmsModel
{
    public string? ReqUrl { get; set; } = "";   
    public string? UserName { get; set; }="";
    public string? Password { get; set; }="";
    public string? SendCompanyName { get; set; }="";
    public string? TestReceiveMobileTel { get; set; }="";
}
