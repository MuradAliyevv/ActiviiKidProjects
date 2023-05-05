using ActiviKidWebUI.Helper;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class ClientController : Controller
    {
        readonly ActiviKidDbContext db;
        [Obsolete]
        Microsoft.AspNetCore.Hosting.IHostingEnvironment env;

        [Obsolete]
        public ClientController(ActiviKidDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var clients = db.OurClients.ToList();
            return View(clients);
        }

        #region News
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Add(IFormFile? file, OurClients clients)
        {

            if (file == null)
            {
                ViewBag.News = "Image not selected";
                return View(clients);
            }
            else
            {
                clients.ImagePath = Image.Add(file, env);

                if (ModelState.IsValid)
                {

                    db.OurClients.Add(clients);
                    db.SaveChanges();
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View(clients);
                }
            }


        }


        public IActionResult Update(int? id)
        {
          
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var clients = db.OurClients.Find(id); 
                return View(clients);
            }
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Update(IFormFile? file, OurClients clients)
        {


            if (file == null)
            {
                if (clients.ImagePath != null)
                {
                    if (ModelState.IsValid)
                    {
                       
                        db.OurClients.Update(clients);
                        db.SaveChanges();
                        ModelState.Clear();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        return View(clients);
                    }
                }
                else
                {
                    ViewBag.News = "Image not selected";

                    return View(clients);
                }


            }
            else
            {
                if (clients.ImagePath != null)
                {
                    Image.Delete(clients.ImagePath, env);
                }

                clients.ImagePath = Image.Add(file, env);

                if (ModelState.IsValid)
                {
                   
                    db.OurClients.Update(clients);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.News = "The information is incomplete";
                    return View(clients);
                }
            }


        }


        [HttpGet]
        [Obsolete]
        public IActionResult Delete(int id)
        {
           
            var clients = db.OurClients.Find(id);
            if (clients != null)
            {
                Image.Delete(clients.ImagePath, env);

             
                db.OurClients.Remove(clients);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("index");
            }

        }
        #endregion
    }
}
