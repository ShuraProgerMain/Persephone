using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone.Interfaces;

internal interface IBuildLogCollector
{
    Task<List<BuildLogData>> GetLastBuildLogs(string branchName = "");
}