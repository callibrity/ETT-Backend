using System.Collections.Generic;
using Xunit;
using ETT_Backend.Repository;
using ETT_Backend.Models;
using ETT_Backend.Tests.Utils;

namespace ETT_Backend.Services.Test
{
  public class EmployeeServiceTest
  {
    [Fact]
    public void RetrieveEmployeeHoursValidEmail()
    {
      List<Employee> dbRetVal = new List<Employee>()
      {
        new Employee("lol1", "lol2", "lol3", 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0),
        new Employee("lul", "lul", "lul", 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0)
      };

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<Employee>(QueryGenerator.GetEmployee("cmason@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EmployeeResponse response = employeeService.RetrieveEmployeeMetrics("cmason@callibrity.com");

      Assert.Equal("lol1", response.EmployeeId);
      Assert.Equal(3.0, response.Billable.CurrentHours);
      Assert.Equal(2.0, response.Billable.CurrentTarget);
      Assert.Equal(1.0, response.Billable.TotalTarget);
      Assert.Equal(-1.0, response.Growth.HoursRemaining);
      Assert.Equal(5.0, response.Growth.HoursUsed);
      Assert.Equal(4.0, response.Growth.TotalGrowth);
    }

    [Fact]
    public void RetrieveEmployeeHoursInvalidEmail()
    {
      List<Employee> dbRetVal = new List<Employee>();

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<Employee>(QueryGenerator.GetEmployee("cmason@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EmployeeResponse response = employeeService.RetrieveEmployeeMetrics("cmason@callibrity.com");

      Assert.Null(response);
    }
  }
}