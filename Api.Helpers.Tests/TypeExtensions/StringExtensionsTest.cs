// Test IsNullOrEmpty with null input returns true
using NUnit.Framework;
using Api.Helpers;
namespace Api.Helpers.Tests;

[TestFixture]
public class StringExtensionsTests
{

    // Test Method 1
    [Test]
    public void Test_ToInt()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.ToInt("123"), Is.EqualTo(123));
            Assert.That(StringExtensions.ToInt(null), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt("abc"), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt(" "), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt("-123"), Is.EqualTo(-123));
            Assert.That(StringExtensions.ToInt("123.95"), Is.EqualTo(123));
            Assert.That(StringExtensions.ToInt("-123.95"), Is.EqualTo(-123));
            Assert.That("123".ToInt(), Is.EqualTo(123));
            Assert.That("123.95".ToInt(), Is.EqualTo(123));
        }

        Assert.Multiple(() =>
        {
            Assert.That(StringExtensions.ToInt("123"), Is.EqualTo(123));
            Assert.That(StringExtensions.ToInt(null), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt("abc"), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt(" "), Is.EqualTo(0));
            Assert.That(StringExtensions.ToInt("-123"), Is.EqualTo(-123));
            Assert.That(StringExtensions.ToInt("123.95"), Is.EqualTo(123));
            Assert.That(StringExtensions.ToInt("-123.95"), Is.EqualTo(-123));
            Assert.That("123".ToInt(), Is.EqualTo(123));
            Assert.That("123.95".ToInt(), Is.EqualTo(123));
        });
    }

    // Test Method 2
    [Test]
    [TestCase("123", ExpectedResult = 123)]
    [TestCase("123.45", ExpectedResult = 123)]
    [TestCase("-123.45", ExpectedResult = -123)]
    [TestCase("abc", ExpectedResult = 0)]
    [TestCase(null, ExpectedResult = 0)]
    [TestCase(" ", ExpectedResult = 0)]
    public int TestCase_ToInt(string? input )
    {
        return StringExtensions.ToInt(input);
    }

    [Test]
    public void Test_ToDecimal()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.ToDecimal("123"), Is.EqualTo(123));
            Assert.That(StringExtensions.ToDecimal(null), Is.EqualTo(0));
            Assert.That(StringExtensions.ToDecimal("abc"), Is.EqualTo(0));
            Assert.That(StringExtensions.ToDecimal(" "), Is.EqualTo(0));
            Assert.That(StringExtensions.ToDecimal("-123"), Is.EqualTo(-123));
            Assert.That(StringExtensions.ToDecimal("123.95"), Is.EqualTo(123.95));
        }
    }

    [Test]
    public void Test_ToGuid()
    {
       // Arrange
        string? input = null;
        // Act
        Guid? result = StringExtensions.ToGuid(input);
        // Assert
        Assert.That(result, Is.Null);

        input = "12345678-1234-1234-1234-123456789012";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.EqualTo(new Guid("12345678-1234-1234-1234-123456789012")));

        input = "00000000-0000-0000-0000-000000000000";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.EqualTo(new Guid("00000000-0000-0000-0000-000000000000")));
        Assert.That(result, Is.EqualTo(Guid.Empty));

        input = "12345678-1234-1234-1234-123456789012-12345678";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);
        // Assert.That(result, Is.EqualTo(Guid.Empty));

        input = "12345678-1234-1234-1234-1234567890123";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);

        input = "abc";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);

        input = " ";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);

        input = "";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);

        input = "abcdefgh-abcd-abcd-abcd-abcdefghijkl";
        result = StringExtensions.ToGuid(input);
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Test_ToBoolean()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.ToBoolean(null), Is.False);
            Assert.That(StringExtensions.ToBoolean("123"), Is.True);
            Assert.That(StringExtensions.ToBoolean("-123"), Is.False);
            Assert.That(StringExtensions.ToBoolean("123.95"), Is.True);
            Assert.That(StringExtensions.ToBoolean("1"), Is.True);
            Assert.That(StringExtensions.ToBoolean("0"), Is.False);
            Assert.That(StringExtensions.ToBoolean("abc"), Is.False);
            Assert.That(StringExtensions.ToBoolean(" "), Is.False);
            Assert.That(StringExtensions.ToBoolean("true"), Is.True);
            Assert.That(StringExtensions.ToBoolean("false"), Is.False);
            Assert.That(StringExtensions.ToBoolean("True"), Is.True);
            Assert.That(StringExtensions.ToBoolean("False"), Is.False);
            Assert.That(StringExtensions.ToBoolean("yes"), Is.True);
            Assert.That(StringExtensions.ToBoolean("no"), Is.False);
            Assert.That(StringExtensions.ToBoolean("on"), Is.True);
            Assert.That(StringExtensions.ToBoolean("off"), Is.False);
            Assert.That(StringExtensions.ToBoolean("T"), Is.True);
            Assert.That(StringExtensions.ToBoolean("F"), Is.False);
            Assert.That(StringExtensions.ToBoolean("Y"), Is.True);
            Assert.That(StringExtensions.ToBoolean("Yes"), Is.True);
            Assert.That(StringExtensions.ToBoolean("Yes1"), Is.False);
            Assert.That(StringExtensions.ToBoolean("N"), Is.False);
            Assert.That(StringExtensions.ToBoolean("a"), Is.True);
            Assert.That(StringExtensions.ToBoolean("abc"), Is.False);
            Assert.That(StringExtensions.ToBoolean(" "), Is.False);
            Assert.That(StringExtensions.ToBoolean("真"), Is.True);
            Assert.That(StringExtensions.ToBoolean("假"), Is.False);
            Assert.That(StringExtensions.ToBoolean("是"), Is.True);
            Assert.That(StringExtensions.ToBoolean("否"), Is.False);
            Assert.That(StringExtensions.ToBoolean("有"), Is.True);
            Assert.That(StringExtensions.ToBoolean("無"), Is.False);
            Assert.That(StringExtensions.ToBoolean("有1"), Is.False);
            Assert.That(StringExtensions.ToBoolean("有2"), Is.False);
        }
    }

    [Test]
    public void Test_ToPascal()
    {
        // Arrange
        string? input = null;
        // Act
        string result = StringExtensions.ToPascal(input);
        // Assert
        Assert.That(result, Is.EqualTo(""));

        input = "abcdKeyValue";
        result = StringExtensions.ToPascal(input);
        Assert.That(result, Is.EqualTo("AbcdKeyValue"));

        input = "AbcdKeyValue";
        result = StringExtensions.ToPascal(input);
        Assert.That(result, Is.EqualTo("AbcdKeyValue"));
    }

    [Test]
    public void Test_ToCamel()
    {
        // Arrange
        string? input = null;
        // Act
        string result = StringExtensions.ToCamel(input);
        // Assert
        Assert.That(result, Is.EqualTo(""));

        input = "KeyValue";
        result = StringExtensions.ToCamel(input);
        Assert.That(result, Is.EqualTo("keyValue"));

        input = "KEYValue";
        result = StringExtensions.ToCamel(input);
        Assert.That(result, Is.EqualTo("kEYValue"));
    }

    [Test]
    public void Test_IsGreaterThan()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsGreaterThan(null, null), Is.False);
            Assert.That(StringExtensions.IsGreaterThan(null, "abc"), Is.False);
            Assert.That(StringExtensions.IsGreaterThan("abc", null), Is.False);
            Assert.That(StringExtensions.IsGreaterThan("abc", "abc"), Is.False);
            Assert.That(StringExtensions.IsGreaterThan("abc", "abcd"), Is.False);
            Assert.That(StringExtensions.IsGreaterThan("abcd", "abc"), Is.True);
        }
    }

    [Test]
    public void Test_IsGreaterThanOrEqual()
    {
         using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsGreaterThanOrEqual(null, null), Is.True);
            Assert.That(StringExtensions.IsGreaterThanOrEqual(null, "abc"), Is.False);
            Assert.That(StringExtensions.IsGreaterThanOrEqual("abc", null), Is.False);
            Assert.That(StringExtensions.IsGreaterThanOrEqual("abc", "abc"), Is.True);
            Assert.That(StringExtensions.IsGreaterThanOrEqual("abc", "abcd"), Is.False);
            Assert.That(StringExtensions.IsGreaterThanOrEqual("abcd", "abc"), Is.True);
        }
    }

    [Test]
    public void Test_IsLessThan()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsLessThan(null, null), Is.False);
            Assert.That(StringExtensions.IsLessThan(null, "abc"), Is.False);
            Assert.That(StringExtensions.IsLessThan("abc", null), Is.False);
            Assert.That(StringExtensions.IsLessThan("abc", "abc"), Is.False);
            Assert.That(StringExtensions.IsLessThan("abc", "abcd"), Is.True);
            Assert.That(StringExtensions.IsLessThan("abcd", "abc"), Is.False);
        }
    }

    [Test]
    public void Test_IsLessThanOrEqual()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsLessThanOrEqual(null, null), Is.True);
            Assert.That(StringExtensions.IsLessThanOrEqual(null, "abc"), Is.False);
            Assert.That(StringExtensions.IsLessThanOrEqual("abc", null), Is.False);
            Assert.That(StringExtensions.IsLessThanOrEqual("abc", "abc"), Is.True);
            Assert.That(StringExtensions.IsLessThanOrEqual("abc", "abcd"), Is.True);
            Assert.That(StringExtensions.IsLessThanOrEqual("abcd", "abc"), Is.False);
        }
    }

    [Test]
    public void Test_IsMatchPattern()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsMatchPattern(null, null), Is.False);
            Assert.That(StringExtensions.IsMatchPattern(null, "abc"), Is.False);
            Assert.That(StringExtensions.IsMatchPattern("abc", null), Is.False);
            Assert.That(StringExtensions.IsMatchPattern("abc", "abc"), Is.True);
            Assert.That(StringExtensions.IsMatchPattern("abc", "abcd"), Is.False);
            Assert.That(StringExtensions.IsMatchPattern("abcd", "abc"), Is.True);
            Assert.That(StringExtensions.IsMatchPattern("abcdefg", "\\w+"), Is.True);
            Assert.That(StringExtensions.IsMatchPattern("abcdefg", "\\d+"), Is.False);
            Assert.That(StringExtensions.IsMatchPattern("abcd1efg", "\\d+"), Is.True);
            Assert.That(StringExtensions.IsMatchPattern("abcdefg@gmail.com", "\\w+@\\w+\\.\\w+"), Is.True);

        }
    }

    [Test]
    public void Test_GetMatchPattern()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.GetMatchPattern(null, null), Is.Empty);
            Assert.That(StringExtensions.GetMatchPattern(null, "abc"), Is.Empty);
            Assert.That(StringExtensions.GetMatchPattern("abc", null), Is.Empty);
            Assert.That(StringExtensions.GetMatchPattern("abc", "abc"), Is.EqualTo("abc"));
            Assert.That(StringExtensions.GetMatchPattern("abc", "abcd"), Is.Empty);
            Assert.That(StringExtensions.GetMatchPattern("abcdefg", "\\w+"), Is.EqualTo("abcdefg"));
            Assert.That(StringExtensions.GetMatchPattern("abcdefg", "\\d+"), Is.Empty);
            Assert.That(StringExtensions.GetMatchPattern("abcd1ef3g", "\\d+"), Is.EqualTo("1"));
            Assert.That(StringExtensions.GetMatchPattern("abcdefg@gmail.com", "\\w+@\\w+\\.\\w+"), Is.EqualTo("abcdefg@gmail.com"));
        }
    }

    [Test]
    public void Test_IsEmail()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsEmail(null), Is.False);
            Assert.That(StringExtensions.IsEmail("abc"), Is.False);
            Assert.That(StringExtensions.IsEmail("abc@"), Is.False);
            Assert.That(StringExtensions.IsEmail("abc@gmail"), Is.False);
            Assert.That(StringExtensions.IsEmail("abc@gmail."), Is.False);
            Assert.That(StringExtensions.IsEmail("abc@gmail.c"), Is.True);
            Assert.That(StringExtensions.IsEmail("abc@gmail.com"), Is.True);
            Assert.That(StringExtensions.IsEmail("abc@gmail.com.tw"), Is.True);
            Assert.That(StringExtensions.IsEmail("abc@gmail.com.tw.cc"), Is.True);
            Assert.That(StringExtensions.IsEmail("abc-gmail.com.tw"), Is.False);
            Assert.That(StringExtensions.IsEmail("abc.gmail.com.tw"), Is.False);
        }
    }

    [Test]
    public void Test_IsInt()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(StringExtensions.IsInt(null), Is.False);
            Assert.That(StringExtensions.IsInt("123"), Is.True);
            Assert.That(StringExtensions.IsInt("123.45"), Is.False);
            Assert.That(StringExtensions.IsInt("abc"), Is.False);
            Assert.That(StringExtensions.IsInt(""), Is.False);
            Assert.That(StringExtensions.IsInt(" "), Is.False);
            Assert.That(StringExtensions.IsInt("123  "), Is.True);
            Assert.That(StringExtensions.IsInt(" 12a3"), Is.False);
            Assert.That(StringExtensions.IsInt("0x123"), Is.False);
        }

    }

    [Test]
    public void Test_IsDouble()
    {
        // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsDouble(input);
        // Assert
        Assert.That(result, Is.False);

        input = "123.45";
        result = StringExtensions.IsDouble(input);
        Assert.That(result, Is.True);

        input = "123";
        result = StringExtensions.IsDouble(input);
        Assert.That(result, Is.True);

        input = "invalid";
        result = StringExtensions.IsDouble(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test_IsDecimal()
    {
        // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsDecimal(input);
        // Assert
        Assert.That(result, Is.False);

        input = "123";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.True);

        input = "123.45";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.True);

        input = "abc";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.False);

        input = ".";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.False);

        input = "23.";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.True);

        input = "123  ";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.True);

        input = " 12a3";
        result = StringExtensions.IsDecimal(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test_IsNumeric()
    {
        // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsNumeric(input);
        // Assert
        Assert.That(result, Is.False);

        input = "123.45";
        result = StringExtensions.IsNumeric(input);
        Assert.That(result, Is.True);

        input = "123";
        result = StringExtensions.IsNumeric(input);
        Assert.That(result, Is.True);

        input = "invalid";
        result = StringExtensions.IsNumeric(input);
        Assert.That(result, Is.False);
    }

    [Test]
    public void Test_IsBoolean()
    {
        // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsBoolean(input);
        // Assert
        Assert.That(result, Is.False);
        input = "true";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "false";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "True";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "False";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "yes";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "On";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);

        input = "T";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "F";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "Y";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "N";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "abc";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = " ";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);

        input = "0";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "1";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "123";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "-123";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);
        input = "123.45";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.True);
        input = "0";
        result = StringExtensions.IsBoolean(input);
        Assert.That(result, Is.False);

    }

    // [Test]
    // [TestCase("True", ExpectedResult = true)]
    // [TestCase("False", ExpectedResult = false)]
    // [TestCase("T", ExpectedResult = true)]
    // [TestCase("F", ExpectedResult = false)]
    // [TestCase("Y", ExpectedResult = true)]
    // [TestCase("Yes", ExpectedResult = true)]
    // [TestCase("N", ExpectedResult = false)]
    // [TestCase("No", ExpectedResult = false)]
    // [TestCase("1", ExpectedResult = true)]
    // [TestCase("0", ExpectedResult = false)]
    // [TestCase("a", ExpectedResult = true)]
    // [TestCase("abc", ExpectedResult = false)]
    // [TestCase(null, ExpectedResult = false)]
    // [TestCase(" ", ExpectedResult = false)]
    // [TestCase(" ", ExpectedResult = false)]
    // [TestCase("-123", ExpectedResult = false)]
    // [TestCase("123.45", ExpectedResult = true)]
    // [TestCase("123", ExpectedResult = true)]
    // [TestCase("真", ExpectedResult = true)]
    // [TestCase("假", ExpectedResult = false)]
    // [TestCase("是", ExpectedResult = true)]
    // [TestCase("否", ExpectedResult = false)]
    // [TestCase("有", ExpectedResult = true)]
    // [TestCase("無", ExpectedResult = false)]
    // public bool TestCase_IsBoolean(string? input)
    // {
    //     // Act
    //     bool result = StringExtensions.IsBoolean(input);
    //     return result;
    // }

    [Test]
    public void Test_IsGuid()
    {
       // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsGuid(input);
        // Assert
        Assert.That(result, Is.EqualTo(false));

        input = "123";
        result = StringExtensions.IsGuid(input);
        Assert.That(result, Is.EqualTo(false));

        input = "12345678-1234-1234-1234-123456789012";
        result = StringExtensions.IsGuid(input);
        Assert.That(result, Is.EqualTo(true));

        input = "12345678-1234-1234-1234-1234567890123";
        result = StringExtensions.IsGuid(input);
        Assert.That(result, Is.EqualTo(false));

        input = "12345678-1234-1234-1234-123456789012-12345678";
        result = StringExtensions.IsGuid(input);
        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public void Test_ParseString()
    {
        // Arrange
        string? input = null;
        // Act
        string result = StringExtensions.ParseString(ref input, ",");
        // Assert
        Assert.That(result, Is.Empty);

        input = "A,B,C";
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("A"));
        Assert.That(input, Is.EqualTo("B,C"));
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("B"));
        Assert.That(input, Is.EqualTo("C"));
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("C"));
        Assert.That(input, Is.EqualTo(""));

        input = "A, B, C ";
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("A"));
        Assert.That(input, Is.EqualTo(" B, C "));
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("B"));
        Assert.That(input, Is.EqualTo(" C "));
        result = StringExtensions.ParseString(ref input, ",");
        Assert.That(result, Is.EqualTo("C"));
        Assert.That(input, Is.EqualTo(""));


        input = " Pr01 + Pr02 + Pr03 + Pr04 ";
        result = StringExtensions.ParseString(ref input, "+");
        Assert.That(result, Is.EqualTo("Pr01"));
        Assert.That(input, Is.EqualTo(" Pr02 + Pr03 + Pr04 "));
        result = StringExtensions.ParseString(ref input, "+");
        Assert.That(result, Is.EqualTo("Pr02"));
        Assert.That(input, Is.EqualTo(" Pr03 + Pr04 "));
        result = StringExtensions.ParseString(ref input, "+");
        Assert.That(result, Is.EqualTo("Pr03"));
        Assert.That(input, Is.EqualTo(" Pr04 "));
        result = StringExtensions.ParseString(ref input, "+");
        Assert.That(result, Is.EqualTo("Pr04"));
        Assert.That(input, Is.EqualTo(""));

    }

    [Test]
    public void Test_ParseLenth()
    {
        // Arrange
        string? input = null;
        // Act
        string result = StringExtensions.ParseLenth(ref input, 1);
        // Assert
        Assert.That(result, Is.EqualTo(""));
        Assert.That(input, Is.EqualTo(""));

        input = "0811N 0921N 1231N 1258N 1721N";
        result = StringExtensions.ParseLenth(ref input, 6);
        Assert.That(result, Is.EqualTo("0811N"));
        Assert.That(input, Is.EqualTo("0921N 1231N 1258N 1721N"));
        result = StringExtensions.ParseLenth(ref input, 6);
        Assert.That(result, Is.EqualTo("0921N"));
        Assert.That(input, Is.EqualTo("1231N 1258N 1721N"));
        result = StringExtensions.ParseLenth(ref input, 6);
        Assert.That(result, Is.EqualTo("1231N"));
        Assert.That(input, Is.EqualTo("1258N 1721N"));
        result = StringExtensions.ParseLenth(ref input, 6);
        Assert.That(result, Is.EqualTo("1258N"));
        Assert.That(input, Is.EqualTo("1721N"));
        result = StringExtensions.ParseLenth(ref input, 6);
        Assert.That(result, Is.EqualTo("1721N"));
        Assert.That(input, Is.EqualTo(""));
    }

    // [Test]
    // public void Test_ParseRemoveString()
    // {
    //     // Arrange
    //     string? input = null;
    //     // Act
    //     string result = StringExtensions.ParseRemoveString(ref input, "+");
    //     // Assert
    //     Assert.That(result, Is.EqualTo(""));
    //     Assert.That(input, Is.Null);

    //     input = "Pr01+Pr02+Pr03+Pr04";
    //     result = StringExtensions.ParseRemoveString(ref input, "+");
    //     Assert.That(result, Is.EqualTo("Pr01Pr02+Pr03+Pr04"));
    //     Assert.That(input, Is.EqualTo("Pr01Pr02+Pr03+Pr04"));
    // }

    // [Test]
    // public void Test_RemoveString()
    // {
    //     // Arrange
    //     string? input = null;
    //     // Act
    //     string result = StringExtensions.RemoveString(input, "+");
    //     // Assert
    //     Assert.That(result, Is.EqualTo(""));

    //     input = "Pr01+Pr02+Pr03+Pr04";
    //     result = StringExtensions.RemoveString(input, "+");
    //     Assert.That(result, Is.EqualTo("Pr01Pr02+Pr03+Pr04"));
    // }

    [Test]
    public void Test_AddSqlSingleQuote()
    {
        // Arrange
        string? input = null;
        // Act
        string result = StringExtensions.AddSqlSingleQuote(input);
        // Assert
        Assert.That(result, Is.EqualTo(""));

        input = "A001,B002,C003";
        result = StringExtensions.AddSqlSingleQuote(input);
        Assert.That(result, Is.EqualTo("'A001','B002','C003'"));

        input = "A1,B02,C003";
        result = StringExtensions.AddSqlSingleQuote(input);
        Assert.That(result, Is.EqualTo("'A1','B02','C003'"));

        input = "A1, B02, C003";
        result = StringExtensions.AddSqlSingleQuote(input);
        Assert.That(result, Is.EqualTo("'A1','B02','C003'"));

        input = " A1, B02 , C003";
        result = StringExtensions.AddSqlSingleQuote(input);
        Assert.That(result, Is.EqualTo("'A1','B02','C003'"));

    }

    [Test]
    public void Test_IsTaiwanId()
    {
        // Arrange
        string? input = null;
        // Act
        bool result = StringExtensions.IsTaiwanId(input);
        // Assert
        Assert.That(result, Is.False);

        input = "A123456789";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.True);

        input = "invalid";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.False);

        input = "H120955737";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.True);

        input = "H1209557370";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.False);

        input = "H120955736";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.False);

        input = "A1234567890";
        result = StringExtensions.IsTaiwanId(input);
        Assert.That(result, Is.False);
    }





}
