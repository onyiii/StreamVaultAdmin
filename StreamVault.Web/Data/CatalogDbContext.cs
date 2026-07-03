using Microsoft.EntityFrameworkCore;
using StreamVault.Web.Models;

namespace StreamVault.Web.Data;

    public sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
    {
        public DbSet<BaseProperties> baseProperties => Set<BaseProperties>();
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<BaseProperties>().HasDiscriminator<string>("ContentType")
                .HasValue<Movie>(nameof(ContentType.Movie)).HasValue<Series>(nameof(ContentType.Series))
                .HasValue<AudioBook>(nameof(ContentType.AudioBook)).HasValue<MusicAlbum>(nameof(ContentType.MusicAlbum));
    }


