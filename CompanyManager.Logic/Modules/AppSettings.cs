using Microsoft.Extensions.Configuration;

namespace CompanyManager.Logic.Modules
{
    public sealed class AppSettings
    {
        #region fields
        private static AppSettings _instance = new AppSettings();
        private static IConfigurationRoot _configurationRoot;
        #endregion fields
        static AppSettings()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{environmentName ?? "Development"}.json", optional: true)
                    .AddEnvironmentVariables();

            _configurationRoot = builder.Build();
        }

        #region properties
        public static AppSettings Instance => _instance;
        #endregion properties
        private AppSettings()
        {
        }

        public string? this[string key] => _configurationRoot[key];
    }
}
