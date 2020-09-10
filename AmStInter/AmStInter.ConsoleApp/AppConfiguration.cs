using Microsoft.Extensions.Configuration;

namespace AmStInter.ConsoleApp
{
    public static class AppConfiguration
    {
        public static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return configuration;
        }
    }
}
