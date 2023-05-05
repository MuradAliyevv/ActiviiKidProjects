using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ActiviKidWebUI.Controllers
{
    public class BasketController : Controller
    {

        readonly ActiviKidDbContext db;


        public BasketController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string? id)
        {

            var vm = new CheckFormModel();
            var products = new List<Product>();
            var product = new Product();
            var basket = new List<Basket>();
            var baskets = new List<Basket>();
            string cookieValueFromReqq = Request.Cookies["activiProduct"];
            var values = cookieValueFromReqq;


            if (values == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    basket = await db.Baskets.Where(p => p.UserId == userId).ToListAsync();
                    if (basket.Count() > 0)
                    {

                        baskets.AddRange(basket);
                        foreach (var item in baskets)
                        {
                            product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                            products.Add(product);
                        }
                    }

                }

            }
            else
            {
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                    basket = await db.Baskets.Where(p => p.UserId == userId).ToListAsync();
                    if (basket.Count() > 0)
                    {
                        baskets.AddRange(basket);
                        foreach (var item in baskets)
                        {
                            product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                            products.Add(product);
                        }
                    }

                }
                basket = db.Baskets.Where(p => p.UserId == values).ToList();
                if (basket.Count() > 0)
                {

                    baskets.AddRange(basket);
                    foreach (var item in baskets.Where(p => p.UserId != userId))
                    {
                        product = await db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefaultAsync(p => p.Id == item.ProductId);
                        products.Add(product);
                    }
                }


            }

            vm.Products = products;
            vm.Baskets = baskets;
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Add(CheckFormModel fm, Order order)
        {
            var products = new List<Product>();
            order = fm.Order;
            db.Order.Add(order);
            db.SaveChanges();
            foreach (var item in fm.Baskets)
            {
                var orders = new Orders();
                var product = db.Products.Include(p=>p.ProductColorSizeCount).FirstOrDefault(p => p.Id == item.ProductId);
                if (item.ColorId == null || item.ColorId > 0)
                {
                    item.ColorId = db.ProductColorSizeCount.FirstOrDefault(p => p.ProductId == product.Id).ColorId;
                }

                var productclorsizecount = db.ProductColorSizeCount.FirstOrDefault(p => p.ColorId == item.ColorId);
                var color = db.Color.FirstOrDefault(p => p.Id == item.ColorId);

                orders.ProductId = product.Id;
                orders.Color = color.Id;
                if (item.Count == 0 || item.Count == null)
                {
                    item.Count = 1;
                }
                orders.Count = (int)item.Count;

                orders.Price = product.Price;

                orders.DisCountPrice = (product.Price - ((product.Price * product.Discount) / 100));

                orders.Total = (decimal)orders.DisCountPrice;
                orders.OrderNumber = item.UserId;
                orders.OrderId = order.Id;
                productclorsizecount.Count = (int)(productclorsizecount.Count - item.Count);
                product.Count = product.Count - item.Count;
                db.Products.Update(product);
                db.ProductColorSizeCount.Update(productclorsizecount);

                if (User.Identity.IsAuthenticated)
                {
                    orders.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();
                }
                else
                {
                    orders.UserId = item.UserId;
                }
                db.Orders.Add(orders);

                var basketss = db.Baskets.FirstOrDefault(b => b.Id == item.Id);

                db.Baskets.Remove(basketss);

                db.SaveChanges();
            }

            ViewBag.Orders = "My Orders menu has been added to the menu section, from where you can track your order";

            ModelState.Clear();
            return RedirectToAction("index", "home");

        }
    }
}
