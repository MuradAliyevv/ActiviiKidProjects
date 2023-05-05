using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ActiviKidWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class OrdersController : Controller
    {
        readonly ActiviKidDbContext db;

        public OrdersController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var order = db.Orders.Include(p => p.Order).Include(p => p.Product).Where(p => p.DeletedDate == null).ToList();

            return View(order);
        }

        public IActionResult Approve(int id)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == id);
            order.approved = true;
            db.Orders.Update(order);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Detail(int id)
        {
            var vm = new OrderViewModel();

            vm.Orders = db.Orders.Include(p => p.Order).Include(p => p.Product).FirstOrDefault(p => p.Id == id);
            vm.Color = db.Color.ToList();
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == id);

            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
