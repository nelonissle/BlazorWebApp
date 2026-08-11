using System.ComponentModel.DataAnnotations;
using SaveUp.Models;

namespace SaveUp.ViewModels;

public sealed class EntryFormViewModel
{
    [Required(ErrorMessage = "Bitte eine Kurzbeschreibung eingeben.")]
    [StringLength(80, ErrorMessage = "Die Kurzbeschreibung darf maximal 80 Zeichen enthalten.")]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.05", "9999", ErrorMessage = "Bitte einen gültigen Preis eingeben.")]
    public decimal? Price { get; set; }

    [Required(ErrorMessage = "Bitte Datum und Uhrzeit auswählen.")]
    public DateTime? SkippedAt { get; set; } = DateTime.Now;

    public SaveUpEntry ToEntry() => new()
    {
        Description = Description.Trim(),
        Price = Price ?? 0m,
        SkippedAt = SkippedAt ?? DateTime.Now
    };

    public void Reset()
    {
        Description = string.Empty;
        Price = null;
        SkippedAt = DateTime.Now;
    }
}
