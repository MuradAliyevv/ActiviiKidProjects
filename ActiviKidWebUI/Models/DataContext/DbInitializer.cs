using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ActiviKidWebUI.Models.DataContext
{
    public static class DbInitializer
    {


        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            const string adminEmail = "muradaliyev20233202@gmail.com";
            const string adminPassword = "Murad2023!";
            const string superAdminRoleName = "SuperAdmin";

            using (var scope = app.ApplicationServices.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<ActiviKidDbContext>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                db.Database.Migrate();

                var role = roleManager.FindByNameAsync(superAdminRoleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = superAdminRoleName
                    };

                    roleManager.CreateAsync(role).Wait();
                }


                var userManeger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var adminUser = userManeger.FindByEmailAsync(adminEmail).Result;

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        EmailConfirmed = true
                    };

                    var userResult = userManeger.CreateAsync(adminUser, adminPassword).Result;

                    if (userResult.Succeeded)
                    {
                        userManeger.AddToRoleAsync(adminUser, superAdminRoleName).Wait();
                    }

                }
                if (!db.SiteInfos.Any())
                {
                    db.SiteInfos.Add(new SiteInfo
                    {
                        Id= 1,
                        Name = "AktiviKid",
                        Address = "baki",
                        Number = "000-00-000-00-00",
                        Email= "muradaliyev20233202@gmail.com",
                        Logo="example.png",
                        FavIcon="exampl.png"

                    });
                    db.SaveChanges();

                }

                if (!db.AboutUs.Any())
                {
                    db.AboutUs.Add(new AboutUs
                    {
                        Id = 1,
                        Description = "Lorem"

                    });
                    db.SaveChanges();

                }

                if (!db.AboutUsRus.Any())
                {
                    db.AboutUsRus.Add(new AboutUsRus
                    {
                        Id = 1,
                        Description = "Lorem"

                    });
                    db.SaveChanges();

                }
            }
            return app;
        }
    }
}
