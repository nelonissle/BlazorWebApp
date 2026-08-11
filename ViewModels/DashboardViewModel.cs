using SaveUp.Models;

namespace SaveUp.ViewModels;

public sealed class DashboardViewModel
{
    public static DashboardViewModel Empty { get; } = new([], 0m, "Noch kein Eintrag");

    private DashboardViewModel(IReadOnlyList<SaveUpEntry> entries, decimal totalAmount, string lastSavedAtLabel)
    {
        Entries = entries;
        TotalAmount = totalAmount;
        LastSavedAtLabel = lastSavedAtLabel;
    }

    public IReadOnlyList<SaveUpEntry> Entries { get; }
    public int EntryCount => Entries.Count;
    public decimal TotalAmount { get; }
    public string LastSavedAtLabel { get; }

    public static DashboardViewModel FromEntries(IReadOnlyList<SaveUpEntry> entries)
    {
        var latestEntry = entries.OrderByDescending(entry => entry.SkippedAt).FirstOrDefault();
        return new DashboardViewModel(
            entries,
            entries.Sum(entry => entry.Price),
            latestEntry is null ? "Noch kein Eintrag" : latestEntry.SkippedAt.ToString("dd.MM.yyyy HH:mm"));
    }
}
