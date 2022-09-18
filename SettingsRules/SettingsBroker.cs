using System.Collections.ObjectModel;
using CARDINAL.Persephone.Configs;
using CARDINAL.Persephone.Helpers;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SaveRules;
using Console = CARDINAL.Persephone.Helpers.Console;

namespace CARDINAL.Persephone.SettingsRules;

internal class SettingsBroker : ISettingsBroker
{
    private readonly SystemData _systemData = new SystemData();

    private IList<BranchSettingConfig> _settingsConfigs = new Collection<BranchSettingConfig>();
    private BranchSettingConfig _defaultBranchConfig = new();

    public BranchSettingConfig DefaultBranchSetting => _defaultBranchConfig;
    public SystemData SystemData => _systemData;

    public async Task Init()
    {
        SettingsBrokerConfig? saveLoad =
            SaveBroker.LoadSerializeData<SettingsBrokerConfig>(_systemData.PathToMainSaveFolder, _systemData.SettingsSaveFile);

        if (saveLoad is not null)
        {
            _settingsConfigs = saveLoad.SettingsConfigs;
            TrySetDefaultSettings(saveLoad.DefaultConfig);
        }
        else
        {
            _defaultBranchConfig = new BranchSettingConfig(
                "main",
                "Changelog Init",
                "Build:",
                @"D:\UnityProjects\RustAndUnityTest\Assets\Plugins\TestDLL",
                10
            );

            await TryAddNewSettings(_defaultBranchConfig);
        }
    }

    public async Task<bool> TryAddNewSettings(BranchSettingConfig branchSettingConfig)
    {
        if (_settingsConfigs.Any(config => config.BranchName == branchSettingConfig.BranchName))
        {
               Console.LogWarning(
                "Couldn't add it because it already BranchName exists. If you want change branch settings, please, use TryUpdateSettings");
            return false;
        }

        _settingsConfigs.Add(branchSettingConfig);

        await SaveData();

        return true;
    }

    public async Task<bool> TryUpdateSettings(BranchSettingConfig branchSettingConfig)
    {
        if (_settingsConfigs.All(config => config.BranchName != branchSettingConfig.BranchName))
        {
            Console.LogWarning(
                "Couldn't add it because there isn't one yet. If you want add branch settings, please, use TryAddNewSettings");

            return false;
        }

        var oldConfig = _settingsConfigs.First(config => config.BranchName == branchSettingConfig.BranchName);
        oldConfig.Join(branchSettingConfig);

        await SaveData();

        return true;
    }

    public bool TrySetDefaultSettings(string branchName)
    {
        var config = _settingsConfigs.FirstOrDefault(config => config.BranchName == branchName);
        
        if (config is not null)
        {
            _defaultBranchConfig = config;
            
            Console.Log("Successful add default settings config");
            
            return true;
        }
        
        Console.LogError("No such configuration found");
        return false;
    }

    public BranchSettingConfig GetSettings(string branchName)
    {
        var config = _settingsConfigs.FirstOrDefault(config => config.BranchName == branchName);

        if(config == null)
        {
            Console.LogError($"Not found SettingsConfig with name: {branchName}");
            return DefaultBranchSetting;
        }

        return config;
    }

    private async Task SaveData()
    {
        await SaveBroker.SaveSerializeData(_systemData.PathToMainSaveFolder, _systemData.SettingsSaveFile,
            new SettingsBrokerConfig(_defaultBranchConfig.BranchName, _settingsConfigs));
    }

}