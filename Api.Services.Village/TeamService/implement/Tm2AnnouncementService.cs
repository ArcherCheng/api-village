using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Tm2AnnouncementService(ILogger<Tm2AnnouncementService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Tm2AnnouncementService>(logger, httpContextAccessor), ITm2AnnouncementService
{

}

