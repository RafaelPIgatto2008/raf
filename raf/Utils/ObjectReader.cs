using System;
using System.IO;
using raf.Models;

namespace raf.Utils;

public static class ObjectReader
{
    public static string Read(string hash)
    {
        var dir = hash.Substring(0, 2);
        var file = hash.Substring(2);

        var path = Path.Combine(".raf", "objects", dir, file);
        var bytes = File.ReadAllBytes(path);
        var content = System.Text.Encoding.UTF8.GetString(bytes);
        var nullIndex = content.IndexOf('\0');

        return content.Substring(nullIndex + 1);
    }

    public static string ExtractTree(string commitContent)
    {
        using var reader = new StringReader(commitContent);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (line.StartsWith("tree "))
            {
                return line.Substring("tree ".Length).Trim();
            }
        }

        throw new Exception("Tree not found");
    }

    public static CommitObject ReadCommitObject(string hash)
    {
        var content = Read(hash);
        using var reader = new StringReader(content);

        var commit = new CommitObject
        {
            CommitHash = hash
        };

        var hasCommitData = false;
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            if (line.StartsWith("tree "))
            {
                commit.Tree = line.Substring("tree ".Length).Trim();
                hasCommitData = true;
                continue;
            }

            if (line.StartsWith("time:"))
            {
                if (!long.TryParse(line.Substring("time:".Length).Trim(), out var time))
                {
                    return null;
                }

                commit.Time = time;
                hasCommitData = true;
                continue;
            }

            if (line.StartsWith("message:"))
            {
                commit.Message = line.Substring("message:".Length).Trim();
                hasCommitData = true;
            }
        }

        if (!hasCommitData || commit.Time == 0)
        {
            return null;
        }

        return commit;
    }
}
