using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.FormModel;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ActiviKidWebUI.Controllers
{
    public class ShopController : Controller
    {
        readonly ActiviKidDbContext db;
        public ShopController(ActiviKidDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(FilterFormModel fm, string? lang, int id = 0)
        {
            if (id != 0)
            {
                ViewData["Id"] = id;
            }

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

            ViewBag.Lang = lang;
            ViewData["ColorId"] = fm.ColorId;
            ViewData["CategoryId"] = fm.CategoryId;
            ViewData["Min"] = fm.Min;
            ViewData["Max"] = fm.Max;
            ViewData["BestSeller"] = fm.BestSeller;
            ViewData["MostLiked"] = fm.MostLiked;
            ViewData["Top"] = fm.Top;
            ViewData["NewArrival"] = fm.NewArrival;
            return View();
        }
    }
}
