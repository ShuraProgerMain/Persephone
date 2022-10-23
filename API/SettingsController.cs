using CARDINAL.Persephone.API.Interfaces;
using CARDINAL.Persephone.Configs;
using CARDINAL.Persephone.Interfaces;
using Console = CARDINAL.Persephone.Helpers.Console;

namespace CARDINAL.Persephone.API;

internal class SettingsController : ISettingsController
{
    private readonly ISettingsBroker _settingsBroker;
    
    public SettingsController(IContext context)
    {
        _settingsBroker = context.SettingsBroker;
    }

    public async void AddNewBranchSettings(BranchSettingConfig config)
    {
        var result = await _settingsBroker.TryAddNewSettings(config);

        if (result)
        {
            Console.Log("Adding config successfully");
        }
        else
        {
            Console.LogError("Error when adding config");
        }
    }
    
    public async void UpdateBranchSettings(BranchSettingConfig config)
    {
        var result = await _settingsBroker.TryUpdateSettings(config);

        if (result)
        {
            Console.Log("Update config successfully");
        }
        else
        {
            Console.LogError("Error when update config");
        }
    }
    
    public void ChooseDefaultSettings(string branchName)
    {
        var result = _settingsBroker.TrySetDefaultSettings(branchName);

        if (result)
        {
            Console.Log("Adding config successfully");
        }
        else
        {
            Console.LogError("Error when adding config");
        }
    }
}