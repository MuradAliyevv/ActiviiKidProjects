using ActiviKidWebUI.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.FormModel;
using System.Security.Claims;
using ActiviKidWebUI.Models.ViewModel;

namespace ActiviKidWebUI.Controllers
{
    public class ContactController : Controller
    {

        readonly ActiviKidDbContext db;
        public ContactController(ActiviKidDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string? lang = null)
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


            var fm = new ContactFormModel();
            fm.SiteInfo = db.SiteInfos.FirstOrDefault();
            return View(fm);
        }

        [HttpPost]
        public IActionResult Index(ContactFormModel fm, Contact contact)
        {
            contact = fm.Contact;
            fm.SiteInfo = db.SiteInfos.FirstOrDefault();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                var configuration = builder.Build();
                var host = configuration["Gmail:Host"];
                var port = int.Parse(configuration["Gmail:Port"]);
                var username = configuration["Gmail:Username"];
                var password = configuration["Gmail:Password"];
                var displayName = configuration["Gmail:DisplayName"];

                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);

                MailMessage mail = new MailMessage($"{contact.Email}", $"{username}");
                mail.Subject = displayName;


                mail.Body = $@"
               <html>
               <head> 
               <style>
               h6{"font-size: 2rem;"}
               p{""} 

              </style>
              </head>
              <body>
              <h6>Email: {contact.Email} </h6>
              <h6>Full Name: {contact.FullName}</h6> 
               <p>{contact.Message}</p>
            </body>
            </html>";
                mail.IsBodyHtml = true;




                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                contact.Isanswerd = false;
            contact.Address = contact.Email;
            contact.Isanswerd = false;
            
                db.Add(contact);
                db.SaveChanges();
                ViewBag.msg = "Your message has been sent successfully";
                ModelState.Clear();
                return View(fm);


        }
    }
}
