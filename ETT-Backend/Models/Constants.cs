using System;
using System.Collections.Generic;

namespace ETT_Backend.Models.Constants
{
  public class Constants
  {
    public static Dictionary<string, string> ConstraintKeys = 
      new Dictionary<string, string>()
      {
        {"employees", "callibrity_email"},
        {"ett_employee", "employee_number"},
        {"ett_employee_metrics", "employee_number_fk, the_year"}
      };
  }

}