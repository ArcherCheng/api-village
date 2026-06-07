using System;
using System.CodeDom;
using System.Collections;

namespace Api.Helpers;

/*
tips:
// DateOnly dateOnly = DateOnly.FromDateTime(source.Value);
// DateTime dateTime = dateOnly.ToDateTime(new TimeOnly(0, 0, 0));
*/
public static class DateTimeExtensions
{
    #region ToDate functions

    public static string? ToIsoDate<T>(this T? source)
    {
        if (source==null) {
            return null;
        }
        switch (typeof(T).Name)
        {
            case "DateTime":
                return ((DateTime)(object)source).ToString("yyyy-MM-dd");
            case "DateOnly":
                //return ((DateOnly)(object)source).ToDateTime(new TimeOnly(0,0)).ToString("yyyy-MM-dd");
                return ((DateOnly)(object)source).ToString("yyyy-MM-dd");
            case "String":
            default:
                var tuples = ParseDateStringYearMonthDay(source.ToString());
                if (tuples == null) {
                    return null;
                }
                return new DateTime(tuples.Value.year,tuples.Value.month,tuples.Value.day).ToString("yyyy-MM-dd");
        }
    }

    public static DateTime? ToDatetime<T>(this T? source)
    {
        if (source==null) {
            return null;
        }
        switch (typeof(T).Name)
        {
            case "DateTime":
                return (DateTime)(object)source;
            case "DateOnly":
                return ((DateOnly)(object)source).ToDateTime(new TimeOnly(0,0));
            case "String":
            default:
                var tuples = ParseDateStringYearMonthDay(source.ToString());
                if (tuples == null) {
                    return null;
                }
                return new DateTime(tuples.Value.year,tuples.Value.month,tuples.Value.day,tuples.Value.hour,tuples.Value.minute,tuples.Value.second);
        }

    }

    public static DateTime? ToDatetimeEnd<T>(this T? source)
    {
        if (source==null) {
            return null;
        }
        switch (typeof(T).Name)
        {
            case "DateTime":
                return ((DateTime)(object)source).AddHours(23).AddMinutes(59).AddSeconds(59);
            case "DateOnly":
                return ((DateOnly)(object)source).ToDateTime(new TimeOnly(23,59,59));
            case "String":
            default:
                var sourceString = source.ToString();
                if (string.IsNullOrWhiteSpace(sourceString)) {
                    return null;
                }
                try
                {
                    var tuples = ParseDateStringYearMonthDay(sourceString);
                    if (tuples == null) {
                        return null;
                    }
                    return new DateTime(tuples.Value.year,tuples.Value.month,tuples.Value.day,23,59,59);
                }
                catch (Exception)
                {
                    return null;
                }
        }
    }

    public static DateOnly? ToDateOnly<T>(this T? source)
    {
        if (source==null) {
            return null;
        }
        switch (typeof(T).Name)
        {
            case "DateTime":
                return DateOnly.FromDateTime((DateTime)(object)source);
            case "DateOnly":
                return (DateOnly)(object)source;
            case "String":
            default:
                var tuples = ParseDateStringYearMonthDay(source.ToString());
                if (tuples == null) {
                    return null;
                }
                return new DateOnly(tuples.Value.year,tuples.Value.month,tuples.Value.day);
        }

    }

    /// <summary>
    /// 中華民國110年12月23日
    /// </summary>
    public static string? ToTaiwanDate<T>(this T? source)
    {
        if (source == null) {
            return null;
        }
        string twdate;
        switch (typeof(T).Name)
        {
            case "DateTime":
                var tempDate = (DateTime)(object)source;
                twdate = $"中華民國{tempDate.Year - 1911}年{tempDate.Month}月{tempDate.Day}日";
                return twdate;
            case "DateOnly":
                var onlyDate = (DateOnly)(object)source;
                twdate = $"中華民國{onlyDate.Year - 1911}年{onlyDate.Month}月{onlyDate.Day}日";
                return twdate;
            case "String":
            default:
                var tuples = ParseDateStringYearMonthDay(source.ToString());
                if (tuples == null) {
                    return null;
                }
                twdate = $"中華民國{tuples.Value.year - 1911}年{tuples.Value.month}月{tuples.Value.day}日";
                return twdate;
        }
    }

