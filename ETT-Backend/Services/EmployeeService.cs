using System;
using System.Linq;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Repository;
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

    public EttEmployee RetrieveEmployeeInfo(string email)
    {
      using (IServiceScope scope = ServiceProvider.CreateScope())
      {
        IDBConnection dbConnection = scope.ServiceProvider.GetRequiredService<IDBConnection>();
        dbConnection.Connect();
        var query = QueryGenerator.GetEmployeeInfo(email);
        var result = dbConnection.ExecuteQuery<EttEmployee>(query);

        return result.Any() ? result.First() : null;
      }
    }

    public EmployeeResponse RetrieveEmployeeMetrics(string email)
    {
      using (IServiceScope scope = ServiceProvider.CreateScope())
      {
        IDBConnection dbConnection = scope.ServiceProvider.GetRequiredService<IDBConnection>();
        dbConnection.Connect();

        var result = dbConnection.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetrics(email));

        return result.Any() ? new EmployeeResponse(result.First()) : null;
      }
    }
  }
}