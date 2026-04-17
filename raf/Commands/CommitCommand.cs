using System;
using System.IO;
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
}