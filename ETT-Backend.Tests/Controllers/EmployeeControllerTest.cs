using Xunit;
using Moq;
using ETT_Backend.Services;
using ETT_Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ETT_Backend.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ETT_Backend.Controllers.Test
{
  public class EmployeeControllerTest
  {
    [Fact]
    public void RetrieveEmployeeInfoSuccess() 
    {
      var testEmail = "test@callibrity.com";
      var employeeInfo = new EttEmployee("test", "name", "Developer");
      var empService = new Mock<IEmployeeService>();
      empService.Setup(serv => serv.RetrieveEmployeeInfo(It.IsAny<string>())).Returns(employeeInfo);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetEmployeeInfo().Result;
      var okResult = result as ObjectResult;
      var objString = okResult.Value.ToString();
      var resValue = JsonConvert.DeserializeObject<EttEmployee>(objString);

      Assert.NotNull(okResult);
      Assert.Equal(200, okResult.StatusCode);
      Assert.Equal("test", resValue.FirstName);
      Assert.Equal("name", resValue.LastName);
      Assert.Equal("Developer", resValue.Role);
      empService.Verify(x => x.RetrieveEmployeeInfo(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void RetrieveEmployeeInfoFail() 
    {
      var testEmail = "test@callibrity.com";
      var empService = new Mock<IEmployeeService>();
      EttEmployee mockRes = null; // Return a null response from service call
      empService.Setup(serv => serv.RetrieveEmployeeInfo(It.IsAny<string>())).Returns(mockRes);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetEmployeeInfo().Result;
      var notFoundResult = result as NotFoundResult;

      Assert.NotNull(notFoundResult);
      Assert.Equal(404, notFoundResult.StatusCode);
      empService.Verify(x => x.RetrieveEmployeeInfo(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void RetrieveEmployeeMetricsSuccess()
    {
      System.DateTime dt = new System.DateTime();
      var testEmail = "test@callibrity.com";
      var employee = new EmployeeMetrics("1111", "test", "name", 100, 100, 100, 100, 100, dt);
      var mockEmployeeRes = new EmployeeResponse(employee);
      var empService = new Mock<IEmployeeService>();
      empService.Setup(serv => serv.RetrieveEmployeeMetrics(It.IsAny<string>())).Returns(mockEmployeeRes);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetEmployeeMetrics().Result;
      var okResult = result as ObjectResult;
      var objString = okResult.Value.ToString();
      var resValue = JsonConvert.DeserializeObject<EmployeeResponse>(objString);

      Assert.NotNull(okResult);
      Assert.Equal(200, okResult.StatusCode);
      Assert.Equal("1111", resValue.EmployeeId);
      empService.Verify(x => x.RetrieveEmployeeMetrics(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void RetrieveEmployeeMetricsFail()
    {
      var testEmail = "test@callibrity.com";
      var empService = new Mock<IEmployeeService>();
      EmployeeResponse mockRes = null; // Return a null response from service call
      empService.Setup(serv => serv.RetrieveEmployeeMetrics(It.IsAny<string>())).Returns(mockRes);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetEmployeeMetrics().Result;
      var notFoundResult = result as NotFoundResult;

      Assert.NotNull(notFoundResult);
      Assert.Equal(404, notFoundResult.StatusCode);
      empService.Verify(x => x.RetrieveEmployeeMetrics(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void RetrieveAllEmployeeMetricsSuccess()
    {
      System.DateTime dt = new System.DateTime();
      var testEmail = "test@callibrity.com";
      var employee1 = new EmployeeMetrics("1111", "test1", "name1", 100, 100, 100, 100, 100, dt);
      var employee2 = new EmployeeMetrics("2222", "test2", "name2", 200, 200, 200, 200, 200, dt);
      var mockEmployeeRes = 
        new List<EmployeeResponse>() 
        {
          new EmployeeResponse(employee1),
          new EmployeeResponse(employee2)
        };
      var empService = new Mock<IEmployeeService>();
      empService.Setup(serv => serv.RetrieveAllEmployeeMetrics()).Returns(mockEmployeeRes);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetAllEmployeeMetrics().Result;
      var okResult = result as ObjectResult;
      var objString = okResult.Value.ToString();
      var resValue = JsonConvert.DeserializeObject<List<EmployeeResponse>>(objString);

      Assert.NotNull(okResult);
      Assert.Equal(200, okResult.StatusCode);
      Assert.Equal("1111", resValue[0].EmployeeId);
      Assert.Equal("2222", resValue[1].EmployeeId);
      Assert.Equal(100, resValue[0].Billable.CurrentHours);
      Assert.Equal(200, resValue[1].Billable.CurrentHours);
      empService.Verify(x => x.RetrieveAllEmployeeMetrics(), Times.Once);
    }

    [Fact]
    public void RetrieveAllEmployeeMetricsFail()
    {
      var testEmail = "test@callibrity.com";
      var empService = new Mock<IEmployeeService>();
      List<EmployeeResponse> mockRes = null; // Return a null response from service call
      empService.Setup(serv => serv.RetrieveAllEmployeeMetrics()).Returns(mockRes);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new EmployeeController(empService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.GetAllEmployeeMetrics().Result;
      var notFoundResult = result as NotFoundResult;

      Assert.NotNull(notFoundResult);
      Assert.Equal(404, notFoundResult.StatusCode);
      empService.Verify(x => x.RetrieveAllEmployeeMetrics(), Times.Once);
    }
  }
}