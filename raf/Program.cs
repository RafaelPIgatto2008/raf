using System;
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
