using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone.API.Interfaces;

public interface ILogsCollectController
{
    public Task<IList<BuildLogData>> CollectLogs(bool updateCache = true);
    public Task<IList<BuildLogData>> GetLogsForChangelog();
}