using System;
using System.IO;
using System.Linq;
using System.Text;
using raf.Commands;
using raf.Utils;

namespace raf.Core;

public class CommitService
{
    public static string Commit(string treeHash, string message)
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var currentCommit = BranchCommand.GetCurrentCommit();
        
        File.WriteAllText(Path.Combine(".raf", "COMMIT_MESSAGE"), message);

        var content = new StringBuilder();
        
        content.AppendLine("tree " + treeHash);
        
        if (!string.IsNullOrEmpty(currentCommit))
        {
            content.AppendLine("parent " + currentCommit);
        }
        
        content.AppendLine("time: " + timeStamp);
        content.AppendLine("message: " + message);
        
        var hash = Hash.ComputeRawHash("commit", content.ToString());

        Save.SaveRawObject(hash, "commit", content.ToString());

        return hash;
    }
}