using System;
using NUnit.Framework;
using Api.Helpers;

namespace Api.Helpers.Tests;

[TestFixture]
public class DateTimeExtensionsTests
{

    [Test]
    public void Test_ToIsoDate()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToIsoDate(new DateTime(2024, 2, 3)), Is.EqualTo("2024-02-03"));
            Assert.That(DateTimeExtensions.ToIsoDate(new DateTime(2024, 2, 29)), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate(new DateOnly(2024, 2, 29)), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("2024/02/29"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("2024.02.29"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("2024-02-29 12:33:26"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("2024-02-29T12:33:26"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("2024.02.29T12:33:26"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("20240229"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("240229"), Is.EqualTo("2024-02-29"));
            Assert.That(DateTimeExtensions.ToIsoDate("1130229"), Is.EqualTo("2024-02-29"));

            Assert.That("20240229".ToIsoDate(), Is.EqualTo("2024-02-29"));
            Assert.That("240229".ToIsoDate(), Is.EqualTo("2024-02-29"));
            Assert.That("24.02.29".ToIsoDate(), Is.EqualTo("0024-02-29"));

            Assert.That("1130229".ToIsoDate(), Is.EqualTo("2024-02-29"));
            Assert.That("113.04.15".ToIsoDate(), Is.EqualTo("0113-04-15"));
        }
    }

    [Test]
    public void Test_ToDatetime()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToDatetime(new DateTime(2024, 2, 3)), Is.EqualTo(new DateTime(2024, 2, 3)));
            Assert.That(DateTimeExtensions.ToDatetime(new DateTime(2024, 2, 29)), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime(new DateOnly(2024, 2, 29)), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime("2024/02/29"), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime("2024.02.29"), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime("2024-02-29 13:33:36"), Is.EqualTo(new DateTime(2024, 2, 29,13,33,36)));
            Assert.That(DateTimeExtensions.ToDatetime("2024-02-29T12:33:26"), Is.EqualTo(new DateTime(2024, 2, 29,12,33,26)));
            Assert.That(DateTimeExtensions.ToDatetime("2024.02.29T12:33:26"), Is.EqualTo(new DateTime(2024, 2, 29,12,33,26)));
            Assert.That(DateTimeExtensions.ToDatetime("20240229"), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime("240229"), Is.EqualTo(new DateTime(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDatetime("1130229"), Is.EqualTo(new DateTime(2024, 02, 29))); // This is a special case for "113" which represents year 113 in Taiwan calendar (i.e., year 1913 in Gregorian calendar)
        }
    }

    [Test]
    public void Test_ToDatetimeEnd()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToDatetimeEnd(new DateTime(2024, 2, 3)), Is.EqualTo(new DateTime(2024, 2, 3, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd(new DateTime(2024, 2, 29)), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd(new DateOnly(2024, 2, 29)), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("2024/02/29"), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("2024.02.29"), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("2024-02-29 13:33:36"), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("2024-02-29T12:33:26"), Is.EqualTo(new DateTime(2024, 2, 29,23,59,59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("2024.02.29T12:33:26"), Is.EqualTo(new DateTime(2024, 2, 29,23,59,59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("20240229"), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("240229"), Is.EqualTo(new DateTime(2024, 2, 29, 23, 59, 59)));
            Assert.That(DateTimeExtensions.ToDatetimeEnd("1130228"), Is.EqualTo(new DateTime(2024, 02, 28, 23, 59, 59)));
            // This is a special case for "113" which represents year 113 in Taiwan calendar (i.e., year 1913 in Gregorian calendar)
        }
    }

    [Test]
    public void Test_ToDateOnly()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToDateOnly(new DateTime(2024, 2, 3)), Is.EqualTo(new DateOnly(2024, 2, 3)));
            Assert.That(DateTimeExtensions.ToDateOnly(new DateTime(2024, 2, 29)), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly(new DateOnly(2024, 2, 29)), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("2024/02/29"), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("2024.02.29"), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("2024-02-29T12:33:26"), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("2024.02.29T12:33:26"), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("20240229"), Is.EqualTo(new DateOnly(2024, 2, 29)));
            Assert.That(DateTimeExtensions.ToDateOnly("240315"), Is.EqualTo(new DateOnly(2024, 3, 15)));
            Assert.That(DateTimeExtensions.ToDateOnly("1130315"), Is.EqualTo(new DateOnly(2024, 3, 15)));
            // This is a special case for "113" which represents year 113 in Taiwan calendar (i.e., year 1913 in Gregorian calendar)
        }
    }

    [Test]
    public void ToTaiwanDate()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToTaiwanDate(new DateTime(2024, 2, 28)), Is.EqualTo("中華民國113年2月28日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate(new DateOnly(2024, 3, 15)), Is.EqualTo("中華民國113年3月15日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate("2024-03-15"), Is.EqualTo("中華民國113年3月15日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate("2024-03-15T15:24:38"), Is.EqualTo("中華民國113年3月15日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate("2024-03-15 15:24:38"), Is.EqualTo("中華民國113年3月15日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate("20240315"), Is.EqualTo("中華民國113年3月15日"));
            Assert.That(DateTimeExtensions.ToTaiwanDate("1130315"), Is.EqualTo("中華民國113年3月15日"));

        }
    }

    [Test]
    public void ToTwDate()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.ToTwDate(new DateTime(2024, 2, 28)), Is.EqualTo("113.02.28"));
            Assert.That(DateTimeExtensions.ToTwDate(new DateOnly(2024, 3, 15)), Is.EqualTo("113.03.15"));
            Assert.That(DateTimeExtensions.ToTwDate("2024-03-15"), Is.EqualTo("113.03.15"));
            Assert.That(DateTimeExtensions.ToTwDate("2024-03-15T15:24:38"), Is.EqualTo("113.03.15"));
            Assert.That(DateTimeExtensions.ToTwDate("2024-03-15 15:24:38"), Is.EqualTo("113.03.15"));
            Assert.That(DateTimeExtensions.ToTwDate("20240315"), Is.EqualTo("113.03.15"));
            Assert.That(DateTimeExtensions.ToTwDate("1130315"), Is.EqualTo("113.03.15"));

        }
    }

    [Test]
    public void Test_ParseDateStringYearMonthDay()
    {
        var result = DateTimeExtensions.ParseDateStringYearMonthDay("2024-02-29");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("2024/02/29");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("2024.02.29");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("20240229");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("240229");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("1130229");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 0, 0, 0)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("2024-02-29T12:30:38");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 12, 30, 38)));

        result = DateTimeExtensions.ParseDateStringYearMonthDay("2024-02-29 12:30:38");
        Assert.That(result, Is.EqualTo((2024, 2, 29, 12, 30, 38)));
    }


    [Test]
    public void Test_IsWeekend()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.IsWeekend(new DateTime(2026, 2, 28)), Is.True);
            Assert.That(DateTimeExtensions.IsWeekend(new DateOnly(2026, 3, 15)), Is.True);
            Assert.That(DateTimeExtensions.IsWeekend("2026-03-15"), Is.True);
            Assert.That(DateTimeExtensions.IsWeekend("2026-03-16T15:24:38"),  Is.False);
            Assert.That(DateTimeExtensions.IsWeekend("2026-03-15 15:24:38"), Is.True);
            Assert.That(DateTimeExtensions.IsWeekend("20260316"), Is.False);
            Assert.That(DateTimeExtensions.IsWeekend("260316"), Is.False);
            Assert.That(DateTimeExtensions.IsWeekend("260314"), Is.True);
            Assert.That(DateTimeExtensions.IsWeekend("1150316"), Is.False);
            Assert.That(DateTimeExtensions.IsWeekend("1150315"), Is.True);
        }
    }

    [Test]
    public void Test_IsYearMonth()
    {
        var result = DateTimeExtensions.IsYearMonth("2025-01");
        Assert.That(result, Is.True);

        result = DateTimeExtensions.IsYearMonth("9999-12");
        Assert.That(result, Is.True);

        result = DateTimeExtensions.IsYearMonth("2025-13");
        Assert.That(result, Is.False);

        result = DateTimeExtensions.IsYearMonth("2028-01");
        Assert.That(result, Is.True);

        result = DateTimeExtensions.IsYearMonth("2126-01");
        Assert.That(result, Is.True);

        result = DateTimeExtensions.IsYearMonth("2127-01");
        Assert.That(result, Is.False);

        result = DateTimeExtensions.IsYearMonth("1926-01");
        Assert.That(result, Is.True);

        result = DateTimeExtensions.IsYearMonth("1915-01");
        Assert.That(result, Is.False);
    }


    [Test]
    public void Test_DateDiffYears()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2022,3,31), new DateTime(2024,3,31)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2022,3,31), new DateTime(2024,3,30)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2022,3,31), new DateTime(2024,3,29)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2022,3,31), new DateTime(2024,3,29), false), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2024,2,29), new DateTime(2026,2,28)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2024,2,29), new DateTime(2026,2,27)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2024,2,29), new DateTime(2026,2,27), false), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateTime(2024,2,29), new DateTime(2026,3,1)), Is.EqualTo(2));
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2022,3,31), new DateOnly(2024,3,31)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2022,3,31), new DateOnly(2024,3,30)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2022,3,31), new DateOnly(2024,3,29)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2022,3,31), new DateOnly(2024,3,29), false), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2024,2,29), new DateOnly(2026,2,28)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2024,2,29), new DateOnly(2026,2,27)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2024,2,29), new DateOnly(2026,2,27), false), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffYears(new DateOnly(2024,2,29), new DateOnly(2026,3,1)), Is.EqualTo(2));
        }

    }

    [Test]
    public void Test_DateDiffDays()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2022,3,31), new DateTime(2024,3,31)), Is.EqualTo(732));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2022,3,31), new DateTime(2024,3,30)), Is.EqualTo(731));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2022,3,31), new DateTime(2024,3,29)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2022,3,31), new DateTime(2024,3,29)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2024,2,29), new DateTime(2026,2,28)), Is.EqualTo(731));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2024,2,29), new DateTime(2026,2,27)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2024,2,29), new DateTime(2026,3,1)), Is.EqualTo(732));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2024,2,29), new DateTime(2026,3,1)), Is.EqualTo(732));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateTime(2024,2,29), new DateTime(2024,2,29)), Is.EqualTo(1));
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2022,3,31), new DateOnly(2024,3,31)), Is.EqualTo(732));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2022,3,31), new DateOnly(2024,3,30)), Is.EqualTo(731));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2022,3,31), new DateOnly(2024,3,29)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2022,3,31), new DateOnly(2024,3,29)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2024,2,29), new DateOnly(2026,2,28)), Is.EqualTo(731));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2024,2,29), new DateOnly(2026,2,27)), Is.EqualTo(730));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2024,2,29), new DateOnly(2026,3,1)), Is.EqualTo(732));
            Assert.That(DateTimeExtensions.DateDiffDays(new DateOnly(2024,2,29), new DateOnly(2026,3,1)), Is.EqualTo(732));
        }
    }

    [Test]
    public void Test_DateDiffMins()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffMins(new DateTime(2022,3,1,8,10,24), new DateTime(2022,3,1,8,15,3)), Is.EqualTo(4));
            Assert.That(DateTimeExtensions.DateDiffMins(new DateTime(2022,3,1,8,10,24), new DateTime(2022,3,1,8,15,33)), Is.EqualTo(5));
        }
    }

    [Test]
    public void Test_DateDiffMonths()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateTime(2022,3,1,8,10,24), new DateTime(2022,4,1,8,15,3)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateTime(2022,3,2,8,10,24), new DateTime(2022,4,1,8,15,3)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateTime(2022,3,1,8,10,24), new DateTime(2022,3,1,8,15,33)), Is.EqualTo(1));
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateOnly(2022,3,1), new DateOnly(2022,4,1)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateOnly(2022,3,2), new DateOnly(2022,4,1)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffMonths(new DateOnly(2022,3,1), new DateOnly(2022,3,1)), Is.EqualTo(1));
        }

    }

    [Test]
    public void Test_DateDiffHalfMonths()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,3,1,8,10,24), new DateTime(2022,4,1,8,15,3)), Is.EqualTo(3));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,3,15,8,10,24), new DateTime(2022,4,1,8,15,3)), Is.EqualTo(3));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,3,15,8,10,24), new DateTime(2022,4,16,8,15,3)), Is.EqualTo(4));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,3,1,8,10,24), new DateTime(2022,3,15,8,15,33)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,3,1,8,10,24), new DateTime(2022,3,31,8,15,33)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,8,25,8,10,24), new DateTime(2023,3,6,8,15,33)), Is.EqualTo(14));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateTime(2022,8,25,8,10,24), new DateTime(2023,3,31,8,15,33)), Is.EqualTo(15));
        }
        using (Assert.EnterMultipleScope())
        {
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,3,1), new DateOnly(2022,4,1)), Is.EqualTo(3));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,3,15), new DateOnly(2022,4,1)), Is.EqualTo(3));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,3,15), new DateOnly(2022,4,16)), Is.EqualTo(4));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,3,1), new DateOnly(2022,3,15)), Is.EqualTo(1));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,3,1), new DateOnly(2022,3,31)), Is.EqualTo(2));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,8,25), new DateOnly(2023 , 3 , 6)), Is.EqualTo(14));
            Assert.That(DateTimeExtensions.DateDiffHalfMonths(new DateOnly(2022,8,25), new DateOnly(2023,3,31)), Is.EqualTo(15));
        }
    }

    [Test]
    public void Test_DateDiffYearsMonthsDays()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateTime(2022, 3, 1), new DateTime(2022, 1, 1)), Is.EqualTo((0, 2, 0)));
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateTime(2021, 3, 10), new DateTime(2024, 1, 1)), Is.EqualTo((2, 9, 22)));
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateTime(2020, 2, 1), new DateTime(2024, 1, 1)), Is.EqualTo((3, 11, 0)));
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateOnly(2022, 3, 1), new DateOnly(2022, 1, 1)), Is.EqualTo((0, 2, 0)));
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateOnly(2021, 3, 10), new DateOnly(2024, 1, 1)), Is.EqualTo((2, 9, 22)));
            Assert.That(() => DateTimeExtensions.DateDiffYearsMonthsDays(new DateOnly(2020, 2, 1), new DateOnly(2024, 1, 1)), Is.EqualTo((3, 11, 0)));
        }

    }

    [Test]
    public void Test_GetYearMonth()
    {
        var newDate = new DateTime(2022,1,1);
        var result = DateTimeExtensions.GetYearMonth(newDate);
        Assert.That(result, Is.EqualTo("2022-01"));

        newDate = new DateTime(2021,3,10);
        result = DateTimeExtensions.GetYearMonth(newDate);
        Assert.That(result, Is.EqualTo("2021-03"));

        var onlyDate = new DateOnly(2021,3,10);
        result = DateTimeExtensions.GetYearMonth(onlyDate);
        Assert.That(result, Is.EqualTo("2021-03"));
    }


    [Test]
    public void Test_GetYearMonthFirstDate()
    {
        var result = DateTimeExtensions.GetYearMonthFirstDate(new DateTime(2022,1,11));
        Assert.That(result, Is.EqualTo(new DateTime(2022,1,1)));

        result = DateTimeExtensions.GetYearMonthFirstDate(new DateOnly(2022,2,28));
        Assert.That(result, Is.EqualTo(new DateTime(2022,2,1)));

        result = DateTimeExtensions.GetYearMonthFirstDate("2022-03");
        Assert.That(result, Is.EqualTo(new DateTime(2022,3,1)));
    }

    [Test]
    public void Test_GetYearMonthLastDate()
    {
        var result = DateTimeExtensions.GetYearMonthLastDate(new DateTime(2022,1,11));
        Assert.That(result, Is.EqualTo(new DateTime(2022,1,31)));

        result = DateTimeExtensions.GetYearMonthLastDate(new DateOnly(2022,2,28));
        Assert.That(result, Is.EqualTo(new DateTime(2022,2,28)));

        result = DateTimeExtensions.GetYearMonthLastDate("2022-03");
        Assert.That(result, Is.EqualTo(new DateTime(2022,3,31)));
    }

    //取得本月結算截止日期，如:202401 25 = 2024-01-25
    [Test]
    public void Test_GetYearMonthEndDate()
    {
        var result = DateTimeExtensions.GetYearMonthEndDate("2023-02",15);
        Assert.That(result, Is.EqualTo(new DateTime(2023,2,15)));

        result = DateTimeExtensions.GetYearMonthEndDate("2023-05",05);
        Assert.That(result, Is.EqualTo(new DateTime(2023,5,05)));

        result = DateTimeExtensions.GetYearMonthEndDate("2024-05", 25);
        Assert.That(result, Is.EqualTo(new DateTime(2024, 5, 25)));
    }

    [Test]
    public void Test_GetYearMonthBeginEndDate()
    {
        var result = DateTimeExtensions.GetYearMonthBeginEndDate("2023-02",30);
        Assert.That(result, Is.EqualTo((new DateTime(2023,2,1),new DateTime(2023,2,28))));

        result = DateTimeExtensions.GetYearMonthBeginEndDate("2023-05",31);
        Assert.That(result, Is.EqualTo((new DateTime(2023,5,1),new DateTime(2023,5,31))));

        result = DateTimeExtensions.GetYearMonthBeginEndDate("2023-05",25);
        Assert.That(result, Is.EqualTo((new DateTime(2023,4,26),new DateTime(2023,5,25))));

    }

    [Test]
    public void Test_GetYearMonthDays()
    {
        var result = DateTimeExtensions.GetYearMonthDays("2023-02");
        Assert.That(result, Is.EqualTo(28));

        result = DateTimeExtensions.GetYearMonthDays("2023-01");
        Assert.That(result, Is.EqualTo(31));

        result = DateTimeExtensions.GetYearMonthDays("2023-04");
        Assert.That(result, Is.EqualTo(30));

        result = DateTimeExtensions.GetYearMonthDays("2023-02");
        Assert.That(result, Is.EqualTo(28));

        result = DateTimeExtensions.GetYearMonthDays("2024-02");
        Assert.That(result, Is.EqualTo(29));
    }

    [Test]
    public void Test_GetYearMonthMonths()
    {
        var result = DateTimeExtensions.GetYearMonthMonths("2023-02");
        Assert.That(result, Is.EqualTo(2));

        result = DateTimeExtensions.GetYearMonthMonths("2020-01");
        Assert.That(result, Is.EqualTo(1));

        result = DateTimeExtensions.GetYearMonthMonths("2020-12");
        Assert.That(result, Is.EqualTo(12));

    }

    [Test]
    public void Test_GetYearMonthYears()
    {
        var result = DateTimeExtensions.GetYearMonthYears("2023-02");
        Assert.That(result, Is.EqualTo(2023));

        result = DateTimeExtensions.GetYearMonthYears("2020-01");
        Assert.That(result, Is.EqualTo(2020));

        result = DateTimeExtensions.GetYearMonthYears("9999-12");
        Assert.That(result, Is.EqualTo(9999));
    }

    [Test]
    public void Test_AddYearMonth()
    {
        var result = DateTimeExtensions.AddYearMonth("2023-02",3);
        Assert.That(result, Is.EqualTo("2023-05"));

        result = DateTimeExtensions.AddYearMonth("2020-01",13);
        Assert.That(result, Is.EqualTo("2021-02"));

        result = DateTimeExtensions.AddYearMonth("2020-01",-13);
        Assert.That(result, Is.EqualTo("2018-12"));

    }

    [Test]
    public void Test_GetYearFirstDateFromYearMonth()
    {
        var result = DateTimeExtensions.GetYearFirstDateFromYearMonth("2023-02");
        Assert.That(result, Is.EqualTo(new DateTime(2023,1,1)));

        result = DateTimeExtensions.GetYearFirstDateFromYearMonth("2023-02",7);
        Assert.That(result, Is.EqualTo(new DateTime(2022,7,1)));

        result = DateTimeExtensions.GetYearFirstDateFromYearMonth("2023-08",7);
        Assert.That(result, Is.EqualTo(new DateTime(2023,7,1)));
    }

    [Test]
    public void Test_GetYearLastDateFromYearMonth()
    {
        var result = DateTimeExtensions.GetYearLastDateFromYearMonth("2023-02");
        Assert.That(result, Is.EqualTo(new DateTime(2023,12,31)));

        result = DateTimeExtensions.GetYearLastDateFromYearMonth("2023-05",7);
        Assert.That(result, Is.EqualTo(new DateTime(2023,6,30)));

        result = DateTimeExtensions.GetYearLastDateFromYearMonth("2023-07",7);
        Assert.That(result, Is.EqualTo(new DateTime(2024,6,30)));
    }

    [Test]
    public void Test_CalcTimeStampFrom2000()
    {
        int timemins1= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "1800");
        Assert.That(timemins1, Is.EqualTo(12120120));
        int timemins2= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "2200");
        Assert.That(timemins2, Is.EqualTo(12120360));
        int submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(240));

        timemins1= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "0800");
        Assert.That(timemins1, Is.EqualTo(12119520));
        timemins2= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "1700");
        Assert.That(timemins2, Is.EqualTo(12120060));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(540));

        timemins2= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,17), "0800");
        Assert.That(timemins2, Is.EqualTo(12120960));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(1440));

        timemins1= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "08:00");
        Assert.That(timemins1, Is.EqualTo(12119520));
        timemins2= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,16), "17:00");
        Assert.That(timemins2, Is.EqualTo(12120060));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(540));

        timemins2= DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2023,1,17), "8:00");
        Assert.That(timemins2, Is.EqualTo(12120960));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(1440));

        timemins1 = DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2024,4,1,0,0,0),"0800");
        Assert.That(timemins1, Is.EqualTo(12754560));
        timemins2 = DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2024,4,1,0,0,0),"1000");
        Assert.That(timemins2, Is.EqualTo(12754680));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(120));

        timemins1 = DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2024,4,1,0,0,0),"0900");
        Assert.That(timemins1, Is.EqualTo(12754620));
        timemins2 = DateTimeExtensions.CalcTimeStampFrom2000(new DateTime(2024,4,1,0,0,0),"1200");
        Assert.That(timemins2, Is.EqualTo(12754800));
        submins = timemins2 - timemins1;
        Assert.That(submins, Is.EqualTo(180));
    }

    [Test]
    public void Test_CheckIsDuplicate()
    {
        var beginDate1 = new DateTime(2024, 4, 1);
        string beginTime1 = "0800";
        var endDate1 = new DateTime(2024, 4, 1);
        string endTime1 = "1600";

        var beginDate2 = new DateTime(2024, 4, 1);
        string beginTime2 = "1530";
        var endDate2 = new DateTime(2024, 4, 1);
        string endTime2 = "1700";
        var result = DateTimeExtensions.CheckIsDuplicate(beginDate1, beginTime1, endDate1, endTime1, beginDate2, beginTime2, endDate2, endTime2);
        Assert.That(result, Is.True);

        beginDate2 = new DateTime(2024, 4, 1);
        beginTime2 = "1600";
        endDate2 = new DateTime(2024, 4, 1);
        endTime2 = "1700";
        result = DateTimeExtensions.CheckIsDuplicate(beginDate1, beginTime1, endDate1, endTime1, beginDate2, beginTime2, endDate2, endTime2);
        Assert.That(result, Is.False);

        beginDate2 = new DateTime(2024, 3, 1);
        beginTime2 = "1600";
        endDate2 = new DateTime(2024, 4, 1);
        endTime2 = "0800";
        result = DateTimeExtensions.CheckIsDuplicate(beginDate1, beginTime1, endDate1, endTime1, beginDate2, beginTime2, endDate2, endTime2);
        Assert.That(result, Is.False);

        beginDate2 = new DateTime(2024, 3, 1);
        beginTime2 = "1600";
        endDate2 = new DateTime(2024, 4, 1);
        endTime2 = "0801";
        result = DateTimeExtensions.CheckIsDuplicate(beginDate1, beginTime1, endDate1, endTime1, beginDate2, beginTime2, endDate2, endTime2);
        Assert.That(result, Is.True);
    }


}
