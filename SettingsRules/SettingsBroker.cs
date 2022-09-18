using System.Collections.ObjectModel;
using CARDINAL.Persephone.Configs;
using CARDINAL.Persephone.Helpers;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SaveRules;
using Console = CARDINAL.Persephone.Helpers.Console;

namespace CARDINAL.Persephone.SettingsRules;

internal class SettingsBroker : ISettingsBroker
{
    private readonly string _fileName = @"\settings.hehe";

    private IList<SettingsConfig> _settingsConfigs = new Collection<SettingsConfig>();
    private SettingsConfig _defaultConfig = new();

    public SettingsConfig DefaultSetting => _defaultConfig;

    public SettingsBroker(IContext context)
    {
    }

    public async Task Init()
    {
        SettingsBrokerConfig? saveLoad =
            SaveBroker.LoadSerializeData<SettingsBrokerConfig>(Paths.PathToMainSaveFolder(), _fileName);

        if (saveLoad is not null)
        {
            _settingsConfigs = saveLoad.SettingsConfigs;
            TrySetDefaultSettings(saveLoad.DefaultConfig);
        }
        else
        {
            _defaultConfig = new SettingsConfig(
                "main",
                "Changelog Init",
                "Build:",
                @"D:\UnityProjects\RustAndUnityTest\Assets\Plugins\TestDLL",
                10
            );

            await TryAddNewSettings(_defaultConfig);
        }
    }

    public async Task<bool> TryAddNewSettings(SettingsConfig settingsConfig)
    {
        if (_settingsConfigs.Any(config => config.BranchName == settingsConfig.BranchName))
        {
               Console.LogWarning(
                "Couldn't add it because it already BranchName exists. If you want change branch settings, please, use TryUpdateSettings");
            return false;
        }

        _settingsConfigs.Add(settingsConfig);

        await SaveData();

        return true;
    }

    public async Task<bool> TryUpdateSettings(SettingsConfig settingsConfig)
    {
        if (_settingsConfigs.All(config => config.BranchName != settingsConfig.BranchName))
        {
            Console.LogWarning(
                "Couldn't add it because there isn't one yet. If you want add branch settings, please, use TryAddNewSettings");
            _settingsConfigs.Add(settingsConfig);
        }

        var oldConfig = _settingsConfigs.First(config => config.BranchName == settingsConfig.BranchName);
        oldConfig.Join(settingsConfig);

        await SaveData();

        return true;
    }

    public bool TrySetDefaultSettings(string branchName)
    {
        var config = _settingsConfigs.FirstOrDefault(config => config.BranchName == branchName);
        
        if (config is not null)
        {
            _defaultConfig = config;
            
            Console.Log("Successful add default settings config");
            
            return true;
        }
        
        Console.LogError("No such configuration found");
        return false;
    }

    public SettingsConfig GetSettings(string branchName)
    {
        var config = _settingsConfigs.FirstOrDefault(config => config.BranchName == branchName);

        if(config == null)
        {
            Console.LogError($"Not found SettingsConfig with name: {branchName}");
            return DefaultSetting;
        }

        return config;
    }

    private async Task SaveData()
    {
        await SaveBroker.SaveSerializeData(Paths.PathToMainSaveFolder(), _fileName,
            new SettingsBrokerConfig(_defaultConfig.BranchName, _settingsConfigs));
    }

}