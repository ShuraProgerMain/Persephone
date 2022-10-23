using CARDINAL.Persephone.Configs;

namespace CARDINAL.Persephone.API.Interfaces;

public interface ISettingsController
{
    public void AddNewBranchSettings(BranchSettingConfig config);
    public void UpdateBranchSettings(BranchSettingConfig config);
    public void ChooseDefaultSettings(string branchName);
}