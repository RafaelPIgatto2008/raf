using System.IO;

namespace raf.Core;

public static class HeadService
{
    public static void Update(string commitHash, string message)
    {
        var headPath = Path.Combine(".raf", "HEAD");
        var headContent = File.ReadAllText(headPath).Trim();

        if (headContent.StartsWith("ref: "))
        {
            var referencePath = headContent["ref: ".Length..].Trim();
            var fullReferencePath = Path.Combine(".raf", referencePath);
            var referenceDirectory = Path.GetDirectoryName(fullReferencePath);

            if (!string.IsNullOrEmpty(referenceDirectory))
                Directory.CreateDirectory(referenceDirectory);

            File.WriteAllText(fullReferencePath, message);
            return;
        }

        File.WriteAllText(headPath, commitHash);
    }
}
