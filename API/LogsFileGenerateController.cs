using CARDINAL.Persephone.Dates;
using CARDINAL.Persephone.Interfaces;

namespace CARDINAL.Persephone;

internal class LogsFileGenerateController : ILogsFileGenerateController
{
    private readonly IChangeLogFileBroker _changeLogFileBroker;

    public LogsFileGenerateController(IContext context)
    {
        _changeLogFileBroker = context.ChangeLogFileBroker;
    }

    public async Task UpdateChangelogFile(IList<BuildLogData> logDatas)
    {
        await _changeLogFileBroker.UpdateLogs(logDatas);
    }
}