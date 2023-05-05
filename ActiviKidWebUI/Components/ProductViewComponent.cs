using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActiviKidWebUI.Components
{
    public class ProductViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public ProductViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int category = 0, bool bestseller = false,
            bool newarrival = false, bool mostliked = false, bool top = false, bool randoms = false,
            int colorId = 0, int min = 0, int max = 0, string lang = "az")
        {
            var vm = new ProductViewModel();
            if (lang == null || lang == "az")
            {
                vm.Products = db.Products.Include(p => p.ProductColorSizeCount).Include(p => p.Category).Where(p => p.DeletedDate == null).ToList();
                vm.ProductColorSizeCounts = db.ProductColorSizeCount.ToList();
            }
            else
            {
                vm.Products = db.ProductRus.Include(p => p.ProductColorSizeCount).Include(p => p.Category).ToList();
                vm.ProductColorSizeCounts = db.ProductColorSizeCountRus.ToList();
            }
                
                if(category > 0)
            {
                vm.Products = vm.Products.Where(p => p.CategoryId == category).ToList();
            }
            if (top == true)
            {
                vm.Products = vm.Products.Where(p => p.Top == true).ToList();
            }
            if (bestseller)
            {
                ViewBag.bestseller = "best-seller";
                ViewBag.Class = "best-seller";
                vm.Products = vm.Products.Where(p => p.BestSeller == true).ToList();
            }
            if (newarrival)
            {
                ViewBag.newarrival = "new";
                ViewBag.Class = "new";
                vm.Products = vm.Products.Where(p => p.NewArrival == true).ToList();
            }
            if (mostliked)
            {
                ViewBag.mostliked = "most-liked";
                ViewBag.Class = "most-liked";
                vm.Products = vm.Products.Where(p => p.MostLiked == true).ToList();
            }
            if (colorId > 0)
            {
                vm.ProductColorSizeCounts = vm.ProductColorSizeCounts.Where(p=> p.ColorId == colorId).ToList();
            }
            if (min > 0)
            {
                vm.Products = vm.Products.Where(p => p.Price > min).ToList();
            }
            if (max > 0)
            {
                vm.Products = vm.Products.Where(p => p.Price < max).ToList();
            }
            if (randoms == true)
            {
                ViewBag.Random = "random";
                ViewBag.Class = "random";
            }

            if (vm.Products.Count() == 0 || vm.Products == null)
            {
                ViewBag.Notfound = "The product you are looking for is currently unavailable";
            }
            if (vm.ProductColorSizeCounts.Count() == 0 || vm.ProductColorSizeCounts == null)
            {
                ViewBag.Notfound = "The product you are looking for is currently unavailable";
            }
            return View(vm);

        }
    }
}
