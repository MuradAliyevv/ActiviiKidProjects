using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public HeaderViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(string lang, string? order)
        {
            var vm = new FooterViewModel();
            vm.SiteInfo = db.SiteInfos.FirstOrDefault();
            ViewBag.Order = order;
            ViewBag.Lang = lang;

            return View(vm);

        }
    }
}
