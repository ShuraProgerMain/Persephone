using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SaveRules;

namespace CARDINAL.Persephone.ChangelogFileGenerationRules;

internal class ChangeLogFileBroker : IChangeLogFileBroker
{
    private readonly ISettingsBroker _settingsBroker;
    private readonly string _fileName;
    private List<string> _allFileLogLines = new();
    
    private int _targetLine;
    private bool FileAvailable => File.Exists(_settingsBroker.DefaultBranchSetting.RepositoryPath + $"/{_fileName}");
    
    public ChangeLogFileBroker(IContext context)
    {
        _settingsBroker = context.SettingsBroker;
        _fileName = _settingsBroker.SystemData.ChangelogSaveFile;
    }

    public async Task UpdateLogs(IList<BuildLogData> logs)
    {
        if (!FileAvailable)
        {
            await ChangelogFileInitialize();
        }

        await ReadAllLines();
        
        int count = logs.Count;
        for (var i = count - 1; i > -1; i--)
        {
            foreach (var message in logs[i].Messages)
            {
                _allFileLogLines.Insert(_targetLine, $"- {message}");
            }

            _allFileLogLines.Insert(_targetLine, $"## {logs[i].Version} *({logs[i].PushDate})*");
        }

        await File.WriteAllLinesAsync(_settingsBroker.DefaultBranchSetting.RepositoryPath + $"/{_fileName}", _allFileLogLines);
    }

    private async Task ChangelogFileInitialize()
    {
        var pathToFile = _settingsBroker.DefaultBranchSetting.RepositoryPath + $"/{_fileName}";
        
        if (!File.Exists(pathToFile))
        {
            var initData = "" +
                           "# Changelog \n" +
                           "<br /> \n\n" +
                           "## Versions \n" +
                           "<br /> \n\n";


            await SaveBroker.SaveText(_settingsBroker.DefaultBranchSetting.RepositoryPath + "/", _fileName, initData);
        }
    }

    private async Task ReadAllLines()
    {
        _allFileLogLines = (await File.ReadAllLinesAsync(_settingsBroker.DefaultBranchSetting.RepositoryPath + $"/{_fileName}")).ToList();

        for (int i = 0; i < _allFileLogLines.Count; i++)
        {
            if (_allFileLogLines[i].Contains("## Version"))
            {
                _targetLine = i + 3;
            }
        }
    }
}