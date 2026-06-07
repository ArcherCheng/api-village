using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Cz2RepairService(ILogger<Cz2RepairService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Cz2RepairService>(logger, httpContextAccessor), ICz2RepaireService
{

}

