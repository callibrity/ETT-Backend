using ETT_Backend.Models;
using System.Collections.Generic;

namespace ETT_Backend.Interfaces
{
  public interface IEmployeeService
  {
    EttEmployee RetrieveEmployeeInfo(string email);
    EmployeeResponse RetrieveEmployeeMetrics(string email);
    List<EmployeeResponse> RetrieveAllEmployeeMetrics();
  }
}