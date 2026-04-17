using System;
using System.IO;
using raf.Commands;
using raf.Utils;

namespace raf.Core;

public class CommitService
{
    public static string Commit(string treeHash, string message)
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var currentCommit = BranchCommand.GetCurrentCommit();
        
        File.WriteAllText(Path.Combine(".raf", "COMMIT_MESSAGE_last"), message);

        var content = $@"tree {treeHash}  parent {currentCommit} date {timeStamp}  message {message}";
        
        var hash = Hash.ComputeRawHash("commit", content);

        Save.SaveRawObject(hash, "commit", content);

        return hash;
    }
}