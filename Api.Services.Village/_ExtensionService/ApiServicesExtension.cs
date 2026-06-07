using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Api.Services;

//[RegisterAsScoped]
public static class ApiServicesExtension
{

    public static IServiceCollection ApiServiceRegister(this IServiceCollection services)
    {
        //自動註冊
        var assemblyTypes = System.Reflection.Assembly.GetExecutingAssembly().GetExportedTypes()
            .Where(x => x.Name.EndsWith("Service") && x.IsClass && !x.IsAbstract && !x.IsGenericType && !x.IsNested);
        foreach (var serviceType in assemblyTypes)
        {
            var serviceName = serviceType.Name;
            //var interfaceType = serviceType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(x => x.Name.Contains(serviceName));
            var interfaceType = serviceType.GetInterfaces().FirstOrDefault(x => x.Name.Contains(serviceName));
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, serviceType);
            }
        }
        Api.Services.ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
        return services;
    }
}