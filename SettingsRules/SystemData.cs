namespace CARDINAL.Persephone.SettingsRules;

internal class SystemData
{
    public string SettingsSaveFile => @"\settings.hehe";
    public string CacheSaveFile => @"\cache.hehe";
    public string ChangelogSaveFile => @"\CHANGELOG.md";
    public string PathToMainSaveFolder => Path.GetTempPath() + "Persephone";
}