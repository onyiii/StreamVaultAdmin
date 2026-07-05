using StreamVault.Web.Models;
using Microsoft.EntityFrameworkCore;
namespace StreamVault.Web.Data;

   
        public static class DatabaseInitializer
        {
            public static async Task InitializeAsync(IServiceProvider services)
            {
                await using var scope = services.CreateAsyncScope();
                var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        
        await db.Database.EnsureCreatedAsync();
                if (await db.ContentItems.AnyAsync()) return;
                db.AddRange(
                    new Movie { Title = "Off Campus", Description = "Love dynamics of a group of Briar University students.", ReleaseDate = new(2024, 5, 17), AgeRating = AgeRating.Teen, Genre = "Science Fiction", DurationMinutes = 128, Director = "Maya Chen" },
                    new Series { Title = "Big Bang Theory", Description = "A balance of science, humor and friendship", ReleaseDate = new(2023, 9, 8), AgeRating = AgeRating.ParentalGuidance, Genre = "Drama", NumberOfSeasons = 3, TotalEpisodes = 24 },
                    new AudioBook { Title = "The Global Tech talent", Description = "How to become a global asset in any area in Technology.", ReleaseDate = new(2025, 2, 11), AgeRating = AgeRating.Everyone, Genre = "Technology", Author = "Nora Bell", Narrator = "David Okoro", DurationMinutes = 540 },
                    new MusicAlbum { Title = "The life of a show girl", Description = "Life after Eras tour", ReleaseDate = new(2025, 6, 20), AgeRating = AgeRating.Everyone, Genre = "Afro-electronic", Artist = "Kemi Vale", TrackCount = 11, RecordLabel = "Night Current" });
                await db.SaveChangesAsync();
            }
        }
    

