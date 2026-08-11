using System.Text.Json;
using SaveUp.Models;

namespace SaveUp.Services;

public sealed class SaveUpRepository
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly string _filePath;

    public SaveUpRepository(IWebHostEnvironment environment)
    {
        var dataDirectory = Path.Combine(environment.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDirectory);
        _filePath = Path.Combine(dataDirectory, "saveup-entries.json");
    }

    public async Task<IReadOnlyList<SaveUpEntry>> GetEntriesAsync()
    {
        await _lock.WaitAsync();

        try
        {
            return (await ReadEntriesUnsafeAsync())
                .OrderByDescending(entry => entry.SkippedAt)
                .ToList();
        }
        finally
        {
            _lock.Release();
        }
    }

    public async Task AddEntryAsync(SaveUpEntry entry)
    {
        await _lock.WaitAsync();

        try
        {
            var entries = await ReadEntriesUnsafeAsync();
            entries.Add(entry);
            await using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, entries, SerializerOptions);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<List<SaveUpEntry>> ReadEntriesUnsafeAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        await using var stream = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<List<SaveUpEntry>>(stream, SerializerOptions) ?? [];
    }
}
