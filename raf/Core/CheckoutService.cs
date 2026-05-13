using System;
using System.IO;
using System.Linq;
using raf.Utils;

namespace raf.Core;

public static class CheckoutService
{
    public static void Checkout(string commitHash)
    {
        var commitContent = ObjectReader.Read(commitHash);

        var treeHash = ObjectReader.ExtractTree(commitContent);

        var treeContent = ObjectReader.Read(treeHash);

        RestoreFiles(treeContent);

        var hasChanges = HasChanges();
        
        if (hasChanges)
        {
            Console.WriteLine("You have alterations don´t commiteds");
            return;
        }

        Console.WriteLine($"Switched to {commitHash}");
    }
    
    public static bool HasChanges()
    {
        var indexPath = Path.Combine(".raf", "index");

        if (!File.Exists(indexPath))
            return false;

        var index = File.ReadAllLines(indexPath)
            .Select(l => l.Split(' '))
            .Where(p => p.Length == 2)
            .ToDictionary(p => p[0], p => p[1]);

        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.AllDirectories)
            .Where(f => !f.Contains(".raf"));

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var currentHash = Hash.ComputeHash(file);

            if (!index.TryGetValue(fileName, out var stagedHash) || stagedHash != currentHash)
                return true;
        }

        return false;
    }
    
    private static void RestoreFiles(string treeContent)
    {
        var lines = treeContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Split(' ');

            if (parts.Length < 3)
                continue;

            var type = parts[0];   // pig
            var hash = parts[1];
            var fileName = parts[2];

            var fileContent = ObjectReader.Read(hash);

            File.WriteAllText(fileName, fileContent);
        }
    }
}