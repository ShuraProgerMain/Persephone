using System.Text.Json.Serialization;
using CARDINAL.Persephone.Dates;

namespace CARDINAL.Persephone.Configs;

public class BuildLogCacheConfig
{
    public string LastBuild { get; }
    public string LastWritingVersion { get; }
    public List<BuildLogData> Logs { get; }

    [JsonConstructor]
    public BuildLogCacheConfig(string lastBuild, string lastWritingVersion , List<BuildLogData> logs)
    {
        LastBuild = lastBuild;
        LastWritingVersion = lastWritingVersion;
        Logs = logs;
    }
}