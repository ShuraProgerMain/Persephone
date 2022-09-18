using System.Text.Json.Serialization;

namespace CARDINAL.Persephone.Configs;

public class SettingsBrokerConfig
{
    public string DefaultConfig { get; }
    public IList<SettingsConfig> SettingsConfigs { get; }
 
    [JsonConstructor]
    public SettingsBrokerConfig(string defaultConfig, IList<SettingsConfig> settingsConfigs)
    {
        DefaultConfig = defaultConfig;
        SettingsConfigs = settingsConfigs;
    }
}