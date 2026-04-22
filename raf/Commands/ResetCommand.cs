using System;
using System.IO;
using System.Linq;

namespace raf.Commands;

public static class ResetCommand
{
    public static void Execute(string target)
    {
        var indexPath = Path.Combine(".raf", "index");

        if (!File.Exists(indexPath))
        {
            Console.WriteLine($"{indexPath} not found");
            return;
        }
        
        var lines = File.ReadAllLines(indexPath);

        if (target == ".")
        {
            File.WriteAllText(indexPath, string.Empty);
            Console.WriteLine($"Index reseted");
            return;
        }
        
        var newLines = lines
            .Where(line => !line.StartsWith(target + " "))
            .ToList();
        
        File.WriteAllLines(indexPath, newLines);
        Console.WriteLine($"{target} reseted");
    }
}