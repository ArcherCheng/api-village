using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Services;
using Api.Models;

namespace Api.Controllers;

[Description("聯誼活動設定作業")]
[AllowAnonymous]
// [TypeFilter(typeof(RbacAuthorizeFilter))]
[ApiController]
[Route("api/[controller]")]
public class Ma1MasterController(ILogger<Ma1MasterController> logger, IMa1MasterService service)
    : ApiControllerBase<Ma1Master, Ma1Master, Ma1Master, IMa1MasterService>(logger,service)
{
    [Description("查詢首長資料")]
    [HttpGet("team/{teamId}/master")]
    public async Task<IActionResult> GetMaster(string teamId)
    {
        var result = await service.GetMa1MasterAsync(teamId);
        return Ok(result);
    }

    [Description("查詢首長教育背景")]
    [HttpGet("team/{teamId}/education")]
    public async Task<IActionResult> GetMasterEducation(string teamId)
    {
        var result = await service.GetMa2MasterEducationListAsync(teamId);
        return Ok(result);
    }

    [Description("查詢首長工作經歷")]
    [HttpGet("team/{teamId}/experience")]
    public async Task<IActionResult> GetMasterExperience(string teamId)
    {
        var result = await service.GetMa2MasterExperienceListAsync(teamId);
        return Ok(result);
    }

    [Description("查詢首長照片")]
    [HttpGet("team/{teamId}/policy")]
    public async Task<IActionResult> GetMasterPolicy(string teamId)
    {
        var result = await service.GetMa2MasterPolicyListAsync(teamId);
        return Ok(result);
    }

    [Description("查詢首長照片")]
    [HttpGet("team/{teamId}/photo")]
    public async Task<IActionResult> GetMasterPhoto(string teamId)
    {
        var result = await service.GetMa2MasterPhotoListAsync(teamId);
        return Ok(result);
    }

    [Description("查詢首長照片")]
    [HttpPost("team/{teamId}")]
    public async Task<IActionResult> EditMaster(string teamId, [FromBody] Ma1Master model)
    {
        var result = await service.GetMa2MasterPhotoListAsync(teamId);
        return Ok(result);
    }


}
