using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Extensions.Logging;

namespace Api.Helpers;

public static class EmailExtentions
{
    private static ILogger _logger = MyFileLoggerFactory.CreateLogger("EmailExtentions");

    public static async Task<SendMessageResult> SendEmailAsync(string subject, string toAddress, string message, string attachFilepath="")
    {
        try {
            if (string.IsNullOrWhiteSpace(toAddress) || toAddress.IsEmail() == false)
            {
                return new SendMessageResult()
                {
                    IsSuccess = false,
                    SendType = 2,
                    SendNo = toAddress,
                    SendMessage = message,
                    SendSubject = subject,
                    ErrorMessage = "收件人地址不可為空"
                };
            }
            
            EmailModel emailModel = GetEamilSettings();
            if (!string.IsNullOrWhiteSpace(emailModel.TestReciveEmail)) {
                toAddress = emailModel.TestReciveEmail!;
            }

            System.Net.Mail.MailMessage mailMessage = new();  
            mailMessage.From = new System.Net.Mail.MailAddress(emailModel.FromEmailAddress!);  
            mailMessage.To.Add(new System.Net.Mail.MailAddress(toAddress));  
            mailMessage.To.Add(new System.Net.Mail.MailAddress(emailModel.FromEmailAddress!));  
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true; //to make message body as html  
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            if (!string.IsNullOrWhiteSpace(attachFilepath)) {
                System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(attachFilepath,System.Net.Mime.MediaTypeNames.Application.Octet);
                mailMessage.Attachments.Add(data);
                // ContentDisposition disposition = data.ContentDisposition;
                // disposition.CreationDate = System.IO.File.GetCreationTime(attachFilepath);
                // disposition.ModificationDate = System.IO.File.GetLastWriteTime(attachFilepath);
                // disposition.ReadDate = System.IO.File.GetLastAccessTime(attachFilepath);
            }

            using (var smtpClient = new System.Net.Mail.SmtpClient())  
            {  
                smtpClient.Host = emailModel.HostName!; // SMTP伺服器地址 
                smtpClient.Port = emailModel.HostPort; // SMTP端口,25,587
                smtpClient.EnableSsl = emailModel.EnableSsl; // 是否啟用SSL  
                smtpClient.UseDefaultCredentials = emailModel.UseDefaultCredentials;
                smtpClient.Credentials = new NetworkCredential(emailModel.LoginName, emailModel.LoginPassword);  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                await smtpClient.SendMailAsync(mailMessage); 
            } 

            return new SendMessageResult() 
            {
                IsSuccess = true,
                SendType = 2,
                SendNo = toAddress,
                SendMessage = message,
                SendSubject = subject,
                ErrorMessage = ""
            };            
        } catch (System.Exception ex) {
            _logger.LogError(ex.ToString()); 
            _logger.LogError("電子郵件發送失敗-microsoft.net.mail"); 
            return new SendMessageResult() 
            {
                IsSuccess = false,
                SendType = 2,
                SendNo = toAddress,
                SendMessage = message,
                SendSubject = subject,
                ErrorMessage = ex.ToString()
            };
        } 
    }

    public static EmailModel GetEamilSettings()
    {
        EmailModel emailModel = new()
        {
            FromEmailAddress = AppSettingsHelper.Configuration.GetSection("EmailSettings:FromEmailAddress").Value,
            HostName = AppSettingsHelper.Configuration.GetSection("EmailSettings:HostName").Value,
            HostPort = int.Parse(AppSettingsHelper.Configuration.GetSection("EmailSettings:HostPort").Value!),
            LoginName = AppSettingsHelper.Configuration.GetSection("EmailSettings:LoginName").Value,
            LoginPassword = AppSettingsHelper.Configuration.GetSection("EmailSettings:LoginPassword").Value,
            EnableSsl = bool.Parse(AppSettingsHelper.Configuration.GetSection("EmailSettings:EnableSsl").Value!),
            UseDefaultCredentials = bool.Parse(AppSettingsHelper.Configuration.GetSection("EmailSettings:UseDefaultCredentials").Value!),
            TestReciveEmail = AppSettingsHelper.Configuration.GetSection("EmailSettings:TestReceiveMail").Value
        };
        return emailModel;
    }    
    
    // public static async Task<SendMessageResult> SendEmail3Async(string subject, string toAddress, string message, string attachFilepath="")
    // {
    //     try {
    //         if (toAddress.IsNullOrEmpty())
    //         {
    //             return new SendMessageResult()
    //             {
    //                 IsSuccess = false,
    //                 ReceiveNo = toAddress,
    //                 SendMessage = message,
    //                 SendSubject = subject,
    //                 ErrorMessage = "收件人地址不可為空"
    //             };
    //         }

    //         EmailModel emailModel = GetEamilSettings();
    //         if (!emailModel.TestReciveEmail.IsNullOrEmpty()) {
    //             toAddress = emailModel.TestReciveEmail;
    //         }

    //         MimeKit.MimeMessage mailMessage = new();  
    //         mailMessage.From.Add(new MimeKit.MailboxAddress(emailModel.FromEmailAddress,emailModel.FromEmailAddress)); 
    //         mailMessage.To.Add(new MimeKit.MailboxAddress(toAddress,toAddress));  
    //         mailMessage.Cc.Add(new MimeKit.MailboxAddress(emailModel.FromEmailAddress,emailModel.FromEmailAddress));  
    //         mailMessage.Subject = subject;
    //         var multipart = new MimeKit.Multipart("mixed");
    //         var body = new MimeKit.TextPart("html") { Text = message };
    //         multipart.Add(body);
    //         if (!attachFilepath.IsNullOrEmpty()) {
    //             var attachment = new MimeKit.MimePart("application", "octet-stream") {
    //                 Content = new MimeKit.MimeContent(File.OpenRead(attachFilepath), MimeKit.ContentEncoding.Default),
    //                 ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
    //                 ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
    //                 FileName = Path.GetFileName(attachFilepath)
    //             };
    //             multipart.Add(attachment);
    //         }
    //         mailMessage.Body = multipart;

    //         using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())  
    //         {  
    //             await smtpClient.ConnectAsync(emailModel.HostName,emailModel.HostPort,emailModel.EnableSsl);
    //             await smtpClient.AuthenticateAsync(emailModel.LoginName, emailModel.LoginPassword);  
    //             await smtpClient.SendAsync(mailMessage); 
    //             await smtpClient.DisconnectAsync(true);
    //         } 

    //         return new SendMessageResult() 
    //         {
    //             IsSuccess = true,
    //             ReceiveNo = toAddress,
    //             SendMessage = message,
    //             SendSubject = "200",
    //             ErrorMessage = ""
    //         };            
    //     } catch (System.Exception ex) {
    //         _logger.LogError(ex.ToString()); 
    //         _logger.LogError("電子郵件發送失敗-MailKit"); 
    //         return new SendMessageResult() 
    //         {
    //             IsSuccess = false,
    //             ReceiveNo = toAddress,
    //             SendMessage = message,
    //             SendSubject = "500",
    //             ErrorMessage = "電子郵件無法發送"
    //         };
    //         //throw new Exception("電子郵件發送失敗"); 
    //     } 
    // }


}

