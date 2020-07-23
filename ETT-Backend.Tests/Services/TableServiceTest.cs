using System.Collections.Generic;
using ETT_Backend.Models;
using ETT_Backend.Repository;
using Newtonsoft.Json.Linq;
using static ETT_Backend.Models.Constants.Constants;
using Xunit;

namespace ETT_Backend.Services.Test
{
  public class TableServiceTest
  {
    [Fact]
    public void CreateInsertEmployeesHeader()
    {
      var expectedInsertHeader = "INSERT INTO employees(name,office,role) VALUES ";
      var testObject = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"role", "Developer"}
      };
      var header = QueryGenerator.InsertObjectHeader("employees", testObject);

      Assert.Equal(expectedInsertHeader, header);
    }

    [Fact]
    public void CreateInsertEmployeesValue()
    {
      var expectedInsertValue = "('testName','Cincinnati','Developer')";
      var testObject = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"role", "Developer"}
      };
      var value = QueryGenerator.InsertObject(testObject);

      Assert.Equal(expectedInsertValue, value);
    }

    [Fact]
    public void ParseNumberValuesWithoutSingleQuotes()
    {
      var expectedInsertValue = "('testName','Cincinnati',12.5)";
      var testObject = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5}
      };
      var value = QueryGenerator.InsertObject(testObject);

      Assert.Equal(expectedInsertValue, value);
    }

    [Fact]
    public void OnConflictStatements()
    {
      var expectedEmployeesConflict = " ON CONFLICT (callibrity_email) DO NOTHING";
      var actualEmployeesConflict = QueryGenerator.Conflict(ConstraintKeys["employees"]);

      var expectedEttEmployeeConflict = " ON CONFLICT (employee_number) DO NOTHING";
      var actualEttEmployeeConflit = QueryGenerator.Conflict(ConstraintKeys["ett_employee"]);

      var expectedEttEmployeeMetricsConflict = " ON CONFLICT (employee_number_fk, the_year) DO NOTHING";
      var actualEttEmployeeMetricsConflict = QueryGenerator.Conflict(ConstraintKeys["ett_employee_metrics"]);

      Assert.Equal(expectedEmployeesConflict, actualEmployeesConflict);
      Assert.Equal(expectedEttEmployeeConflict, actualEttEmployeeConflit);
      Assert.Equal(expectedEttEmployeeMetricsConflict, actualEttEmployeeMetricsConflict);
    }

    [Fact]
    public void CreateFullInsertStatement()
    {
      var tableService = new TableService();
      var expectedInsertValue = "INSERT INTO employees(name,office,hours) VALUES ('testName','Cincinnati',12.5),('anotherTest','Cincinnati',30) ON CONFLICT (callibrity_email) DO NOTHING";
      var testObject1 = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5}
      };
      var testObject2 = new JObject()
      {
        {"name", "anotherTest"},
        {"office", "Cincinnati"},
        {"hours", 30}
      };
      var testList = new List<dynamic>() {
        testObject1.ToString(),
        testObject2.ToString()
      };
      var insertStatement = tableService.CreateInsertStatement("employees", testList);

      Assert.Equal(expectedInsertValue, insertStatement);
    }
  }
}