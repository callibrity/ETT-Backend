using Xunit;
using Moq;
using ETT_Backend.Services;
using ETT_Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ETT_Backend.Controllers.Test
{
  public class EmployeeControllerTest
  {
    [Fact]
    public void RetrieveEmployeeHoursvalidEmail()
    {
      var testEmail = "test@callibrity.com";
      var employee = new Employee("1111", "test", "name", 100, 100, 100, 100, 100, 100, 100, 100);
      var mockEmployeeRes = new EmployeeResponse(employee);
      var empService = new Mock<EmployeeService>();
      empService.Setup(serv => serv.RetrieveEmployeeMetrics(testEmail)).Returns(mockEmployeeRes);
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

      Assert.NotNull(result);
    }
  }
}