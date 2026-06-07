using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> IncludeAll<T>(this IQueryable<T> source) where T : class
    {
        var type = typeof(T);
        var properties = type.GetProperties(); 
        foreach (var property in properties)
        {
            var isVirtual = property.GetGetMethod()?.IsVirtual;
            if (isVirtual==true && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
            {
                source = source.Include(property.Name);
            }
        }
        return source;
    }

    public static IQueryable<T> IncludeTable<T>(this IQueryable<T> source, string includeTable) where T : class
    {
        source = source.Include(includeTable);
        return source;
    }

    public static IQueryable<T> OrderByCustom<T>(this IQueryable<T> source, string orderByColumn, bool isAscending)
    {
        try
        {
            var type = typeof(T);
            var para = Expression.Parameter(type, "p");
            var property = type.GetProperty(orderByColumn.ToPascal())!;
            var propertyAccess = Expression.MakeMemberAccess(para, property);
            var orderByExpr = Expression.Lambda(propertyAccess, para);
            var orderBy = "OrderBy";
            if (!isAscending)
                orderBy = "OrderByDescending";

            MethodCallExpression resultExpr = Expression.Call(
                typeof(Queryable)
                , orderBy
                , new Type[] { type, property.PropertyType }
                , source.Expression
                , Expression.Quote(orderByExpr));

            return source.Provider.CreateQuery<T>(resultExpr);

        }
        catch (System.Exception)
        {
            return source;
        }
    }

    public static IQueryable<T> ThenByCustom<T>(this IQueryable<T> source, string orderByColumn, bool isAscending)
    {
        try
        {
            var type = typeof(T);
            var para = Expression.Parameter(type, "p");
            var property = type.GetProperty(orderByColumn.ToPascal())!;
            var propertyAccess = Expression.MakeMemberAccess(para, property);
            var orderByExpr = Expression.Lambda(propertyAccess, para);
            var thenBy = "ThenBy";
            if (!isAscending)
                thenBy = "ThenByDescending";

            MethodCallExpression resultExpr = Expression.Call(
                typeof(Queryable)
                , thenBy
                , new Type[] { type, property.PropertyType }
                , source.Expression
                , Expression.Quote(orderByExpr));

            return source.Provider.CreateQuery<T>(resultExpr);
        }
        catch (System.Exception)
        {
            return source;
        }
    }

    public static IQueryable<T> OrderByCustom2<T>(this IQueryable<T> source, string sortBy)
    {
        var type = typeof(T);
        var para = Expression.Parameter(type);
        //var memberInfo = type.GetMember(property)[0];
        var memberInfo = type.GetProperty(sortBy.ToPascal())!;
        var memberExp = Expression.MakeMemberAccess(para, memberInfo);
        var propertyType = type.GetProperty(sortBy)!.PropertyType;

        if (propertyType.IsEnum)
        {
            var asExpr = Expression.Convert(memberExp, typeof(int));
            return source.OrderBy(Expression.Lambda<Func<T, int>>(asExpr, para));
        }
        if (propertyType == typeof(string))
        {
            return source.OrderBy(Expression.Lambda<Func<T, string>>(memberExp, para));
        }
        if (propertyType == typeof(DateTime))
        {
            return source.OrderBy(Expression.Lambda<Func<T, DateTime>>(memberExp, para));
        }
        if (propertyType == typeof(DateTime?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, DateTime?>>(memberExp, para));
        }

        if (propertyType == typeof(int))
        {
            return source.OrderBy(Expression.Lambda<Func<T, int>>(memberExp, para));
        }
        if (propertyType == typeof(int?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, int?>>(memberExp, para));
        }

        if (propertyType == typeof(decimal))
        {
            return source.OrderBy(Expression.Lambda<Func<T, decimal>>(memberExp, para));
        }
        if (propertyType == typeof(decimal?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, decimal?>>(memberExp, para));
        }

        if (propertyType == typeof(bool))
        {
            return source.OrderBy(Expression.Lambda<Func<T, bool>>(memberExp, para));
        }
        if (propertyType == typeof(bool?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, bool?>>(memberExp, para));
        }

        if (propertyType == typeof(double))
        {
            return source.OrderBy(Expression.Lambda<Func<T, double>>(memberExp, para));
        }
        if (propertyType == typeof(double?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, double?>>(memberExp, para));
        }

        if (propertyType == typeof(float))
        {
            return source.OrderBy(Expression.Lambda<Func<T, float>>(memberExp, para));
        }
        if (propertyType == typeof(float?))
        {
            return source.OrderBy(Expression.Lambda<Func<T, float?>>(memberExp, para));
        }

        throw new Exception("Unsupported data type：" + propertyType);
    }

    public static IQueryable<T> OrderByDescendingCustom2<T>(this IQueryable<T> source, string sortBy)
    {
        var type = typeof(T);
        var para = Expression.Parameter(type);
        //var memberInfo = type.GetMember(property)[0];
        var memberInfo = type.GetProperty(sortBy.ToPascal())!;
        var memberExp = Expression.MakeMemberAccess(para, memberInfo);
        var propertyType = type.GetProperty(sortBy)!.PropertyType;

        if (propertyType.IsEnum)
        {
            var asExpr = Expression.Convert(memberExp, typeof(int));
            return source.OrderByDescending(Expression.Lambda<Func<T, int>>(asExpr, para));
        }
        if (propertyType == typeof(string))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, string>>(memberExp, para));
        }
        if (propertyType == typeof(DateTime))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, DateTime>>(memberExp, para));
        }
        if (propertyType == typeof(DateTime?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, DateTime?>>(memberExp, para));
        }

        if (propertyType == typeof(int))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, int>>(memberExp, para));
        }
        if (propertyType == typeof(int?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, int?>>(memberExp, para));
        }

        if (propertyType == typeof(decimal))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, decimal>>(memberExp, para));
        }
        if (propertyType == typeof(decimal?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, decimal?>>(memberExp, para));
        }

        if (propertyType == typeof(bool))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, bool>>(memberExp, para));
        }
        if (propertyType == typeof(bool?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, bool?>>(memberExp, para));
        }

        if (propertyType == typeof(double))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, double>>(memberExp, para));
        }
        if (propertyType == typeof(double?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, double?>>(memberExp, para));
        }

        if (propertyType == typeof(float))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, float>>(memberExp, para));
        }
        if (propertyType == typeof(float?))
        {
            return source.OrderByDescending(Expression.Lambda<Func<T, float?>>(memberExp, para));
        }

        throw new Exception("Unsupported data type：" + propertyType);
    }
}
