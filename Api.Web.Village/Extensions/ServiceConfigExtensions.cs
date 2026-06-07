using Api.Models;
using Api.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Api.Web;

public static class ServiceConfigExtensions
{
    public static void ConfigDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>((serviceProvider, dbContextBuilder) =>
        {
            dbContextBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        ////https://learn.microsoft.com/zh-tw/aspnet/core/security/authentication/identity?view=aspnetcore-10.0&tabs=net-cli
        // services.AddDbContext<AppDbContext>((options) =>
        // {
        //     options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        // });
        // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        // .AddEntityFrameworkStores<AppDbContext>();
    }

    public static void ConfigControllers(this IServiceCollection services)
    {
        // // https://blog.csdn.net/iml6yu/article/details/135126557
        // services.AddControllers().AddJsonOptions(options =>
        // {
        //     // 设置编码格式
        //     options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        //     // 是否格式化文本
        //     options.JsonSerializerOptions.WriteIndented = true;
        //     // 字段采用驼峰式命名
        //     options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        //     // 忽略null值
        //     options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        //     // 忽略只读字段
        //     options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        //     // 允许属性值末尾存在逗号
        //     options.JsonSerializerOptions.AllowTrailingCommas = true;
        //     // 处理循环引用类型
        //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //     //枚举类型转string配置（避免转int）
        //     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //     // 添加时间格式化转换器
        //     options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
        //     // 添加Object格式化转换器
        //     options.JsonSerializerOptions.Converters.Add(new ObjectJsonConverter());
        // });

        ////Task<IActionResult> AddAppTempExcelAsync([FromBody] IEnumerable<dynamic> modelList)  會錯誤
        //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        ////Task<IActionResult> AddAppTempExcelAsync([FromBody] IEnumerable<dynamic> modelList)
        services.AddControllers().AddNewtonsoftJson( options => {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });


    }

    public static void ConfigCors(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(options =>
        {
            var allowedCorsUrls = config.GetSection("AppSettings:allowedCorsUrls").Value?.Split(",");
            //var allowedOriginsUrls = config.GetSection("AppSettings:AllowCorsUrls").Value?.Split(",");
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    // 轉存檔案可以存在 http header 之中
                    .WithOrigins(allowedCorsUrls!)
                    .WithExposedHeaders("*");
            });
            options.AddPolicy("CorsPolicy", builder => {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    // 轉存檔案可以存在 http header 之中
                    .WithOrigins(allowedCorsUrls!)
                    .WithExposedHeaders("*");
            });
        });
    }

    public static void ConfigIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options => {});

    public static void ConfigSingalR(this IServiceCollection services, IConfiguration config)
    {
        services.AddSignalR();
    }

    public static void ConfigAuthentication(this IServiceCollection services, IConfiguration config) =>
        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                //var jwtSettings = config.GetSection("JwtSettings");
                // Ps.create an environment variable,
                // we have to open the cmd window as an administrator and type the following command:
                // set SECRET "CodeMazeSecretKey" /M  ==>指定系統環境全域變數，而非區域變數
                // var secretKey = System.Environment.GetEnvironmentVariable("SECRET");
                //var jwtSettings = Config.GetSection("JwtSettings");
                // var ValidIssuer = config.GetValue<string>("JwtSettings:ValidIssuer")??"https://localhost:4200";
                // var ValidAudience = config.GetValue<string>("JwtSettings:ValidAudience")??"https://localhost:5001";
                var secretKey = config.GetValue<string>("JwtSettings:TokenKey")??"justdo.tw@webapi for JwtBearer Security Salt Key use JwtSettings:TokenKey and SymmetricSecurityKey";
                opt.IncludeErrorDetails = true;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    // ValidIssuer = config.GetValue<string>("JwtSettings:ValidIssuer")!,
                    ValidateAudience = false,
                    // ValidAudience =  config.GetValue<string>("JwtSettings:ValidAudience")!,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secretKey)),
                    ValidateLifetime = true
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            }
        );

        // 設定 RBAC 權限檢查,已經改成 AuthorizationFilter,比較簡單易控制
        // services.AddScoped<IAuthorizationHandler, RBACHandler>();
        // services.AddAuthorization(
        //     options => {
        //         options.AddPolicy("RBAC", policy =>
        //         {
        //             policy.Requirements.Add(new RBACRequirement());
        //         });
        //     }
        // );

    /// for controller upload file ex: Image,photo
    public static void ConfigFormOption(this IServiceCollection services) =>
        services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(
            options => {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
                options.MemoryBufferThreshold = int.MaxValue;
            }
        );

    public static void ConfigResponseCompression(this IServiceCollection services) =>
        services.AddResponseCompression(options => {
            options.EnableForHttps = true;
            options.MimeTypes = new[]
            {
                "application/javascript","application/json","application/xml",
                "text/css","text/html","text/javascript","text/plain","text/xml","text/json","text/csv",
                "image/apng","image/png","image/jpeg","image/svg+xml","image/avif","image/gif"
            };
        });

    //// https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-5-with-jwt-and-swagger/
    // public static void ConfigSwaggerGen(this IServiceCollection services) =>
    //     services.AddSwaggerGen(swagger =>
    //     {
    //         swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{Title = "PartyMatch", Version = "v1"});
    //         swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    //         {
    //             In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    //             Description = "JWT Token 認證,請輸入: Bearer {token}",
    //             Name = "Authorization",
    //             Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,   //.ApiKey,
    //             Scheme = "Bearer",
    //             BearerFormat = "JWT"
    //         });
    //         swagger.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    //         {
    //             {
    //                 new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    //                 {
    //                     Reference = new Microsoft.OpenApi.Models.OpenApiReference
    //                     {
    //                         Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
    //                         Id = "Bearer"
    //                     },
    //                     Name = "Bearer",
    //                 },
    //                 new List<string>()
    //             },
    //         });
    //     });

    // public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();
}


