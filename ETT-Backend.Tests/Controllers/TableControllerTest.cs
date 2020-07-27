using Xunit;
using Moq;
using ETT_Backend.Services;
using ETT_Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ETT_Backend.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ETT_Backend.Controllers.Test
{
  public class TableControllerTest
  {
    [Fact]
    public void InsertDataSuccess()
    {
      var testEmail = "test@callibrity.com";
      var testObject1 = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5}
      };
      var testObject2 = new JObject()
      {
        {"name", "anotherTest"},
        {"office", "Cincinnati"},
        {"hours", 30}
      };
      var testRows = new List<dynamic>() {
        testObject1.ToString(),
        testObject2.ToString()
      };
      var tableService = new Mock<ITableService>();
      tableService.Setup(serv => serv.InsertRows(It.IsAny<string>(), testRows)).Returns(2);
      var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
      {
        new Claim(ClaimTypes.Name, "test"),
        new Claim(ClaimTypes.NameIdentifier, "1"),
        new Claim("email", testEmail),
      }, "mock"));

      var controller = new TableController(tableService.Object);
      controller.ControllerContext = new ControllerContext()
      {
        HttpContext = new DefaultHttpContext() { User = user }
      };
      
      var result = controller.InsertData("employees", testRows);
      var okResult = result as OkResult;

      Assert.NotNull(okResult);
      Assert.Equal(200, okResult.StatusCode);
      tableService.Verify(x => x.InsertRows(It.IsAny<string>(), It.IsAny<List<dynamic>>()), Times.Once);
    }
  }
}