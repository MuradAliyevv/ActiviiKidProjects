using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ActiviKidWebUI.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ActiviKidDbContext db;
        public SubscribeController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("/subscribe/subs")]
        [Route("/subscribe/subs/{id}")]
        [Route("/az/subscribe/subs/{id}")]
        [Route("/en/subscribe/subs/{id}")]
        public IActionResult Subs(string id)
        {
            var subscribers = new Newsletter();


            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new
                {
                    error = true,
                    msg = "The email format entered is incorrect!",
                });
            }
            else
            {
                subscribers.Email = id;

                if (ModelState.IsValid)
                {
                    var email = db.Newsletter.FirstOrDefault(p => p.Email == id);
                    if (email != null)
                    {
                        return Json(new
                        {
                            error = true,
                            msg = "This email is already subscribed!",
                        });
                    }
                    else
                    {
                        db.Newsletter.Add(subscribers);
                        db.SaveChanges();
                        return Json(new
                        {
                            error = false,
                            msg = "Thank you for subscribing!",
                        });
                    }

                }
                else
                {
                    return Json(new
                    {
                        error = true,
                        msg = "The email format entered is incorrect!",
                    });
                }
            }




        }

        public IActionResult UnSubs(int? id)
        {

            if (id == null)
            {
                return Json(new
                {
                    error = true,
                    msg = "Your Email Not Fount",
                });
            }
            else
            {
                var subscribers = db.Newsletter.Find(id);
                subscribers.DeletedDate = DateTime.Now;
                db.Newsletter.Update(subscribers);
                db.SaveChanges();
                return RedirectToAction("index", "home");
            }



        }
    }
}
