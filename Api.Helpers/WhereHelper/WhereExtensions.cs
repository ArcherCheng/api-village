/*
https://entityframeworkcore.com/knowledge-base/59715659/linq-to-sql-generates-a-string-comparison
https://www.c-sharpcorner.com/UploadFile/c42694/dynamic-query-in-linq-using-predicate-builder/
http://www.albahari.com/nutshell/predicatebuilder.aspx
https://petemontgomery.wordpress.com/2011/02/10/a-universal-predicatebuilder/
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Helpers; 

#nullable disable
public class WhereExtensions
{
    public static Expression<Func<T, bool>> True<T>() { return x => true; }

    public static Expression<Func<T, bool>> BuildComIdExpression<T>(Guid comId)
    {
        ParameterExpression paramExpr = Expression.Parameter(typeof(T), "p");
        MemberExpression leftProperty = Expression.Property(paramExpr, "ComId");
        ConstantExpression constValue = Expression.Constant(comId);
        Expression<Func<T, bool>> comIdExpression = Expression.Lambda<Func<T, bool>>(Expression.Equal(leftProperty, constValue), paramExpr);
        return comIdExpression;
    }


    public static Expression<Func<T, bool>> BuildWhereExpression<T>(List<WhereCondition> conditionList)
    {
        Expression<Func<T, bool>> whereExpressionResult = null;
        if (conditionList == null || conditionList.Count == 0) {
            return WhereExtensions.True<T>();
        } 
        int bracket = 1;
        int maxBracket = conditionList.Max(x => x.BracketOr);
        while (true)
        {
            List<WhereCondition> conditionListGroup = new List<WhereCondition>();
            foreach (var item in conditionList)
            {
                if (item.BracketOr == bracket) {
                    conditionListGroup.Add(item);
                }
            }
            if (conditionListGroup.Count > 0 ) {
                var expression =  BuildWhereExpressionAtSameBracket<T>(conditionListGroup);
                if (whereExpressionResult == null) {
                    whereExpressionResult = expression;
                } else {
                    whereExpressionResult = whereExpressionResult.OrElse(expression);
                }
            }
            bracket ++;
            if (bracket > maxBracket) {
                break;
            }
        }
        return whereExpressionResult;
    }

    private static Expression<Func<T, bool>> BuildWhereExpressionAtSameBracket<T>(List<WhereCondition> conditionList)
    {
        List<Expression<Func<T, bool>>> whereExpressionAndList = new List<Expression<Func<T, bool>>>();
        List<Expression<Func<T, bool>>> whereExpressionOrList = new List<Expression<Func<T, bool>>>();
        if (conditionList == null || conditionList.Count == 0) {
            return WhereExtensions.True<T>();
        } 

        try
        {
            for (int i = 0; i < conditionList.Count; i++)
            {
                Expression<Func<T, bool>> resultExpression = null;
                WhereCondition item = conditionList[i];
                
                if (item.Type.ToLower() == "string" || item.Type.ToLower() == "s" ) {
                    resultExpression = CreateStringLambda<T>(item);
                } else {
                    resultExpression = CreateNumericLambda<T>(item);
                }

                if ("and".Equals(item.AndOr.ToLower())) {
                    whereExpressionAndList.Add(resultExpression);
                } else {
                    whereExpressionOrList.Add(resultExpression);
                }
            }
        }
        catch (System.Exception )
        {
            throw ;
        }

        Expression<Func<T, bool>> whereExpressionAnd = null;
        foreach (var item in whereExpressionAndList)
        {
            if (whereExpressionAnd == null) {
                whereExpressionAnd = item;
            } else {
                whereExpressionAnd = whereExpressionAnd.AndAlso(item);
            }
        }

        Expression<Func<T, bool>> whereExpressionOr = null;
        foreach (var item in whereExpressionOrList)
        {
            if (whereExpressionOr == null) {
                whereExpressionOr = item;
            } else {
                whereExpressionOr = whereExpressionOr.OrElse(item);
            }
        }

        Expression<Func<T, bool>> whereExpressionResult;
        if (whereExpressionAnd == null) {
            if (whereExpressionOr == null) {
                return WhereExtensions.True<T>();
            } else {
                whereExpressionResult = whereExpressionOr;
            }
        } else {
            if (whereExpressionOr == null) {
                whereExpressionResult = whereExpressionAnd;
            } else {
                whereExpressionResult = whereExpressionAnd.AndAlso(whereExpressionOr);
            }
        }
        return whereExpressionResult; 
    }

    public static Expression<Func<T, bool>> CreateStringLambda<T>(WhereCondition condition)
    {
        Expression expression;             
        MethodInfo methodInfo;
        MethodCallExpression callExpression;
        UnaryExpression notExpression;

        ParameterExpression paramExpr = Expression.Parameter(typeof(T), "p");
        MemberExpression leftProperty = Expression.Property(paramExpr, condition.Field.ToPascal());
        ConstantExpression rightValue = Expression.Constant(condition.Value);
        switch (condition.Operator.ToLower())
        {
            case "==":
            case "=":
                expression = Expression.Equal(leftProperty, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<>":
            case "!=":
                expression = Expression.NotEqual(leftProperty, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case ">=":
                Expression<Func<string>> strLambda = () => condition.Value;
                MethodInfo callMethod = typeof(string).GetMethod("CompareTo", new[]{typeof(string)});
                Expression callExpr = Expression.Call(leftProperty, callMethod, strLambda.Body);
                expression = Expression.GreaterThanOrEqual(callExpr, Expression.Constant(0));
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<=":
                strLambda = () => condition.Value;
                callMethod = typeof(string).GetMethod("CompareTo", new[]{typeof(string)});
                callExpr = Expression.Call(leftProperty, callMethod, strLambda.Body);
                expression = Expression.LessThanOrEqual(callExpr, Expression.Constant(0));
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case ">":
                strLambda = () => condition.Value;
                callMethod = typeof(string).GetMethod("CompareTo", new[]{typeof(string)});
                callExpr = Expression.Call(leftProperty, callMethod, strLambda.Body);
                expression = Expression.GreaterThan(callExpr, Expression.Constant(0));
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<":
                strLambda = () => condition.Value;
                callMethod = typeof(string).GetMethod("CompareTo", new[]{typeof(string)});
                callExpr = Expression.Call(leftProperty, callMethod, strLambda.Body);
                expression = Expression.LessThan(callExpr, Expression.Constant(0));
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "%":
            case "like":
                methodInfo = typeof(string).GetMethod("Contains", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                return Expression.Lambda<Func<T, bool>>(callExpression,paramExpr);
            case "!%":
            case "!like":
            case "not like":
                methodInfo = typeof(string).GetMethod("Contains", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                notExpression = Expression.Not(callExpression);
                return Expression.Lambda<Func<T, bool>>(notExpression, paramExpr);
            case "in":
                methodInfo = typeof(string).GetMethod("Contains", new Type[] {typeof(string)});
                callExpression = Expression.Call(rightValue, methodInfo, leftProperty);
                return Expression.Lambda<Func<T, bool>>(callExpression,paramExpr);
            case "out":
            case "!in":
            case "not in":
                methodInfo = typeof(string).GetMethod("Contains", new Type[] {typeof(string)});
                callExpression = Expression.Call(rightValue, methodInfo, leftProperty);
                notExpression = Expression.Not(callExpression);
                return Expression.Lambda<Func<T, bool>>(notExpression,paramExpr);
            case "^":
            case "start":
                methodInfo = typeof(string).GetMethod("StartsWith", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                return Expression.Lambda<Func<T, bool>>(callExpression,paramExpr);
            case "!^":
            case "!start":
            case "not start":
                methodInfo = typeof(string).GetMethod("StartsWith", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                notExpression = Expression.Not(callExpression);
                return Expression.Lambda<Func<T, bool>>(notExpression, paramExpr);
            case "$":
            case "end":
                methodInfo = typeof(string).GetMethod("EndsWith", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                return Expression.Lambda<Func<T, bool>>(callExpression,paramExpr);
            case "!$":
            case "!end":
            case "not end":
                methodInfo = typeof(string).GetMethod("EndsWith", new Type[] {typeof(string)});
                callExpression = Expression.Call(leftProperty, methodInfo, rightValue);
                notExpression = Expression.Not(callExpression);
                return Expression.Lambda<Func<T, bool>>(notExpression, paramExpr);
            default:
                expression = Expression.Equal(leftProperty, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
        }
    }

    public static Expression<Func<T, bool>> CreateNumericLambda<T>(WhereCondition condition)
    {
        Expression expression;
        ParameterExpression paramExpr = Expression.Parameter(typeof(T), "p");
        MemberExpression leftProperty = Expression.Property(paramExpr, condition.Field.ToPascal());
        bool isNullable = leftProperty.Type.IsGenericType && leftProperty.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
        Type nonNullableType = null;
        ConstantExpression rightValue = GetConstantExpression(condition, isNullable, out nonNullableType);
        UnaryExpression leftMember = Expression.Convert(leftProperty, nonNullableType);

        switch (condition.Operator)
        {
            case "==":
            case "=":
                expression = Expression.Equal(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<>":
            case "!=":
                expression = Expression.NotEqual(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case ">":
                expression = Expression.GreaterThan(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<":
                expression = Expression.LessThan(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case ">=":
                expression = Expression.GreaterThanOrEqual(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            case "<=":
                expression = Expression.LessThanOrEqual(leftMember, rightValue);
                return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            // case "null":
            //     expression = Expression.Equal(leftMember, rightValue);
            //     return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            // case "!null":
            //     expression = Expression.NotEqual(leftMember, rightValue);
            //     return Expression.Lambda<Func<T, bool>>(expression, paramExpr);
            default:
                return null;
        }
    }

    private static bool IsNullableType(Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    private static Expression GetPropertyExpression(ParameterExpression paramExpr, string propertyName)
    {
        // Again, check for dot notation - the property expression will differ
        if (propertyName.Contains("."))
        {
            // Check for collections
            var properties = propertyName.Split(".");
            var firstProp = Expression.Property(paramExpr, properties[0]);
            var isCollection = typeof(List<>).IsAssignableFrom(paramExpr.Type);
            if(isCollection)
            {
                throw new NotImplementedException("Currently cannot across child collection");
            }
            if(isCollection)
            {
                return properties.Aggregate(paramExpr, (Expression parent, string path) => Expression.Property(paramExpr, path));
            }
            else
            {
                return properties.Aggregate(paramExpr, (Expression parent, string path) => Expression.Property(paramExpr, path));
            }
        }
        else
        {
            return Expression.Property(paramExpr, propertyName);
        }
    }

    private static ConstantExpression GetConstantExpression(WhereCondition condition, bool isNullable, out Type nonNullableType) 
    {
        ConstantExpression constantExpr = null;
        switch (condition.Type.ToLower())
        {
            case "b":
            case "boolean":
            case "bool":
            case "bit":
                if (isNullable){
                    nonNullableType = typeof(bool?);
                } else {
                    nonNullableType =  typeof(bool);
                }
                if (condition.Value == "1" || condition.Value == "true") {
                    constantExpr = Expression.Constant(true, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(false, nonNullableType);
                }
                break;
            case "n":
            case "number":
            case "long":
            case "int":
                if (isNullable) {
                    nonNullableType = typeof(int?);
                } else {
                    nonNullableType =  typeof(int);
                }
                if (int.TryParse(condition.Value, out int intValue)) {
                    constantExpr = Expression.Constant(intValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(0, nonNullableType);
                }
                break;
            case "double":
                if (isNullable) {
                    nonNullableType = typeof(double?); 
                } else {
                    nonNullableType =  typeof(double);
                }
                if (double.TryParse(condition.Value, out double dobValue)) {
                    constantExpr = Expression.Constant(dobValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(0, nonNullableType);
                }
                break;
            case "dec":
            case "decimal":
                if (isNullable) {
                    nonNullableType = typeof(decimal?);
                } else {
                    nonNullableType =  typeof(decimal);
                }
                if (decimal.TryParse(condition.Value, out decimal decValue)) {
                    constantExpr = Expression.Constant(decValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(0, nonNullableType);
                }
                break;
            case "do":
            case "dateonly":
                if (isNullable) {
                    nonNullableType = typeof(DateOnly?);
                } else {
                    nonNullableType =  typeof(DateOnly);
                }
                if (DateOnly.TryParse(condition.Value, out DateOnly dateOnlyValue)) {
                    constantExpr = Expression.Constant(dateOnlyValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(null, nonNullableType);
                }                
                break;
            case "d":
            case "datetime":
            case "date":
                if (isNullable) {
                    nonNullableType = typeof(DateTime?);
                } else {
                    nonNullableType =  typeof(DateTime);
                }
                if (DateTime.TryParse(condition.Value, out DateTime dateValue)) {
                    constantExpr = Expression.Constant(dateValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(null, nonNullableType);
                }
                break;
            case "u":
            case "g":
            case "uuid":
            case "guid":
                if (isNullable) {
                    nonNullableType = typeof(Guid?);
                } else {
                    nonNullableType =  typeof(Guid);
                }
                if (Guid.TryParse(condition.Value, out Guid guidValue)) {
                    constantExpr = Expression.Constant(guidValue, nonNullableType);
                } else {
                    constantExpr = Expression.Constant(Guid.Empty, nonNullableType);
                }
                break;
            default:
                throw new NotImplementedException(condition.Type + " condition type error ");
        }
        return constantExpr;
    }
}

static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var paramExpr = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], paramExpr);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], paramExpr);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), paramExpr);
    }

    public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var paramExpr = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], paramExpr);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], paramExpr);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), paramExpr);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}