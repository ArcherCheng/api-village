using System;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Api.Helpers;
using Api.Services;
using Api.Services.Village;
using Api.Models;
using Microsoft.Reporting.NETCore;

namespace Api.Controllers;

/*
return function
OK => returns the 200 status code
NotFound => returns the 404 status code
BadRequest => returns the 400 status code
NoContent => returns the 204 status code
Created, CreatedAtRoute, CreatedAtAction => returns the 201 status code
Unauthorized => returns the 401 status code
Forbid => returns the 403 status code
StatusCode => returns the status code we provide as input
*/

// 必須設定[TypeFilter(typeof(RbacAuthorizeFilter))]才能做權限控制
// [TypeFilter(typeof(RbacAuthorizeFilter))]
// [Authorize]
// [AllowAnonymous]
[Route("api/[controller]")]
[Description("ApiControllerBase 基準系統")]
[ApiController]
public abstract class ApiControllerBase<TEntity, TView, TDto, IService>(ILogger logger, IBaseService service) : ControllerBase
    where TEntity: BaseEntity, IBaseEntity
    where TView: class
    where TDto: class
    where IService: IBaseService
{
    protected Microsoft.Extensions.Logging.ILogger _logger = logger;
    protected IBaseService _BaseService = service;

    #region query
    /// <summary>
    /// 查詢單筆鍵值資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[Authorize]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    //[AllowAnonymous]
    [Description("查詢表格鍵值的資料")]
    [HttpGet("team/{teamId}/id/{id}")]
    public virtual async Task<IActionResult> GetById(string teamId,string id)
    {
        TEntity? result = await this._BaseService.GetByIdAsync<TEntity>(id);
        if (result == null)
        {
            this._logger.LogWarning($"{typeof(IService)} GetByIdAsync not found id = {id}");
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// 查詢表格全部資料
    /// </summary>
    /// <returns></returns>
    //[Authorize]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    //[AllowAnonymous]
    [Description("顯示表格全部的資料")]
    [HttpGet("team/{teamId}/all")]
    public virtual async Task<IActionResult> GetAll(string teamId)
    {
        IList<TEntity> result = await this._BaseService.GetAllAsync<TEntity>();
        if (result == null)
        {
            this._logger.LogWarning($"{typeof(IService)} GetAllAsync not found");
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// 查詢表格條件資料
    /// </summary>
    /// <param name="baseParas"></param>
    /// <returns></returns>
    //[Authorize]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    //[AllowAnonymous]
    [Description("顯示表格第一筆的資料")]
    [HttpGet("team/{teamId}/first")]
    public virtual async Task<IActionResult> GetViewFirst(string teamId, [FromQuery] BaseParas baseParas)
    {
        TView? result = await this._BaseService.GetViewFirstAsync<TView>(baseParas);
        if (result == null)
        {
            this._logger.LogWarning($"{typeof(IService)} GetViewListAsync not found by condition {baseParas.WhereConditionList!.ToString()}");
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// 查詢表格條件資料
    /// </summary>
    /// <param name="baseParas"></param>
    /// <returns></returns>
    //[Authorize]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    //[AllowAnonymous]
    [Description("顯示表格條件的資料")]
    [HttpGet("team/{teamId}/list")]
    public virtual async Task<IActionResult> GetViewList(string teamId, [FromQuery] BaseParas baseParas)
    {
        IList<TView> result = await this._BaseService.GetViewListAsync<TView>(baseParas);
        if (result == null)
        {
            this._logger.LogWarning($"{typeof(IService)} GetViewListAsync not found by condition {baseParas.WhereConditionList!.ToString()}");
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// 查詢列表資料
    /// </summary>
    /// <param name="baseParas"></param>
    /// <returns></returns>
    //[Authorize]
    //[AllowAnonymous]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    [Description("查詢表格分頁的資料")]
    [HttpGet("team/{teamId}/pageList")]
    public virtual async Task<IActionResult> GetViewPageList(string teamId, [FromQuery] BaseParas baseParas)
    {
        PageListResult<TView> result = await this._BaseService.GetViewPageListAsync<TView>(baseParas);
        if (result == null)
        {
            this._logger.LogWarning($"{typeof(IService)} GetViewPageListAsync not found by condition {baseParas.WhereConditionList!.ToString()}");
            return NotFound();
        }
        return Ok(result);
    }
    #endregion

    #region Add,update,delete
    /// <summary>
    /// 新增資料
    /// </summary>
    /// <param name="viewmodel"></param>
    /// <returns></returns>
    [Authorize]
    //[AllowAnonymous]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    [Description("新增資料")]
    [HttpPost("team/{teamId}")]
    public virtual async Task<IActionResult> Add(string teamId, [FromBody] TDto dtoModel)
    {
        if (!ModelState.IsValid)
        {
            Dictionary<string, string[]?> ModelStateErrors = ModelState.Where(x => x.Value?.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value?.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError($"{typeof(TDto)} Add ModelState.IsValid Error {ModelStateErrors}");
            return UnprocessableEntity(ModelStateErrors);
        }
        var model = AgileObjects.AgileMapper.Mapper.Map(dtoModel).ToANew<TEntity>();
        var msg = await this._BaseService.ValidationAsync(model,CrudMode.Insert);
        if (string.IsNullOrWhiteSpace(msg))
        {
            await this._BaseService.AddAsync<TEntity>(model);
            return Ok(model);
        }
        return BadRequest(msg);
    }

    /// <summary>
    /// 修改資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="viewmodel"></param>
    /// <returns></returns>
    // [Authorize]
    //[AllowAnonymous]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    [Description("修改資料")]
    [HttpPut("team/{teamId}/{id}")]
    public virtual async Task<IActionResult> UpdateByView(string teamId, string id, [FromBody] TDto dtoModel)
    {
        if (!ModelState.IsValid)
        {
            Dictionary<string, string[]?> ModelStateErrors = ModelState.Where(x => x.Value?.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value?.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError($"{typeof(TDto)} Add ModelState.IsValid Error {ModelStateErrors}");
            return UnprocessableEntity(ModelStateErrors);
        }
        var model = await this._BaseService.GetByIdAsync<TEntity>(id);
        if (model == null){
            return NotFound();
        }
        AgileObjects.AgileMapper.Mapper.Map(dtoModel).Over<TEntity>(model);
        var msg = await this._BaseService.ValidationAsync(model,CrudMode.Update);
        if (string.IsNullOrWhiteSpace(msg))
        {
            await this._BaseService.UpdateAsync<TEntity>( id, model );
            return Ok(model);
        }
        return BadRequest(msg);
    }

    /// <summary>
    /// 刪除資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    //[AllowAnonymous]
    //[TypeFilter(typeof(RbacAuthorizeFilter))]
    [Description("刪除資料")]
    [HttpDelete("team/{teamId}/{id}")]
    public virtual async Task<IActionResult> Delete(string teamId, string id)
    {
        var model = await this._BaseService.GetByIdAsync<TEntity>(id);
        if (model == null){
            return NotFound();
        }
        string? msg = await this._BaseService.ValidationAsync(model,CrudMode.Delete);
        if (string.IsNullOrWhiteSpace(msg))
        {
            await this._BaseService.DeleteAsync<TEntity>(id);
            return Ok(model);
        }
        return BadRequest(msg);
    }
    #endregion

    #region report
    /// <summary>
    /// 列印報表
    /// //https://github.com/lkosson/reportviewercore/
    /// </summary>
    /// <param name="baseParas"></param>
    /// <param name="reportPath"></param>
    /// <param name="reportName"></param>
    /// <returns></returns>
    //https://github.com/lkosson/reportviewercore/
    [Authorize]
    [AllowAnonymous]
    // [TypeFilter(typeof(RbacAuthorizeFilter))]
    [Description("列印報表")]
    [HttpPost("team/{teamId}/report/{reportPath}/{reportName}")]
    public async Task<IActionResult> CreateRdlcReport(string teamId,string reportPath,string reportName, [FromBody] BaseParas baseParas)
    {
        if (!ModelState.IsValid)
        {
            Dictionary<string, string[]> ModelStateErrors = ModelState.Where(x => x.Value!.Errors.Count > 0).ToDictionary(k => k.Key, k => k.Value!.Errors.Select(e => e.ErrorMessage).ToArray());
            this._logger.LogError($"{typeof(BaseParas)} Add ModelState.IsValid Error {ModelStateErrors}");
            return UnprocessableEntity(ModelStateErrors);
        }
        this.CheckReportParas(baseParas);
        IList<TView> dataSource = await this._BaseService.GetViewListAsync<TView>(baseParas);
        //dataSource.SetListAmtZeroByUserType(_userData.UserType);
        List<ReportParameter> reportParameters = this._BaseService.GetReportParameters(baseParas.ReportSignParas!);

        string templateFilepath = Api.Helpers.AppSettingsHelper.GetReportTemplateFilePath(reportPath,reportName);
        (string? outputFileName, string? renderFormat) = Api.Helpers.GenRdlcReportExtensions.GetOutputReportNameAndType(reportName,baseParas.ReportSignParas?.ReportType!);
        byte[] rdlcResult = Api.Helpers.GenRdlcReportExtensions.GenerateRdlcTemplate<TView>(templateFilepath,dataSource,reportParameters,renderFormat);
        return File(rdlcResult,System.Net.Mime.MediaTypeNames.Application.Octet,outputFileName);
    }

    protected bool CheckReportParas(BaseParas baseParas,bool canEmpty = false)
    {
        if (baseParas.ReportSignParas == null) {
            if (!canEmpty) {
                this._logger.LogError("Report Sign Paras is null");
                throw new Exception("Report Sign Paras is null");
            }
        } else if (baseParas.WhereConditionList?.Count == 0) {
            if (!canEmpty) {
                this._logger.LogError("Report Search Condition List is zero");
                throw new Exception("Report Search Condition List is zero");
            }
        }
        return true;
    }

    // [Authorize]
    // [Description("列印報表")]
    // [HttpPost("report-miniEexcel/{reportPath}/{reportName}")]
    // public virtual async Task<IActionResult> GetMiniExcelReportAsync([FromBody] BaseParas baseParas, string reportName, string reportPath)
    // {
    //     var resourcesFolder = Api.Helpers.AppSettingsHelper.ResourcesFolder();
    //     var templateFile = System.IO.Path.Combine(resourcesFolder,"ExcelTemplate",reportPath,reportName);
    //     var dataSource = await this._BaseService.GetViewListAsync<TView>(baseParas);
    //     dataSource.HiddenAmtFieldsZeroByUserType(_userData);
    //     // var path = Path.Combine("Resources" ,$"{System.Guid.NewGuid()}.xlsx");
    //     // MiniExcelLibs.MiniExcel.SaveAsByTemplate(path, templatePath, reportData);
    //     System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
    //     memoryStream.SaveAsByTemplate(templateFile, dataSource);
    //     memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
    //     return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
    //     {
    //         FileDownloadName = $"{reportName}-{System.DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx"  //"report.xlsx"
    //     };
    // }
    #endregion

    #region batch-log
    //[Authorize]
    //[TypeFilter(typeof(RbacActionFilter))]
    //[AllowAnonymous]
    // [Description("查詢批次過帳錯誤記錄")]
    // [HttpGet("batchLog/{batchName}")]
    // public virtual async Task<IActionResult> GetAppBatchMsgAsync(string batchName)
    // {
    //     var result = await this._BaseService.GetAppBatchMsgAsync(batchName);
    //     if (result == null)
    //     {
    //         return NotFound();
    //     }
    //     return Ok(result);
    // }
    #endregion

    // 改成不在 header 傳送, 由 pageListResult Json 傳回即可
    // protected void AddPaginationHeader(Pagination pagination)
    // {
    //     // Response.AddPaginationHeader(pagination);
    // }

    // protected ActionResult TryScope(Func<ActionResult> func)
    // {
    //     try
    //     {
    //         using var scope = new System.Transactions.TransactionScope();
    //         var result = func();
    //         scope.Complete();
    //         return result;
    //     }
    //     catch(Exception ex)
    //     {
    //         _logger.LogError(ex.ToString());
    //         return BadRequest(ex.ToString()); // 自定义的error view
    //     }
    //     // return base.TryScope(() => {
    //     //     _BaseService.AddAsync<TEntity>();
    //     //     return OkResult();
    //     // });
    // }
}


