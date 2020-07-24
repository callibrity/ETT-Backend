using System;
using System.Collections.Generic;

namespace ETT_Backend.Models.Constants
{
  public class Constants
  {
    public static Dictionary<string, List<string>> ConstraintKeys = 
      new Dictionary<string, List<string>>()
      {
        {"employees", new List<string>() {"callibrity_email"}},
        {"ett_employee", new List<string>() {"employee_number"}},
        {"ett_employee_metrics", new List<string>() {"employee_number_fk", "the_year"}}
      };
  }

}