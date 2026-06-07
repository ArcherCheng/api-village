
using System.Threading.Tasks;
using Api.Models;


namespace Api.Services;

public interface IAk0KeyRuleService : IApiBaseService
{
    Task<Ak0KeyRule?> GetKeyRuleModelAsync(string ruleId);
    Task<string?> GetKeyRuleIdValueAsync(string ruleId);
    Task<IEnumerable<Ak0KeyRule>?> GetKeyRuleListByGroupAsync(string ruleGroup);
}