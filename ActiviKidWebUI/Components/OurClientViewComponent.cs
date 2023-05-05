using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Components
{
    public class OurClientViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public OurClientViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = db.OurClients.ToList();
            return View(client);

        }
    }
}
