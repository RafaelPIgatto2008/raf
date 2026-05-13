using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using raf.Core;

namespace raf.Commands;

public class CommitCommand
{
    public static void Execute(string message)
    {
        var treeHash = TreeService.CreateTree();
        var commitHash = CommitService.Commit(treeHash, message);
        
        HeadService.Update(commitHash);
        
        BranchCommand.UpdateCurrentBranch(commitHash);

        Console.WriteLine($"Commit criado: {commitHash}");
    }

    public static void List()
    {
        var path = Path.Combine(".raf", "objects");

        if (!Directory.Exists(path))
        {
            Console.WriteLine("No commits found.");
            return;
        }
        
        var files = Directory
            .GetDirectories(path)
            .SelectMany(Directory.GetFiles)
            .OrderByDescending(File.GetCreationTime)
            .ToList();
        
        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            
            string timeLine = "";
            string message = "";
            string treeHash = "";
            
            foreach (var line in lines)
            { 
                if (line.StartsWith("time:")) 
                    timeLine = line.Substring("time:".Length).Trim();
                
                else if (line.StartsWith("message:")) 
                    message = line.Substring("message:".Length).Trim();
                
                else if (line.StartsWith("tree "))
                    treeHash = line.Substring("tree ".Length).Trim();
            }
            
            if (timeLine == null)
            { 
                continue;
            }
            
            if (!long.TryParse(timeLine, out var unixTime))
            {
                continue;
            }
            
            var date = DateTimeOffset
                .FromUnixTimeSeconds(unixTime)
                .DateTime;
            
            var hash = Path.GetFileName(file);
            var tree = treeHash;
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"commit: {hash}");
            Console.WriteLine($"Tree: {tree}");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Date: {date:dd/MM/yyyy}");
            
            Console.ResetColor();
            Console.WriteLine($"    {message}");
            Console.WriteLine();
        }
    }
}