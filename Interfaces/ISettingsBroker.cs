using CARDINAL.Persephone.Configs;

namespace CARDINAL.Persephone.Interfaces;

internal interface ISettingsBroker
{
    public SettingsConfig DefaultSetting { get; }
    public Task Init();
    public Task<bool> TryAddNewSettings(SettingsConfig settingsConfig);
    public Task<bool> TryUpdateSettings(SettingsConfig settingsConfig);
    public SettingsConfig GetSettings(string branchName);
}