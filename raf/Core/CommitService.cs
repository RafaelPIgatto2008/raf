using System;
using raf.Utils;

namespace raf.Core;

public class CommitService
{
    public static string Commit(string treeHash, string message)
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var content = $@"tree: {treeHash}, date: {timeStamp}, mess: {message}";
        
        var hash = Hash.ComputeRawHash("commit", content);

        Save.SaveRawObject(hash, "commit", content);

        return hash;
    }
}