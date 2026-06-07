using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ak0KeyCodeService(ILogger<Ak0KeyCodeService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ak0KeyCodeService>(logger,httpContextAccessor)  , IAk0KeyCodeService
{
    public async Task<IEnumerable<KeyValuePair<string,string>>> GetKeyValueListAsync(string group)
    {
        using var db = NewDb();
        var query = await db.Ak0KeyCode
            .AsNoTracking()
            .Where(x => x.CodeGroup == group)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.CodeValue)
            .Select(x => new KeyValuePair<string,string>(x.CodeValue,x.CodeLabel))
            .ToListAsync();
        return query;
    }
}