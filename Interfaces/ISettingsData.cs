namespace CARDINAL.Persephone.Interfaces;

internal interface ISettingsData
{
    public string InitialLogKey { get; }
    public string RepositoryPath { get; }
    public string BuildKeyMessage { get; }
    public string PathToCache { get; }
    public byte DefaultReceivedCommitRange { get; }
    

    bool ChangeInitialLogKey(string key);
    bool ChangeRepositoryPath(string path);
    bool ChangeBuildKeyMessage(string keyMessage);
}