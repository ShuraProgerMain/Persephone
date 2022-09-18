namespace CARDINAL.Persephone.API.Interfaces;

public interface IPersephoneApi
{
    public ILogsCollectController GetLogsCollector();
    public ILogsFileGenerateController GetLogsFileGenerator();
}