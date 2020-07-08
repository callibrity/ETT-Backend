using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETT_Backend.Configuration
{
  public static class AppConfiguration
  {
    private static IConfigurationRoot configuration;
    static AppConfiguration()
    {
      ServiceCollection serviceCollection = new ServiceCollection();
      ConfigureServices(serviceCollection);
    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {
      string dir = System.AppDomain.CurrentDomain.BaseDirectory;
      string github = Environment.GetEnvironmentVariable("GITHUB");
      string appSettingsFile = String.IsNullOrEmpty(github) ? "appsettings.json" : "sample.appsettings.json";
      configuration = new ConfigurationBuilder()
          .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile(appSettingsFile, false)
          .Build();

      serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
    }

    public static string GetValue(string configurationKey)
    {
      string result = Environment.GetEnvironmentVariable(configurationKey.ToUpper());
      if (String.IsNullOrEmpty(result))
        result = configuration.GetSection(configurationKey).Value;
      return result;
    }
  }
}
