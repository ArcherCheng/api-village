using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Helpers;
using Api.Models;

namespace Api.Services;
public static class KeyRulesHelper
{
    //static readonly ILogger<AppKeyRulesHelper> _logger;

    // static AppKeyRulesHelper()
    // {
    //     //_logger = Api.Helpers.MyFileLoggerFactory.CreateLogger<AppKeyRulesHelper>();
    // }

    static AppDbContext NewDb()
    {
        return new AppDbContext();
    }

    #region Get & Set AppKeyRules

    public static PasswordRule CreateAppKeyRulesPassword()
    {
        using var db = NewDb();
        PasswordRule passwordRule = new();
        var ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0010");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0010", "是否有強制密碼變更作業(1=是/0=否)", "0");
        passwordRule.IsForceChangePassword = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0011");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0011", "強制幾天須密碼變更一次(30)", "30");
        passwordRule.ForceChangeDays = StringExtensions.ToInt(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0012");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0012", "密碼變更最少字元長度(6)", "6");
        passwordRule.MinLenth = StringExtensions.ToInt(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0013");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0013", "密碼是否強制大寫英文字元(1=是/0=否)", "0");
        passwordRule.IsUpperWord = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0014");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0014", "密碼是否強制小寫英文字元(1=是/0=否)", "0");
        passwordRule.IsLowerWord = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0015");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0015", "密碼是否強制數字字元(1=是/0=否)", "0");
        passwordRule.IsNumberWord = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0016");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0016", "密碼是否強制特殊符號字元(1=是/0=否)", "0");
        passwordRule.IsSpecialWord = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0017");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0017", "密碼是否強制英數字組合字元(1=是/0=否)", "0");
        passwordRule.IsWordAndNumber = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0018");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0018", "密碼是否允許重復使用(1=是/0=否)", "0");
        passwordRule.IsAllowRepeat = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0019");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0019", "是否啟用郵件通知用戶登入訊息(1=是/0=否)", "0");
        passwordRule.IsEmailToLoginUser = StringExtensions.ToBoolean(ruleModel.RuleValue);

        ruleModel = db.Ak0KeyRule.AsNoTracking().FirstOrDefault(x => x.RuleId == "PW1X0020");
        ruleModel = CheckKeyRuleValueExist(db, ruleModel, "AUT", "PW1X0020", "是否啟用使用者登入不同機器檢查驗證碼(1=是/0=否)", "0");
        passwordRule.IsVerifyCode = StringExtensions.ToBoolean(ruleModel.RuleValue);

        passwordRule.RegularEx = CreatePasswordRegular(passwordRule);
        return passwordRule;
    }

    public static string CreatePasswordRegular(PasswordRule rules)
    {
        string reg = "^.*";
        reg += "(?=.{" + rules.MinLenth.ToString() +",})";
        if (rules.IsUpperWord) {
            reg += "(?=.*[A-Z])";
        }
        if (rules.IsLowerWord) {
            reg += "(?=.*[a-z])";
        }
        if (rules.IsNumberWord) {
            reg += "(?=.*[0-9])";
        }
        if (rules.IsSpecialWord) {
            //reg += @"(?=.*[~@#%&_;,/\!\=\:\>\<\.\*\(\)\+\?\$\^\}\|\{\-\[\]\\\?])";
            reg += @"(?=.*[\W])";
        }
        if (rules.IsWordAndNumber) {
            reg += "(?=.*[a-z]|[A-Z])(?=.*[0-9])";
        }
        return reg+".*$";
    }
    #endregion

    public static int CaculateTimeMins(string atTime="",int onOffType=1)
    {
        if (string.IsNullOrWhiteSpace(atTime) || atTime.Length<4) {
            if (onOffType == 2) {
                return 1200; //PM: 2000
            } else {
                return 420;  //AM: 0700
            }
        }
        _ = int.TryParse(atTime.AsSpan(0, 2), out int hours);
        _ = int.TryParse(atTime.AsSpan(2, 2), out int mins);
        return hours * 60 + mins;
    }

    public static Ak0KeyRule CheckKeyRuleValueExist(AppDbContext db, Ak0KeyRule? ruleModel, string group, string ruleId, string label, string value)
    {
        if (ruleModel == null) {
            ruleModel = new Ak0KeyRule()
            {
                RuleGroup = group,
                RuleId = ruleId,
                RuleLabel = label,
                RuleValue = value,
            };
            db.Ak0KeyRule.Add(ruleModel);
            db.SaveChanges();
        } else if (ruleModel.RuleLabel != label) {
            ruleModel.RuleLabel = label;
            db.Ak0KeyRule.Update(ruleModel);
            db.SaveChanges();
        }
        return ruleModel;
    }



}