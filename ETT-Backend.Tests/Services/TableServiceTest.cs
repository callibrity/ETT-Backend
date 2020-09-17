using System.Collections.Generic;
using ETT_Backend.Repository;
using Newtonsoft.Json.Linq;
using static ETT_Backend.Models.Constants.Constants;
using Xunit;
using ETT_Backend.Tests.Utils;

namespace ETT_Backend.Services.Test
{
  public class TableServiceTest
  {
    [Fact]
    public void InsertRowsSuccess()
    {
      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      var expectedInsertValue = 
        "INSERT INTO employees(name,office,hours,callibrity_email) VALUES ('testName','Cincinnati',12.5,'test123@test.com'),('anotherTest','Cincinnati',30,'test456@test.com')"
        + " ON CONFLICT (callibrity_email) DO UPDATE SET name=EXCLUDED.name,office=EXCLUDED.office,hours=EXCLUDED.hours";
      
      var testObject1 = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5},
        {"callibrity_email", "test123@test.com"},
      };
      var testObject2 = new JObject()
      {
        {"name", "anotherTest"},
        {"office", "Cincinnati"},
        {"hours", 30},
        {"callibrity_email", "test456@test.com"},
      };
      var testList = new List<dynamic>() 
      {
        testObject1.ToString(),
        testObject2.ToString()
      };

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteNonQuery(expectedInsertValue))
         .Returns(1);

      TableService tableService = new TableService(serviceProvider.Build());

      var actualResponse = tableService.InsertRows("employees", testList);

      Assert.Equal(1, actualResponse);
    }

    [Fact]
    public void CreateFullInsertStatement()
    {
      var tableService = new TableService(new MockServiceProvider().Build());
      var expectedInsertValue = 
        "INSERT INTO employees(name,office,hours,callibrity_email) VALUES ('testName','Cincinnati',12.5,'test123@test.com'),('anotherTest','Cincinnati',30,'test456@test.com')"
        + " ON CONFLICT (callibrity_email) DO UPDATE SET name=EXCLUDED.name,office=EXCLUDED.office,hours=EXCLUDED.hours";
      var testObject1 = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5},
        {"callibrity_email", "test123@test.com"},
      };
      var testObject2 = new JObject()
      {
        {"name", "anotherTest"},
        {"office", "Cincinnati"},
        {"hours", 30},
        {"callibrity_email", "test456@test.com"},
      };
      var testList = new List<dynamic>() 
      {
        testObject1.ToString(),
        testObject2.ToString()
      };
      var insertStatement = tableService.CreateInsertStatement("employees", testList);

      Assert.Equal(expectedInsertValue, insertStatement);
    }
  }
}