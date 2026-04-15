using System.IO;
using System.Linq;

namespace raf.Utils;

public static class Save
{
    public static void SaveObject(string hash, string filePath)
    {
        var content = File.ReadAllBytes(filePath);

        var header = $"blob {content.Length}\0";
        var store = System.Text.Encoding.UTF8.GetBytes(header)
                .Concat(content)
                .ToArray();

        var dir = hash.Substring(0, 2);
        var file = hash.Substring(2);

        var rafPath= Path.Combine(Directory.GetCurrentDirectory(), ".raf");
        var objectDir = Path.Combine(rafPath, "objects", dir);
        
        Directory.CreateDirectory(objectDir);

        var objectPath = Path.Combine(objectDir, file);

        if (!File.Exists(objectPath))
        {
            File.WriteAllBytes(objectPath, store);
        }
    }
}