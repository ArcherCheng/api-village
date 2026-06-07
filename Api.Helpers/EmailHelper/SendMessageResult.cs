using System;
using System.Net;
using System.Net.Mail;

namespace Api.Helpers;

public class SendMessageResult
{
    public bool IsSuccess { get; set; } = false;
    public string SendNo { get; set; } = "";
    public int SendType { get; set; } = 1;  // 1:SMS 2:Email
    public DateTime SendDate {get;  set;} = DateTime.Now;
    public string SendSubject { get; set; } = "";
    public string SendMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
}