using System.Text.Json.Serialization;

namespace CARDINAL.Persephone.Configs;

public class SettingsBrokerConfig
{
    public string DefaultConfig { get; }
    public IList<BranchSettingConfig> SettingsConfigs { get; }
 
    [JsonConstructor]
    public SettingsBrokerConfig(string defaultConfig, IList<BranchSettingConfig> settingsConfigs)
    {
        DefaultConfig = defaultConfig;
        SettingsConfigs = settingsConfigs;
    }
}