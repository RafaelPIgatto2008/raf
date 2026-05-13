using System;
using System.Collections.Generic;
using System.Linq;
using raf.Commands;

namespace raf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var command = args.FirstOrDefault();

            switch (command)
            {
                case "init":
                    InitCommand.InitRepository();
                    break;
                
                case "clean":
                    Console.WriteLine("Are you sure you want to delete the entire raf directory y/n?");
                    var imput = Console.ReadLine();

                    if (imput?.ToLower() != "y")
                    {
                        Console.WriteLine("Command canceled");
                        return;
                    }
                    
                    CleanCommand.Execute();
                    break;
                
                case "help":
                    ListCommand.Execute();
                    break;

                case "add":
                    var file = args.ElementAtOrDefault(1);
                    AddCommand.Add(file);
                    break;
                
                case "status":
                    StatusCommand.Status();
                    break;
                
                case "commit":
                    if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
                    {
                        Console.WriteLine("Missing commit message");
                        return;
                    }
                    
                    CommitCommand.Execute(string.Join(" ", args.Skip(1)));
                    break;
                
                case "update":
                    if (args.Length > 1 && args[1] == "--tool")
                    {
                        UpdateToolCommand.Execute();
                        break;
                    }
                    
                    Console.WriteLine("Did you mean 'update --tool'? ");
                    break;
                
                case "log":
                    CommitCommand.List();
                    break;
                
                case "switch":
                    if (args.Length > 2 && args[1] == "-n" && args[2] != null)
                    {
                        BranchCommand.Create(args[2]);
                    }

                    else if (args.Length > 1 && args[1] == "--list")
                    {
                        BranchCommand.ListBranches();
                    }

                    else if (args.Length > 1 && args[1] != string.Empty)
                    {
                        BranchCommand.SwitchBranch(args[1]);
                    }

                    else
                    {
                        Console.WriteLine("Run the 'raf help' command for a list of commands about switch");
                    }
                    
                    break;
                
                case "reset":
                    if (args.Length > 1 && string.IsNullOrEmpty(args[1]))
                    {
                        Console.WriteLine("Missing reset target");
                        return;
                    }
                    
                    ResetCommand.Execute(args[1]);
                    break;
                
                default:
                    Console.WriteLine("Command not recognized");
                    Console.WriteLine("Run the 'raf help' command for a list of commands");

                    break;
            }
        }
    }
}
