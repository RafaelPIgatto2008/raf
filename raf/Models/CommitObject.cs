namespace raf.Models;

public class CommitObject
{
    public string CommitHash { get; set; } = string.Empty;
    public string Tree { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public long Time { get; set; }
}
