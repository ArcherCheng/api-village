using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ma2MasterExperienceService(ILogger<Ma2MasterExperienceService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ma2MasterExperienceService>(logger, httpContextAccessor), IMa2MasterExperienceService
{

}