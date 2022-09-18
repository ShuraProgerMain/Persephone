using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone;

public interface ILogsFileGenerateController
{
    public Task UpdateChangelogFile(IList<BuildLogData> logDatas);
}