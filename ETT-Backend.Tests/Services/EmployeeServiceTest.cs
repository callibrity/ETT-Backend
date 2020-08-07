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
      System.DateTime dt = new System.DateTime();
      List<EmployeeMetrics> dbRetVal = new List<EmployeeMetrics>()
      {
        new EmployeeMetrics("lol1", "lol2", "lol3", 1.0, 2.0, 3.0, 4.0, 5.0, dt),
        new EmployeeMetrics("lul", "lul", "lul", 2.0, 2.0, 2.0, 2.0, 2.0, dt)
      };

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetrics("cmason@callibrity.com")))
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
      List<EmployeeMetrics> dbRetVal = new List<EmployeeMetrics>();

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetrics("cmason@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EmployeeResponse response = employeeService.RetrieveEmployeeMetrics("cmason@callibrity.com");

      Assert.Null(response);
    }

    [Fact]
    public void RetrieveEmployeeRoleString()
    {
      var expectedQuery = "SELECT first_name, last_name, role FROM public.ett_employee WHERE employee_email = 'test@callibrity.com'";
      var query = QueryGenerator.GetEmployeeInfo("test@callibrity.com");
      Assert.Equal(expectedQuery, query);
    }

    [Fact]
    public void RetrieveEmployeeInfo()
    {
      List<EttEmployee> dbRetVal = new List<EttEmployee>();

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EttEmployee>(QueryGenerator.GetEmployeeInfo("test@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EttEmployee response = employeeService.RetrieveEmployeeInfo("test@callibrity.com");

      Assert.Null(response);
    }
  }
}