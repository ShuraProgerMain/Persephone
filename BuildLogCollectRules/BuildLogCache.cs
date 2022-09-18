using System.Collections.ObjectModel;
using CARDINAL.Persephone.Configs;
using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Helpers;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SaveRules;

namespace CARDINAL.Persephone.BuildLogCollectRules;

internal class BuildLogCache : IBuildLogCache
{
    private string _lastBuildVersion = "0.1";
    private string _lastWritingVersion = "0.1";

    private List<BuildLogData> _lastLogs = new();
    private readonly List<BuildLogData>? _logs = new();

    public List<BuildLogData> LastLogs => _logs;
    public string LastBuildVersion => _lastBuildVersion;


    private readonly string _fileName = "/cache.hehe";

    public BuildLogCache(IContext context)
    {
        BuildLogCacheConfig? saveLoad =
            SaveBroker.LoadSerializeData<BuildLogCacheConfig>(Paths.PathToMainSaveFolder(), _fileName);

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

        await SaveBroker.SaveSerializeData(Paths.PathToMainSaveFolder(), _fileName, 
            new BuildLogCacheConfig(_lastBuildVersion, _lastWritingVersion,_logs));
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
        await SaveBroker.SaveSerializeData(Paths.PathToMainSaveFolder(), _fileName, 
            new BuildLogCacheConfig(_lastBuildVersion, _lastWritingVersion,_logs));
        return logsForChangelog;
    }
}