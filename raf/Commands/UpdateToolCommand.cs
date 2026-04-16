using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace raf.Commands;

public static class UpdateToolCommand
{
    public static void Execute()
    {
        Console.WriteLine("Atualizando a ferramenta raf...");

        var projectPath = FindProjectPath();

        if (projectPath == null)
        {
            Console.WriteLine("Projeto não encontrado");
            return;
        }

        IncrementVersion(projectPath);

        GenerateAndRunScript(projectPath);
    }
    
    private static string FindProjectPath()
    {
        var dir = Directory.GetCurrentDirectory();

        var csproj = Directory.GetFiles(dir, "*.csproj").FirstOrDefault();

        return csproj;
    }
    
    private static void IncrementVersion(string csprojPath)
    {
        var content = File.ReadAllText(csprojPath);

        var match = Regex.Match(content, @"<Version>(.*?)</Version>");

        if (!match.Success)
        {
            Console.WriteLine("Versão não encontrada");
            return;
        }

        var version = match.Groups[1].Value;
        var parts = version.Split('.').Select(int.Parse).ToArray();

        parts[2]++;

        var newVersion = $"{parts[0]}.{parts[1]}.{parts[2]}";

        content = content.Replace(
            $"<Version>{version}</Version>",
            $"<Version>{newVersion}</Version>"
        );

        File.WriteAllText(csprojPath, content);

        Console.WriteLine($"Versão atualizada: {newVersion}");
    }
    
    private static void GenerateAndRunScript(string projectPath)
    {
        var scriptPath = Path.Combine(Path.GetTempPath(), "raf-update.bat");

        var script = $@"
            @echo off
            echo Buildando...
            cd raf
            dotnet clean
            dotnet tool uninstall -g raf

            echo Atualizando tool...
            dotnet tool install --global --add-source ./nupkg raf

            echo Atualizado
            pause
            ";
        
        File.WriteAllText(scriptPath, script);

        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c start cmd /k \"{scriptPath}\"",
            UseShellExecute = true
        };

        Process.Start(psi);
    }
}