using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActiviKidWebUI.Models.DataContext;

namespace ActiviKidWebUI.Controllers
{
    public class OrderController : Controller
    {
        readonly ActiviKidDbContext db;

        public OrderController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            var order = db.Orders.Include(p=>p.Product).Where(p=>p.UserId == id).ToList();

            return View(order);
        }
    }
}
