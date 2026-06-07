using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Api.Models;


namespace Api.Services;

public class Ak0KeyRuleService(ILogger<Ak0KeyRuleService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ak0KeyRuleService>(logger,httpContextAccessor), IAk0KeyRuleService
{
    public async Task<Ak0KeyRule?> GetKeyRuleModelAsync(string ruleId)
    {
        using var db = NewDb();
        var result = await db.Ak0KeyRule.FirstOrDefaultAsync(x => x.RuleId == ruleId);
        return result;
    }

    public async Task<string?> GetKeyRuleIdValueAsync(string ruleId)
    {
        using var db = NewDb();
        var result = await db.Ak0KeyRule.FirstOrDefaultAsync(x => x.RuleId == ruleId);
        return result?.RuleValue;
    }

    public async Task<IEnumerable<Ak0KeyRule>?> GetKeyRuleListByGroupAsync(string ruleGroup)
    {
        using var db = NewDb();
        var result = await db.Ak0KeyRule.Where(x => x.RuleGroup == ruleGroup).ToListAsync();
        return result;
    }
}

