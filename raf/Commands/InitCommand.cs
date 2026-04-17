
using System;
using System.IO;

namespace raf.Commands;

public static class InitCommand
{
    public static void InitRepository()
    {
        var rafPath = Path.Combine(Directory.GetCurrentDirectory(), ".raf");

        if (Directory.Exists(rafPath))
        {
            Console.WriteLine("Repositório já inicializado");
            return;
        }

        Directory.CreateDirectory(rafPath);
        Directory.CreateDirectory(Path.Combine(rafPath, "objects"));
        Directory.CreateDirectory(Path.Combine(rafPath, "refs"));
        Directory.CreateDirectory(Path.Combine(rafPath, "refs", "heads"));
        Directory.CreateDirectory(Path.Combine(rafPath, "logs"));
        File.WriteAllText(Path.Combine(rafPath, "COMMIT_MESSAGE"), null);
        
        File.WriteAllText(Path.Combine(rafPath, "HEAD"), "ref: refs/heads/main");
        File.WriteAllText(Path.Combine(rafPath, "refs", "heads", "main"), string.Empty);
        
        File.WriteAllText(Path.Combine(rafPath, "index"), "");

        Console.WriteLine("Repositório inicializado com sucesso");
    }
}