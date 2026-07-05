using StreamVault.Web.Models;

namespace StreamVault.Web.ViewModels;

    public record CatalogIndexViewModel(IReadOnlyList<BaseProperties> Items, string Search, ContentType? Type);


