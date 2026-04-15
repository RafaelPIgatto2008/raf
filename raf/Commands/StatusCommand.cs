
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace raf.Commands;

public class StatusCommand
{
    public static void Status()
    {
        // Get the current directory, raf path and index path
        var currentDir = Directory.GetCurrentDirectory();
        var rafPath = Path.Combine(currentDir, ".raf");
        var indexPath = Path.Combine(rafPath, "index"); 
        
        var allFiles = Directory.GetFiles(currentDir)
            .Where(f => !f.Contains(".raf"))
            .Select(Path.GetFileName)
            .ToList();

        var stagedFiles = new Dictionary<string, string>();

        if (File.Exists(indexPath))
        {
            var lines = File.ReadAllLines(indexPath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                    stagedFiles.Add(parts[0], parts[1]);
            }
        }
        
        // Separate in two lists untracked and modified
        var untracked = new List<string>();
        var modified = new List<string>();
        
        foreach (var file in allFiles)
        {
            if (!stagedFiles.ContainsKey(file))
            {
                untracked.Add(file);
            }
            else
            {
                var currentHash = ComputeHash(file);
                
                // Compare the current hash with the new hash to see if was modified
                if (stagedFiles[file] != currentHash)
                {
                    modified.Add(file);
                }
            }
        }
        
        var stagedOnly = stagedFiles.Keys
            .Where(f => allFiles.Contains(f))
            .ToList();

        Console.WriteLine("==== STATUS ====\n");

        if (untracked.Any())
        {
            Console.WriteLine("Não rastreados:");
            untracked.ForEach(f => Console.WriteLine($"  {f}"));
            Console.WriteLine();
        }

        if (stagedOnly.Any())
        {
            Console.WriteLine("Pendentes:");
            stagedOnly.ForEach(f => Console.WriteLine($"  {f}"));
            Console.WriteLine();
        }

        if (modified.Any())
        {
            Console.WriteLine("Modificados:");
            modified.ForEach(f => Console.WriteLine($"  {f}"));
            Console.WriteLine();
        }

        if (!untracked.Any() && !stagedOnly.Any() && !modified.Any())
        {
            Console.WriteLine("Nada para mostrar");
        }
    }
    
    /// <summary>
    /// A method to hash the file
    /// </summary>
    static string ComputeHash(string filePath)
    {
        using var sha1 = System.Security.Cryptography.SHA1.Create();

        var content = File.ReadAllBytes(filePath);
        var header = $"pig {content.Length}\0";
        var store = System.Text.Encoding.UTF8.GetBytes(header)
            .Concat(content)
            .ToArray();

        var hashBytes = sha1.ComputeHash(store);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}