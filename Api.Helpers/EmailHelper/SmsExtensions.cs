using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Api.Helpers;

public static class SmsExtentions
{
    private static ILogger _logger = MyFileLoggerFactory.CreateLogger("SmsExtentions");

    public static async Task<SendMessageResult> SendSmsAsync(string empName, string mobileTel, string smsBody )
    {
        try
        {
            if (string.IsNullOrWhiteSpace(mobileTel))
            {
                return new SendMessageResult()
                {
                    IsSuccess = false,
                    SendType = 1,
                    SendNo = mobileTel,
                    SendMessage = smsBody,
                    SendSubject = empName,
                    ErrorMessage = "手機號碼不可為空白"
                };
            }

            //string reqUrl = "https://smsapi.mitake.com.tw/api/mtk/SmSend?CharsetURL=UTF-8";
            SmsModel smsModel = GetSmsSettings();
            var reqUri = new Uri(smsModel.ReqUrl!);
            if (!string.IsNullOrWhiteSpace(smsModel.TestReceiveMobileTel)){
                mobileTel = smsModel.TestReceiveMobileTel!;
            }

            // Use HttpClient new method
            using var client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                    {"username",smsModel.UserName!},  //87717558SMS
                    {"password",smsModel.Password!},  //Modern@9157
                    {"dstaddr",mobileTel},
                    {"destname",empName},
                    {"smbody",smsBody}
                };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(reqUri, content); //.PatchAsync(reqUrl,content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                string contentType = response.Content.Headers.ContentType!.ToString();
                bool isSuccess = false;
                string errMsg = responseString;
                if (responseString.Contains("msgid") || responseString.Contains("AccountPoint")) {
                    isSuccess = true;
                    errMsg = "";
                } 
                ////write to log
                _logger.LogWarning(smsBody);
                _logger.LogWarning(responseString);
                Console.WriteLine(smsBody);
                Console.WriteLine(responseString);
                return new SendMessageResult() {
                    IsSuccess = isSuccess,
                    SendType = 1,
                    SendNo = mobileTel,
                    SendMessage = smsBody,
                    SendSubject = empName,
                    ErrorMessage = errMsg
                };
            } else {
                return new SendMessageResult() {
                    IsSuccess = false,
                    SendType = 1,
                    SendNo = mobileTel,
                    SendMessage = smsBody,
                    SendSubject = empName,
                    ErrorMessage = response.ReasonPhrase ?? "手機簡訊發送失敗"
                };
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.ToString()); 
            _logger.LogError("手機簡訊發送失敗"); 
            //throw new Exception("手機簡訊發送失敗"); 
            return new SendMessageResult() {
                IsSuccess = false,
                SendType = 1,
                SendNo = mobileTel,
                SendMessage = smsBody,
                SendSubject = empName,
                ErrorMessage = ex.ToString()
            };
        }

    }

    public static SmsModel GetSmsSettings()
    {
        var smsModel = new SmsModel
        {
            ReqUrl = AppSettingsHelper.Configuration.GetSection("SmsSettings:ReqUrl").Value,
            UserName = AppSettingsHelper.Configuration.GetSection("SmsSettings:UserName").Value,
            Password = AppSettingsHelper.Configuration.GetSection("SmsSettings:Password").Value,
            SendCompanyName = AppSettingsHelper.Configuration.GetSection("SmsSettings:SendCompanyName").Value,
            TestReceiveMobileTel = AppSettingsHelper.Configuration.GetSection("SmsSettings:TestReceiveMobileTel").Value
        };
        return smsModel;
    }
 
    // public static async string SendSms2Async(string reqUri, string smsMsg) 
    // {
    //     // 已過時方法
    //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reqUri);
    //     request.Method="POST";
    //     request.ContentType = "application/x-www-form-urlencoded";

    //     byte[] bytes = Encoding.UTF8.GetBytes(smsMsg);
    //     request.ContentLength = bytes.Length;
    //     request.GetRequestStream().Write(bytes,0,bytes.Length);

    //     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //     StreamReader sr = new(response.GetResponseStream());
    //     string result = sr.ReadToEnd();
    //     return result;
    // }


    // public static async string SendSms3Async(string reqUri, string smsMsg)
    // {
    //     // HttpClient new method
    //     using (var client = new HttpClient())
    //     {
    //         string responseString="";
    //         var values = new Dictionary<string,string>
    //         {
    //             // {"username",userName},  //87717558SMS
    //             // {"password",password},  //Modern@9157
    //             // {"dstaddr",mobileTel},
    //             // {"destname",empName},
    //             // {"smbody",smsBody}
    //         };
    //         var content = new FormUrlEncodedContent(values);
    //         var response = await client.PostAsync(reqUri,content); //.PatchAsync(reqUrl,content);
    //         if (response.IsSuccessStatusCode) {
    //             responseString = await response.Content.ReadAsStringAsync();
    //             Console.WriteLine(responseString);
    //             Console.WriteLine(smsMsg);
    //         }
    //         return responseString;                
    //     }        
    // }

    // public static async Task SendOverMaxWorkdaysSmsAsync(string empId, DateTime atDate, string atTime, AppDbContext db = null) {
    //     db ??= NewDb();
    //     //check today dutyId 是否符合7休1;
    //     var hm1emp10 = await db.Hm1Emp10s.AsNoTracking().FirstOrDefaultAsync(x => x.EmpId == empId);
    //     var result = await Api.Helpers.EmailExtentions.SendSmsAsync(hm1emp10.EmpName, hm1emp10.MobileTel, atDate, atTime, SmsType.Employee);
    //     int smsType = (int)SmsType.Employee;
    //     var tc2day10Msg = new Tc2Day10Msg
    //     {
    //         EmpId = empId,
    //         AtDate = System.DateTime.Now,
    //         SmsType = smsType,
    //         MobileTel = hm1emp10.MobileTel,
    //         Notes = result
    //     };
    //     db.Tc2Day10Msgs.Add(tc2day10Msg);
    //     await db.SaveChangesAsync();
    //     if (_appKeyRulesTimecard.IsNotifyManager) {
    //         var manager = await db.Hm1Emp10s.AsNoTracking().FirstOrDefaultAsync(x => x.EmpId == hm1emp10.ManagerId && x.OutDate == null);
    //         if (manager == null) {
    //             var hm0Dep10 = await db.Hm0Dep10s.AsNoTracking().FirstOrDefaultAsync(x => x.DepId == hm1emp10.DepId);
    //             manager = await db.Hm1Emp10s.AsNoTracking().FirstOrDefaultAsync(x => x.EmpId == hm0Dep10.DepEmpId && x.OutDate == null);
    //         }
    //         smsType = (int)SmsType.DepManager;
            
    //         if (manager == null) {
    //             result = await Api.Helpers.EmailExtentions.SendSmsAsync( "Manager", _appKeyRulesTimecard.ManagerMobileNo, atDate, atTime, SmsType.HrOperator);
    //         } else {
    //             string msgEmp = $"{manager.EmpName}，電話{manager.MobileTel}";
    //             result = await Api.Helpers.EmailExtentions.SendSmsAsync(msgEmp, manager.MobileTel, atDate, atTime, SmsType.DepManager);
    //             tc2day10Msg = new Tc2Day10Msg
    //             {
    //                 EmpId = empId,
    //                 AtDate = System.DateTime.Now,
    //                 SmsType = smsType,
    //                 MobileTel = manager.MobileTel,
    //                 Notes = result
    //             };                
    //             db.Tc2Day10Msgs.Add(tc2day10Msg);
    //             await db.SaveChangesAsync();                
    //         }
    //     }        
    //     return;
    // }
}

public enum SmsType 
{
    Employee=1,
    DepManager,
    HrOperator,
    Admin
}