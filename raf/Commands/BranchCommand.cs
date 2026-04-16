using System;
using System.IO;
using System.Linq;

namespace raf.Commands;

public static class BranchCommand
{
    public static void Create(string name)
    {
        var branchPath = Path.Combine(".raf", "refs", "heads", name);

        if (File.Exists(branchPath))
        {
            Console.WriteLine("Branch já existe");
            return;
        }

        var currentCommit = GetCurrentCommit();

        File.WriteAllText(branchPath, currentCommit ?? "");

        Console.WriteLine($"Branch criada, você está na branch {name}");
    }
    
    private static string GetCurrentCommit()
    {
        var headPath = Path.Combine(".raf", "HEAD");

        if (!File.Exists(headPath))
            return null;

        var headRef = File.ReadAllText(headPath).Trim();

        var branchPath = Path.Combine(".raf", headRef);

        if (!File.Exists(branchPath))
            return null;

        return File.ReadAllText(branchPath).Trim();
    }
    
    public static void ListBranches()
    {
        var path = Path.Combine(".raf", "refs", "HEAD");

        var branchs = Directory.GetFiles(path)
            .Select(Path.GetFileName);
        
        Console.WriteLine("Branchs created: ");
        
        foreach (var branch in branchs)
        {
            Console.WriteLine($"{branch}");
        }
    }
    
    public static void SwitchBranch(string name)
    {
        var path = Path.Combine(".raf", "refs", "heads", name);

        if (!File.Exists(path))
        {
            Console.WriteLine($"Branch {name} not found");
            return;
        }
        
        File.WriteAllText(".raf/HEAD", $"refs/heads/{name}"); 
        Console.WriteLine($"Switched to branch name {name}");
    }
}