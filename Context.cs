using CARDINAL.Persephone.API;
using CARDINAL.Persephone.API.Interfaces;
using CARDINAL.Persephone.BuildLogCollectRules;
using CARDINAL.Persephone.ChangelogFileGenerationRules;
using CARDINAL.Persephone.GitRules;
using CARDINAL.Persephone.Interfaces;
using CARDINAL.Persephone.SettingsRules;

namespace CARDINAL.Persephone;

internal class Context : IContext
{
    private IGitBroker _gitBroker;
    private IChangeLogFileBroker _changeLogFileBroker;
    private ISettingsBroker _settingsBroker;
    private IBuildLogCollector _buildLogCollector;
    private IBuildLogCache _buildLogCache;
    private ILogsCollectController _logsCollectController;
    private ILogsFileGenerateController _logsFileGenerateController;

    public IGitBroker GitBroker => _gitBroker;
    public IChangeLogFileBroker ChangeLogFileBroker => _changeLogFileBroker;
    public ISettingsBroker SettingsBroker => _settingsBroker;
    public IBuildLogCollector BuildLogCollector => _buildLogCollector;
    public IBuildLogCache BuildLogCache => _buildLogCache;
    
    public ILogsCollectController LogsCollectController => _logsCollectController;
    public ILogsFileGenerateController LogsFileGenerateController => _logsFileGenerateController;

    public async Task InitContext()
    {
        await BrokerBindings();
        
        _buildLogCache = new BuildLogCache(this);
        _buildLogCollector = new BuildLogCollector(this);
        
        ControllerBindings();
    }

    private async Task BrokerBindings()
    {
        _settingsBroker = new SettingsBroker(this);
        _gitBroker = new GitBroker(this);
        await _settingsBroker.Init();
        _changeLogFileBroker = new ChangeLogFileBroker(this);
    }
    
    private void ControllerBindings()
    {
        _logsCollectController = new LogsCollectController(this);
        _logsFileGenerateController = new LogsFileGenerateController(this);
    }
}