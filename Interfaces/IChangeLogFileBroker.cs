using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone.Interfaces;

internal interface IChangeLogFileBroker
{
    public Task UpdateLogs(IList<BuildLogData> logs);
}