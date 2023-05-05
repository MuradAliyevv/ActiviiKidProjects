using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public CategoryViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(bool header = false, string? lang = "az")
        {
            var vm = new HomeViewModel();
            if (lang == null || lang == "az")
            {
                 vm.Category = db.Categories.ToList();
            }
            else
            {
                vm.Category = db.CategoryRus.ToList();
            }
            if (header == true)
            {
                ViewBag.header = header;
            }
            return View(vm);

        }
    }
}

