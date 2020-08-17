using System;
using System.Collections.Generic;
using ETT_Backend.Models;
using Newtonsoft.Json.Linq;

namespace ETT_Backend.Repository
{
  public static class QueryGenerator
  {
    public static string GetEmployeeInfo(string employeeEmail)
    {
      return "SELECT first_name, last_name, role "
           + "FROM public.ett_employee "
           + $"WHERE employee_email = '{employeeEmail}'";
    }

    public static string GetEmployeeMetricsHeader()
    {
      return "SELECT e.employee_number, e.first_name, e.last_name, "
           + "em.yearly_billable_target_hours, em.billable_target_to_date, em.current_billable_hours, em.target_training_hours, em.current_training_hours, em.updated_at "
           + "FROM public.ett_employee e "
           + "JOIN ett_employee_metrics em on em.employee_number_fk = e.employee_number ";
    }

    public static string WhereEmail(string email) 
    {
      return $"WHERE e.employee_email = '{email}'";
    }

    public static string WhereRole(string role)
    {
      return $"WHERE e.role = '{role}'";
    }

    public static string InsertObjectHeader(string tableName, JObject obj)
    {
      string insertStatement = $"INSERT INTO {tableName.ToLower()}(";
      foreach (var prop in obj)
      {
        insertStatement += $"{prop.Key.ToLower()},";
      }
      insertStatement = insertStatement.TrimEnd(',');
      insertStatement += ")";
      return insertStatement + " VALUES ";
    }

    /*
    * Iterates through the values in a JObject and puts them into 
    * insert statement form
    */
    public static string InsertObject(JObject obj)
    {
      string values = "(";
      foreach (var prop in obj)
      {
        double num;
        if (!double.TryParse(prop.Value.ToString(), out num))
          values += $"'{prop.Value}',";
        else
          values += $"{prop.Value},";
      }
      values = values.TrimEnd(',');
      return values += ")";
    }

    public static string OnConflict(List<string> keys, JObject row)
    {
      var conflict = " ON CONFLICT (";
      foreach (var key in keys)
      {
        conflict += $"{key},";
      }
      conflict = conflict.TrimEnd(',');
      conflict += ") DO UPDATE SET ";
      foreach (var property in row)
      {
        if (!keys.Contains(property.Key.ToLower()))
          conflict += $"{property.Key.ToLower()}=EXCLUDED.{property.Key.ToLower()},";
      }
      conflict = conflict.TrimEnd(',');
      return conflict;
    }
  }
}
