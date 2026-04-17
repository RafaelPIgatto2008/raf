using System;
using System.IO;
using System.Linq;
using raf.Models;

namespace raf.Commands;

public static class ListCommand
{
    public static void Execute()
    {
        Console.WriteLine("Commands:");
        Command.AllCommands();
        Console.WriteLine("=======================================");
    }
}
