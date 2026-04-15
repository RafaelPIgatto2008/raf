using System;
using System.IO;

namespace raf.Commands;

public static class CleanCommand
{
    public static void Execute()
    {
        var rafPath = Path.Combine(Directory.GetCurrentDirectory(), ".raf");
        var objectsPath = Path.Combine(rafPath, "objects");

        if (!Directory.Exists(rafPath))
        {
            Console.WriteLine($"Não foi possivel encontrar o diretorio {rafPath}");
            return;
        }
        
        if (!Directory.Exists(objectsPath))
        {
            Console.WriteLine("Diretorio invalido, comando cancelado");
            return;
        }
        
        var indexContent = File.ReadAllLines(Path.Combine(rafPath, "index"));
        if (indexContent.Length > 0)
        {
            Console.WriteLine("Você tem arquivos alterados e salvos no index, deseja excluir o historico dos arquivos y/n ?");
            var imput = Console.ReadLine();

            if (imput?.ToLower() != "y")
            {
                Console.WriteLine("Cancelando a exclusão do diretorio");
                return;
            }
        }
        
        try
        {
            Directory.Delete(rafPath, true);
            Console.WriteLine($"Deleted raf directory {rafPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete raf directory {rafPath}: {ex.Message}");
        }
    }
}