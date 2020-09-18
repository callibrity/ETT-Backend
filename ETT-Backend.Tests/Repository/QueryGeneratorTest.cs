using Xunit;
using System;
using ETT_Backend.Repository;
using Newtonsoft.Json.Linq;
using static ETT_Backend.Models.Constants.Constants;

namespace ETT_Backend.Repository.Test
{
  public class QueryGeneratorTest
  {

    [Fact]
    public void ShouldReturnQueryForGettingEmployee()
    {
      string expected = "SELECT e.employee_number, e.first_name, e.last_name, "
      + "em.yearly_billable_target_hours, em.billable_target_to_date, em.current_billable_hours, em.target_training_hours, em.current_training_hours, em.updated_at "
      + "FROM public.ett_employee e "
      + "JOIN ett_employee_metrics em on em.employee_number_fk = e.employee_number "
      + $"WHERE e.employee_email = 'first.last@email.com'";
      string email = "first.last@email.com";
      string actual = QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereEmail(email);
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldReturnQueryForGettingAllEmployees()
    {
      string expected = "SELECT e.employee_number, e.first_name, e.last_name, "
      + "em.yearly_billable_target_hours, em.billable_target_to_date, em.current_billable_hours, em.target_training_hours, em.current_training_hours, em.updated_at "
      + "FROM public.ett_employee e "
      + "JOIN ett_employee_metrics em on em.employee_number_fk = e.employee_number "
      + $"WHERE e.role = 'Developer'";
      string actual = QueryGenerator.GetEmployeeMetricsHeader() + QueryGenerator.WhereRole("Developer");
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void RetrieveEmployeeRoleString()
    {
      var expectedQuery = "SELECT first_name, last_name, role FROM public.ett_employee WHERE employee_email = 'test@callibrity.com'";
      var query = QueryGenerator.GetEmployeeInfo("test@callibrity.com");
      Assert.Equal(expectedQuery, query);
    }

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
      var actualEttEmployeeConflict = QueryGenerator.OnConflict(ConstraintKeys["ett_employee"], ettEmployeeObject);

      Assert.Equal(expectedEttEmployeeConflict, actualEttEmployeeConflict);
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
        {"the_year", 2020},
        {"billable_target_to_date", 100},
        {"updated_at", dt}
      };
      var expectedEmployeesConflict = 
        " ON CONFLICT (employee_number_fk,the_year) DO UPDATE SET yearly_billable_target_hours=EXCLUDED.yearly_billable_target_hours,"
        + "current_billable_hours=EXCLUDED.current_billable_hours,target_training_hours=EXCLUDED.target_training_hours,current_training_hours=EXCLUDED.current_training_hours,"
        + "billable_target_to_date=EXCLUDED.billable_target_to_date,updated_at=EXCLUDED.updated_at";
      var actualEmployeesConflict = QueryGenerator.OnConflict(ConstraintKeys["ett_employee_metrics"], ettEmployeeMetrics);

      Assert.Equal(expectedEmployeesConflict, actualEmployeesConflict);
    }
  }
}