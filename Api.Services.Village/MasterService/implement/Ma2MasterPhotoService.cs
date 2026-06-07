using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public class Ma2MasterPhotoService(ILogger<Ma2MasterPhotoService> logger, IHttpContextAccessor httpContextAccessor)
    : ApiBaseService<Ma2MasterPhotoService>(logger, httpContextAccessor), IMa2MasterPhotoService
{

}