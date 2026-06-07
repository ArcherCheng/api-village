// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Linq;
// using System.Reflection;
// using NUnit.Framework;
// using Api.Helpers;

// namespace Api.Helpers.Tests;

// [TestFixture]
// public class RouteAnalyzerTest
// {


//     // [Test]
//     // public IEnumerable<RouteInfo> ImportRouteInfos()
//     // {
//     //     // Arrange
//     //     var mockActionDescriptorCollectionProvider = new Mock<IActionDescriptorCollectionProvider>();
//     //     var actionDescriptors = new List<ControllerActionDescriptor>
//     //     {
//     //         new ControllerActionDescriptor
//     //         {
//     //             ControllerName = "TestController",
//     //             ActionName = "TestAction",
//     //             DisplayName = "TestController.TestAction",
//     //             AttributeRouteInfo = new Microsoft.AspNetCore.Mvc.Routing.AttributeRouteInfo { Template = "test" },
//     //             ActionConstraints = new List<Microsoft.AspNetCore.Mvc.ActionConstraints.IActionConstraintMetadata>
//     //             {
//     //                 new Microsoft.AspNetCore.Mvc.ActionConstraints.HttpMethodActionConstraint(new[] { "GET" })
//     //             }
//     //         }
//     //     };
//     //     mockActionDescriptorCollectionProvider.Setup(x => x.ActionDescriptors.Items).Returns(actionDescriptors);

//     //     var helper = new ImportRouteInfosHelper(mockActionDescriptorCollectionProvider.Object);

//     //     // Act
//     //     var result = helper.ImportRouteInfos().ToList();

//     //     // Assert
//     //     Assert.AreEqual(1, result.Count);
//     //     Assert.AreEqual("GET", result[0].HttpMethod);
//     //     Assert.AreEqual("/test", result[0].HttpRoute);
//     //     Assert.AreEqual("TestController.TestAction", result[0].DisplayName);
//     // }

//     // [Test]
//     // public IEnumerable<RouteInfo> ImportReportInfos(string resourcesDir, string reportsDir)
//     // {

//     // }
// }