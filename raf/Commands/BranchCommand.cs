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
    
    public static string GetCurrentCommit()
    {
        var currentBranch = GetCurrentBranchName();
        var headPath = Path.Combine(".raf", "refs", "heads", currentBranch);

        if (!File.Exists(headPath))
            return null;

        var commitHash = File.ReadAllText(headPath);

        if (string.IsNullOrEmpty(commitHash))
        {
            Console.WriteLine("This commit don´t have parents");
            return null;
        }
        
        return commitHash;
    }
    
    public static void ListBranches()
    {
        var path = Path.Combine(".raf", "refs", "heads");

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
        var currentBranch = GetCurrentBranchName();
        
        if (!File.Exists(path))
        {
            Console.WriteLine($"Branch {name} not found");
            return;
        }

        if (currentBranch == name)
        {
            Console.WriteLine($"You are already on branch {name}");
            return;
        }
        
        File.WriteAllText(".raf/HEAD", $"refs/heads/{name}"); 
        Console.WriteLine($"Switched to branch name {name}");
    }
    
    public static string GetCurrentBranchName()
    {
        var head = File.ReadAllText(".raf/HEAD").Trim();

        return head.Replace("ref: refs/heads/", "");
    }
    
    public static void UpdateCurrentBranch(string commitHash)
    {
        var headRef = File.ReadAllText(".raf/HEAD").Trim();

        if (headRef.StartsWith("ref: "))
        {
            headRef = headRef.Replace("ref: ", "");
        }
        
        var branchPath = Path.Combine(".raf", headRef);

        File.WriteAllText(branchPath, commitHash);
    }
}