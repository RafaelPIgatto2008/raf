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
            Console.WriteLine("Repositório já inicializado.");
            return;
        }

        Directory.CreateDirectory(rafPath);
        Directory.CreateDirectory(Path.Combine(rafPath, "objects"));
        Directory.CreateDirectory(Path.Combine(rafPath, "refs"));

        File.WriteAllText(Path.Combine(rafPath, "HEAD"), "File generate by Rafael Pigatto" + "ref: refs/heads/main");
        
        File.WriteAllText(Path.Combine(rafPath, "index"), "");

        Console.WriteLine("Repositório inicializado com sucesso");
    }
}