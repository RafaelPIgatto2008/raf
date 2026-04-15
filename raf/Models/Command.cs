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
            new("init", "Inicializa repositório"),
            new("add", "Adiciona arquivos"),
            new("status", "Mostra status"),
            new("clean", "Remove .raf"),
            new("help", "Lista comandos"),
            new("commit", "Salva as alterações feitas")
        };
            
        foreach (var cmd in commands)
        {
            Console.WriteLine($"raf {cmd.Name,-15} ==> {cmd.Description}");
        }
    }
}