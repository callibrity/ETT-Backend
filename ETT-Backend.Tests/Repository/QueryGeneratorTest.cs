using Xunit;
using System;
using ETT_Backend.Repository;

namespace ETT_Backend.Repository.Test
{
  public class QueryGeneratorTest
  {

    [Fact]
    public void ShouldReturnQueryForGettingEmployee()
    {
      string expected = "SELECT e.employee_number, e.first_name, e.last_name, "
      + "em.yearly_billable_target_hours, em.billable_target_to_date, em.current_billable_hours, em.target_training_hours, em.current_training_hours, em.total_yearly_pto, em.overflow_pto, em.used_pto, em.updated_at "
      + "FROM public.ett_employee e "
      + "JOIN ett_employee_metrics em on em.employee_number_fk = e.employee_number "
      + $"WHERE e.employee_email = 'first.last@email.com'";
      string email = "first.last@email.com";
      string actual = QueryGenerator.GetEmployee(email);
      Assert.Equal(expected, actual);
    }

  }
}