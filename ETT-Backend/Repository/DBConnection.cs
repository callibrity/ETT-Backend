using Npgsql;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ETT_Backend.Configuration;

namespace ETT_Backend.Repository
{
  public class DBConnection : IDisposable, IDBConnection
  {
    public bool IsConnectionOpen
    {
      get
      {
        return connection.State == System.Data.ConnectionState.Open;
      }
      private set { }
    }
    private NpgsqlConnection connection;

    public DBConnection(IConfiguration configuration)
    {
      switch(configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT")) {
        case "PRODUCTION":
          connection = new NpgsqlConnection(configuration.GetValue<string>("CONNECTION_PRODUCTION"));
          break;
        case "STAGING":
          connection = new NpgsqlConnection(configuration.GetValue<string>("CONNECTION_STAGING"));
          break;
        default:
          connection = new NpgsqlConnection(configuration.GetValue<string>("CONNECTION"));
          break;
      }
    }

    public void Connect()
    {
      connection.Open();
    }

    public void Dispose()
    {
      connection.Dispose();
    }

    public List<T> ExecuteQuery<T>(string query)
    {
      var cmd = new NpgsqlCommand(query, connection);
      List<T> result = new List<T>();
      var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        object[] arr = new object[reader.VisibleFieldCount];
        for (int i = 0; i < reader.VisibleFieldCount; i++)
        {
          arr[i] = reader.GetValue(i);
        }
        T obj = (T)Activator.CreateInstance(typeof(T), arr);
        result.Add(obj);
      }

      return result;
    }

    public int ExecuteNonQuery(string query)
    {
      var cmd = new NpgsqlCommand(query, connection);
      return cmd.ExecuteNonQuery();
    }
  }
}