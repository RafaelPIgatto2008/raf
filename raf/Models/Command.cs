using System;
using System.Collections.Generic;

namespace raf.Models;

public class Command
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public Command(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
    
    public static void AllCommands()
    {
        var commands = new List<Command>
        {
            new("init", "Initialize repository"),
            new("add", "Add files"),
            new("status", "Show status"),
            new("clean", "Remove .raf"),
            new("help", "List commands"),
            new("commit", "Save changes"),
            new("commit --list", "List of commits"),
            new("update --tool", "Update the raf tool"),
            new("switch Branch", "Switch branch"),
            new("switch -n Branch", "Create a branch and switch to it"),
            new("switch --list", "List created branches"),
            new("reset <file>", "Reset the selected file in index"),
            new("reset .", "Reset all files in index"),
            new("log", "Show the history of commits")
        };
            
        foreach (var cmd in commands)
        {
            Console.WriteLine($"raf {cmd.Name,-15} ==> {cmd.Description}");
        }
    }
}
