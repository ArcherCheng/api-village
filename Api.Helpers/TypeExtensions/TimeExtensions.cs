using System;
using System.Collections;

namespace Api.Helpers;

public static class TimeExtensions
{

    #region time functions
    /// <summary>
    /// return HHMM
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToHHMM(this DateTime source)
    {
        var hh  = source.Hour;
        //var hh  = source.Hour.ToString().PadLeft(2,'0');
        var mm  = source.Minute;
        //var mm  = source.Minute.ToString().PadLeft(2,'0');
        return $"{hh:00}:{mm:00}";
    }


    /// <summary>
    /// 只轉出時間字串,HH:mm:ss
    /// </summary>
    public static string ToTime(this DateTime source)
    {
        return source.ToString("HH:mm:ss");
    }


    /// <summary>
    /// return Is HHMM
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsHHMM(this string source)
    {
        if (string.IsNullOrWhiteSpace(source) || source.Length < 4 || source.Length > 5) {
            return false;
        }

        char[] delimiterChars = [' ', ',', '.', '@', '#', '*', '+', '-', '_', '\t'];
        if (source.Any(c => delimiterChars.Contains(c))) {
            return false;
        }

        int hours, minutes;
        if (source.Length == 5) {
            var times = source.Split(':');
            if (times.Length == 2) {
                _=int.TryParse(times[0], out hours);
                _=int.TryParse(times[1], out minutes);
            } else {
                return false;
            }
        } else{
            _=int.TryParse(source.AsSpan(0,2), out hours);
            _=int.TryParse(source.AsSpan(2,2), out minutes);
        }

        if (hours>=0 && hours<=36 && minutes>=0 && minutes<=59) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判定時間格式,如:"23:18:59"
    /// </summary>
    public static bool IsTime(this string source)
    {
        if (string.IsNullOrWhiteSpace(source) || source.Length != 8) {
            return false;
        }

        char[] delimiterChars = [' ', ',', '.', '@', '#', '*', '+', '-', '_', '\t'];
        if (source.Any(c => delimiterChars.Contains(c))) {
            return false;
        }

        int hours=99, minutes=99, seconds=99;
        var times = source.Split(':');
        if (times.Length == 3) {
            _=int.TryParse(times[0], out hours);
            _=int.TryParse(times[1], out minutes);
            _=int.TryParse(times[2], out seconds);
        }

        if (hours>=0 && hours<=36 && minutes>=0 && minutes<=59 && seconds>=0 && seconds<=59) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 計算兩個時間之間的分鐘數: 0815 09000000000000
    /// </summary>
    public static int DiffTimeToMinutes(this string? source, string? endTime)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return 0;
        }
        if (string.IsNullOrWhiteSpace(endTime)) {
            return 0;
        }
        int mins1 = source.TimeToMinutes();
        int mins2 = endTime.TimeToMinutes();
        if (mins2 < mins1) {
            mins2 += 1440;
        }
        return mins2 - mins1;
    }
    #endregion

    #region time Calculate
    public static int TimeToMinutes(this string? source)
    {
        if (string.IsNullOrWhiteSpace(source)) {
            return 0;
        }
        int hours = 0;
        int minutes = 0;
        int pos = source.IndexOf(':'); //"08:25"
        if (pos>0) {
            _ = int.TryParse(source.AsSpan(0,2), out hours);
            _ = int.TryParse(source.AsSpan(3,2), out minutes);
        } else {
            _ = int.TryParse(source.AsSpan(0,2), out hours);
            _ = int.TryParse(source.AsSpan(2,2), out minutes);
        }
        return hours * 60 + minutes;
    }

    public static string AddTime(this string source, int addMins)
    {
        if (string.IsNullOrEmpty(source)) {
            return "";
        }
        int hours = 0;
        int minutes = 0;
        int pos = source.IndexOf(':'); //"08:25"
        if (pos>0) {
            _ = int.TryParse(source.AsSpan(0,2), out hours);
            _ = int.TryParse(source.AsSpan(3,2), out minutes);
        } else {
            _ = int.TryParse(source.AsSpan(0,2), out hours);
            _ = int.TryParse(source.AsSpan(2,2), out minutes);
        }
        int totalMins = hours * 60 + minutes + addMins;
        if (totalMins<0) {
            totalMins += 1440;
        }
        string hour = (totalMins / 60).ToString("00");
        string minute = (totalMins % 60).ToString("00");
        if (source.Contains(':')) {
            return $"{hour}:{minute}";
        } else
        {
            return $"{hour}{minute}";
        }
    }

    public static string CutTime(this string source, int cutMins=30, int beginEnd=2 )
    {
        if (cutMins == 0) {
            cutMins=1;
        }

        int minutes;
        string beginHHMM;
        if (source.Contains(':')) {
            _ = int.TryParse(source.AsSpan(3,2), out minutes);
            beginHHMM = string.Concat(source.AsSpan(0,2), ":00");
        } else {
            _ = int.TryParse(source.AsSpan(2,2), out minutes);
            beginHHMM = string.Concat(source.AsSpan(0,2), "00");
        }

        int quotient = minutes / cutMins;  //商數
        int remainder  = minutes % cutMins;  //餘數
        int resultMins;
        if (beginEnd == 1) {
            if (remainder > 0) {
                quotient++;
            }
            resultMins = quotient * cutMins;
            return beginHHMM.AddTime(resultMins);
        } else {
            resultMins = quotient * cutMins;
            return beginHHMM.AddTime(resultMins);
        }
    }

    public static void BitArraySetRange(this BitArray source, int start, int end, bool value)
    {
        for (int i = start; i <= end; i++)
        {
            source.Set(i,value);
        }
    }

    public static (string startTime, string endTime) BitArray1440PosToStartEndTime(this BitArray source, int startPos)
    {
        string startTime="";
        string endTime="";
        for (int i = startPos; i < source.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(startTime)) {
                if(source[i] == true ) {
                    startTime = Postion1440ToTime(i);
                }
            } else {
                if (string.IsNullOrWhiteSpace(endTime)) {
                    if(source[i] == false) {
                        endTime = Postion1440ToTime(i - 1);
                    }
                } else {
                    break;
                }
            }
        }
        return (startTime, endTime);
    }

    public static string Postion1440ToTime(int posLen)
    {
        string hour = (posLen / 60).ToString("00");
        string minute = (posLen % 60).ToString("00");
        return $"{hour}:{minute}";
    }
    #endregion
}
