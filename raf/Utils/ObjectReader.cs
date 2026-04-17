using System;
using System.IO;

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
        var lines = commitContent.Split('\n');

        foreach (var line in lines)
        {
            if (line.StartsWith("tree "))
                return line.Replace("tree ", "").Trim();
        }

        throw new Exception("Tree not found");
    }
}
