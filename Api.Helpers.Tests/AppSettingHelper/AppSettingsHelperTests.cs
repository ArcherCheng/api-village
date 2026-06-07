using Api.Helpers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Api.Helpers.Tests;

[TestFixture]
public class AppSettingsHelperTests
{
    private IConfiguration? _originalConfiguration;
    private string _testDir = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestTemp", Guid.NewGuid().ToString());
        if (Directory.Exists(_testDir))
        {
            Directory.Delete(_testDir, true);
        }
        Directory.CreateDirectory(_testDir);
        Debug.WriteLine($"Created test directory: {_testDir}");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (Directory.Exists(_testDir))
        {
            Directory.Delete(_testDir, true);
        }
    }

    [SetUp]
    public void Setup()
    {
        // Save the original configuration to restore it later
        _originalConfiguration = AppSettingsHelper.Configuration;
    }

    [TearDown]
    public void Teardown()
    {
        // Restore the original configuration
        AppSettingsHelper.Configuration = _originalConfiguration!;
    }

    private static void SetupInMemoryConfiguration(Dictionary<string, string?> configValues)
    {
        AppSettingsHelper.Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();
    }

    [Test]
    public void Test_BuildConfiguration()
    {
        // Act
        var config = AppSettingsHelper.BuildConfiguration();

        // Assert
        Assert.That(config, Is.Not.Null);
        Assert.That(config.GetSection("ConnectionStrings:DefaultConnection").Value, Is.Null.Or.Not.Empty);
    }

    [Test]
    public void ConnectionStr_WhenConfigExists_ReturnsConfigValue()
    {
        // Arrange
        const string expected = "TestConnection";
        var config = new Dictionary<string, string?>
        {
            { "ConnectionStrings:DefaultConnection", expected }
        };
        SetupInMemoryConfiguration(config);

        var result = AppSettingsHelper.ConnectionStr();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ConnectionStr_WhenConfigMissing_ReturnsFallbackValue()
    {
        // Arrange
        const string expected = "server=.\\SQLEXPRESS01; database=Village2026; User ID=SqlDbUser; Password=sql@123456; Integrated Security=true;TrustServerCertificate=true;Persist Security Info=False;";
        SetupInMemoryConfiguration(new Dictionary<string, string?>());

        // Act
        var result = AppSettingsHelper.ConnectionStr();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ResourcesFolder_WhenConfigExists_ReturnsConfigValue()
    {
        // Arrange
        const string expected = "TestResources";
        var config = new Dictionary<string, string?>
        {
            { "AppSettings:Resources", expected }
        };
        SetupInMemoryConfiguration(config);

        // Act
        var result = AppSettingsHelper.ResourcesFolder();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ResourcesFolder_WhenConfigMissing_ReturnsFallbackValue()
    {
        // Arrange
        const string expected = "Resources";
        SetupInMemoryConfiguration(new Dictionary<string, string?>());

        // Act
        var result = AppSettingsHelper.ResourcesFolder();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ReportTemplateFolder_WhenConfigExists_ReturnsConfigValue()
    {
        // Arrange
        const string expected = "TestReportTemplate";
        var config = new Dictionary<string, string?>
        {
            { "AppSettings:ReportTemplate", expected }
        };
        SetupInMemoryConfiguration(config);

        // Act
        var result = AppSettingsHelper.ReportTemplateFolder();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ReportTemplateFolder_WhenConfigMissing_ReturnsFallbackValue()
    {
        // Arrange
        const string expected = "ReportTemplate";
        SetupInMemoryConfiguration(new Dictionary<string, string?>());

        // Act
        var result = AppSettingsHelper.ReportTemplateFolder();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void JwtValidIssuer_WhenConfigExists_ReturnsConfigValue()
    {
        // Arrange
        const string expected = "TestIssuer";
        var config = new Dictionary<string, string?>
        {
            { "JwtSettings:ValidIssuer", expected }
        };
        SetupInMemoryConfiguration(config);

        // Act
        var result = AppSettingsHelper.JwtValidIssuer();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void JwtValidIssuer_WhenConfigMissing_ReturnsFallbackValue()
    {
        // Arrange
        const string expected = "https://*.justdo.tw";
        SetupInMemoryConfiguration(new Dictionary<string, string?>());

        // Act
        var result = AppSettingsHelper.JwtValidIssuer();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void JwtValidAudience_WhenConfigExists_ReturnsConfigValue()
    {
        // Arrange
        const string expected = "http://test.audience.com";
        var config = new Dictionary<string, string?>
        {
            { "JwtSettings:ValidAudience", expected }
        };
        SetupInMemoryConfiguration(config);

        // Act
        var result = AppSettingsHelper.JwtValidAudience();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void JwtValidAudience_WhenConfigMissing_ReturnsFallbackValue()
    {
        // Arrange
        const string expected = "https://localhost:5001";
        SetupInMemoryConfiguration(new Dictionary<string, string?>());

        // Act
        var result = AppSettingsHelper.JwtValidAudience();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetReportTemplateFilePath_WhenFileExists_ReturnsCorrectPath()
    {
        // Arrange
        var originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(_testDir);

        const string resourcesFolder = "TestRes";
        const string reportTemplateFolder = "TestTpl";
        const string reportPath = "Employee";
        const string reportName = "EmployeeCert";

        var config = new Dictionary<string, string?>
        {
            { "AppSettings:Resources", resourcesFolder },
            { "AppSettings:ReportTemplate", reportTemplateFolder }
        };
        SetupInMemoryConfiguration(config);

        var expectedPath = Path.Combine(resourcesFolder, reportTemplateFolder, reportPath, reportName + ".rdlc");
        var fileDir = Path.GetDirectoryName(expectedPath);
        if (!string.IsNullOrEmpty(fileDir))
        {
            Directory.CreateDirectory(fileDir);
        }
        File.Create(expectedPath).Close();

        try
        {
            // Act
            var result = AppSettingsHelper.GetReportTemplateFilePath(reportPath, reportName);

            // Assert
            Assert.That(result, Is.EqualTo(expectedPath));
        }
        finally
        {
            // Cleanup
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }

    [Test]
    public void GetReportTemplateFilePath_WhenFileNotExists_ThrowsException()
    {
        // Arrange
        var originalDirectory = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(_testDir);

        const string resourcesFolder = "TestRes";
        const string reportTemplateFolder = "TestTpl";
        const string reportPath = "Employee";
        const string reportName = "NonExistentReport";

        var config = new Dictionary<string, string?>
        {
            { "AppSettings:Resources", resourcesFolder },
            { "AppSettings:ReportTemplate", reportTemplateFolder }
        };
        SetupInMemoryConfiguration(config);

        try
        {
            // Act & Assert
            var ex = Assert.Throws<Exception>(() => AppSettingsHelper.GetReportTemplateFilePath(reportPath, reportName));
            Assert.That(ex?.Message, Is.EqualTo("File not find"));
        }
        finally
        {
            Directory.SetCurrentDirectory(originalDirectory);
        }
    }

    [TestCase("true", true)]
    [TestCase("false", false)]
    [TestCase("TRUE", false)] // Note: implementation is case-sensitive
    [TestCase(null, false)]
    [TestCase("any other string", false)]
    public void IsDevelopment_ReturnsCorrectBoolean(string? isDevelopmentValue, bool expected)
    {
        // Arrange
        var config = new Dictionary<string, string?>
        {
            { "AppSettings:IsDevelopment", isDevelopmentValue }
        };
        SetupInMemoryConfiguration(config);

        // Act
        var result = AppSettingsHelper.IsDevelopment();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ApplicationRootDirectory_ReturnsExecutingAssemblyDirectory()
    {
        // Arrange
        var expected = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        // Act
        var result = AppSettingsHelper.ApplicationRootDirectory();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void GetAppSettings_LoadsConfigurationFromFile()
    {
        // Arrange
        var appRoot = AppSettingsHelper.ApplicationRootDirectory();
        var appSettingsPath = Path.Combine(appRoot, "appsettings.json");
        var originalContent = File.Exists(appSettingsPath) ? File.ReadAllText(appSettingsPath) : null;
        var appSettingsContent = @"{ ""TestKey"": ""TestValue"" }";
        File.WriteAllText(appSettingsPath, appSettingsContent);

        try
        {
            // Act
            var configuration = AppSettingsHelper.GetAppSettings();
            var value = configuration["TestKey"];

            // Assert
            Assert.That(value, Is.EqualTo("TestValue"));
        }
        finally
        {
            // Cleanup
            if (originalContent != null)
            {
                File.WriteAllText(appSettingsPath, originalContent);
            }
            else if(File.Exists(appSettingsPath))
            {
                File.Delete(appSettingsPath);
            }
        }
    }
}