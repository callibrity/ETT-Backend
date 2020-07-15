using System;
using System.Collections.Generic;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Repository;

namespace ETT_Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeResponse RetrieveEmployeeInfo(string email)
        {
            try
            {
                Employee employee;
                using (DBConnection dbconnection = new DBConnection())
                {
                    dbconnection.Connect();
                    string query = QueryGenerator.GetEmployee(email);
                    employee = dbconnection.ExecuteQuery<Employee>(query)[0];
                }
                return new EmployeeResponse(employee);
            }
            catch (Exception e)
            {
                Console.Write("EmployeeService employee retrieval failed\n", e.Message);
                return null;
            }
        }
    }
}