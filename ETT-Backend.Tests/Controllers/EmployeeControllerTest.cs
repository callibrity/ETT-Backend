using Xunit;
using Moq;
using ETT_Backend.Services;
using ETT_Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ETT_Backend.Interfaces;
using Newtonsoft.Json;

namespace ETT_Backend.Controllers.Test
{
  public class EmployeeControllerTest
  {
    [Fact]
    public void RetrieveEmployeeMetrics()
    {
      System.DateTime dt = new System.DateTime();
      var testEmail = "test@callibrity.com";
      var employee = new Employee("1111", "test", "name", 100, 100, 100, 100, 100, dt);
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
    public void FailToRetrieveEmployeeMetrics()
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
  }
}