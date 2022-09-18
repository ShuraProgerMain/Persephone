namespace CARDINAL.Persephone.Configs;

public class BranchSettingConfig
{
    private readonly string _branchName;
    private string _initialLogKey = string.Empty;
    private string _buildKeyMessage = string.Empty;
    private string _repositoryPath = string.Empty;
    private byte _defaultReceivedCommitRange = 10;

    public string BranchName => _branchName;
    public string InitialLogKey => _initialLogKey;
    public string BuildKeyMessage => _buildKeyMessage;
    public string RepositoryPath => _repositoryPath;
    public byte DefaultReceivedCommitRange => _defaultReceivedCommitRange;


    /// <summary>
    /// BranchSettingConfig constructor. If you want update branch setting need push new SettingsConfig with new data
    /// </summary>
    /// <param name="branchName">Name branch format: If local {"main"} else if remote {"origin/main"}</param>
    /// <param name="initialLogKey">The commit text below which the generator will not go down
    /// </param>
    /// <param name="buildKeyMessage">
    /// The keyword or sentence for which the beginning of each version will be located
    /// </param>
    /// <param name="repositoryPath">
    /// Path to your local repository
    /// </param>
    /// <param name="defaultReceivedCommitRange">
    /// Starting number of commits received. Not changed
    /// </param>
    /// <example>
    /// <code>
    /// new SettingsConfig("main", "Changelog Init", "Build:", @"D:\UnityProjects\RustAndUnityTest\Assets\Plugins\TestDLL", 10); 
    /// </code>
    /// </example>
    public BranchSettingConfig(
        string branchName = "main",
        string initialLogKey = "",
        string buildKeyMessage = "",
        string repositoryPath = "",
        byte defaultReceivedCommitRange = 10
    )
    {
        _branchName = branchName;
        _initialLogKey = initialLogKey == string.Empty ? _initialLogKey : initialLogKey;
        _buildKeyMessage = buildKeyMessage == string.Empty ? _buildKeyMessage : buildKeyMessage;
        _repositoryPath = repositoryPath == string.Empty ? _repositoryPath : repositoryPath;
        _defaultReceivedCommitRange =
            defaultReceivedCommitRange == 10 ? _defaultReceivedCommitRange : defaultReceivedCommitRange;
    }

    public void Join(BranchSettingConfig config)
    {
        _initialLogKey = config.InitialLogKey == string.Empty ? _initialLogKey : config.InitialLogKey;
        _buildKeyMessage = config.BuildKeyMessage == string.Empty ? _buildKeyMessage : config.BuildKeyMessage;
        _repositoryPath = config.RepositoryPath == string.Empty ? _repositoryPath : config.RepositoryPath;
        _defaultReceivedCommitRange = config.DefaultReceivedCommitRange == 10
            ? _defaultReceivedCommitRange
            : config.DefaultReceivedCommitRange;
    }
}