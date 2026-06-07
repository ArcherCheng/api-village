using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Description("檔案上傳或下載")]
//[AllowAnonymous]
[Authorize]
[ApiController] // 屬性會讓模型驗證錯誤 ModelState.IsValid 自動觸發 HTTP 400 回應。
[Route("api/[controller]")]
public class FileController(ILogger<FileController> logger, IConfiguration configuration) : ControllerBase
{
    private readonly ILogger<FileController> _logger = logger;
    private readonly IConfiguration _configuration = configuration;

    //protected ApiUserData _apiUserData = Api.Helpers.HttpContextHelper.GetCurrentApiUserData();

    private readonly static Dictionary<string, string> _contentTypes = new()
    {
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".txt", "text/plain"},
        {".css", "text/css"},
        {".html", "text/html"},
        {".pdf",  "text/pdf"}
    };

    [Description("上傳單一檔案")]
    [HttpPost("team/{teamId}/upload/{typeFolder}"), DisableRequestSizeLimit]
    public async Task<IActionResult> FileUpload(string teamId, string typeFolder)
    {
        try
        {
            // var file = Request.Form.Files[0];
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files[0];
            if (file.Length == 0)
            {
                return BadRequest("file length error");
            }

            //var myUserId = _apiUserData.UserId.ToString();
            var resourcesFolder = _configuration.GetSection("AppSettings:Resources").Value??"Resources";
            var fullPathFolder = Path.Combine(Directory.GetCurrentDirectory(), resourcesFolder, typeFolder);
            if (!Directory.Exists(fullPathFolder))
            {
                DirectoryInfo dir = Directory.CreateDirectory(fullPathFolder);
            }

            var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            var isNewGuid = formCollection.First(x => x.Key == "isNewGuid").Value;
            if (isNewGuid=="true" || isNewGuid=="yes") {
                int idx = fileName.LastIndexOf(".");
                string type=".txt";
                if (idx > 0) {
                    type = fileName[..idx];
                }
                fileName = Guid.NewGuid().ToString()+type;
            }
            var saveFileName = Path.Combine(fullPathFolder, fileName);
            // var hrefPath = Path.Combine(fileFolder, fileName);
            using (var stream = new FileStream(saveFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new ApiUploadResult(typeFolder, fileName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, $"internal server error: {ex}");
        }
    }

    //[Authorize]
    [Description("上傳個人相片檔")]
    [HttpPost("team/{teamId}/upload-image/{typeFolder}/{idFolder}"), DisableRequestSizeLimit]
    public async Task<IActionResult> FileUploadImageAsync(string teamId, string typeFolder,string idFolder)
    {
        try
        {
            // var file = Request.Form.Files[0];
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files[0];
            if (file.Length == 0)
            {
                return BadRequest("file length error");
            }

            var resourcesFolder = _configuration.GetSection("AppSettings:Resources").Value??"Resources";
            var fullPathFolder = Path.Combine(Directory.GetCurrentDirectory(), resourcesFolder,"image", typeFolder, idFolder);
            if (!Directory.Exists(fullPathFolder))
            {
                DirectoryInfo dir = Directory.CreateDirectory(fullPathFolder);
            }

            var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
            var isNewGuid = formCollection.First(x => x.Key == "isNewGuid").Value;
            if (isNewGuid=="true" || isNewGuid=="yes") {
                int idx = fileName.LastIndexOf(".");
                string type=".txt";
                if (idx > 0) {
                    type = fileName[..idx];
                }
                fileName = Guid.NewGuid().ToString()+type;
            }
            var saveFileName = Path.Combine(fullPathFolder, fileName);
            // var hrefPath = Path.Combine(fileFolder, fileName);
            using (var stream = new FileStream(saveFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var filePath = @$"Resources/image/{typeFolder}/{idFolder}/{fileName}";
            return Ok(new ApiUploadResult(filePath, fileName));
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, $"internal server error: {ex}");
        }
    }

    [Description("上傳多個圖檔")]
    [HttpPost("team/{teamId}/upload-images/{typeFolder}"), DisableRequestSizeLimit]
    public async Task<IActionResult> FilesUploadImagesAsync(string teamId, string typeFolder)
    {
        try
        {
            // var file = Request.Form.Files[0];
            var formCollection = await Request.ReadFormAsync();
            if (formCollection.Files.Any(f => f.Length == 0))
                return BadRequest("file length error");

            var resourcesFolder = _configuration.GetSection("AppSettings:Resources").Value??"Resources";
            // var myUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var fullPathFolder = Path.Combine(Directory.GetCurrentDirectory(), resourcesFolder!,"image", typeFolder);
            if (!Directory.Exists(fullPathFolder))
            {
                DirectoryInfo dir = Directory.CreateDirectory(fullPathFolder);
            }

            IList<ApiUploadResult> uploadResultList = new List<ApiUploadResult>();
            foreach(var file in formCollection.Files)
            {
                var fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
                var saveFileName = Path.Combine(fullPathFolder, fileName);
                using (var stream = new FileStream(saveFileName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                uploadResultList.Add(new ApiUploadResult(typeFolder, fileName));
            }
            // var hrefPath = Path.Combine(fileFolder, fileName);
            return Ok(uploadResultList);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.ToString());
            return StatusCode(500, $"internal server error: {ex}");
        }
    }

    [Description("下載我的檔案")]
    [HttpGet("team/{teamId}/downlaod/my-self/{fileName}"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadMyFilesAsync(string teamId, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return NotFound();
        }
        var myUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var path = $@"Resources/my-self/{myUserId}/{fileName}";
        var memoryStream = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memoryStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);

        // 回傳檔案到 Client 需要附上 Content Type,否則瀏覽器會解析失敗。
        return new FileStreamResult(memoryStream, _contentTypes[Path.GetExtension(path).ToLowerInvariant()]);
    }

    [Description("下載檔案")]
    [HttpGet("team/{teamId}/downlaod/{filePath}"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadFilesAsync(string teamId, string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return NotFound();
        }

        var path = $@"Resources/{filePath}";
        var memoryStream = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memoryStream);
        }
        memoryStream.Seek(0, SeekOrigin.Begin);

        // 回傳檔案到 Client 需要附上 Content Type,否則瀏覽器會解析失敗。
        return new FileStreamResult(memoryStream, _contentTypes[Path.GetExtension(path).ToLowerInvariant()]);
    }


    [Description("Claims清單")]
    [HttpGet("team/{teamId}/claims")]
    public IActionResult GetClaims(string teamId)
    {
        var result = User.Claims.Select(p => new {p.Type, p.Value});
        return Ok(result);
    }

}
