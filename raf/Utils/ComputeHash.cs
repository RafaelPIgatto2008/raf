using System;
using System.IO;
using System.Linq;

namespace raf.Utils;

public class Hash
{
    public static string ComputeHash(string filePath)
    {
        using var sha1 = System.Security.Cryptography.SHA1.Create();

        var content = File.ReadAllBytes(filePath);
        var header = $"blob {content.Length}\0";
        var store = System.Text.Encoding.UTF8.GetBytes(header)
            .Concat(content)
            .ToArray();

        var hashBytes = sha1.ComputeHash(store);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}