using CARDINAL.Persephone.API;
using CARDINAL.Persephone.API.Interfaces;

namespace CARDINAL.Persephone.Interfaces;

internal interface IContext
{
    public IGitBroker GitBroker { get; }
    public IChangeLogFileBroker ChangeLogFileBroker { get; }
    public ISettingsBroker SettingsBroker { get; }
    public IBuildLogCollector BuildLogCollector { get; }
    public IBuildLogCache BuildLogCache { get; }
    public ILogsCollectController LogsCollectController { get; }
    public ILogsFileGenerateController LogsFileGenerateController { get; }
    public ISettingsController SettingsController { get; }
    public Task InitContext();
}