using ETT_Backend.Models;

namespace ETT_Backend.Interfaces
{
  public interface IEmployeeService
  {
    EttEmployee RetrieveEmployeeInfo(string email);
    EmployeeResponse RetrieveEmployeeMetrics(string email);
  }
}