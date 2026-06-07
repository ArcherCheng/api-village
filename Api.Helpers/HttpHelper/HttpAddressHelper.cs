using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System;
using System.Net;

namespace Api.Helpers;

// 設定網路HTTP存取Service，給 Entity.DoWriteUser()，services.AddDbContext<AppDbContext> 用
// 1.設定 HttpContext
// startup.cs
// public void ConfigureServices(IServiceCollection services)
//   //for asp.net core 2.2
//   services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//   //for asp.net core 3.0
//   services.AddHttpContextAccessor();
// public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
//   //not need
//   //HttpContextHelpers.Configure(services.GetRequiredService<IHttpContextAccessor>());

public static class HttpAddressHelper
{
    // public static Microsoft.AspNetCore.Http.HttpContext CurrentHttpContext
    // {
    //     get
    //     {
    //         HttpContextAccessor accessor = new();
    //         if (accessor.HttpContext is null)
    //         {
    //             throw new InvalidOperationException("HttpContext is not available.");
    //         }
    //         return accessor.HttpContext;
    //     }
    // }

    // public static string GetHostIp()
    // {
    //     var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
    //     foreach (var ip in host.AddressList)
    //     {
    //         if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    //         {
    //             return ip.ToString();
    //         }
    //     }
    //     return System.Net.Dns.GetHostName(); //"No Ip";
    // }

    public static string GetClientIP()
    {
        try
        {
            string ip = "";
            var httpContext = new HttpContextAccessor().HttpContext;

            // X-Forwarded-For (csv list):  Using the First entry in the list seems to work
            // for 99% of cases however it has been suggested that a better (although tedious)
            // approach might be to read each IP from right to left and use the first public IP.
            // http://stackoverflow.com/a/43554000/538763
            // ip = httpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',').FirstOrDefault();
            // if (!string.IsNullOrEmpty(ip)){
            //     return ip;
            // }

            if (httpContext == null) return "";

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            ip = httpContext.Connection.RemoteIpAddress != null
                ? httpContext.Connection.RemoteIpAddress.ToString()
                : "";

            if (string.IsNullOrWhiteSpace(ip) && httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues value))
            {
                ip = value.FirstOrDefault() ?? "";
            }

            return ip ?? "";
        }
        catch (System.Exception)
        {
            return "";
            //throw;
        }
    }
}


