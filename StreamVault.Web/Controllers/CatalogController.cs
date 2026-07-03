using Microsoft.AspNetCore.Mvc;
using StreamVault.Web.Models;
using StreamVault.Web.Services;
using StreamVault.Web.ViewModels;
namespace StreamVaultAdmin.Web.Controllers;

public sealed class CatalogController(ICatalogService catalog) : Controller
{
    public async Task<IActionResult> Index(string? search, ContentType? type) =>
        View(new CatalogIndexViewModel(await catalog.SearchAsync(search, type), search?.Trim() ?? "", type));

    [HttpGet] public IActionResult Create() => View(new BasePropertiesForm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BasePropertiesForm form)
    {
        if (!ModelState.IsValid) return View(form);
        await catalog.CreateAsync(form);
        TempData["Success"] = $"{form.Title} was added.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var form = await catalog.GetFormAsync(id);
        return form is null ? NotFound() : View(form);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BasePropertiesForm form)
    {
        if (id != form.Id) return BadRequest();
        if (!ModelState.IsValid) return View(form);
        if (!await catalog.UpdateAsync(id, form)) return NotFound();
        TempData["Success"] = $"{form.Title} was updated.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await catalog.FindAsync(id);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await catalog.DeleteAsync(id)) return NotFound();
        TempData["Success"] = "The catalogue item was deleted.";
        return RedirectToAction(nameof(Index));
    }
}
