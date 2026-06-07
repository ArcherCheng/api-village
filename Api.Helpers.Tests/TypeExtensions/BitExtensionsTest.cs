using System;
using NUnit.Framework;
using Api.Helpers;
using System.Collections;

namespace Api.Helpers.Tests;

[TestFixture]
public class BitExtensionsTests
{
    [Test]
    public void Test_CloneBitArray()
    {
        BitArray bitArray1 = new System.Collections.BitArray(10,false);
        BitArray bitArray2 = new System.Collections.BitArray(10,true);

        bitArray1.CloneBitArray(bitArray2);
        Assert.That(bitArray1, Is.EqualTo(bitArray2));

        bitArray1.Set(5, false);
        Assert.That(bitArray1, !Is.EqualTo(bitArray2));

        bitArray2.Set(1, false);
        bitArray2.Set(5, false);
        Assert.That(bitArray1, !Is.EqualTo(bitArray2));

        bitArray1.Set(1, false);
        Assert.That(bitArray1, Is.EqualTo(bitArray2));

    }

    [Test]
    public void Test_SetOnMonthBitArray()
    {
        BitArray bitArray = new(46080,false);
        DateTime beginDate = new(2024, 6, 1);
        string beginTime = "0800";
        DateTime endDate = new(2024, 6, 1);
        string endTime = "3600";

        var (beginPos, endPos) = BitExtensions.CalcMonthBitArrayPosRange(beginDate, beginTime, endDate, endTime);
        bitArray.SetOnMonthBitArray(beginDate, beginTime, endDate, endTime);
        var (beginPos2, endPos2) = BitExtensions.CalcMonthBitArrayPosRange(new DateTime(2024, 6, 10), "0800", new DateTime(2024, 6, 10), "1700");
        bitArray.SetOnMonthBitArray(new DateTime(2024, 6, 10), "0800", new DateTime(2024, 6, 10), "1700");
        for (int i = 0; i < 46080; i++)
        {
            if (i >= beginPos && i < endPos)
            {
                Assert.That(bitArray[i], Is.True);
            }
            else if (i >= beginPos2 && i < endPos2)
            {
                Assert.That(bitArray[i], Is.True);
            }
            else
            {
                Assert.That(bitArray[i], Is.False);
            }
        }
    }

    [Test]
    public void Test_SetOffMonthBitArray()
    {
        BitArray bitArray = new(46080,true);
        DateTime beginDate = new(2024, 6, 1);
        string beginTime = "0800";
        DateTime endDate = new(2024, 6, 1);
        string endTime = "3600";

        var (beginPos, endPos) = BitExtensions.CalcMonthBitArrayPosRange(beginDate, beginTime, endDate, endTime);
        bitArray.SetOffMonthBitArray(beginDate, beginTime, endDate, endTime);
        var (beginPos2, endPos2) = BitExtensions.CalcMonthBitArrayPosRange(new DateTime(2024, 6, 10), "0800", new DateTime(2024, 6, 10), "1700");
        bitArray.SetOffMonthBitArray(new DateTime(2024, 6, 10), "0800", new DateTime(2024, 6, 10), "1700");
        for (int i = 0; i < 46080; i++)
        {
            if (i >= beginPos && i < endPos)
            {
                Assert.That(bitArray[i], Is.False);
            }
            else if (i >= beginPos2 && i < endPos2)
            {
                Assert.That(bitArray[i], Is.False);
            }
            else
            {
                Assert.That(bitArray[i], Is.True);
            }
        }
    }

    [Test]
    public void Test_CalcMonthBitArrayPosRange()
    {
        DateTime beginDate = new(2024, 6, 1);
        string beginTime = "0800";
        DateTime endDate = new(2024, 6, 1);
        string endTime = "3600";

        var (beginPos, endPos) = BitExtensions.CalcMonthBitArrayPosRange(beginDate, beginTime, endDate, endTime);
        Assert.That(beginPos, Is.EqualTo(60 * 8));
        Assert.That(endPos, Is.EqualTo(60 * 36));

        var (beginPos2, endPos2) = BitExtensions.CalcMonthBitArrayPosRange(new DateTime(2024, 6, 10), "0800", new DateTime(2024, 6, 10), "1700");
        Assert.That(beginPos2, Is.EqualTo(1440*9 + 480));
        Assert.That(endPos2, Is.EqualTo(1440*9 + 1020));
    }

    [Test]
    public void Test_GetMonthBitArrayPosDDHHMM()
    {
        BitArray bitArray = new(46080,false);
        DateTime beginDate = new(2024, 6, 1);
        string beginTime = "0800";
        DateTime endDate = new(2024, 6, 1);
        string endTime = "3600";

        bitArray.SetOnMonthBitArray(beginDate, beginTime, endDate, endTime);
        var (beginDDHHMM, endDDHHMM, endPos2) = bitArray.GetMonthBitArrayPosDDHHMM(0);
        Assert.That(beginDDHHMM.DD, Is.EqualTo(1));
        Assert.That(beginDDHHMM.HH, Is.EqualTo(8));
        Assert.That(beginDDHHMM.MM, Is.EqualTo(0));
        Assert.That(endDDHHMM.DD, Is.EqualTo(2));
        Assert.That(endDDHHMM.HH, Is.EqualTo(12));
        Assert.That(endDDHHMM.MM, Is.EqualTo(0));
        Assert.That(endPos2, Is.EqualTo(60 * 36));

        bitArray.SetOnMonthBitArray(new(2024, 6, 10), "0830", new DateTime(2024, 6, 10), "1730");
        (beginDDHHMM, endDDHHMM, endPos2) = bitArray.GetMonthBitArrayPosDDHHMM(endPos2);
        Assert.That(beginDDHHMM.DD, Is.EqualTo(10));
        Assert.That(beginDDHHMM.HH, Is.EqualTo(8));
        Assert.That(beginDDHHMM.MM, Is.EqualTo(30));
        Assert.That(endDDHHMM.DD, Is.EqualTo(10));
        Assert.That(endDDHHMM.HH, Is.EqualTo(17));
        Assert.That(endDDHHMM.MM, Is.EqualTo(30));
        Assert.That(endPos2, Is.EqualTo(1440*9 + 1050));
    }

    [Test]
    public void Test_CalcMonthBitArrayPosDDHHMM()
    {
        var (dd,hh, mm) = BitExtensions.CalcMonthBitArrayPosDDHHMM(60 * 8);
        Assert.That(dd, Is.EqualTo(1));
        Assert.That(hh, Is.EqualTo(8));
        Assert.That(mm, Is.EqualTo(0));
        (dd,hh, mm) = BitExtensions.CalcMonthBitArrayPosDDHHMM(60 * 36);
        Assert.That(dd, Is.EqualTo(2));
        Assert.That(hh, Is.EqualTo(12));
        Assert.That(mm, Is.EqualTo(0));
        (dd,hh, mm) = BitExtensions.CalcMonthBitArrayPosDDHHMM(1440*9 + 510);
        Assert.That(dd, Is.EqualTo(10));
        Assert.That(hh, Is.EqualTo(8));
        Assert.That(mm, Is.EqualTo(30));
        (dd,hh, mm) = BitExtensions.CalcMonthBitArrayPosDDHHMM(1440*9 + 1050);
        Assert.That(dd, Is.EqualTo(10));
        Assert.That(hh, Is.EqualTo(17));
        Assert.That(mm, Is.EqualTo(30));

    }
}
