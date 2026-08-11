namespace SaveUp.Models;

public sealed class SaveUpEntry
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public DateTime SkippedAt { get; init; }
}
