using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Api.Helpers;

public class ControllerInfo
{
    public string? BaseController { get; set; } = "";
    public string? Controller { get; set; } = "";
    public string? Description { get; set; } = "";
}

public static class ControllersHelper
{
    public static IEnumerable<ControllerInfo> ImportControllerInfos()
    {
        List<ControllerInfo> result = new List<ControllerInfo>();
        //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        System.Reflection.Assembly asm = System.Reflection.Assembly.GetCallingAssembly();
        var controllerList = asm.GetTypes()
            .Where(type =>
            {
                return typeof(Microsoft.AspNetCore.Mvc.ControllerBase).IsAssignableFrom(type);
            })
            .OrderBy(x => x.Name);

        foreach (var item in controllerList)
        {
            var info = new ControllerInfo();
            info.BaseController = item.BaseType?.Name;
            info.Controller = item.Name.Replace("Controller", "");
            var descAttr = item.GetCustomAttribute(attributeType: typeof(DescriptionAttribute), inherit: false) as DescriptionAttribute;
            if (descAttr != null)
            {
                info.Description = descAttr.Description;
                result.Add(info);
            }
        }
        return result;
        /// use assembly reflectioin 
        // Assembly asm = Assembly.GetExecutingAssembly();
        // var result = asm.ExportedTypes //asm.GetTypes()  // 
        //     .Where(type => typeof(ControllerBase).IsAssignableFrom(type)) //filter controllers
        //     .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
        //     .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
        //     .Select(x => new
        //     {
        //         Controller = x.DeclaringType.Name.Replace("Controller", ""),
        //         Action = x.Name,
        //         Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))),
        //         DeclaringType = x.DeclaringType
        //     })
        //     .OrderBy(x => x.Controller).ThenBy(x => x.Action)
        //     .ToList();
        // return result;            
    }
}
