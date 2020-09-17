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
    public void RetrieveEmployeeInfoSuccess()
    {
      List<EttEmployee> dbRetVal = new List<EttEmployee>() 
      {
        new EttEmployee("test", "name", "Developer")
      };

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EttEmployee>(QueryGenerator.GetEmployeeInfo("test@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EttEmployee response = employeeService.RetrieveEmployeeInfo("test@callibrity.com");

      Assert.Equal("test", response.FirstName);
      Assert.Equal("name", response.LastName);
      Assert.Equal("Developer", response.Role);
    }

    [Fact]
    public void RetrieveEmployeeInfoNoResults()
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
    
    [Fact]
    public void RetrieveEmployeeMetricsSuccess()
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
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereEmail("cmason@callibrity.com")))
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
    public void RetrieveEmployeeMetricsNoResults()
    {
      List<EmployeeMetrics> dbRetVal = new List<EmployeeMetrics>();

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereEmail("cmason@callibrity.com")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      EmployeeResponse response = employeeService.RetrieveEmployeeMetrics("cmason@callibrity.com");

      Assert.Null(response);
    }

    [Fact]
    public void RetrieveAllEmployeeMetricsSuccess()
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
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereRole("Developer")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      List<EmployeeResponse> response = employeeService.RetrieveAllEmployeeMetrics();

      Assert.Equal("lol1", response[0].EmployeeId);
      Assert.Equal(3.0, response[0].Billable.CurrentHours);
      Assert.Equal(2.0, response[0].Billable.CurrentTarget);
      Assert.Equal(1.0, response[0].Billable.TotalTarget);
      Assert.Equal(-1.0, response[0].Growth.HoursRemaining);
      Assert.Equal(5.0, response[0].Growth.HoursUsed);
      Assert.Equal(4.0, response[0].Growth.TotalGrowth);

      Assert.Equal("lul", response[1].EmployeeId);
      Assert.Equal(2.0, response[1].Billable.CurrentHours);
      Assert.Equal(2.0, response[1].Billable.CurrentTarget);
      Assert.Equal(2.0, response[1].Billable.TotalTarget);
      Assert.Equal(0, response[1].Growth.HoursRemaining);
      Assert.Equal(2.0, response[1].Growth.HoursUsed);
      Assert.Equal(2.0, response[1].Growth.TotalGrowth);
    }

    [Fact]
    public void RetrieveAllEmployeeMetricsFail()
    {
      List<EmployeeMetrics> dbRetVal = new List<EmployeeMetrics>();

      MockServiceProvider serviceProvider = new MockServiceProvider()
        .AddMockDBConnection();

      serviceProvider.MockDBConnection
         .Setup(_ => _.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereRole("Developer")))
         .Returns(dbRetVal);

      EmployeeService employeeService = new EmployeeService(serviceProvider.Build());
      var response = employeeService.RetrieveAllEmployeeMetrics();

      Assert.Null(response);
    }

  }
}