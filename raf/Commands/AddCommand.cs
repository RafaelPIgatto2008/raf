using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using raf.Utils;
using static raf.Utils.Save;


namespace raf.Commands;

public class AddCommand
{
    public static void Add(string target)
    {
        if (string.IsNullOrEmpty(target))
        {
            Console.WriteLine("Informe um arquivo ou '.'");
            return;
        }

        if (target == ".")
        {
            AddAllFiles();
            return;
        }

        if (!File.Exists(target))
        {
            Console.WriteLine("Arquivo não encontrado.");
            return;
        }

        AddSingleFile(target);
    }
    
    public static void AddSingleFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            Console.WriteLine("Arquivo não encontrado.");
            return;
        }

        var rafPath = Path.Combine(Directory.GetCurrentDirectory(), ".raf");
        var indexPath = Path.Combine(rafPath, "index");
        
        var hash = Hash.ComputeHash(filePath);
        
        // Save the objects
        SaveObject(hash, filePath);

        // Read existent index
        var indexEntries = new Dictionary<string, string>();

        if (File.Exists(indexPath))
        {
            var lines = File.ReadAllLines(indexPath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2)
                    indexEntries[parts[0]] = parts[1];
            }
        }

        var fileName = Path.GetFileName(filePath);

        // Add or refresh
        indexEntries[fileName] = hash;

        // Save index
        var newLines = indexEntries
            .Select(kvp => $"{kvp.Key}|{kvp.Value}")
            .ToArray();

        File.WriteAllLines(indexPath, newLines);

        Console.WriteLine($"Adicionado: {fileName}");
    }
    
    public static void AddAllFiles()
    {
        var currentDir = Directory.GetCurrentDirectory();

        var files = Directory.GetFiles(currentDir)
            .Where(f => !f.Contains(".raf"))
            .ToList();

        foreach (var file in files)
        {
            AddSingleFile(file);
        }

        Console.WriteLine("Todos os arquivos adicionados");
    }
}