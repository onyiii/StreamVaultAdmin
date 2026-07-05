using StreamVault.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace StreamVault.Web.ViewModels;


   public class BasePropertiesForm : IValidatableObject
    {
        public int? Id { get; set; }
        [Display(Name = "Content type")] public ContentType Type { get; set; }
        [Required, StringLength(160)] public string Title { get; set; } = "";
        [Required, StringLength(2000)] public string Description { get; set; } = "";
        [Display(Name = "Release date"), DataType(DataType.Date)] public DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        [Display(Name = "Age rating")] public AgeRating AgeRating { get; set; }
        [Required, StringLength(80)] public string Genre { get; set; } = "";
        [Display(Name = "Duration (minutes)")] public int? DurationMinutes { get; set; }
        [StringLength(120)] public string? Director { get; set; }
        [Display(Name = "Number of seasons")] public int? NumberOfSeasons { get; set; }
        [Display(Name = "Total episodes")] public int? TotalEpisodes { get; set; }
        [StringLength(120)] public string? Author { get; set; }
        [StringLength(120)] public string? Narrator { get; set; }
        [StringLength(120)] public string? Artist { get; set; }
        [Display(Name = "Track count")] public int? TrackCount { get; set; }
        [Display(Name = "Record label"), StringLength(120)] public string? RecordLabel { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext _) => Type switch
        {
            ContentType.Movie => MovieErrors(),
            ContentType.Series => SeriesErrors(),
            ContentType.AudioBook => AudiobookErrors(),
            ContentType.MusicAlbum => AlbumErrors(),
            _ => []
        };

        private IEnumerable<ValidationResult> MovieErrors()
        {
            if (DurationMinutes is null or <= 0) yield return Error("Duration must be positive.", nameof(DurationMinutes));
            if (string.IsNullOrWhiteSpace(Director)) yield return Error("Director is required.", nameof(Director));
        }
        private IEnumerable<ValidationResult> SeriesErrors()
        {
            if (NumberOfSeasons is null or <= 0) yield return Error("Number of seasons must be positive.", nameof(NumberOfSeasons));
            if (TotalEpisodes is null or <= 0) yield return Error("Total episodes must be positive.", nameof(TotalEpisodes));
        }
        private IEnumerable<ValidationResult> AudiobookErrors()
        {
            if (string.IsNullOrWhiteSpace(Author)) yield return Error("Author is required.", nameof(Author));
            if (string.IsNullOrWhiteSpace(Narrator)) yield return Error("Narrator is required.", nameof(Narrator));
            if (DurationMinutes is null or <= 0) yield return Error("Duration must be positive.", nameof(DurationMinutes));
        }
        private IEnumerable<ValidationResult> AlbumErrors()
        {
            if (string.IsNullOrWhiteSpace(Artist)) yield return Error("Artist is required.", nameof(Artist));
            if (TrackCount is null or <= 0) yield return Error("Track count must be positive.", nameof(TrackCount));
            if (string.IsNullOrWhiteSpace(RecordLabel)) yield return Error("Record label is required.", nameof(RecordLabel));
        }
        private static ValidationResult Error(string message, string member) => new(message, [member]);
    }


