
using System;
using System.IO;
using System.Linq;

namespace raf.Utils;

public class Hash
{
    public static string ComputeHash(string filePath)
    {
        var content = File.ReadAllBytes(filePath);
        return ComputeHash("pig", content);
    }

    public static string ComputeRawHash(string type, string content)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        return ComputeHash(type, bytes);
    }

    private static string ComputeHash(string type, byte[] content)
    {
        using var sha1 = System.Security.Cryptography.SHA1.Create();

        var header = $"{type} {content.Length}\0";
        var store = System.Text.Encoding.UTF8.GetBytes(header)
            .Concat(content)
            .ToArray();

        var hashBytes = sha1.ComputeHash(store);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}