using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ActiviKidWebUI.Controllers
{
    public class NewsController : Controller
    {
        readonly ActiviKidDbContext db;
        public NewsController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string? lang)
        {
            string cookieValueFromReqq = Request.Cookies["activiProduct"];

            var values = cookieValueFromReqq;
            string cookieValueFromReq = "";
            if (values == null)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(2000);
                cookieOptions.Path = "/";
                var vauesss = Guid.NewGuid().ToString();
                Response.Cookies.Append("activiProduct", $"{vauesss}", cookieOptions);
            }
            else
            {
                cookieValueFromReq = Request.Cookies["activiProduct"];
            }


            var value = cookieValueFromReq;
            var count = 0;
            var order = new Orders();

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                count = count + db.Baskets.Where(p => p.UserId == user).ToList().Count();
                order = db.Orders.FirstOrDefault(p => p.UserId == user);
            }
            if (db.Orders.FirstOrDefault(p => p.UserId == value) != null)
            {
                order = db.Orders.FirstOrDefault(p => p.UserId == value);
            }

            count = count + db.Baskets.Where(p => p.UserId == value.ToString()).ToList().Count();

            var countt = 0;
            if (count > 0)
            {
                countt = count;
            }


            ViewBag.Count = countt;
            ViewBag.Order = null;
            if (order != null)
            {
                ViewBag.Order = order.UserId;
            }


            ViewBag.Lang = lang;


            var vm = new NewsViewModel();
            if (lang == null || lang == "az")
            {
                vm.News = db.News.ToList();
            }
            else
            {
                vm.News = db.NewsRus.ToList();
            }
            return View(vm);
        }
    }
}
