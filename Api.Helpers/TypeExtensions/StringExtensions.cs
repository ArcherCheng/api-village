// https://learn.microsoft.com/en-us/dotnet/api/system.string.replace?view=net-10.0
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Helpers;

public static partial class StringExtensions
{
    #region ToType Extensions Functions
    public static int ToInt(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return 0;
        }
        if (int.TryParse(source, out int result)) {
            return result;
        }
        if (decimal.TryParse(source, out decimal numValue)) {
            return (int)numValue;
        }
        return 0;
    }

    public static decimal ToDecimal(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return 0.0m;
        }
        if (decimal.TryParse(source, out decimal result)) {
            return result;
        } else {
            return 0.0m;
        }
    }


    public static Guid? ToGuid(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return null;
        }
        if (Guid.TryParse(source, out Guid guid))
        {
            return guid;
        }
        return null;
    }

    public static Boolean ToBoolean(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return false;
        }
        string value = source!.Trim().ToUpper();
        if (value=="1" || value=="T" || value=="Y" || value=="A" || value=="TRUE" || value=="YES" || value=="ON" || value=="真" || value=="是" || value=="有") {
            return true;
        }
        if (Decimal.TryParse(source, out decimal numValue)) {
            if (numValue > 0) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 大寫開始
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToPascal(this string? source)
    {
        if (string.IsNullOrEmpty(source))
            return "";
        return string.Concat(source[..1].ToUpper(), source[1..]);
    }

    /// <summary>
    /// 小寫開始
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToCamel(this string? source)
    {
        if (string.IsNullOrEmpty(source))
            return "";
        return string.Concat(source[..1].ToLower(), source[1..]);
    }
    #endregion

    #region String Compare
    public static bool IsGreaterThan(this string? source, string? compareStr
        , StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (source == null || compareStr == null)
        {
            return false;
        }
        return string.Compare(source, compareStr, comparison) > 0;
    }

    public static bool IsGreaterThanOrEqual(this string? source, string? compareStr
        , StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (source == null || compareStr == null)
        {
            return compareStr == source;
        }
        return string.Compare(source, compareStr, comparison) >= 0;
    }

    public static bool IsLessThan(this string? source, string? compareStr
        , StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (source == null || compareStr == null)
        {
            return false;
        }
        return string.Compare(source, compareStr, comparison) < 0;
    }

    public static bool IsLessThanOrEqual(this string? source, string? compareStr
        , StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (source == null || compareStr == null)
        {
            return compareStr == source;
        }
        return string.Compare(source, compareStr, comparison) <= 0;
    }


    #endregion

    #region Regex
    public static bool IsMatchPattern(this string? source, string? regPattern)
    {
        if (string.IsNullOrWhiteSpace(source)|| string.IsNullOrWhiteSpace(regPattern))
            return false;
        return Regex.IsMatch(source, regPattern);
    }

    public static string GetMatchPattern(this string? source, string? regPattern)
    {
        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(regPattern))
            return "";
        return Regex.Match(source, regPattern).Value;
    }

    // private static partial Regex MyEmailRegex();
    // [GeneratedRegex(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$")]
    public static bool IsEmail(this string? source)
    {
        if(string.IsNullOrWhiteSpace(source)) return false;

        // var reg = MyEmailRegex();
        // return reg.IsMatch(source);

        var reg2 = new Regex(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$");
        return reg2.IsMatch(source);
    }
    #endregion

    #region is Type
    public static bool IsInt(string? source)
    {
        return int.TryParse(source, out int _);
    }
    public static bool IsDecimal(string? source)
    {
        return decimal.TryParse(source, out decimal _);
    }

    public static bool IsDouble(string? source)
    {
        return double.TryParse(source, out double _);
    }

    public static bool IsNumeric(string? source)
    {
        return decimal.TryParse(source, out decimal _);
    }

    public static bool IsBoolean(string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return false;
        }
        string value = source!.Trim().ToUpper();
        if (value=="1" || value=="T" || value=="Y" || value=="A" || value=="TRUE" || value=="YES" || value=="ON" || value=="真" || value=="是" || value=="有") {
            return true;
        }
        if (Decimal.TryParse(source, out decimal numValue)) {
            if (numValue > 0) {
                return true;
            }
        }
        return false;
    }

    public static bool IsGuid(string? source)
    {
        return Guid.TryParse(source, out Guid _);
    }

    #endregion



    #region parse reference string
    /// <summary>
    /// 字串拆解，"hello;world"，拆解傳回 hello，保留 world
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ParseString(ref string? source,string splitChar=";")
    {
        if (string.IsNullOrWhiteSpace(source)){
            source = "";
            return "";
        }

        string result;
        int pos = source!.IndexOf(splitChar);
        if (pos >= 0) {
            result = source[..pos];
            source = source[(pos + splitChar.Length)..];
        } else {
            result = source;
            source = "";
        }
        return result.Trim();
    }

    /// <summary>
    /// 取得部分字串,並把原始字串減除。
    /// Pr01 = SplitByLen("Pr01+Pr02+Pr03+Pr04",4)  ==>  +Pr02+Pr03+Pr03+Pr04
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ParseLenth(ref string? source, int len=1)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            source = "";
            return "";
        }

        string result;
        if (source.Length < len)
        {
            result = source;
            source = "";
        }
        else
        {
            result = source[..len];
            source = source[len..];
        }
        return result.Trim();
    }

    // public static string ParseRemoveString(ref string? source, string? removeStr)
    // {
    //     if (string.IsNullOrWhiteSpace(source))
    //     {
    //         return "";
    //     }
    //     if (string.IsNullOrEmpty(removeStr))
    //     {
    //         return source;
    //     }
    //     int pos = source.IndexOf(removeStr);
    //     if (pos >=0 )
    //     {
    //         source = source.Remove(pos, removeStr.Length);
    //     }
    //     return source;
    // }

    // public static string RemoveString(this string? source, string? removeStr)
    // {
    //     if (string.IsNullOrWhiteSpace(source))
    //     {
    //         return "";
    //     }
    //     if (string.IsNullOrEmpty(removeStr))
    //     {
    //         return source;
    //     }
    //     int pos = source.IndexOf(removeStr);
    //     if (pos >=0 )
    //     {
    //         source = source.Remove(pos, removeStr.Length);
    //     }
    //     return source;
    // }

    #endregion

    /// <summary>
    /// "078003,083001,099110,103005" => "'078003','083001','099110','103005'"
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string AddSqlSingleQuote(string? source,string splitChar=",")
    {
        string result = "";
        if (string.IsNullOrWhiteSpace(source)){
            return result;
        }
        // 已經包含單引號，表示已經處理過了，直接回傳
        if (source.Contains('\'', StringComparison.CurrentCulture)){
            return source;
        }

        var arr = source.Split(splitChar);
        foreach (var item in arr)
        {
            result += $",'{item.Trim()}'";
        }
        return result[1..];
    }

    #region  身分證字號
    /// <summary>
    /// 驗證身分證字號
    /// H120955737=>17+120955737 * 19876543211 % 10 == 0
    /// H=17
    /// (1)英文代號以下表轉換成數字
    /// A = 10 台北市
    /// B = 11 台中市
    /// C = 12 基隆市
    /// D = 13 台南市
    /// E = 14 高雄市
    /// F = 15 台北縣
    /// G = 16 宜蘭縣
    /// H = 17 桃園縣
    /// J = 18 新竹縣
    /// K = 19 苗栗縣
    /// L = 20 台中縣
    /// M = 21 南投縣
    /// N = 22 彰化縣
    /// P = 23 雲林縣
    /// Q = 24 嘉義縣
    /// S = 26 高雄縣
    /// T = 27 屏東縣
    /// U = 28 花蓮縣
    /// V = 29 台東縣
    /// W = 32 金門縣
    /// X = 30 澎湖縣
    /// Y = 31 陽明山
    /// Z = 33 連江縣
    /// I = 34 嘉義市
    /// O = 35 新竹市
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsTaiwanId(string? source)
    {
        if (string.IsNullOrWhiteSpace(source) || source!.Length != 10)
        {
            return false;
        }
        source = source.ToUpper();
        //////////////////01234567890123456789012345
        string letters = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
        // H120955737 => 17 120955737
        string digitId = (letters.IndexOf(source[0]) + 10).ToString()+ source[1..];
        int[] weights = [1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1];
        int sum = 0;
        for (int i = 0; i <= 10; i++)
        {
            sum += (digitId[i] - '0') * weights[i];  //digitId[i] - '0' ascii char to int
        }
        return sum % 10 == 0;
    }

    #endregion

}

