using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Api.Models;

public static class BaseEntityExtensions
{
    public static void WriteInfo(this IBaseEntity entity, string userName)
    {
        var str = $"user={userName} time={System.DateTime.Now} ip={Api.Helpers.HttpAddressHelper.GetClientIP()}";
        entity.WriteInfo = str;
    }

    // public static string GetIdType(this IBaseEntity entity)
    // {
    //     return entity.GetKeyType();
    // }

    // public static object GetPropertyValue(this IBaseEntity entity, string name)
    // {
    //     return entity.GetPropertyValue(name);
    // }

    // public static string GetPropertyType(this IBaseEntity entity, string name)
    // {
    //     return entity.GetPropertyType(name);
    // }

    // public static string GetKeyName(this IBaseEntity entity)
    // {
    //     PropertyInfo key = entity.GetType().GetProperties()
    //     .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     return key.Name;
    // }

    // public static object GetKeyLabel(this IBaseEntity entity)
    // {
    //     PropertyInfo key = entity.GetType().GetProperties()
    //     .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     return key.GetValue(entity)!;
    // }

    // public static string GetKeyType(this IBaseEntity entity)
    // {
    //     PropertyInfo key = entity.GetType().GetProperties()
    //     .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     var type = key.GetType();
    //     return type.ToString();
    // }

}

