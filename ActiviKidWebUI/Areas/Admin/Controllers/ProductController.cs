using ActiviKidWebUI.Helper;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.FormModel;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace ActiviKidWebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ProductController : Controller
    {
        readonly ActiviKidDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public ProductController(ActiviKidDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index(int pageIndex = 1, int pageSize = 10)
        {

            var products = db.Products.Where(p => p.DeletedDate == null).Include(p => p.Category);
            var pagedModel = new PagedViewModel<Product>(products, pageIndex, pageSize, null);
            return View(pagedModel);
        }

        public IActionResult Add()
        {

            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Obsolete]
        public IActionResult Add(ProductFormModel pfm, ProductRus productRus, Product product)
        {
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            product = pfm.Product;
            productRus.GenderId= product.GenderId;
            productRus.MostLiked= product.MostLiked;
            productRus.ConstantCount= product.ConstantCount;
            productRus.Discount=product.Discount;
            productRus.Count= product.Count;
            productRus.CategoryId= product.CategoryId;
            productRus.BestSeller= product.BestSeller;
            productRus.CategoryId = product.CategoryId;
            productRus.Name= pfm.ProductRus.Name;
            productRus.Description = pfm.ProductRus.Description;
            productRus.Price= product.Price;
            try
            {


                product.NewArrival = true;

                db.Products.Add(product);
                db.SaveChanges();
                productRus.Id = product.Id;
                db.ProductRus.Add(productRus);
                db.SaveChanges();

                ViewBag.Error = "Product successfully added";


                return RedirectToAction("Index","Product");
            }
            catch (Exception)
            {

                return View(pfm);
                throw;
            }

        }

        public IActionResult Update(int id)
        {
            var pfm = new ProductFormModel();
            pfm.Product = db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == id);
            pfm.ProductRus = db.ProductRus.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == id);
            var sizcolor = db.ProductColorSizeCount.Include(p => p.Color).Include(p => p.Product).Where(p => p.ProductId == id).ToList();
            if (pfm.Product != null)
            {
                ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
                ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
                return View(pfm);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public IActionResult Detail(int id)
        {
            var pfm = new ProductFormModel();
            pfm.Product = db.Products.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == id);
            pfm.ProductRus = db.ProductRus.Include(p => p.ProductColorSizeCount).FirstOrDefault(p => p.Id == id);
            var sizcolor = db.ProductColorSizeCount.Include(p => p.Color).Include(p => p.Product).Where(p => p.ProductId == id).ToList();
            if (pfm.Product != null)
            {
                ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
                ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
                return View(pfm);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Update(ProductFormModel pfm, ProductRus productRus, Product product)
        {
            product = pfm.Product;
            productRus.GenderId = product.GenderId;
            productRus.MostLiked = product.MostLiked;
            productRus.ConstantCount = product.ConstantCount;
            productRus.Discount = product.Discount;
            productRus.Count = product.Count;
            productRus.CategoryId = product.CategoryId;
            productRus.BestSeller = product.BestSeller;
            productRus.CategoryId = product.CategoryId;
            productRus.Name = pfm.ProductRus.Name;
            productRus.Description = pfm.ProductRus.Description;
            productRus.Price = product.Price;
            ViewBag.Category = new SelectList(db.Categories.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Gender = new SelectList(db.Genders.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");




       
            product.UpdatedDate = DateTime.Now;
            productRus.Id= product.Id;
            db.Update(product);
            db.Update(productRus);
            db.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult AddProductDetail(int id)
        {
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult AddProductDetail(ProductColorSizeCount colorSizeCount, ProductColorSizeCountRus colorSizeCountRus, IFormFile file)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);
            var productRus = db.ProductRus.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);

            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            ViewBag.Id = product.Id;

            if (product.Count == null)
            {
                product.Count = 0;
                productRus.Count = 0;
            }

            if (product.ConstantCount == null)
            {
                product.ConstantCount = 0;
                productRus.ConstantCount = 0;
            }

            product.Count = product.Count + colorSizeCount.Count;
            product.ConstantCount = product.ConstantCount + colorSizeCount.Count;
            productRus.Count = product.Count;
            productRus.ConstantCount = product.ConstantCount;
            if (colorSizeCount.ColorId == null || colorSizeCount.Count == 0 || colorSizeCount.Count == null)
            {
                ViewBag.Error = "Please enter the information completely";
                return RedirectToAction("update", new { id = product.Id });
            }
            var sizecolorcount = db.ProductColorSizeCount.FirstOrDefault(p => p.ColorId == colorSizeCount.ColorId && p.ProductId == colorSizeCount.ProductId);
            var sizecolorcountRus = db.ProductColorSizeCountRus.FirstOrDefault(p => p.ColorId == colorSizeCount.ColorId && p.ProductId == colorSizeCount.ProductId);
            if (sizecolorcount == null)
            {
                if(file != null)
                {
                    colorSizeCount.ImagePath = Image.Add(file, env);
                    colorSizeCountRus.ImagePath = colorSizeCount.ImagePath;
                }
                db.ProductColorSizeCount.Add(colorSizeCount);
               


                db.SaveChanges();

                colorSizeCountRus.Id = colorSizeCount.Id;
                colorSizeCountRus.ColorId = colorSizeCount.ColorId;
                colorSizeCountRus.Count = colorSizeCount.Count;
                
                db.ProductColorSizeCountRus.Add(colorSizeCountRus);
       


                product.NewArrival = true;


                db.Products.Update(product);
                db.ProductRus.Update(productRus);
                db.SaveChanges();
                ViewBag.Error = "Product successfully added";





                var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var displayName = configuration["Gmail:DisplayName"];

                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                if (db.Newsletter.FirstOrDefault() != null)
                {
                    string DomainName = HttpContext.Request.Host.Value;
                    foreach (var item in db.Newsletter.Where(p => p.DeletedDate == null).ToList())
                    {
                        MailMessage mail = new MailMessage(new MailAddress(username, displayName), new MailAddress(item.Email));
                        mail.Subject = displayName;



                        mail.Body = $@"    <table style=""padding:0.1%; border: 1px solid rgba(0, 0, 0, 0.11);"">
        <tbody "">
            <tr>
                <td colspan=""3"">
                    <a href=""
                        {DomainName}/blogsmore/#product-{product.Id}"">
                        <img width=""400"" height=""400"" src=""http://creatisite.com/assets/img/{colorSizeCount.ImagePath}"" alt=""{product.ImagePath}"">
                    </a>
                    
                </td>
            </tr>
            <tr style=""display: flex; justify-content: space-around; align-items: center; background-color:rgb(245, 247, 246); padding: 0 3%;"">
                <td>
                 
                    <a href=""{DomainName}/shop/product-{product.Id}"" style=""text-decoration: none; color: black;"">
                        <h3>
                            Go To Product
                        </h3>

                    </a>
                    
                </td>

                <td style=""margin: 0 auto;"">
                   <a href=""http://creatisite.com/"" style=""text-decoration: none; color: black;"">
                    <h3>
                        From: {displayName}
                      </h3>
                   </a>

                </td>
                <td style=""margin: 0 auto;"">
                   <a href=""http://creatisite.com/subscribe/unsubs/{item.Id}"" style=""text-decoration: none; color: rgba(0, 0, 0, 0.81);"">
                    <h3>
                        Unsubscribe
                      </h3>
                   </a>

                </td>
            </tr>
        </tbody>
    </table>";


                        mail.IsBodyHtml = true;



                        SmtpClient smtpClient = new SmtpClient(host, port);
                        smtpClient.Credentials = new NetworkCredential(username, password);
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mail);
                    }

                }



                return View(colorSizeCount);
            }
            else
            {
                ViewBag.Error = "The size and color you entered is available. To update the quantity, you can go to the product update section and find this size and color and update it";
                return View(colorSizeCount);
            }

        }

        public IActionResult UpdateProductDetail(int id)
        {
            var detail = db.ProductColorSizeCount.Find(id);
            var detailRus = db.ProductColorSizeCountRus.Find(id);
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");
            ViewBag.Id = id;
            return View(detail);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult UpdateProductDetail(ProductColorSizeCount colorSizeCount, ProductColorSizeCountRus colorSizeCountRus, int counts, IFormFile file)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);
            var productRus = db.ProductRus.FirstOrDefault(p => p.Id == colorSizeCount.ProductId);
            if (product.Count == null)
            {
                product.Count = 0;
                productRus.Count = 0;
            }

            if (product.ConstantCount == null)
            {
                product.ConstantCount = 0;
                productRus.ConstantCount = 0;
            }
            product.ConstantCount = product.ConstantCount - counts;
            product.Count = product.Count - counts;
            product.ConstantCount = product.ConstantCount + colorSizeCount.Count;
            product.Count = product.Count + colorSizeCount.Count;
            productRus.ConstantCount = product.ConstantCount;
            productRus.Count = product.Count;
            ViewBag.Color = new SelectList(db.Color.Where(p => p.DeletedDate == null).ToList(), "Id", "Name");

            if (file != null)
            {
                Image.Delete(colorSizeCount.ImagePath, env);
                colorSizeCount.ImagePath = Image.Add(file, env);
                colorSizeCountRus.ImagePath = colorSizeCount.ImagePath;

            }
            else
            {
                colorSizeCount.ImagePath = colorSizeCount.ImagePath;
                colorSizeCountRus.ImagePath = colorSizeCount.ImagePath;
            }

            db.Products.Update(product);
            productRus.Id = product.Id;
            db.ProductRus.Update(productRus);

            db.ProductColorSizeCount.Update(colorSizeCount);
            colorSizeCountRus.Id = colorSizeCount.Id;
            colorSizeCountRus.ColorId = colorSizeCount.ColorId;
            colorSizeCountRus.Count = colorSizeCount.Count;
            db.ProductColorSizeCountRus.Update(colorSizeCountRus);
            db.SaveChanges();
            return RedirectToAction("update", new { id = colorSizeCount.ProductId });
        }

        [Obsolete]
        public IActionResult DeleteProductDetail(int id)
        {
            var detail = db.ProductColorSizeCount.Find(id);
            var detailrus = db.ProductColorSizeCountRus.FirstOrDefault(p=> p.Id == id);

            var product = db.Products.FirstOrDefault(p => p.Id == detail.ProductId);

            product.ConstantCount = product.ConstantCount - detail.Count;
            product.Count = product.Count - detail.Count;
            Image.Delete(detail.ImagePath, env);
            db.ProductColorSizeCount.Remove(detail);
            db.ProductColorSizeCountRus.Remove(detailrus);
            db.Products.Update(product);
            db.SaveChanges();
            return RedirectToAction("update", new { id = product.Id });
        }


        public IActionResult Delete(int id)
        {

            var product = db.Products.FirstOrDefault(p => p.Id == id);
            var productrus = db.ProductRus.FirstOrDefault(p => p.Id == id);
            var productcolorsize = db.ProductColorSizeCount.FirstOrDefault(p => p.ProductId == id);
            var productcolorsizerus = db.ProductColorSizeCountRus.FirstOrDefault(p => p.ProductId == id);
            if (productcolorsize != null)
            {
                db.ProductColorSizeCountRus.Remove(productcolorsizerus);
                db.ProductColorSizeCount.Remove(productcolorsize);
            }

            
            db.Products.Remove(product);
            db.ProductRus.Remove(productrus);
            db.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
