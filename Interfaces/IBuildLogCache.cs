using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone.Interfaces;

internal interface IBuildLogCache
{
    public List<BuildLogData>? LastLogs { get; }
    public string LastBuildVersion { get; }


    public void SetLogs(List<BuildLogData> logs);
    public BuildLogData GetLog(string version);
    public Task<IList<BuildLogData>> GetLogsForChangelog();
}