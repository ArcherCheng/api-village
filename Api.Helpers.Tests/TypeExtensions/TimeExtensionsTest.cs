// Test IsNullOrEmpty with null input returns true
using NUnit.Framework;
using Api.Helpers;
namespace Api.Helpers.Tests;

[TestFixture]
public class TimeExtensionsTests
{

    // Test Method 1
    [Test]
    public void Test_ToHHMM()
    {
        // Arrange
        var dateTime = new DateTime(2024, 1, 1, 8, 15, 0);
        // Act
        var result = dateTime.ToHHMM();
        // Assert
        Assert.That(result, Is.EqualTo("08:15"));
    }

    [Test]
    public void Test_ToTime()
    {
        // Arrange
        var dateTime = new DateTime(2024, 1, 1, 8, 5, 8);
        // Act
        var result = dateTime.ToTime();
        // Assert
        Assert.That(result, Is.EqualTo("08:05:08"));

        dateTime = new DateTime(2024, 1, 1, 0, 0, 0);
        result = dateTime.ToTime();
        Assert.That(result, Is.EqualTo("00:00:00"));

        dateTime = new DateTime(2024, 1, 1, 23, 59, 59);
        result = dateTime.ToTime();
        Assert.That(result, Is.EqualTo("23:59:59"));
    }

