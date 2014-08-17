using AnimalStore.Common.Configuration;

namespace AnimalStore.Common.Helpers
{
  public class Environment
  {
    public static bool IsNotDevelopment()
    {
      IConfiguration configuration = new Configuration.Configuration();
      return configuration.GetEnvironment() != "Development";
    }
  }
}