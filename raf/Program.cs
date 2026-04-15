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
                
                default:
                    Console.WriteLine("Comando não reconhecido");
                    break;
            }
        }
    }
}