//https://blog.csdn.net/iml6yu/article/details/135126557
//问题描述 webapi 当使用System.Text.Json类库进行json转化时，如果没有明确类型的基础类型，则会出现转换值为ValueKind:Object {xxxxx}等问题
public class ObjectJsonConverter : JsonConverter<Object>
{
    public override object? Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        if (reader.TokenType == System.Text.Json.JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out DateTime dateTime))
                return dateTime;
            return reader.GetString();
        }
        else if (reader.TokenType == System.Text.Json.JsonTokenType.Number)
        {
            if (reader.TryGetInt32(out int intNum))
                return intNum;
            else if (reader.TryGetDouble(out double doubleNum))
                return doubleNum;
            else
                return reader.GetDecimal();
        }
        return reader.GetDecimal();
    }

    public override void Write(System.Text.Json.Utf8JsonWriter writer, object value, System.Text.Json.JsonSerializerOptions options)
    {
        Type objType = value.GetType();
        if (value == null)
            writer.WriteNullValue();
        if (objType == typeof(string) || objType == typeof(DateTime) || objType == typeof(Guid))
            writer.WriteStringValue(value!.ToString());
        else if (objType == typeof(int))
            writer.WriteNumberValue((int)value!);
        else if (objType == typeof(double))
            writer.WriteNumberValue((double)value!);
        else if (objType == typeof(decimal))
            writer.WriteNumberValue((decimal)value!);
        else if (objType == typeof(char))
            writer.WriteNumberValue((char)value!);
        else if (objType == typeof(bool))
            writer.WriteBooleanValue((bool)value!);
       else
            writer.WriteStringValue(value!.ToString());
    }
}

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref System.Text.Json.Utf8JsonReader reader, Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
    {
        if (reader.TokenType == System.Text.Json.JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out DateTime dateTime))
            {
                return dateTime;
            }
        }
        return reader.GetDateTime();
    }

    public override void Write(System.Text.Json.Utf8JsonWriter writer, DateTime value, System.Text.Json.JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
    }
}
