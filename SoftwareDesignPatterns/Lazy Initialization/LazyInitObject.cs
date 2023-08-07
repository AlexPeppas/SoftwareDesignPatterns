using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns.Lazy_Initialization
{
    public sealed class LazyInitObject
    {
        private Lazy<ConfigurationSettings> lazySettings;

        public LazyInitObject(Guid configId)
        {
            ConfigId = configId;

            this.lazySettings = new Lazy<ConfigurationSettings>(() =>
            {
                Console.WriteLine("Loading configuration settings ...");
                return LoadSomeSettingsFromAFile();
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public ConfigurationSettings ConfigSettings { get => lazySettings.Value; }

        public Guid ConfigId { get; set; }

        private ConfigurationSettings LoadSomeSettingsFromAFile()
        {
            // Simulate loading configuration from a file
            return new ConfigurationSettings
            {
                SomeSetting = "Value from config",
                AnotherSetting = 42
            };
        }
    }

    public sealed class ConfigurationSettings
    {
        public string SomeSetting { get; set; }

        public int AnotherSetting { get; set; }
    }
}
