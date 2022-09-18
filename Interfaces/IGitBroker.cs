using LibGit2Sharp;

namespace CARDINAL.Persephone.Interfaces;

internal interface IGitBroker
{
    List<Commit> GetRangeCommits(int range, string branchName);
    void CommitFile(string fileToCommit, string message);
}