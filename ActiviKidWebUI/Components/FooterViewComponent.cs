using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Components
{
    public class FooterViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public FooterViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string? lang)
        {
            var vm = new FooterViewModel();
            if (lang == null || lang == "az")
            {
                vm.SiteInfo = db.SiteInfos.FirstOrDefault();
                vm.Services = db.Services.ToList();
            }
           else {
                vm.SiteInfo = db.SiteInfos.FirstOrDefault();
                vm.Services = db.ServicesRus.ToList();
            }
            return View(vm);

        }
    }
}
