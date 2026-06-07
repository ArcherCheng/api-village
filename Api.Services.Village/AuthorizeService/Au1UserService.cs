using Microsoft.EntityFrameworkCore;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Api.Helpers;

namespace Api.Services;

public class Au1UserService(ILogger<Au1UserService> logger, IHttpContextAccessor httpContextAccessor)
    : AuthBaseService<Au1UserService>(logger, httpContextAccessor), IAu1UserService
{

}