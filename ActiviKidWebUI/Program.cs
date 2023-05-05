using ActiviKidWebUI.Helper;
using ActiviKidWebUI.Models.DataContext;
using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ActiviKidDbContext>(cfg =>
{

    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"));
});

builder.Services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
});

builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{

    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöpqrsştuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;

}).AddPasswordValidator<CustomPasswordValidator>()
  .AddUserValidator<CustomUserValidator>()
  .AddErrorDescriber<CustomIdentityErrorDescriber>()
  .AddEntityFrameworkStores<ActiviKidDbContext>()
  .AddDefaultTokenProviders();

CookieBuilder cookieBuilder = new CookieBuilder();
cookieBuilder.Name = "Activikid";
cookieBuilder.HttpOnly = false;
cookieBuilder.SameSite = SameSiteMode.Lax;
cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;

builder.Services.ConfigureApplicationCookie(opts =>
{

    opts.Cookie = cookieBuilder;
    opts.SlidingExpiration = true;
    opts.ExpireTimeSpan = System.TimeSpan.FromDays(60);

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.Seed();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseRequestLocalization(cfg =>
{
    cfg.AddSupportedCultures("az", "en");
    cfg.AddSupportedUICultures("az", "en");
    cfg.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
    cfg.RequestCultureProviders.Add(new AppCultureProvider());
});
app.MapAreaControllerRoute("adminArea",
areaName: "Admin",
pattern: "admin/{lang=az}/{controller=SiteInfo}/{action=index}/{id?}",
constraints: new { lang = "az|en" });

app.MapControllerRoute(
    name: "default",
    pattern: "{lang=az}/{controller=Home}/{action=Index}/{id?}",
    constraints: new { lang = "az|en" });
app.Run();
