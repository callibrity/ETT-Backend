using System;
using System.Linq;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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

        var result = dbConnection.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereEmail(email));

        return result.Any() ? new EmployeeResponse(result.First()) : null;
      }
    }

    public List<EmployeeResponse> RetrieveAllEmployeeMetrics()
    {
      using (IServiceScope scope = ServiceProvider.CreateScope())
      {
        IDBConnection dbConnection = scope.ServiceProvider.GetRequiredService<IDBConnection>();
        dbConnection.Connect();

        var results = dbConnection.ExecuteQuery<EmployeeMetrics>(QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereRole("Developer"));
        var castedResults = new List<EmployeeResponse>();
        foreach (var res in results) 
        {
          castedResults.Add(new EmployeeResponse(res));
        }
        return castedResults.Any() ? castedResults : null;
      }
    }
  }
}