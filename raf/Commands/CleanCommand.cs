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
            Console.WriteLine($"Could not find directory {rafPath}");
            return;
        }
        
        if (!Directory.Exists(objectsPath))
        {
            Console.WriteLine("Invalid directory, command canceled");
            return;
        }
        
        var indexContent = File.ReadAllLines(Path.Combine(rafPath, "index"));
        if (indexContent.Length > 0)
        {
            Console.WriteLine("You have changed files saved in the index. Do you want to delete the file history y/n?");
            var imput = Console.ReadLine();

            if (imput?.ToLower() != "y")
            {
                Console.WriteLine("Canceling directory deletion");
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
