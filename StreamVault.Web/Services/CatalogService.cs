using Microsoft.EntityFrameworkCore;
using StreamVault.Web.Data;
using StreamVault.Web.Models;
using StreamVault.Web.ViewModels;

namespace StreamVault.Web.Services;

    public sealed class CatalogService(CatalogDbContext db) : ICatalogService
    {
        public async Task<IReadOnlyList<BaseProperties>> SearchAsync(string? search, ContentType? type)
        {
            IQueryable<BaseProperties> query = db.ContentItems.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(item => item.Title.Contains(term));
            }
            if (type is not null) query = FilterByType(query, type.Value);
            return await query.OrderBy(item => item.Title).ToListAsync();
        }

        public async Task<BasePropertiesForm?> GetFormAsync(int id)
        {
            var item = await db.ContentItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return item is null ? null : MapToForm(item);
        }

        public async Task<int> CreateAsync(BasePropertiesForm form)
        {
            var item = CreateItem(form.Type);
            item.UpdateCommonFields(form);
            item.UpdateSpecificFields(form);
            db.Add(item);
            await db.SaveChangesAsync();
            return item.Id;
        }

        public async Task<bool> UpdateAsync(int id, BasePropertiesForm form)
        {
            var item = await db.ContentItems.SingleOrDefaultAsync(x => x.Id == id);
            if (item is null || item.Type != form.Type) return false;
            item.UpdateCommonFields(form);
            item.UpdateSpecificFields(form);
            await db.SaveChangesAsync();
            return true;
        }

        public Task<BaseProperties?> FindAsync(int id) =>
            db.ContentItems.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await db.ContentItems.FindAsync(id);
            if (item is null) return false;
            db.Remove(item);
            await db.SaveChangesAsync();
            return true;
        }

        private static BaseProperties CreateItem(ContentType type) => type switch
        {
            ContentType.Movie => new Movie(),
            ContentType.Series => new Series(),
            ContentType.AudioBook => new AudioBook(),
            ContentType.MusicAlbum => new MusicAlbum(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        private static IQueryable<BaseProperties> FilterByType(IQueryable<BaseProperties> query, ContentType type) => type switch
        {
            ContentType.Movie => query.OfType<Movie>(),
            ContentType.Series => query.OfType<Series>(),
            ContentType.AudioBook => query.OfType<AudioBook>(),
            ContentType.MusicAlbum => query.OfType<MusicAlbum>(),
            _ => query
        };

        private static BasePropertiesForm MapToForm(BaseProperties item)
        {
            var form = new BasePropertiesForm
            {
                Id = item.Id,
                Type = item.Type,
                Title = item.Title,
                Description = item.Description,
                ReleaseDate = item.ReleaseDate,
                AgeRating = item.AgeRating,
                Genre = item.Genre
            };
            switch (item)
            {
                case Movie x: form.DurationMinutes = x.DurationMinutes; form.Director = x.Director; break;
                case Series x: form.NumberOfSeasons = x.NumberOfSeasons; form.TotalEpisodes = x.TotalEpisodes; break;
                case AudioBook x: form.Author = x.Author; form.Narrator = x.Narrator; form.DurationMinutes = x.DurationMinutes; break;
                case MusicAlbum x: form.Artist = x.Artist; form.TrackCount = x.TrackCount; form.RecordLabel = x.RecordLabel; break;
            }
            return form;
        }
    }


