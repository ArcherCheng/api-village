using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public abstract class BaseEntity
{
    public virtual string GetKeyType()
    {
        return "int";
        // return "string";
        // return "guid";
        // return "datetime";
        // return "long";
    }

    // public virtual T? GetKeyValue<T>()
    // {
    //     T? t = default;
    //     return t;
    // }

    // public virtual object GetPropertyValue(string name)
    // {
    //     return this.GetType().GetProperty(name)?.GetValue(this)!;
    // }

    // public virtual string GetPropertyType(string name)
    // {
    //     return this.GetType().GetProperty(name)?.GetType().Name!;
    // }


    // public string GetKeyName()
    // {
    //     //找不到，因為是用 database first 產生的 Model，上面沒有 [Key] 的定義。
    //     PropertyInfo key = this.GetType().GetProperties()
    //         .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     return key.Name;
    // }

    // public object GetKeyLabel()
    // {
    //     //找不到，因為是用 database first 產生的 Model，上面沒有 [Key] 的定義。
    //     PropertyInfo key = this.GetType().GetProperties()
    //         .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     return key.GetValue(this)!;
    // }

    // public string GetKeyType()
    // {
    //     //找不到，因為是用 database first 產生的 Model，上面沒有 [Key] 的定義。
    //     PropertyInfo key = this.GetType().GetProperties()
    //     .FirstOrDefault(x => x.GetCustomAttributes().Any(a =>((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null ))!;
    //     if (key==null) {
    //         return "Key Attribute Not Definded";
    //     }
    //     var type = key.GetType();
    //     return type.ToString();
    // }

}

public struct IdType
{
    public string stringId;
    public int intId;
    public long longId;
    public Guid guidId;
    public DateTime dtId;
}

