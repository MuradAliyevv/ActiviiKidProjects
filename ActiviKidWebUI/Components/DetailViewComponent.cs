using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Claims;

namespace ActiviKidWebUI.Components
{
    public class DetailViewComponent : ViewComponent
    {
        readonly ActiviKidDbContext db;
        public DetailViewComponent(ActiviKidDbContext db)
        {
            this.db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int productId, string? lang)
        {
            var vm = new ProductDetailViewModel();
            var colors = new List<dynamic>();

            if (lang == null || lang == "az")
            {
                

                vm.Product = db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == productId);

                vm.ProductColorSizeCount = db.ProductColorSizeCount.Where(p => p.ProductId == productId).ToList();
                foreach (var item in vm.ProductColorSizeCount.Where(p => p.ProductId == productId))
                {
                    foreach (var itemm in db.Color)
                    {
                        if (item.ColorId == itemm.Id)
                        {
                            colors.Add(itemm);
                        }

                    }

                }
                vm.Color = colors;
            }
            else{
                

                vm.Product = db.ProductRus.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == productId);

                vm.ProductColorSizeCount = db.ProductColorSizeCountRus.Where(p => p.ProductId == productId).ToList();
                foreach (var item in vm.ProductColorSizeCount.Where(p => p.ProductId == productId))
                {
                    foreach (var itemm in db.ColorRus)
                    {
                        if (item.ColorId == itemm.Id)
                        {
                            colors.Add(itemm);
                        }

                    }

                }
                vm.Color = colors;
            }

            return View(vm);

        }

    }
}
