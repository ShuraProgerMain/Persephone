using CARDINAL.Persephone.API;
using CARDINAL.Persephone.API.Interfaces;
using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Interfaces;

namespace CARDINAL.Persephone;

internal class LogsCollectController : ILogsCollectController
{
    private readonly IBuildLogCollector _buildLogCollector;
    private readonly IBuildLogCache _buildLogCache;

    public LogsCollectController(IContext context)
    {
        _buildLogCollector = context.BuildLogCollector;
        _buildLogCache = context.BuildLogCache;
    }

    public async Task<IList<BuildLogData>> CollectLogs(bool updateCache = true)
    {
        var collectedLogs = await _buildLogCollector.GetLastBuildLogs();

        if (updateCache)
            _buildLogCache.SetLogs(collectedLogs);

        return collectedLogs;
    }

    public async Task<IList<BuildLogData>> GetLogsForChangelog()
    {
        return await _buildLogCache.GetLogsForChangelog();
    }
}