using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ma1MasterService(ILogger<Ma1MasterService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ma1MasterService>(logger, httpContextAccessor), IApiBaseService
{
    public async Task<Ma1Master?> GetMa1MasterAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma1Master
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<List<Ma2MasterEducation>> GetMa2MasterEducationListAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma2MasterEducation
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        return result;
    }

    public async Task<List<Ma2MasterExperience>> GetMa2MasterExperienceListAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma2MasterExperience
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        return result;

    }

    public async Task<List<Ma2MasterPartner>> GetMa2MasterPartnerListAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma2MasterPartner
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        return result;
    }

    public async Task<List<Ma2MasterPhoto>> GetMa2MasterPhotoListAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma2MasterPhoto
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        return result;
    }

    public async Task<List<Ma2MasterPolicy>> GetMa2MasterPolicyListAsync(string teamId)
    {
        var db = NewDb();
        var result = await db.Ma2MasterPolicy
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        return result;
    }

}

