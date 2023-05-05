using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.Entity.Identity;
using ActiviKidWebUI.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ActiviKidWebUI.Models.FormModel;
using ActiviKidWebUI.Models.FormModel;


namespace ActiviKidWebUI.Controllers
{
    [Route("/Account")]
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly UserManager<AppUser> userManager;
        readonly ActiviKidDbContext db;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userInManager, ActiviKidDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userInManager;
            this.db = db;
        }


        [AllowAnonymous]
        [Route("/Account/Login")]
        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("/Account/Login")]
        [Route("/Login")]
        [HttpPost]
        async public Task<IActionResult> Login(UserFormModel model)
        {
            string UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindByNameAsync(model.Username);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
                var result = await signInManager.PasswordSignInAsync(appUser, model.Password, true, true);

                if (result.Succeeded)
                {

                    string redirect = Request.Query["returnUrl"];
                    if (string.IsNullOrWhiteSpace(redirect))
                        return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    goto showSameView;
                }
            }
        showSameView:
            return View(model);
        }

        [AllowAnonymous]
        [Route("/Account/Logout")]
        [Route("/Login")]
        async public Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [Route("/Account/Register")]
        [Route("/Register")]
        public IActionResult Register(RegisterFormModel model, bool register)
        {
            if (model.Email == null || model.UserName == null || model.Password == null)
            {
                return View();
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Account/Register")]
        [Route("/Register")]
        async public Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {

                Customer customer = new Customer()
                {
                   
                    Name = model.Name,
                    Surname = model.Surname,

                };
                db.Customers.Add(customer);
                db.SaveChanges();

                var appUser = new AppUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    CustomerId = customer.Id
                };
                var result = await userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(Login));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }

        public IActionResult Accessdenied()
        {
            return RedirectToAction("Login");
        }
    }
}