    /// <summary>
    /// return 115.12.23
    /// </summary>
    public static string? ToTwDate<T>(this T? source)
    {
        if (source == null) {
            return null;
        }
        string twdate;
        switch (typeof(T).Name)
        {
            case "DateTime":
                var tempDate = (DateTime)(object)source;
                twdate = $"{tempDate.Year - 1911:000}.{tempDate.Month:00}.{tempDate.Day:00}";
                return twdate;
            case "DateOnly":
                var onlyDate = (DateOnly)(object)source;
                twdate = $"{onlyDate.Year - 1911:000}.{onlyDate.Month:00}.{onlyDate.Day:00}";
                return twdate;
            case "String":
            default:
                var tuples = ParseDateStringYearMonthDay(source.ToString());
                if (tuples == null) {
                    return null;
                }
                twdate = $"{tuples.Value.year - 1911:000}.{tuples.Value.month:00}.{tuples.Value.day:00}";
                return twdate;
        }
    }

    public static (int year,int month,int day,int hour,int minute,int second)? ParseDateStringYearMonthDay(string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return null;
        }
        int year=0,month=0,day=0,hour=0,minute=0,second=0;
        char[] keywords = ['/','-','.',' ','T',':'];
        bool hasKeywords = keywords.Any(x=>source!.Contains(x));
        if (hasKeywords) {
            var dateArrs = source!.Split(keywords);
            if (dateArrs.Length > 0 && !int.TryParse(dateArrs[0],out year)){
                year = 0;
            }
            if (dateArrs.Length > 1 && !int.TryParse(dateArrs[1],out month)){
                month=0;
            }
            if (dateArrs.Length > 2 && !int.TryParse(dateArrs[2],out day)){
                day=0;
            }
            if (dateArrs.Length > 3 && !int.TryParse(dateArrs[3],out hour)){
                hour = 0;
            }
            if (dateArrs.Length > 4 && !int.TryParse(dateArrs[4],out minute)){
                minute = 0;
            }
            if (dateArrs.Length > 5 && !int.TryParse(dateArrs[5],out second)){
                second = 0;
            }
        } else {
            //only date string without any keywords, try to parse by length
            if(source!.Length == 6 ){
                //短西元年:"yyMMdd"
                if (!int.TryParse(source!.AsSpan(0, 2),out year)){
                   year = 0;
                } else {
                    if (year >= 50){
                        year += 1900;
                    } else {
                        year += 2000;
                    }
                }
                if (!int.TryParse(source!.AsSpan(2, 2),out month)){
                    month=0;
                }
                if (!int.TryParse(source!.AsSpan(4, 2),out day)){
                    day=0;
                }
            } else if(source!.Length == 7){
                //中華民國年:0531206,0421206
                if (!int.TryParse(source!.AsSpan(0, 3),out year)){
                    year = 0;
                } else
                {
                    year += 1911;
                }
                if (!int.TryParse(source!.AsSpan(3, 2),out month)){
                    month=0;
                }
                if (!int.TryParse(source!.AsSpan(5, 2),out day)){
                    day=0;
                }
            } else if(source!.Length == 8){
                //長西元年:"yyyyMMdd"
                if (!int.TryParse(source!.AsSpan(0, 4),out year)){
                    year = 0;
                }
                if (!int.TryParse(source!.AsSpan(4, 2),out month)){
                    month=0;
                }
                if (!int.TryParse(source!.AsSpan(6, 2),out day)){
                    day=0;
                }
            }
        }

