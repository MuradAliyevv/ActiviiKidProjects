using ActiviKidWebUI.Helper;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.FormModel;
using ActiviKidWebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SiteInfoController : Controller
    {
        readonly ActiviKidDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        [Obsolete]
        public SiteInfoController(ActiviKidDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var vm = new SiteInfoViewModel();
            vm.SiteInfo = db.SiteInfos.FirstOrDefault();
            vm.AboutUs = db.AboutUs.FirstOrDefault();
            vm.SocialLinks = db.SocialLinks.ToList();
            vm.Services = db.Services.ToList();
            vm.ServicesRus = db.ServicesRus.ToList();
            vm.Categories = db.Categories.ToList();
            vm.CategoryRus = db.CategoryRus.ToList();
            vm.Colors = db.Color.ToList();
            vm.Colors = db.Color.ToList();
            vm.News = db.News.ToList();
            vm.Genders = db.Genders.ToList();
            vm.GendersRus = db.GendersRus.ToList();
            return View(vm);
        }

        #region SiteInfo

        public IActionResult Update(int id)
        {
            var info = db.SiteInfos.Find(id);

            return View(info);
        }


        [HttpPost]
        [Obsolete]
        public IActionResult Update(IFormFile? fileLogo, IFormFile? fileIcon, SiteInfo siteInfo)
        {
            if (fileLogo == null && siteInfo.Logo == null)
            {
                return View(siteInfo);
            }
            if (fileIcon == null && siteInfo.FavIcon == null)
            {
                return View(siteInfo);
            }
            if (fileIcon != null)
            {
                Image.Delete(siteInfo.FavIcon, env);
                siteInfo.FavIcon = Image.Add(fileIcon, env);
            }
            if (fileLogo != null)
            {
                Image.Delete(siteInfo.Logo, env);
                siteInfo.Logo = Image.Add(fileLogo, env);
            }


            if (ModelState.IsValid)
            {
                db.SiteInfos.Update(siteInfo);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View(siteInfo);
            }

        }
        #endregion


        #region SocialLink

        public IActionResult SocialAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SocialAdd(SocialLink SocialLink)
        {
            SocialLink.Name = SocialLink.Name.ToUpper();

            if (ModelState.IsValid)
            {
                db.SocialLinks.Add(SocialLink);
                db.SaveChanges();
                ModelState.Clear();
                return View();
            }
            else
            {
                return View(SocialLink);
            }

        }


        public IActionResult SocialUpdate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var SocialLink = db.SocialLinks.Find(id);
                return View(SocialLink);
            }

        }

        [HttpPost]
        public IActionResult SocialUpdate(SocialLink SocialLink)
        {
            SocialLink.Name = SocialLink.Name.ToUpper();
            if (ModelState.IsValid)
            {
                db.SocialLinks.Update(SocialLink);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                return View(SocialLink);
            }

        }


        public IActionResult SocialDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var SocialLink = db.SocialLinks.Find(id);
                return View(SocialLink);
            }

        }

        [HttpPost]
        public IActionResult SocialDelete(SocialLink SocialLink)
        {
            if (ModelState.IsValid)
            {
                db.SocialLinks.Remove(SocialLink);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                return View(SocialLink);
            }

        }

        #endregion


        #region AboutUs

        public IActionResult AboutusUpdate(int? id)
        {
            var fm = new AboutUsFormModel();
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                fm.AboutUs = db.AboutUs.Find(id);
                fm.AboutUsRus = db.AboutUsRus.Find(id);
                return View(fm);
            }

        }

        [HttpPost]
        [Obsolete]
        public IActionResult AboutusUpdate(AboutUsFormModel aboutUsForm, AboutUs aboutUs, AboutUsRus aboutUsRus)
        {
            aboutUs = aboutUsForm.AboutUs;
            aboutUsRus = aboutUsForm.AboutUsRus;
            

            aboutUsRus.Id = aboutUs.Id;
            db.AboutUs.Update(aboutUs);
            db.AboutUsRus.Update(aboutUsRus);
            db.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("index");


        }
        #endregion


        #region Category
        public IActionResult CategoryAdd()
        {
            return View();
        }





        [HttpPost]
        public IActionResult CategoryAdd(CategoryFormModel cfm, Category category, CategoryRus categoryRus)
        {

            if (cfm.Category.Name != null)
            {
                category = cfm.Category;
                categoryRus = cfm.CategoryRus;
                db.Categories.Add(category);
                db.SaveChanges();
                categoryRus.Id = category.Id;
                db.CategoryRus.Add(categoryRus);
                db.SaveChanges();
                return RedirectToAction("index");
            }





            return View(cfm);
        }



        public IActionResult CategoryUpdate(int? id)
        {
            CategoryFormModel cfm = new CategoryFormModel();
            cfm.Category = db.Categories.Find(id);
            cfm.CategoryRus = db.CategoryRus.Find(id);
            return View(cfm);
        }







        [HttpPost]
        public IActionResult CategoryUpdate(CategoryFormModel cfm, Category category, CategoryRus categoryRus)
        {

            if (cfm.Category.Name != null)
            {
                category = cfm.Category;
                categoryRus = cfm.CategoryRus;
                category.UpdatedDate = DateTime.UtcNow;

                db.Categories.Update(category);
                categoryRus.Id = category.Id;
                db.CategoryRus.Update(categoryRus);
                db.SaveChanges();
                return RedirectToAction("index");
            }





            return View(category);
        }





        public IActionResult CategoryDelete(int id)
        {

            var category = db.Categories.Find(id);
            var categoryRus = db.CategoryRus.Find(id);
            db.Categories.Remove(category);
            db.CategoryRus.Remove(categoryRus);
            db.SaveChanges();
            return RedirectToAction("index");







        }
        #endregion


        #region Gender
        public IActionResult GenderAdd()
        {
            return View();
        }





        [HttpPost]
        public IActionResult GenderAdd(GenderFormModel cfm, Genders genders, GendersRus gendersRus)
        {

            if (cfm.Gender.Name != null)
            {
                genders = cfm.Gender;
                gendersRus = cfm.GenderRus;
                db.Genders.Add(genders);
                db.SaveChanges();
                gendersRus.Id = genders.Id;
                db.GendersRus.Add(gendersRus);
                db.SaveChanges();
                return RedirectToAction("index");
            }





            return View(cfm);
        }



        public IActionResult GenderUpdate(int? id)
        {
            GenderFormModel cfm = new GenderFormModel();
            cfm.Gender = db.Genders.Find(id);
            cfm.GenderRus = db.GendersRus.Find(id);
            return View(cfm);
        }







        [HttpPost]
        public IActionResult GenderUpdate(GenderFormModel cfm, Genders genders, GendersRus gendersRus)
        {

            if (cfm.Gender.Name != null)
            {
                genders = cfm.Gender;
                gendersRus = cfm.GenderRus;
                genders.UpdatedDate = DateTime.UtcNow;

                db.Genders.Update(genders);
                gendersRus.Id = genders.Id;
                db.GendersRus.Update(gendersRus);
                db.SaveChanges();
                return RedirectToAction("index");
            }





            return View(cfm);
        }





        public IActionResult GenderDelete(int id)
        {

            var gender = db.Genders.Find(id);
            var genderRus = db.GendersRus.Find(id);
            db.GendersRus.Remove(genderRus);
            db.Genders.Remove(gender);
            db.SaveChanges();
            return RedirectToAction("index");







        }
        #endregion


        #region Color
        public IActionResult AddColor()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult AddColor(IFormFile file, ColorRusFormModel cfm, Color? color, ColorRus colorrus)
        {
            color = cfm.Color;
            colorrus = cfm.ColorRus;
            if (file == null)
            {
                return View();
            }
            else
            {
                color.ImagePath = Image.Add(file, env);
                db.Color.Add(color);
                colorrus.Id = color.Id;
                colorrus.ImagePath = color.ImagePath;
                db.SaveChanges();

                colorrus.ImagePath = color.ImagePath;
                colorrus.Id = color.Id;

                db.ColorRus.Add(colorrus);
                db.SaveChanges();

                return RedirectToAction("index");
            }

        }


        public IActionResult UpdateColor(int id)
        {
            var cfm = new ColorRusFormModel();
            cfm.Color = db.Color.Find(id);
            cfm.ColorRus = db.ColorRus.Find(id);
            return View(cfm);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult UpdateColor(ColorRusFormModel cfm, IFormFile file, Color? color, ColorRus colorRus)
        {
            color = cfm.Color;
            colorRus = cfm.ColorRus;
            if (file == null)
            {
                if (color.ImagePath == null)
                {
                    return View();
                }
                else
                {
                    colorRus.ImagePath = color.ImagePath;
                    colorRus.Id = color.Id;
                    db.Color.Update(color);
                    db.ColorRus.Update(colorRus);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }

            }
            else
            {
                Image.Delete(color.ImagePath, env);
                color.ImagePath = Image.Add(file, env);
                colorRus.ImagePath = color.ImagePath;
                colorRus.Id = color.Id;
                db.Color.Update(color);
                db.ColorRus.Update(colorRus);
                db.SaveChanges();
                return RedirectToAction("index");
            }

        }



        [Obsolete]
        public IActionResult DeleteColor(int id)
        {
            var color = db.Color.Find(id);
            var colorrus = db.ColorRus.Find(id);
            Image.Delete(color.ImagePath, env);
            db.Color.Remove(color);
            db.ColorRus.Remove(colorrus);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        #endregion


        #region Services
        public IActionResult ServicesAdd()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult ServicesAdd(ServicesFormModel servicesFormModel, Services services, ServicesRus servicesRus)
        {
            services = servicesFormModel.Service;
            servicesRus = servicesFormModel.ServicesRus;


                db.Services.Add(services);
                db.SaveChanges();
                servicesRus.ImagePath = services.ImagePath;
                servicesRus.Id = services.Id;
                db.ServicesRus.Add(servicesRus);
            db.SaveChanges();
            ModelState.Clear();
                return View();

        }



        public IActionResult ServicesUpdate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var services = db.Services.Find(id);
                return View(services);
            }

        }

        [HttpPost]
        [Obsolete]
        public IActionResult ServicesUpdate(Services services, ServicesRus servicesRus)
        {


            services.UpdatedDate = DateTime.Now;
            servicesRus.ImagePath = services.ImagePath;
            servicesRus.Id = services.Id;
            db.Services.Update(services);
            db.ServicesRus.Update(servicesRus);
            db.SaveChanges();
    
           
            ModelState.Clear();
            return RedirectToAction("index");


        }

        public IActionResult ServicesDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var services = db.Services.Find(id);
                return View(services);
            }

        }

        public IActionResult ServicesDelete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            else
            {
                var services = db.Services.Find(id);
                var servicesrus = db.ServicesRus.Find(id);
                db.Services.Remove(services);
                db.ServicesRus.Remove(servicesrus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        #endregion

        #region News
        public IActionResult NewsAdd()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult NewsAdd(IFormFile? file, NewsFormModel nwfm, NewsRus newsRus, News news)
        {
            news = nwfm.News;
            newsRus = nwfm.NewsRus;
            if (file == null)
            {
                ViewBag.News = "Image not selected";
                return View(nwfm);
            }
            else
            {
                news.ImagePath = Image.Add(file, env);
                newsRus.ImagePath = news.ImagePath;
                if (ModelState.IsValid)
                {
                    db.News.Add(news);
                    db.SaveChanges();
                    newsRus.Id = news.Id;
                    db.NewsRus.Add(newsRus);
                    db.SaveChanges();
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View(nwfm);
                }
            }


        }


        public IActionResult NewsUpdate(int? id)
        {
            var nwfm = new NewsFormModel();
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                nwfm.News = db.News.Find(id);
                nwfm.NewsRus = db.NewsRus.Find(id);
                return View(nwfm);
            }
        }

        [HttpPost]
        [Obsolete]
        public IActionResult NewsUpdate(IFormFile? file, NewsFormModel nwfm, NewsRus newsRus, News news)
        {
            news = nwfm.News;
            newsRus = nwfm.NewsRus;

            if (file == null)
            {
                if (news.ImagePath != null)
                {
                    if (ModelState.IsValid)
                    {
                        newsRus.ImagePath = news.ImagePath;
                        newsRus.Id= news.Id;
                        db.News.Update(news);
                        db.NewsRus.Update(newsRus);
                        db.SaveChanges();
                        ModelState.Clear();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        return View(nwfm);
                    }
                }
                else
                {
                    ViewBag.News = "Image not selected";
                    return View(nwfm);
                }


            }
            else
            {
                if (news.ImagePath != null)
                {
                    Image.Delete(news.ImagePath, env);
                }

                news.ImagePath = Image.Add(file, env);
                newsRus.ImagePath = news.ImagePath; 
                newsRus.Id = news.Id;
                if (ModelState.IsValid)
                {
                    db.News.Update(news);
                    db.NewsRus.Update(newsRus);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.News = "The information is incomplete";
                    return View(nwfm);
                }
            }


        }


        [HttpGet]
        [Obsolete]
        public IActionResult NewsDelete(int id)
        {
            var news = db.News.Find(id);
            var newsrus = db.NewsRus.Find(id);
            if (news != null)
            {
                Image.Delete(news.ImagePath, env);

                db.News.Remove(news);
                db.NewsRus.Remove(newsrus);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }
        #endregion
    }
}
