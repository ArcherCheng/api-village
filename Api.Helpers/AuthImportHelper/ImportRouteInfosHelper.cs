using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// https://github.com/antonioseric/AspNetCore.RouteAnalyzer
namespace Api.Helpers;

public class RouteInfo
{
    public string HttpMethod { get; set; } = "GET";
    public string HttpRoute { get; set; } = "";
    public string CtrllerId { get; set; } = "";
    public string ActionId { get; set; } = "";
    public string ConDescription { get; set; } = "";
    public string ActDescription { get; set; } = "";
    public string CtrllerActionId { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Attributes {get; set;} = "";
    public bool IsRbacAuthorize { get; set; } = false;
    public int SortOrder { get; set; }
}

public interface IRouteAnalyzer
{
    IEnumerable<RouteInfo> ImportRouteInfos();
    IEnumerable<RouteInfo> ImportReportInfos(string resourcesDir,string reportsDir);
}

public class RouteAnalyzer : IRouteAnalyzer
{
    private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;

    public RouteAnalyzer(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
    }

    public IEnumerable<RouteInfo> ImportRouteInfos()
    {
        int seq=1;
        string oldCtrllerId = "AAAAAAAAA";
        List<RouteInfo> result = new();
        var actionDescriptors = this.actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>();
        foreach (var actionItem in actionDescriptors)
        {
            RouteInfo info = new()
            {
                HttpMethod = string.Join(", ", actionItem.ActionConstraints?.OfType<HttpMethodActionConstraint>().SingleOrDefault()?.HttpMethods ?? new string[] { "any" }),
                HttpRoute = $"/{actionItem.AttributeRouteInfo?.Template}",
                DisplayName = actionItem.DisplayName!,
                CtrllerId = actionItem.ControllerName,
                //系統 Action 會自動去掉 Async 字尾
                ActionId = actionItem.ActionName,
                ActDescription = actionItem.ActionName
            };
            if (info.CtrllerId.StartsWith("Zx")) {
                // 不處理 Customer Controller
                continue;
            }
            string? desc1 = "", desc2 = "";
            if (actionItem.ControllerTypeInfo.CustomAttributes!.Select(x => x.AttributeType).Contains(typeof(DescriptionAttribute)))
            {
                desc1 = actionItem.ControllerTypeInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(DescriptionAttribute))?.ConstructorArguments[0].Value?.ToString();
                info.ConDescription = desc1??"";
            }
            if (actionItem.MethodInfo.CustomAttributes!.Select(x => x.AttributeType).Contains(typeof(DescriptionAttribute)))
            {
                desc2 = actionItem.MethodInfo.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(DescriptionAttribute))?.ConstructorArguments[0].Value?.ToString();
                info.ActDescription = desc2??"";
            }
            info.IsRbacAuthorize = false;
            info.Attributes = string.Join(", ", actionItem.MethodInfo.CustomAttributes!.Select(x => x.AttributeType.Name));
            if (actionItem.MethodInfo.CustomAttributes!.Select(x => x.AttributeType).Contains(typeof(TypeFilterAttribute)))
            {
                info.IsRbacAuthorize = true;
            }
            if (info.CtrllerId == oldCtrllerId) {
                seq++;
            } else {
                oldCtrllerId = info.CtrllerId;
                seq = 1;
            }
            info.SortOrder = seq;
            info.CtrllerActionId = $"{actionItem.ControllerName}Controller.{actionItem.ActionName}";
            result.Add(info);
        }
        return result;
    }

    public IEnumerable<RouteInfo> ImportReportInfos(string resourcesDir,string reportsDir)
    {
        List<RouteInfo> result = new();
        var fullPathDir = System.IO.Path.Combine(resourcesDir, reportsDir);
        var allDirs = System.IO.Directory.GetDirectories(fullPathDir);
        foreach (var dir in allDirs)
        {
            if (dir.Contains("Zx")) {
                // 不處理 Customer Controller
                continue;
            }
            var allFiles = System.IO.Directory.GetFiles(dir);
            int idx = dir.LastIndexOf('\\');
            int seq = 100;
            foreach (var file in allFiles)
            {
                if (file.IndexOf("rdlc")<0) {
                    continue;
                }
                idx = file.LastIndexOf('\\');
                var reportFullName = file.Substring(idx+1,0);
                idx = reportFullName.IndexOf('-');
                var controller = reportFullName.Substring(0,idx);
                idx = reportFullName.LastIndexOf('.');
                var reportShortName = reportFullName.Substring(0,idx);

                RouteInfo info = new()
                {
                    HttpMethod = "POST",
                    HttpRoute = $"/api/{controller}/{reportShortName}",  //report/{reportfolder}/
                    DisplayName = controller,
                    CtrllerId = controller,
                    ActionId = reportShortName,
                    ActDescription = "列印報表",
                    IsRbacAuthorize = true,
                    CtrllerActionId = $"{controller}Controller.{reportShortName}",
                    SortOrder = seq++
                };
                result.Add(info);
            }
        }

        return result;
    }
}