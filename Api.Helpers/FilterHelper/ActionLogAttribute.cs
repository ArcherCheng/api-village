using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net;

namespace Api.Helpers;

public class ActionLogFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
        var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        resultContext.RouteData.Values.TryGetValue("controller", out var controller);
        resultContext.RouteData.Values.TryGetValue("action", out var action);
        resultContext.RouteData.Values.TryGetValue("area", out var area);
        // var area = resultContext.ActionDescriptor.RouteValues["area"];
        // var controller = resultContext.ActionDescriptor.RouteValues["controller"];
        // var action = resultContext.ActionDescriptor.RouteValues["action"];
        var method = resultContext.HttpContext.Request.Method;
        var path = resultContext.HttpContext.Request.Path;
        Console.WriteLine($"{controller}-{action}: {method}-{path}");
    }


    public static string GetMacAddress()
    {
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            // 過濾條件：介面必須已啟用，狀態為 Up
            if (ni.OperationalStatus != OperationalStatus.Up)
                continue;

            // 過濾掉虛擬或不需要的介面（根據需求自訂）
            // 這裡以介面類型排除 VPN/Loopback/Tunnel 之類的非實體介面
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback || ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                continue;

            var mac = ni.GetPhysicalAddress();
            // 將 MAC 以常見格式顯示，例如 "AA-BB-CC-DD-EE-FF"
            string macString = BitConverter.ToString(mac.GetAddressBytes());
            return macString;

            // 取得該介面的 IPv4/IPv6 位址
            // var ipProps = ni.GetIPProperties();
            // 先找 Unicast 位址中符合 IPv4
            // var ipv4Info = ipProps.UnicastAddresses
            //                   .FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            // 也可以改成取得 IPv6
            // var ipv6Info = ipProps.UnicastAddresses
            //                   .FirstOrDefault(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6);

            // Console.WriteLine($"介面: {ni.Name}");
            // Console.WriteLine($"  描述: {ni.Description}");
            // Console.WriteLine($"  MAC: {macString}");
            // if (ipv4Info != null)
            //     Console.WriteLine($"  IPv4: {ipv4Info.Address}");
            // else
            //     Console.WriteLine("  IPv4: 無可用位址");

            // Console.WriteLine();
        }
        return "00-00-00-00-00-00"; // 若無可用介面，回傳預設值
    }


}


