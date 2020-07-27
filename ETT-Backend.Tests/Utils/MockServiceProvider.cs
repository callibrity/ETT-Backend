using ETT_Backend.Repository;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT_Backend.Tests.Utils
{
  public class MockServiceProvider
  {
    public ServiceCollection ServiceCollection;

    public Mock<IDBConnection> MockDBConnection;

    public MockServiceProvider()
    {
      ServiceCollection = new ServiceCollection();
    }

    public ServiceProvider Build() => ServiceCollection.BuildServiceProvider();
  }

  public static class MockServiceCollectionExtensions
  {
    public static MockServiceProvider AddMockDBConnection(this MockServiceProvider mockServiceCollection)
    {
      mockServiceCollection.MockDBConnection = new Mock<IDBConnection>();
      mockServiceCollection.ServiceCollection.AddScoped(provider => mockServiceCollection.MockDBConnection.Object);

      return mockServiceCollection;
    }

    //Define more downstream services here
  }
}
