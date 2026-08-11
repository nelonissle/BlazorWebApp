using SaveUp.Models;

namespace SaveUp.ViewModels;

public sealed class SavingsListViewModel
{
    public static SavingsListViewModel Empty { get; } = new([], 0m);

    private SavingsListViewModel(IReadOnlyList<SaveUpEntry> entries, decimal totalAmount)
    {
        Entries = entries;
        TotalAmount = totalAmount;
    }

    public IReadOnlyList<SaveUpEntry> Entries { get; }
    public decimal TotalAmount { get; }

    public static SavingsListViewModel FromEntries(IReadOnlyList<SaveUpEntry> entries) =>
        new(entries.OrderByDescending(entry => entry.SkippedAt).ToList(), entries.Sum(entry => entry.Price));
}
