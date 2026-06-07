using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Helpers;

public static class BitExtensions
{

    public static BitArray CloneBitArray(this BitArray source, BitArray bitArray,int index=0 )
    {
        for (int i = 0; i < bitArray.Count; i++)
        {
            source[i + index] = bitArray[i];
        }
        return source;
    }

    /// <summary>
    /// this source bitArray=46080
    /// 60*24=1440
    /// 1440*32=46080
    /// </summary>
    public static BitArray SetOnMonthBitArray(this BitArray source, DateTime beginDate, string beginTime, DateTime endDate, string endTime)
    {
        if ( string.IsNullOrWhiteSpace(beginTime) || string.IsNullOrWhiteSpace(endTime) || beginTime.IsGreaterThan("3600") || endTime.IsGreaterThan("3600") )
            return source;

        var (beginPos, endPos) = CalcMonthBitArrayPosRange(beginDate, beginTime, endDate, endTime);
        for (int i = beginPos; i < endPos; i++)
        {
            source[i] = true;
        }
        return source;
    }

    public static BitArray SetOnMonthBitArray(this BitArray source, DateOnly beginDate, string beginTime, DateOnly endDate, string endTime)
    {
        return SetOnMonthBitArray(source,  beginDate.ToDateTime(new TimeOnly(0,0,0)), beginTime, endDate.ToDateTime(new TimeOnly(0,0,0)), endTime);
    }

    public static BitArray SetOffMonthBitArray(this BitArray source, DateTime beginDate, string beginTime, DateTime endDate, string endTime)
    {
        if ( string.IsNullOrWhiteSpace(beginTime) || string.IsNullOrWhiteSpace(endTime) || beginTime.IsGreaterThan("3600") || endTime.IsGreaterThan("3600") )
            return source;

        var (beginPos, endPos) = CalcMonthBitArrayPosRange(beginDate, beginTime, endDate, endTime);
        for (int i = beginPos; i < endPos; i++)
        {
            source[i] = false;
        }
        return source;
    }

    public static BitArray SetOffMonthBitArray(this BitArray source, DateOnly beginDate, string beginTime, DateOnly endDate, string endTime)
    {
        return SetOffMonthBitArray(source,  beginDate.ToDateTime(new TimeOnly(0,0,0)), beginTime, endDate.ToDateTime(new TimeOnly(0,0,0)), endTime);
    }

    public static (int beginPos, int endPos) CalcMonthBitArrayPosRange (DateTime beginDate, string beginTime, DateTime endDate, string endTime)
    {
        int day1 = beginDate.Day;
        int beginHour = int.Parse(beginTime.Substring(0,2));
        int beginMins = int.Parse(beginTime.Substring(2,2));
        var beginPos = (day1 - 1) * 1440 + beginHour * 60 + beginMins;

        if (beginDate == endDate && endTime.CompareTo(beginTime)< 0 )
        {
            endDate = endDate.AddDays(1);
        }
        int day2 = endDate.Day;
        int endHour = int.Parse(endTime.Substring(0,2));
        int endMins = int.Parse(endTime.Substring(2,2));
        var endPos = (day2 - 1) * 1440 + endHour * 60 + endMins;
        return (beginPos,endPos);
    }

    public static (int beginPos, int endPos) CalcMonthBitArrayPosRange (DateOnly beginDate, string beginTime, DateOnly endDate, string endTime)
    {
        return CalcMonthBitArrayPosRange(beginDate.ToDateTime(new TimeOnly(0,0,0)), beginTime, endDate.ToDateTime(new TimeOnly(0,0,0)), endTime);
    }

    public static ((int DD, int HH, int MM) BeginDDHHMM, (int DD, int HH, int MM) EndDDHHMM, int EndPos)
        GetMonthBitArrayPosDDHHMM (this BitArray source, int beginPos)
    {
        int pos1=0,pos2=0,endPos=0;
        for (int i = beginPos; i < 46080; i++)
        {
            if (pos1 == 0 && source[i] == true ) {
                pos1 = i;
            } else if (pos1 > 1 && pos2 == 0 && source[i] == false ) {
                pos2 = i;
            }

            if (pos1 > 0 && pos2 > 0) {
                endPos = i;
                break;
            }
        }
        var tupple1 = CalcMonthBitArrayPosDDHHMM(pos1);
        var tupple2 = CalcMonthBitArrayPosDDHHMM(pos2);
        return (tupple1,tupple2,endPos);
    }

    public static (int dd, int hh, int mm) CalcMonthBitArrayPosDDHHMM (int pos)
    {
        if (pos == 0)
            return (0,0,0);

        int dd = (pos / 1440)+1;
        int hh = pos % 1440 / 60;
        int mm = pos % 1440 % 60;
        return (dd,hh,mm);
    }

}