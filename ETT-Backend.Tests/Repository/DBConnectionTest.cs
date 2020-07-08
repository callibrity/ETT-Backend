using Xunit;
using System.Collections.Generic;
using ETT_Backend.Repository;

namespace ETT_Backend.Repository.Test
{
  public class DBConnectionTest
  {
    private class TestClass
    {
      public string str;
      public int num;

      public TestClass(string str, int num)
      {
        this.str = str;
        this.num = num;
      }
    }

    [Fact]
    public void ShouldCreateDBConnectionObject()
    {
      DBConnection con = new DBConnection();
      Assert.NotNull(con);
    }

    [Fact]
    public void ConnectionShouldInitiallyBeClosed()
    {
      DBConnection con = new DBConnection();
      Assert.False(con.IsConnectionOpen);
    }

    [Fact]
    public void ShouldConnectToDatabase()
    {

      DBConnection con = new DBConnection();
      con.Connect();
      Assert.True(con.IsConnectionOpen);
    }

    [Fact]
    public void ShouldCLoseConnection()
    {
      DBConnection con = new DBConnection();
      con.Connect();
      con.Dispose();
      Assert.False(con.IsConnectionOpen);
    }

    [Fact]
    public void ShouldCloseConnectionAfterDisposal()
    {
      DBConnection con;
      using (con = new DBConnection())
      {
        con.Connect();
      }

      Assert.False(con.IsConnectionOpen);
    }

    [Fact]
    public void ShouldHaveOneRow()
    {
      List<TestClass> temp = null;
      DBConnection con;
      using (con = new DBConnection())
      {
        con.Connect();
        temp = con.ExecuteQuery<TestClass>(@"Select 'hi', 7");
      }
      Assert.Equal("hi", temp[0].str);
      Assert.Equal(7, temp[0].num);
    }

    [Fact]
    public void ShouldReturnNegativeOneOnQuery()
    {
      DBConnection con;
      using (con = new DBConnection())
      {
        con.Connect();
        Assert.Equal(-1, con.ExecuteNonQuery("Select NOW()"));
      }

    }

  }
}