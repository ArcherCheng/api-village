using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ma2MasterPartnerService(ILogger<Ma2MasterPartnerService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ma2MasterPartnerService>(logger, httpContextAccessor), IMa2MasterPartnerService
{

}