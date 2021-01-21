using Microsoft.Extensions.Configuration;

namespace ControlSystem.Tests.Enviroment
{
    public static class ConfLoader
    {
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public static ControlLineSettings GetControlLineSettings()
        {
            return InitConfiguration().GetSection("ControlLineSettings").Get<ControlLineSettings>();
        }
    }
}