    [Test]
    public void Test_IsHHMM()
    {
        // // Arrange
        // var timeString = "08:15";
        // // Act
        // var result = timeString.IsHHMM();
        // // Assert
        // Assert.That(result, Is.True);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.IsHHMM("123"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("1:2:3"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("123456"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("12345"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("1234:"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("1:234"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("123:4"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("1234"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("12:34"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("25:00"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("36:00"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("36:59"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("37:01"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("23:60"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("23:59"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("2359"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("2360"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("24:00"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("00:00"), Is.True);
            Assert.That(TimeExtensions.IsHHMM("00 00"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("00,00"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("00.00"), Is.False);
            Assert.That(TimeExtensions.IsHHMM("00+00"), Is.False);
        }
    }

    [Test]
    public void Test_IsTime()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.IsTime(""), Is.False);
            Assert.That(TimeExtensions.IsTime("123"), Is.False);
            Assert.That(TimeExtensions.IsTime("1:2:3"), Is.False);
            Assert.That(TimeExtensions.IsTime("123456"), Is.False);
            Assert.That(TimeExtensions.IsTime("12345"), Is.False);
            Assert.That(TimeExtensions.IsTime("123:45"), Is.False);
            Assert.That(TimeExtensions.IsTime("1234:5"), Is.False);
            Assert.That(TimeExtensions.IsTime("12:34:5"), Is.False);
            Assert.That(TimeExtensions.IsTime("1:23:4"), Is.False);
            Assert.That(TimeExtensions.IsTime("1:2:3"), Is.False);
            Assert.That(TimeExtensions.IsTime("123:4"), Is.False);
            Assert.That(TimeExtensions.IsTime("08:15:00"), Is.True);
            Assert.That(TimeExtensions.IsTime("23:59:59"), Is.True);
            Assert.That(TimeExtensions.IsTime("24:00:00"), Is.True);
            Assert.That(TimeExtensions.IsTime("00:00:00"), Is.True);
            Assert.That(TimeExtensions.IsTime("00 00 00"), Is.False);
            Assert.That(TimeExtensions.IsTime("00,00,00"), Is.False);
            Assert.That(TimeExtensions.IsTime("00.00.00"), Is.False);
            Assert.That(TimeExtensions.IsTime("00+00+00"), Is.False);
        }
    }

    [Test]
    public void Test_DiffTimeToMinutes()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.DiffTimeToMinutes("08:15:00", "08:45:00"), Is.EqualTo(30));
            Assert.That(TimeExtensions.DiffTimeToMinutes("23:00:00", "00:30:00"), Is.EqualTo(90));
            Assert.That(TimeExtensions.DiffTimeToMinutes("00:00:00", "23:59:59"), Is.EqualTo(1439));
            Assert.That(TimeExtensions.DiffTimeToMinutes("12:30:00", "12:30:00"), Is.EqualTo(0));
        }
    }

    [Test]
    public void Test_TimeToMinutes()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.TimeToMinutes("08:15:00"), Is.EqualTo(495));
            Assert.That(TimeExtensions.TimeToMinutes("08:15"), Is.EqualTo(495));
            Assert.That(TimeExtensions.TimeToMinutes("0815"), Is.EqualTo(495));
            Assert.That(TimeExtensions.TimeToMinutes("081500"), Is.EqualTo(495));
            Assert.That(TimeExtensions.TimeToMinutes("08:45:00"), Is.EqualTo(525));
        }
    }

    [Test]
    public void Test_AddTime()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.AddTime("08:15:00", 30), Is.EqualTo("08:45"));
            Assert.That(TimeExtensions.AddTime("23:00:00", 90), Is.EqualTo("24:30"));
            Assert.That(TimeExtensions.AddTime("00:00:00", 1439), Is.EqualTo("23:59"));
            Assert.That(TimeExtensions.AddTime("12:30:00", 0), Is.EqualTo("12:30"));

            Assert.That(TimeExtensions.AddTime("08:15", 30), Is.EqualTo("08:45"));
            Assert.That(TimeExtensions.AddTime("23:00", 90), Is.EqualTo("24:30"));
            Assert.That(TimeExtensions.AddTime("00:00", 1439), Is.EqualTo("23:59"));
            Assert.That(TimeExtensions.AddTime("12:30", 0), Is.EqualTo("12:30"));

            Assert.That(TimeExtensions.AddTime("0815", 30), Is.EqualTo("0845"));
            Assert.That(TimeExtensions.AddTime("2300", 90), Is.EqualTo("2430"));
            Assert.That(TimeExtensions.AddTime("0000", 1439), Is.EqualTo("2359"));
            Assert.That(TimeExtensions.AddTime("1230", 0), Is.EqualTo("1230"));
        }
    }

    [Test]
    public void Test_CutTime()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.CutTime("08:15:00"), Is.EqualTo("08:00"));
            Assert.That(TimeExtensions.CutTime("08:45:00"), Is.EqualTo("08:30"));
            Assert.That(TimeExtensions.CutTime("23:59:59"), Is.EqualTo("23:30"));
            Assert.That(TimeExtensions.CutTime("00:00:00"), Is.EqualTo("00:00"));
            Assert.That(TimeExtensions.CutTime("12:30:00"), Is.EqualTo("12:30"));

            //下班時間擷取到前一個整點或半點
            Assert.That(TimeExtensions.CutTime("08:15", 30, 2), Is.EqualTo("08:00"));
            Assert.That(TimeExtensions.CutTime("08:45", 30, 2), Is.EqualTo("08:30"));
            Assert.That(TimeExtensions.CutTime("23:59", 30, 2), Is.EqualTo("23:30"));
            Assert.That(TimeExtensions.CutTime("17:38", 30, 2), Is.EqualTo("17:30"));
            Assert.That(TimeExtensions.CutTime("12:30", 30, 2), Is.EqualTo("12:30"));

            Assert.That(TimeExtensions.CutTime("0815", 30, 2), Is.EqualTo("0800"));
            Assert.That(TimeExtensions.CutTime("0845", 30, 2), Is.EqualTo("0830"));
            Assert.That(TimeExtensions.CutTime("2359", 30, 2), Is.EqualTo("2330"));
            Assert.That(TimeExtensions.CutTime("1738", 30, 2), Is.EqualTo("1730"));
            Assert.That(TimeExtensions.CutTime("1239", 30, 2), Is.EqualTo("1230"));

            Assert.That(TimeExtensions.CutTime("18:14", 15, 2), Is.EqualTo("18:00"));
            Assert.That(TimeExtensions.CutTime("18:46", 15, 2), Is.EqualTo("18:45"));
            Assert.That(TimeExtensions.CutTime("23:59", 15, 2), Is.EqualTo("23:45"));
            Assert.That(TimeExtensions.CutTime("17:38", 15, 2), Is.EqualTo("17:30"));
            Assert.That(TimeExtensions.CutTime("12:39", 15, 2), Is.EqualTo("12:30"));
            Assert.That(TimeExtensions.CutTime("12:49", 15, 2), Is.EqualTo("12:45"));

            //上班時間擷取到下個整點或半點
            Assert.That(TimeExtensions.CutTime("08:15", 30, 1), Is.EqualTo("08:30"));
            Assert.That(TimeExtensions.CutTime("08:45", 30, 1), Is.EqualTo("09:00"));
            Assert.That(TimeExtensions.CutTime("23:59", 30, 1), Is.EqualTo("24:00"));
            Assert.That(TimeExtensions.CutTime("00:00", 30, 1), Is.EqualTo("00:00"));
            Assert.That(TimeExtensions.CutTime("12:30", 30, 1), Is.EqualTo("12:30"));

            Assert.That(TimeExtensions.CutTime("0815", 30, 1), Is.EqualTo("0830"));
            Assert.That(TimeExtensions.CutTime("0845", 30, 1), Is.EqualTo("0900"));
            Assert.That(TimeExtensions.CutTime("2359", 30, 1), Is.EqualTo("2400"));
            Assert.That(TimeExtensions.CutTime("0000", 30, 1), Is.EqualTo("0000"));
            Assert.That(TimeExtensions.CutTime("1230", 30, 1), Is.EqualTo("1230"));
            Assert.That(TimeExtensions.CutTime("1231", 30, 1), Is.EqualTo("1300"));
        }
    }

    [Test]
    public void Test_BitArraySetRange()
    {
        using (Assert.EnterMultipleScope())
        {
            var bitArray = new System.Collections.BitArray(10);
            bitArray.Set(2, true);
            bitArray.Set(5, true);
            bitArray.Set(7, true);

            Assert.That(bitArray.Get(0), Is.False);
            Assert.That(bitArray.Get(1), Is.False);
            Assert.That(bitArray.Get(2), Is.True);
            Assert.That(bitArray.Get(3), Is.False);
            Assert.That(bitArray.Get(4), Is.False);
            Assert.That(bitArray.Get(5), Is.True);
            Assert.That(bitArray.Get(6), Is.False);
            Assert.That(bitArray.Get(7), Is.True);
            Assert.That(bitArray.Get(8), Is.False);
            Assert.That(bitArray.Get(9), Is.False);

            bitArray.SetAll(false);
            TimeExtensions.BitArraySetRange(bitArray, 2, 5, true);
            Assert.That(bitArray.Get(0), Is.False);
            Assert.That(bitArray.Get(1), Is.False);
            Assert.That(bitArray.Get(2), Is.True);
            Assert.That(bitArray.Get(3), Is.True);
            Assert.That(bitArray.Get(4), Is.True);
            Assert.That(bitArray.Get(5), Is.True);
            Assert.That(bitArray.Get(6), Is.False);
            Assert.That(bitArray.Get(7), Is.False);
            Assert.That(bitArray.Get(8), Is.False);
            Assert.That(bitArray.Get(9), Is.False);
        }
    }

    [Test]
    public void Test_BitArray1440PosToStartEndTime()
    {
        using (Assert.EnterMultipleScope())
        {
            var bitArray = new System.Collections.BitArray(1440);
            bitArray.Set(480, true); // 08:00
            bitArray.Set(481, true);
            bitArray.Set(482, true);
            bitArray.Set(483, true);
            bitArray.Set(484, true);
            bitArray.Set(485, true);
            bitArray.Set(486, true);
            bitArray.Set(487, true);
            bitArray.Set(488, true);
            bitArray.Set(489, true); // 08:09
            bitArray.Set(540, false); // 09:00

            var (startTime, endTime) = bitArray.BitArray1440PosToStartEndTime(480);
            Assert.That(startTime, Is.EqualTo("08:00"));
            Assert.That(endTime, Is.EqualTo("08:09"));
        }
    }

    [Test]
    public void Test_Postion1440ToTime()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(TimeExtensions.Postion1440ToTime(0), Is.EqualTo("00:00"));
            Assert.That(TimeExtensions.Postion1440ToTime(60), Is.EqualTo("01:00"));
            Assert.That(TimeExtensions.Postion1440ToTime(480), Is.EqualTo("08:00"));
            Assert.That(TimeExtensions.Postion1440ToTime(1439), Is.EqualTo("23:59"));
            Assert.That(TimeExtensions.Postion1440ToTime(1440), Is.EqualTo("24:00"));
            Assert.That(TimeExtensions.Postion1440ToTime(1450), Is.EqualTo("24:10"));
        }
    }


}