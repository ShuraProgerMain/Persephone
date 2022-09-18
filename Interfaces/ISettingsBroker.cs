using CARDINAL.Persephone.Configs;

namespace CARDINAL.Persephone.Interfaces;

internal interface ISettingsBroker
{
    public BranchSettingConfig DefaultBranchSetting { get; }
    public Task Init();
    public Task<bool> TryAddNewSettings(BranchSettingConfig branchSettingConfig);
    public Task<bool> TryUpdateSettings(BranchSettingConfig branchSettingConfig);
    public bool TrySetDefaultSettings(string branchName);
    public BranchSettingConfig GetSettings(string branchName);
}