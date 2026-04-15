using System;
using System.IO;
using System.Linq;
using raf.Utils;

namespace raf.Core;

public static class TreeService
{
    public static string CreateTree()
    {
        var indexPath = Path.Combine(".raf", "index");
        
        if (!File.Exists(indexPath))
            throw new Exception("Index não encontrado");

        var lines = File.ReadAllLines(indexPath);

        var treeLines = lines.Select(line =>
        {
            var parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
                throw new Exception($"Linha inválida no index: '{line}'");
            
            var fileName = parts[0];
            var hashFile = parts[1];

            return $"pig {hashFile} {fileName}";
        });
        
        var treeContent = string.Join("\n", treeLines);
        
        var hash = Hash.ComputeRawHash("tree", treeContent);

        Save.SaveRawObject(hash, "tree", treeContent);

        return hash;
    }
}