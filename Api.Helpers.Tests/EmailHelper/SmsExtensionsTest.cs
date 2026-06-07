using NUnit.Framework;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Api.Helpers;

namespace Api.Helpers.Tests;

[TestFixture]
public class SmsExtentionsTest
{
    // [Test]
    // public async Task SendSmsWithCorrectParameters()
    // {
    //     // Arrange
    //     // var empName = "Archer Cheng";
    //     // var mobileTel = "0970922888";
    //     // var smsBody = "Test message";
    //     // Act
    //     //var result = await SmsExtentions.SendSmsAsync(empName, mobileTel, smsBody);

    //     // Assert
    //     // Assert.IsFalse(result.IsSuccess);
    //     // Assert.AreEqual(smsBody, result.Message);
    //     //Assert.AreEqual("帳號已被停用", result.Error);
    // }

    // [Test]
    // public void GetSmsSettings()
    // {
    //     // Set up
    //     var expectedModel = new Api.Helpers.SmsModel();
    //     expectedModel.ReqUrl = "https://smsapi.mitake.com.tw/api/mtk/SmSend?CharsetURL=UTF-8";
    //     expectedModel.UserName = "87717558SMS";
    //     expectedModel.Password = "Modern@9157";
    //     expectedModel.SendCompanyName = "新軟資訊";
    //     expectedModel.TestReceiveMobileTel = "0970922888";

    //     // Act
    //     var result = SmsExtentions.GetSmsSettings();

    //     // Assert
    //     Assert.That(result,Is.Not.Null);
    //     Assert.That(expectedModel.ReqUrl, Is.EqualTo(result.ReqUrl));
    //     Assert.That(expectedModel.UserName, Is.EqualTo(result.UserName));
    //     Assert.That(expectedModel.Password, Is.EqualTo(result.Password));
    //     Assert.That(expectedModel.SendCompanyName, Is.EqualTo(result.SendCompanyName));
    //     Assert.That(expectedModel.TestReceiveMobileTel, Is.EqualTo(result.TestReceiveMobileTel));
    // }
    
}
