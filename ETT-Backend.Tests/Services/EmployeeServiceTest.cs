using Xunit;

namespace ETT_Backend.Services.Test
{
  public class EmployeeServiceTest
  {
    [Fact]
    public void RetrieveEmployeeHoursvalidEmail() 
    {
      var empService = new EmployeeService();
      var hoursResponse = empService.RetrieveEmployeeMetrics("cmason@callibrity.com");
      Assert.Equal("0919", hoursResponse.EmployeeId);
      Assert.Equal(20, hoursResponse.Billable.CurrentHours);
    }

    [Fact]
    public void RetrieveEmployeeHoursInvalidEmail() 
    {
      var empService = new EmployeeService();
      var hoursResponse = empService.RetrieveEmployeeMetrics("test@doesntexist.com");
      Assert.Equal(null, hoursResponse);
    }
  }
}