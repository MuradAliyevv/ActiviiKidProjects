using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Components
{
    public class FilterViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public FilterViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int productId, string? lang)
        {
            ViewBag.Lang = lang;
            if (lang == null || lang == "az")
            {
                ViewBag.Category = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.Color = new SelectList(db.Color.ToList(), "Id", "Name");
            }
           else {
                ViewBag.Category = new SelectList(db.CategoryRus.ToList(), "Id", "Name");
                ViewBag.Color = new SelectList(db.ColorRus.ToList(), "Id", "Name");
            }
            return View();

        }

    }
}
