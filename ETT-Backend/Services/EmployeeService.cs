using System;
using System.Linq;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETT_Backend.Services
{
  public class EmployeeService : IEmployeeService
  {
    IServiceProvider ServiceProvider;

    public EmployeeService(IServiceProvider serviceProvider)
    {
      ServiceProvider = serviceProvider;
    }

    public EmployeeResponse RetrieveEmployeeMetrics(string email)
    {
      using (IServiceScope scope = ServiceProvider.CreateScope())
      {
        IDBConnection dbConnection = scope.ServiceProvider.GetRequiredService<IDBConnection>();
        dbConnection.Connect();

        var result = dbConnection.ExecuteQuery<Employee>(QueryGenerator.GetEmployee(email));

        return result.Any() ? new EmployeeResponse(result.First()) : null;
      }
    }
  }
}