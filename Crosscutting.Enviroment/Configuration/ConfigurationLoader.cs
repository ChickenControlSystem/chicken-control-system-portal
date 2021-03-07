using Microsoft.Extensions.Configuration;

namespace Crosscutting.Configuration
{
    public class ConfigurationLoader
    {
        private readonly string _file;

        public ConfigurationLoader(string file)
        {
            _file = file;
        }

        public static ConfigurationLoader GetTestConfigurationLoader()
        {
            return new ConfigurationLoader("appsettings.test.json");
        }

        public static ConfigurationLoader GetBuildConfigurationLoader()
        {
            return new ConfigurationLoader("appsettings.json");
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(_file)
                .Build();
            return config;
        }

        public ControlLineSettings GetControlLineSettings()
        {
            return InitConfiguration().GetSection("ControlLineSettings").Get<ControlLineSettings>();
        }
    }
}