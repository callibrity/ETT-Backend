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
      ConfigureServices();
    }

    private static void ConfigureServices()
    {
      string dir = System.AppDomain.CurrentDomain.BaseDirectory;
      string github = Environment.GetEnvironmentVariable("GITHUB");
      string appSettingsFile = String.IsNullOrEmpty(github) ? "appsettings.json" : "appsettings.Development.json";
      configuration = new ConfigurationBuilder()
          .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile(appSettingsFile, false)
          .Build();
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
