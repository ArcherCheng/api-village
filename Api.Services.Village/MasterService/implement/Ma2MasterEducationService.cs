using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ma2MasterEducationService(ILogger<Ma2MasterEducationService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ma2MasterEducationService>(logger, httpContextAccessor), IMa2MasterEducationService
{

}