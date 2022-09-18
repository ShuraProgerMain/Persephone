using CARDINAL.Persephone.Interfaces;
using LibGit2Sharp;

namespace CARDINAL.Persephone.GitRules;

internal class GitBroker : IGitBroker
{
    private readonly ISettingsBroker _settingsBroker;

    public GitBroker(IContext context)
    {
        _settingsBroker = context.SettingsBroker;
    }

    public List<Commit> GetRangeCommits(int range, string branchName)
    {
        var repository = new Repository(_settingsBroker.DefaultSetting.RepositoryPath);
        
        return repository.Branches[branchName].Commits.Take(range).ToList();
    }

    public void CommitFile(string fileToCommit, string message)
    {
        try
        {
            var repository = new Repository(_settingsBroker.DefaultSetting.RepositoryPath);

            var state = repository.RetrieveStatus(new StatusOptions()).Modified.FirstOrDefault(file => file.FilePath.Contains(fileToCommit.Replace(@"\", "/")));;

            if (state != null)
            {
                repository.Index.Add(fileToCommit);
                repository.Index.Write();

                Signature author = new Signature("Persephone", "@ihateyou", DateTime.Now);
                Signature committer = author;

                Commit commit = repository.Commit(message, author, committer);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void Push()
    {
        //TODO: add "autoPush bool" to settingsData

        using var repo = new Repository(_settingsBroker.DefaultSetting.RepositoryPath);
        Remote remote = repo.Network.Remotes["origin"];
        var options = new PushOptions();
        options.CredentialsProvider = (_url, _user, _cred) => 
            new UsernamePasswordCredentials { Username = "ShuraProgerMain", Password = "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" };
        repo.Network.Push(remote, @"refs/heads/main", options);
    }
}