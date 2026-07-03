using StreamVault.Web.Models;
using StreamVault.Web.ViewModels;

namespace StreamVault.Web.Services;

    public interface ICatalogService
    {
        Task<IReadOnlyList<BaseProperties>> SearchAsync(string? search, ContentType? type);
        Task<BasePropertiesForm?> GetFormAsync(int id);
        Task<int> CreateAsync(BasePropertiesForm form);
        Task<bool> UpdateAsync(int id, BasePropertiesForm form);
        Task<BaseProperties?> FindAsync(int id);
        Task<bool> DeleteAsync(int id);
    }

