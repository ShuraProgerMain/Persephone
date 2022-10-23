using System.Collections.ObjectModel;
using CARDINAL.Persephone.Configs;
using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SaveRules;
using CARDINAL.Persephone.SettingsRules;

namespace CARDINAL.Persephone.BuildLogCollectRules;

internal class BuildLogCache : IBuildLogCache
{
    private string _lastBuildVersion = "nothing";
    private string _lastWritingVersion = "nothing";

    private List<BuildLogData> _lastLogs = new();
    private readonly List<BuildLogData>? _logs = new();

    public List<BuildLogData> LastLogs => _logs;
    public string LastBuildVersion => _lastBuildVersion;


    private readonly SystemData _systemData;

    public BuildLogCache(IContext context)
    {
        _systemData = context.SettingsBroker.SystemData;
        
        BuildLogCacheConfig? saveLoad =
            SaveBroker.LoadSerializeData<BuildLogCacheConfig>(_systemData.PathToMainSaveFolder, _systemData.CacheSaveFile);

        if (saveLoad != null)
        {
            _lastBuildVersion = saveLoad.LastBuild;
            _lastWritingVersion = saveLoad.LastWritingVersion;
            _logs = saveLoad.Logs;
        }
    }


    public async void SetLogs(List<BuildLogData> logs)
    {
        _lastLogs.Clear();
        
        if (logs.Count <= 0) return;
        
        if (_logs?.Count > 0)
        {
            foreach (var unused in from log in _logs
                     from buildLog in logs
                     where buildLog.Version == log.Version
                     select buildLog)
            {
                return;
            }
        }


        _lastBuildVersion = logs[0].Version;
        _lastLogs = logs;
        _logs?.AddRange(logs);

        await SaveBroker.SaveSerializeData(_systemData.PathToMainSaveFolder, _systemData.CacheSaveFile, 
            new BuildLogCacheConfig(_lastBuildVersion, _lastWritingVersion, _logs));
    }

    public BuildLogData GetLog(string version)
    {
        throw new NotImplementedException();
    }

    public BuildLogData GetLastLog()
    {
        throw new NotImplementedException();
    }

    public async Task<IList<BuildLogData>> GetLogsForChangelog()
    {
        IList<BuildLogData> logsForChangelog = new Collection<BuildLogData>();

        if (_lastBuildVersion == _lastWritingVersion) return logsForChangelog;
        
        foreach (var log in _logs)
        {
            if (log.Version.Contains(_lastWritingVersion)) break;
            
            logsForChangelog.Add(log);
        }

        _lastWritingVersion = _logs[0].Version;
        await SaveBroker.SaveSerializeData(_systemData.PathToMainSaveFolder, _systemData.CacheSaveFile, 
            new BuildLogCacheConfig(_lastBuildVersion, _lastWritingVersion,_logs));
        return logsForChangelog;
    }
}