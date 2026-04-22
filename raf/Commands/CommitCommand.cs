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
            .GetFiles(path)
            .OrderByDescending(File.GetCreationTime)
            .ToList();

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);

            string commitLine = "";
            string timeLine = "";
            string message = "";

            foreach (var line in lines)
            {
                if (line.StartsWith("commit"))
                    commitLine = line;

                if (line.StartsWith("time:"))
                    timeLine = line.Replace("time:", "").Trim();

                if (line.StartsWith("message:"))
                    message = line.Replace("message:", "").Trim();
            }

            var parts = commitLine.Split(' ');
            var hash = parts.Length > 1 ? parts[1] : "unknown";

            var date = DateTimeOffset
                .FromUnixTimeSeconds(long.Parse(timeLine))
                .DateTime;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"commit {hash}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Date: {date:dd/MM/yyyy HH:mm}");

            Console.ResetColor();
            Console.WriteLine($"    {message}");
            Console.WriteLine();
        }
    }
}