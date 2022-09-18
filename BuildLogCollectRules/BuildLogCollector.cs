using System.Globalization;
using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Interfaces;

namespace CARDINAL.Persephone.BuildLogCollectRules;

internal class BuildLogCollector : IBuildLogCollector
{
    private readonly IGitBroker _gitBroker;
    private readonly ISettingsBroker _settingsBroker;
    private readonly IBuildLogCache _buildLogCache;

    public BuildLogCollector(IContext context)
    {
        _gitBroker = context.GitBroker;
        _settingsBroker = context.SettingsBroker;
        _buildLogCache = context.BuildLogCache;
    }

    public Task<List<BuildLogData>> GetLastBuildLogs(string branchName = "")
    {
        byte lastCommitPosition = 0;
        var logs = new List<BuildLogData>();
        var currentLog = new BuildLogData(string.Empty, DateTime.Now.ToString(CultureInfo.InvariantCulture),
            new List<string>());

        branchName = branchName == string.Empty ? _settingsBroker.DefaultBranchSetting.BranchName : branchName;
        var config = _settingsBroker.GetSettings(branchName);


        var readContinue = true;
        while (readContinue)
        {
            var commits = _gitBroker.GetRangeCommits(config.DefaultReceivedCommitRange + lastCommitPosition, branchName);

            for (; lastCommitPosition < commits.Count; lastCommitPosition++)
            {
                var commit = commits[lastCommitPosition];
                var message = commit.Message;

                if (message.Contains("changelog commit")) continue;
                
                if (message.Contains(config.InitialLogKey) || message.Contains(_buildLogCache.LastBuildVersion))
                {
                    if (currentLog.Version != string.Empty && currentLog.Messages?.Count > 0) logs.Add(currentLog);

                    readContinue = false;
                    break;
                }

                var key = config.KeyMessages!.FirstOrDefault(key => message.Contains(key));
                
                if (key != null)
                {
                    if (currentLog.Version != string.Empty)
                    {
                        logs.Add(currentLog);
                    }

                    var version = GetVersionFromString(message, key);
                    var date = GetDateStringFromDateTimeOffset(commit.Author.When);
                    
                    currentLog = new BuildLogData(version, date, new List<string>());

                    continue;
                }

                currentLog.Messages?.Add(message);
            }
        }
        
        return Task.FromResult(logs);
    }

    private string GetVersionFromString(string str, string key)
    {
        return str.Replace(" ", "")
            .Replace("\n", "")
            .Replace(key, "");
    }

    private string GetDateStringFromDateTimeOffset(DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToString("yyyy-MM-dd hh:mm:sstt ",
            CultureInfo.CreateSpecificCulture("en-Us"));
    }
}