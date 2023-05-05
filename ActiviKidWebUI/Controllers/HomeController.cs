using ActiviKidWebUI.Models;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.ContentModel;
using System.Diagnostics;
using System.Security.Claims;

namespace ActiviKidWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ActiviKidDbContext db;

        public HomeController(ILogger<HomeController> logger, ActiviKidDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(int id = 0, string? lang = null)
        {
            if(id != 0)
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

            var vm = new HomeViewModel();
            ViewBag.Lang = lang;
            if (lang == "az" || lang == null)
            {
                vm.Category = db.Categories.ToList();
                if (id > 0)
                {
                    ViewData["Id"] = id;
                }
            }
            else
            {
                vm.Category = db.CategoryRus.ToList();
            }
            return View(vm);
        }


        public IActionResult AddToCart(int? id, ProductViewModel vm, ProductDetailViewModel pdvm)
        {
            if (id == null)
            {
                id = pdvm.Basket.ProductId;
            }


            var basket = new Basket();

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
                string cookieValueFromReqqs = Request.Cookies["activiProduct"];
                cookieValueFromReq = cookieValueFromReqqs;
            }
            else
            {
                cookieValueFromReq = Request.Cookies["activiProduct"];
            }

            var userId = cookieValueFromReq;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
            }

            var basketrow = db.Baskets.FirstOrDefault(p => p.ProductId == id && p.UserId == userId);
            if (vm.Basket != null)
            {
                basket = vm.Basket;
                var basketrows = db.Baskets.FirstOrDefault(p => p.ProductId == vm.Basket.ProductId && p.UserId == userId);
                if (basketrows == null)
                {
                    basket.ProductId = (int)id;
                    basket.UserId = userId;
                    basket.ColorId = pdvm.Basket.ColorId;
                    if (pdvm.Basket.ColorId == 0)
                    {
                        basket.ColorId = null;
                    }
                    basket.Count = pdvm.Count;
                    db.Baskets.Add(basket);
                    db.SaveChanges();

                    if (pdvm.Basket != null)
                    {
                        if (pdvm.Basket.ProductId > 0)
                        {
                            return RedirectToAction("index");
                        }
                    }
   
                    return Json(new
                    {
                        error = false,
                        msg = "Siz bu product-i elave etdiniz"
                    });
                }
            }
            else
            {
                if (basketrow != null)
                {
                    if (pdvm.Basket != null)
                    {
                        if (pdvm.Basket.ProductId > 0)
                        {
                            return RedirectToAction("index");
                        }
                    }
                    return Json(new
                    {
                        error = true,
                        msg = "Siz bu product-i elave etmisiniz"
                    });
                }
                else
                {
                  
                    basket.ProductId = (int)id;
                    basket.UserId = userId;
                    basket.ColorId = pdvm.ColorId;
                    if (pdvm.ColorId == 0)
                    {
                        basket.ColorId = null;
                    }
                    basket.Count = pdvm.Count;

                    db.Baskets.Add(basket);
                    db.SaveChanges();


                    if (pdvm.Basket != null)
                    {
                        if (pdvm.Basket.ProductId > 0)
                        {
                            return RedirectToAction("index");
                        }
                    }
                    return Json(new
                    {
                        error = false,
                        msg = "Siz bu product-i elave etdiniz"
                    });
                }
            }

            return RedirectToAction("index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}