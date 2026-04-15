using System;
using raf.Core;

namespace raf.Commands;

public class CommitCommand
{
    public static void Execute(string message)
    {
        var treeHash = TreeService.CreateTree();
        var commitHash = CommitService.Commit(treeHash, message);

        HeadService.Update(commitHash);

        Console.WriteLine($"Commit criado: {commitHash}");
    }
}