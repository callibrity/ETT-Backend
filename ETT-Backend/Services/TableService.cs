using System;
using System.Collections.Generic;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Models.Constants;
using ETT_Backend.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static ETT_Backend.Models.Constants.Constants;

namespace ETT_Backend.Services
{
  public class TableService : ITableService
  {
    public int InsertRows(string table, List<dynamic> rows)
    {
      try
      {
        int dbResponse = 0;
        using (DBConnection dbconnection = new DBConnection())
        {
          dbconnection.Connect();
          string insertStatement = CreateInsertStatement(table, rows);
          dbResponse = dbconnection.ExecuteNonQuery(insertStatement);
        }
        return dbResponse;
      }
      catch (Exception e)
      {
        Console.Write("TableService insert failed\n", e.Message);
        return -1;
      }
    }

    public string CreateInsertStatement(string table, List<dynamic> rows)
    {
      // Parse the first object sent. Use this to reference the column names sent over
      var firstRowObject = JObject.Parse(rows[0].ToString());
      // INSERT INTO table_name(columnns...) VALUES
      var insertStatement = QueryGenerator.InsertObjectHeader(table.ToLower(), firstRowObject);
      foreach (var row in rows)
      {
        var parsedRow = JObject.Parse(row.ToString());
        // (values...),(values...)
        insertStatement += $"{QueryGenerator.InsertObject(parsedRow)},";
      }
      insertStatement = insertStatement.TrimEnd(',');
      // ON CONFLICT DO UPDATE SET columns=EXCLUDED.columns
      insertStatement += QueryGenerator.OnConflict(ConstraintKeys[table.ToLower()], firstRowObject);
      return insertStatement;
    }
  }
}