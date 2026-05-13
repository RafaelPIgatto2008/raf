
using System.IO;
using System.Linq;

namespace raf.Utils;

public static class Save
{
    public static void SaveObject(string hash, string filePath)
    {
        var content = File.ReadAllBytes(filePath);
        var store = BuildObjectBytes("pig", content);

        SaveBytes(hash, store);
    }

    public static void SaveRawObject(string hash, string type, string content)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(content);
        var store = BuildObjectBytes(type, bytes);

        SaveBytes(hash, store);
    }

    private static byte[] BuildObjectBytes(string type, byte[] content)
    {
        var header = $"{type} {content.Length}\0";
        return System.Text.Encoding.UTF8.GetBytes(header)
            .Concat(content)
            .ToArray();
    }

    private static void SaveBytes(string hash, byte[] store)
    {
        var dir = hash.Substring(0, 2);
        var file = hash.Substring(2);

        var rafPath = Path.Combine(Directory.GetCurrentDirectory(), ".raf");
        var objectDir = Path.Combine(rafPath, "objects", dir);
        
        Directory.CreateDirectory(objectDir);

        var objectPath = Path.Combine(objectDir, file);

        if (!File.Exists(objectPath))
        {
            File.WriteAllBytes(objectPath, store);
        }
    }
}