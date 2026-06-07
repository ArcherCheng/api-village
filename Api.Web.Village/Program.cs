using Api.Helpers;
using Api.Services;
using Api.Web;
using Microsoft.AspNetCore.HttpOverrides;
// using NLog;

var builder = WebApplication.CreateBuilder(args);
// LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigIISIntegration();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
// builder.Services.AddSingleton<PresenceTracker>();
// builder.Services.ConfigureLoggerService();
// add global exception handler, for app.UseExceptionHandler to work, must add this service
// builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

//builder.Services.Configure<ApiBehaviorOptions>(options => {options.SuppressModelStateInvalidFilter = true;});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.ConfigDbContext(builder.Configuration);
builder.Services.ConfigControllers();
builder.Services.ConfigCors(builder.Configuration);
builder.Services.ConfigAuthentication(builder.Configuration);
builder.Services.ConfigFormOption();
builder.Services.ConfigResponseCompression();
// builder.Services.ConfigSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
//add customer service
builder.Services.ApiServiceRegister();



//Step 1:
var app = builder.Build();
Console.WriteLine("begin app");
// must set congiguration at here for development mode
Api.Helpers.AppSettingsHelper.Configuration = app.Configuration;
Api.Helpers.MyFileLoggerFactory.LoggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
Api.Helpers.MyFileLoggerFactory.LoggerFactory.AddLogFilePath();
// var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
// Api.Helpers.HttpContextHelper.SetHttpContext(httpContextAccessor);

// var logger = app.Services.GetRequiredService<ILoggerManager>();
// app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler(opt => { });


// Step2
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // http://localhost:5000/openapi/v1.json
    app.MapOpenApi();
    // // http://localhost:5000/swagger/index.html
    // app.UseSwagger();
    // app.UseSwaggerUI(options =>
    // {
    //     //options.SwaggerEndpoint("/openapi/v1.json", "v1");
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    // });
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// step 3:
if (app.Configuration.GetSection("AppSettings:UseHttps").Value == "true")
{
    app.UseHttpsRedirection();
}

// step 4:
app.MapStaticAssets();
app.UseDefaultFiles(); //Serve files from wwwroot
app.UseStaticFiles();  //Serve files from wwwroot
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(builder.Environment.ContentRootPath,"Resources")),
    RequestPath = "/Resources"
});

// step 5:
app.UseCors("CorsPolicy");
// app.UseCors();
// var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Console.WriteLine(environment);
// var myConfig = app.Configuration.GetSection("MySetting").Value;
// Console.WriteLine(myConfig);

// step 6:
app.UseAuthentication();
app.UseAuthorization();

// step 7:
app.UseResponseCompression();
//// 轉存檔案可以存在 http header 之中
app.UseForwardedHeaders(new ForwardedHeadersOptions{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
});

// step 8:
// app.MapHub<PresenceHub>("/hubs/presence");
// app.MapHub<PartyChatGroupHub>("/hubs/PartyChatGroup");
// app.MapHub<PartyChatOtherHub>("/hubs/PartyChatOther");
//Console.WriteLine("app.MapHub<PresenceHub>(\"/hubs/presence\");");
app.MapControllers();
app.MapFallbackToController("Index","FallBack");
// var seed= new Seed();
// seed.SeedPartyData();

// step 9:
Console.WriteLine("end app");
app.Run();
