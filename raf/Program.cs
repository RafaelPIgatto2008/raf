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
                    Console.WriteLine("Tem certeza que deseja excluir o diretorio raf inteiro y/n ? ");
                    var imput = Console.ReadLine();

                    if (imput?.ToLower() != "y")
                    {
                        Console.WriteLine("Comando cancelado");
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
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Está faltando a mensagem");
                        return;
                    }
                    
                    CommitCommand.Execute(string.Join(" ", args.Skip(1)));
                    break;
                
                case "update":
                    if (args.Length > 1 && args[1] == "--tool")
                    {
                        UpdateToolCommand.Execute();
                    }

                    break;
                
                case "switch":
                    if (args.Length > 2 && args[0] == "-n" && args[1] != string.Empty)
                    {
                        BranchCommand.Create(args[1]);
                    }

                    if (args.Length > 1 && args[1] == "--list")
                    {
                        BranchCommand.ListBranches();
                    }

                    if (args.Length > 1 && args[1] != string.Empty)
                    {
                        BranchCommand.SwitchBranch(args[1]);
                    }
                    
                    break;
                
                default:
                    Console.WriteLine("Comando não reconhecido");
                    break;
            }
        }
    }
}