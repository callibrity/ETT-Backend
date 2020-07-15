using System;

namespace ETT_Backend.Repository
{
    public static class QueryGenerator
    {
        public static string GetEmployee(string employeeEmail)
        {
            return "SELECT e.employee_number, e.first_name, e.last_name, "
                 + "em.yearly_billable_target_hours, em.billable_target_to_date, em.current_billable_hours, em.target_training_hours, em.current_training_hours, em.total_yearly_pto, em.overflow_pto, em.used_pto "
                 + "FROM public.ett_employee e "
                 + "JOIN ett_employee_metrics em on em.employee_number_fk = e.employee_number "
                 + $"WHERE e.employee_email = '{employeeEmail}'";
        }
    }
}
