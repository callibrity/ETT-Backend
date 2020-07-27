using System.Collections.Generic;

namespace ETT_Backend.Repository
{
  public interface IDBConnection
  {
    bool IsConnectionOpen { get; }
    void Connect();
    void Dispose();
    int ExecuteNonQuery(string query);
    List<T> ExecuteQuery<T>(string query);
  }
}