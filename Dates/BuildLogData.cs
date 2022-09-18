namespace CARDINAL.Persephone.Dates;

public class BuildLogData
{
    public string Version { get; }
    public string PushDate { get; }
    public List<string> Messages { get; }

    public BuildLogData(string version, string pushDate, List<string> messages)
    {
        this.Version = version;
        this.Messages = messages;
        this.PushDate = pushDate;
    }
}