        try
        {
            var date = new DateTime(year, month, day, hour, minute, second);
            return (year,month,day,hour,minute,second);
        }
        catch (Exception)
        {
            return null;
        }
    }

    #endregion

    #region Is Date Functions
    public static bool IsWeekend<T>(this T? source)
    {
        if (source == null) {
            return false;
        }
        switch (typeof(T).Name)
        {
            case "DateTime":
                return ((DateTime)(object)source).DayOfWeek == DayOfWeek.Saturday || ((DateTime)(object)source).DayOfWeek == DayOfWeek.Sunday;
            case "DateOnly":
                return ((DateOnly)(object)source).DayOfWeek == DayOfWeek.Saturday || ((DateOnly)(object)source).DayOfWeek == DayOfWeek.Sunday;
            case "String":
            default:
                var dateTime = ToDatetime(source.ToString());
                if (dateTime == null) {
                    return false;
                }
                return dateTime.Value.DayOfWeek == DayOfWeek.Saturday || dateTime.Value.DayOfWeek == DayOfWeek.Sunday;
        }
    }

    public static bool IsYearMonth(this string yearMonth)
    {
        if(yearMonth.Length != 7 || yearMonth[4] != '-')
            return false;

        if(yearMonth == "9999-12")
            return true;

        if (!int.TryParse(yearMonth.AsSpan(0,4),out int _year))
            return false;

        int currYear = System.DateTime.Now.Year;
        if (_year > currYear + 100 || _year < currYear - 100 )
            return false;

        if (!int.TryParse(yearMonth.AsSpan(5,2),out int _mm))
            return false;

        if (_mm > 12 || _mm < 1)
            return false;

        return true;
    }
    #endregion

    #region DateDiff Functions
    /// <summary>
    /// 計算兩個日期之間差幾年
    /// </summary>
    public static int DateDiffYears(this DateTime dateStart, DateTime? dateEnd2 = null, bool isOver = true)
    {
        DateTime dateEnd = DateTime.Today;
        if (dateEnd2 != null) {
            dateEnd = (DateTime)dateEnd2;
        }

        int years = dateEnd.Year - dateStart.Year;
        if (isOver) {
            if (dateStart.Date > dateEnd.AddYears(-years).AddDays(1)) years--;
        }

        return years;
    }

    public static int DateDiffYears(this DateOnly dateStart, DateOnly? dateEnd2 = null, bool isOver = true)
    {
        var date1 = dateStart.ToDateTime(new TimeOnly(0,0));
        DateTime? date2 = null;
        if (dateEnd2 != null) {
            date2 = dateEnd2.Value.ToDateTime(new TimeOnly(0,0));
        }
        return DateDiffYears(date1,date2,isOver);
    }

    /// <summary>
    /// 計算兩個日期之間的日數
    /// </summary>
    public static int DateDiffDays(this DateTime dateStart, DateTime dateEnd)
    {
        //2024/1/31 - 2024/1/1 = 31
        int days = (int)new TimeSpan(dateEnd.Ticks - dateStart.Ticks ).TotalDays + 1;
        return days;
    }

    public static int DateDiffDays(this DateOnly dateStart, DateOnly dateEnd)
    {
        //2024/1/31 - 2024/1/1 = 31
        int days = dateEnd.DayNumber - dateStart.DayNumber + 1;
        return days;
    }

    /// <summary>
    /// 計算兩個日期之間的分鐘數
    /// </summary>
    public static int DateDiffMins(this DateTime dateStart, DateTime dateEnd)
    {
        var mins = (int) new TimeSpan(dateEnd.Ticks - dateStart.Ticks ).TotalMinutes;
        return mins;
    }

    /// <summary>
    /// 計算兩個日期之間的月數
    /// </summary>
    public static int DateDiffMonths(this DateTime dateStart, DateTime dateEnd)
    {
        int months1 = dateStart.Year * 12 + dateStart.Month;
        int months2 = dateEnd.Year * 12 + dateEnd.Month;
        if (dateEnd.Day >= dateStart.Day) {
            return months2 - months1 + 1;
        } else {
            return months2 - months1;
        }
    }

    public static int DateDiffMonths(this DateOnly dateStart, DateOnly dateEnd)
    {
        var date1 = dateStart.ToDateTime(new TimeOnly(0,0));
        var date2 = dateEnd.ToDateTime(new TimeOnly(0,0));
        return DateDiffMonths(date1,date2);
    }

    /// <summary>
    /// 計算兩個日期之間的半月數
    /// </summary>
    public static int DateDiffHalfMonths(this DateTime dateStart, DateTime dateEnd)
    {
        int years = dateEnd.Year - dateStart.Year;
        int months = (dateEnd.Month - dateStart.Month +1 ) * 2;
        if (dateStart.Day > 15) {
            months -= 1;
        }
        if (dateEnd.Day <= 15) {
            months -= 1;
        }
        return years*24 + months;
    }

    public static int DateDiffHalfMonths(this DateOnly dateStart, DateOnly dateEnd)
    {
        var date1 = dateStart.ToDateTime(new TimeOnly(0,0));
        var date2 = dateEnd.ToDateTime(new TimeOnly(0,0));
        return DateDiffHalfMonths(date1,date2);
    }

    /// <summary>
    /// 計算兩個日期之間相差的(年,月,日)
    /// 例如: 2020-01-10.DateDiffYearsMonthsDays(2021-01-1) => 0,11,22
    /// </summary>
    public static (int years,int months, int days) DateDiffYearsMonthsDays(this DateTime source, DateTime dateEnd)
    {
        DateTime date1 = source;
        DateTime date2 = dateEnd;
        if (source > dateEnd) {
            date2 = source;
            date1 = dateEnd;
        }

        int years = date2.Year - date1.Year;
        int months = date2.Month - date1.Month;
        if (months < 0 ) {
            years -= 1;
            months += 12;
        }

        int days = date2.Day - date1.Day ;
        if(days < 0) {
            days = System.DateTime.DaysInMonth(date1.Year, date1.Month) - date1.Day + date2.Day;
            months -= 1;
            if (months < 0) {
                months = 11;
                years -= 1;
            }
        }
        return (years,months,days);
    }

    public static (int years,int months, int days) DateDiffYearsMonthsDays(this DateOnly source, DateOnly dateEnd)
    {
        var date1 = source.ToDateTime(new TimeOnly(0,0));
        var date2 = dateEnd.ToDateTime(new TimeOnly(0,0));
        return DateDiffYearsMonthsDays(date1,date2);
    }

    #endregion

    #region Get Date Functions
    /// <summary>
    /// translate 2022-01-18 to 202201
    /// </summary>
    public static string GetYearMonth(this DateTime source)
    {
        string yearMonth = source.ToString("yyyy-MM");
        return yearMonth;
    }

    public static string GetYearMonth(this DateOnly source)
    {
        string yearMonth = source.ToString("yyyy-MM");
        return yearMonth;
        //return GetYearMonth(source.ToDateTime(new TimeOnly(0,0)));
    }

    /// <summary>
    /// get first date of year month
    /// </summary>
    public static DateTime GetYearMonthFirstDate(this DateTime atDate)
    {
        int xyear = atDate.Year;
        int xmonth = atDate.Month;
        DateTime xdate = new(xyear,xmonth,1);
        return xdate;
    }

    public static DateTime GetYearMonthFirstDate(this DateOnly atDate)
    {
        return GetYearMonthFirstDate(atDate.ToDateTime(new TimeOnly(0,0)));
    }

    public static DateTime GetYearMonthFirstDate(this string yearMonth)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime xdate = new(xyear,xmonth,1);
        return xdate;
    }

    public static DateTime GetYearMonthLastDate(this DateTime atDate)
    {
        int xyear = atDate.Year;
        int xmonth = atDate.Month;
        DateTime xdate = new(xyear,xmonth,1);
        xdate = xdate.AddMonths(1).AddDays(-1);
        return xdate;
    }

    public static DateTime GetYearMonthLastDate(this DateOnly atDate)
    {
        return GetYearMonthLastDate(atDate.ToDateTime(new TimeOnly(0,0)));
    }

    public static DateTime GetYearMonthLastDate(this string yearMonth)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime xdate = new(xyear,xmonth,1);
        xdate = xdate.AddMonths(1).AddDays(-1);
        return xdate;
    }

    /// <summary>
    /// 取得本月結算截止日期，如:202401 25 = 2024-01-25
    /// </summary>
    public static DateTime GetYearMonthEndDate(this string yearMonth, int endDay=30)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        if (endDay >= 30) {
            DateTime xdate = new(xyear,xmonth,1);
            xdate = xdate.AddMonths(1).AddDays(-1);
            return xdate;
        } else {
            do {
                try {
                    DateTime xdate = new(xyear,xmonth,endDay);
                    return xdate;
                } catch (Exception) {
                    endDay--;
                }
            } while (true);
        }
    }

    /// <summary>
    /// 依每月最後結算日(如25)，拆出開始及結束日期(2024-01-26 2024-02-25)
    /// </summary>
    public static (DateTime BeginDate, DateTime EndDate ) GetYearMonthBeginEndDate(this string yearMonth, int endDay = 30)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime beginDate,endDate;
        if (endDay >= 30)
        {
            beginDate = new(xyear,xmonth,1);
            endDate = beginDate.AddMonths(1).AddDays(-1);
            return (beginDate,endDate);
        }
        do
        {
            try
            {
                endDate = new(xyear,xmonth,endDay);
                beginDate = endDate.AddMonths(-1).AddDays(1);
                return (beginDate, endDate);
            }
            catch (Exception)
            {
                endDay--;
            }
        } while (true);

    }

    public static int GetYearMonthDays(this string yearMonth)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime xdate = new(xyear,xmonth,1);
        xdate = xdate.AddMonths(1).AddDays(-1);
        return xdate.Day;
    }

    public static int GetYearMonthMonths(this string yearMonth)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        return xmonth;
    }

    public static int GetYearMonthYears(this string yearMonth)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        string strMonth = yearMonth.ToString();
        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        return xyear;
    }

    public static string AddYearMonth(this string yearMonth, int value)
    {
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        var firstDate = yearMonth.GetYearMonthFirstDate();
        var monthDate = firstDate.AddMonths(value);
        return monthDate.GetYearMonth();
    }

    public static DateTime GetYearFirstDateFromYearMonth(this string yearMonth, int beginMonth = 1)
    {
        string strMonth = yearMonth.ToString();
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime xdate = new(xyear,1,1);
        if (beginMonth > 1) {
            if (beginMonth > xmonth) {
                xyear -= 1;
            }
            xdate = new DateTime(xyear, beginMonth, 1);
        }
        return xdate;
    }

    public static DateTime GetYearLastDateFromYearMonth(this string yearMonth, int beginMonth = 1)
    {
        string strMonth = yearMonth.ToString();
        if(!yearMonth.IsYearMonth())
            throw new Exception($"{yearMonth} yearMonth error");

        int xyear = int.Parse(strMonth.AsSpan(0,4));
        int xmonth = int.Parse(strMonth.AsSpan(5,2));
        DateTime xdate = new(xyear,12,31);
        if (beginMonth > 1)
        {
            if (beginMonth <= xmonth) {
                xyear += 1;
            }
            xdate = new DateTime(xyear, beginMonth, 1).AddDays(-1);
        }
        return xdate;
    }


    #endregion

    #region Check Is Duplicate function
    /// <summary>
    /// 計算從 2000-1-1 到目前的分鐘數 TimeStamp,
    /// 用於檢查請假加班時間是否有重複
    /// </summary>
    public static int CalcTimeStampFrom2000(this DateTime endDateTime, string atTime="0000")
    {
        DateTime beginDateTime = new(2000,1,1,0,0,0);
        DateTime date = endDateTime;
        int hours = 0, mins = 0;
        if (atTime.Contains(':')) {
            int index = atTime.IndexOf(':');
            _=int.TryParse(atTime.AsSpan(0,index), out hours);
            _=int.TryParse(atTime.AsSpan(index+1,2), out mins);
        } else{
            _=int.TryParse(atTime.AsSpan(0,2), out hours);
            _=int.TryParse(atTime.AsSpan(2,2), out mins);
        }
        if (hours >= 24)
        {
            date = date.AddDays(1);
            hours -= 24;
        }
        DateTime tempDateTime = new(date.Year,date.Month,date.Day, hours, mins, 0);
        int timeStamp = Convert.ToInt32(tempDateTime.Subtract(beginDateTime).TotalMinutes);
        return timeStamp;
    }

    /// <summary>
    /// 用於檢查請假加班時間是否有重複
    /// </summary>
    public static bool CheckIsDuplicate(DateTime sourceBeginDate, string sourceTimeBegin,DateTime sourceEndDate,string sourceTimeEnd, DateTime distBeginDate, string distTimeBegin, DateTime distEndDate, string distTimeEnd)
    {
        int beginStampA = sourceBeginDate.CalcTimeStampFrom2000(sourceTimeBegin);
        int beginStampB = sourceEndDate.CalcTimeStampFrom2000(sourceTimeEnd);
        int endStampA = distBeginDate.CalcTimeStampFrom2000(distTimeBegin);
        int endStampB = distEndDate.CalcTimeStampFrom2000(distTimeEnd);

        if (endStampA <= beginStampA && endStampB <= beginStampA) {
            return false;
        } else if (endStampA >= beginStampB && endStampB >= beginStampB) {
            return false;
        }
        return true;
        // if (endStampA < beginStampA && endStampB > beginStampA) {
        //     return true;
        // } else if (endStampA >= beginStampA && endStampB <= beginStampB) {
        //     return true;
        // } else if (endStampA < beginStampB && endStampB >= beginStampB) {
        //     return true;
        // }
        // return false;
    }

    #endregion

}

