using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Services;

/// https://dotnetcoretutorials.com/servicelocator-shim-for-net-core/
/// var ot2Day10Service = Api.Services.ServiceLocator.Current.GetInstance<IOt2Day10Service>();
public class ServiceLocator 
{
    private ServiceProvider _currentServiceProvider;
    private static ServiceProvider? _serviceProvider;

    public ServiceLocator(ServiceProvider currentServiceProvider)
    {
        _currentServiceProvider = currentServiceProvider;
    }

    public static ServiceLocator Current
    {
        get { 
            return new ServiceLocator(_serviceProvider!);
        }
    }

    public static void SetLocatorProvider(ServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object? GetInstance(Type serviceType)
    {
        return _currentServiceProvider.GetService(serviceType);
    }

    public TService GetInstance<TService>()
    {
        return _currentServiceProvider.GetService<TService>()!;
    }
}

// public class Net46ServiceLocator
// {
//     private readonly Dictionary<Type, object> _serviceDictionary = new Dictionary<Type, object>();
//     private static Net46ServiceLocator _current;
//     public static Net46ServiceLocator Current 
//     {
//         get {
//             if (_current == null) {
//                 _current = new Net46ServiceLocator();
//             }
//             return _current;
//         }
//         set {
//             _current = value;
//         }
//     }
//     public TService GetInstance<TService>() where TService: class
//     {
//         TService service = default(TService);
//         if (_serviceDictionary.ContainsKey(typeof(TService)) == true) {
//             service = _serviceDictionary[typeof(TService)] as TService;
//         }
//         return service;
//     }
//     public void SetInstance<TService>(TService service) where TService : class 
//     {
//         if (service == null) throw new ArgumentNullException();
//         if (_serviceDictionary.ContainsKey(typeof(TService))==false) {
//             _serviceDictionary.Add(typeof(TService), service);
//         } else {
//             _serviceDictionary[typeof(TService)] = service;
//         }
//     }
// }

