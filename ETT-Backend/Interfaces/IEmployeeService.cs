using ETT_Backend.Models;

namespace ETT_Backend.Interfaces 
{
    public interface IEmployeeService
    {
        EmployeeResponse RetrieveEmployeeInfo(string email);
    }
}