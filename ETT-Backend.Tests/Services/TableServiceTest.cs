using System.Collections.Generic;
using ETT_Backend.Repository;
using Newtonsoft.Json.Linq;
using static ETT_Backend.Models.Constants.Constants;
using Xunit;
using ETT_Backend.Tests.Utils;

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
    public void OnConflictStatementEmployees()
    {
      var employeesObject = new JObject()
      {
        {"name", "anotherTest"},
        {"role", "developer"},
        {"office", "Cincinnati"},
        {"email", "test@callibrity.com"},
        {"skills", "testing"},
        {"interests", "work"},
        {"bio", "grew up testing"},
        {"photo", "imgur.com"},
        {"callibrity_email", "test@callibrity.com"}
      };
      var expectedEmployeesConflict = 
        " ON CONFLICT (callibrity_email) DO UPDATE SET name=EXCLUDED.name,role=EXCLUDED.role,office=EXCLUDED.office,email=EXCLUDED.email,"
        + "skills=EXCLUDED.skills,interests=EXCLUDED.interests,bio=EXCLUDED.bio,photo=EXCLUDED.photo";
      var actualEmployeesConflict = QueryGenerator.OnConflict(ConstraintKeys["employees"], employeesObject);

      Assert.Equal(expectedEmployeesConflict, actualEmployeesConflict);
    }

    [Fact]
    public void OnConflictStatementEttEmployee()
    {
      var ettEmployeeObject = new JObject()
      {
        {"first_name", "anotherTest"},
        {"last_name", "developer"},
        {"employee_number", "Cincinnati"},
        {"employee_email", "test@callibrity.com"}
      };
      var expectedEttEmployeeConflict = " ON CONFLICT (employee_number) DO UPDATE SET first_name=EXCLUDED.first_name,last_name=EXCLUDED.last_name,employee_email=EXCLUDED.employee_email";
      var actualEttEmployeeConflit = QueryGenerator.OnConflict(ConstraintKeys["ett_employee"], ettEmployeeObject);

      Assert.Equal(expectedEttEmployeeConflict, actualEttEmployeeConflit);
    }

    [Fact]
    public void OnConflictStatementEttEmployeeMetrics()
    {
      System.DateTime dt = new System.DateTime();
      var ettEmployeeMetrics = new JObject()
      {
        {"employee_number_fk", "101"},
        {"yearly_billable_target_hours", 100},
        {"current_billable_hours", 100},
        {"target_training_hours", 100},
        {"current_training_hours", 100},
        {"total_yearly_pto", 100},
        {"overflow_pto", 100},
        {"used_pto", 100},
        {"the_year", 2020},
        {"billable_target_to_date", 100},
        {"updated_at", dt}
      };
      var expectedEmployeesConflict = 
        " ON CONFLICT (employee_number_fk,the_year) DO UPDATE SET yearly_billable_target_hours=EXCLUDED.yearly_billable_target_hours,"
        + "current_billable_hours=EXCLUDED.current_billable_hours,target_training_hours=EXCLUDED.target_training_hours,current_training_hours=EXCLUDED.current_training_hours,"
        + "total_yearly_pto=EXCLUDED.total_yearly_pto,overflow_pto=EXCLUDED.overflow_pto,used_pto=EXCLUDED.used_pto,billable_target_to_date=EXCLUDED.billable_target_to_date,updated_at=EXCLUDED.updated_at";
      var actualEmployeesConflict = QueryGenerator.OnConflict(ConstraintKeys["ett_employee_metrics"], ettEmployeeMetrics);

      Assert.Equal(expectedEmployeesConflict, actualEmployeesConflict);
    }

    [Fact]
    public void CreateFullInsertStatement()
    {
      var tableService = new TableService(new MockServiceProvider().Build());
      var expectedInsertValue = 
        "INSERT INTO employees(name,office,hours,callibrity_email) VALUES ('testName','Cincinnati',12.5,'test123@test.com'),('anotherTest','Cincinnati',30,'test456@test.com')"
        + " ON CONFLICT (callibrity_email) DO UPDATE SET name=EXCLUDED.name,office=EXCLUDED.office,hours=EXCLUDED.hours";
      var testObject1 = new JObject()
      {
        {"name", "testName"},
        {"office", "Cincinnati"},
        {"hours", 12.5},
        {"callibrity_email", "test123@test.com"},
      };
      var testObject2 = new JObject()
      {
        {"name", "anotherTest"},
        {"office", "Cincinnati"},
        {"hours", 30},
        {"callibrity_email", "test456@test.com"},
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