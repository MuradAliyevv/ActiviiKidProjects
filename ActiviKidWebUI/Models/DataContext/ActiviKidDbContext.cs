using ActiviKidWebUI.Models.Entity;
using ActiviKidWebUI.Models.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace ActiviKidWebUI.Models.DataContext
{
    public class ActiviKidDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {



         public ActiviKidDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<SiteInfo> SiteInfos { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AboutUsRus> AboutUsRus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRus> CategoryRus { get; set; }
        public DbSet<ActiviKidWebUI.Models.Entity.Color> Color { get; set; }
        public DbSet<ColorRus> ColorRus { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Genders> Genders { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<GendersRus> GendersRus { get; set; }
        public DbSet<NewsRus> NewsRus { get; set; }
        public DbSet<OurClients> OurClients { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRus> ProductRus { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<ProductColorSizeCount> ProductColorSizeCount { get; set; }
        public DbSet<ProductColorSizeCountRus> ProductColorSizeCountRus { get; set; }

        public DbSet<Services> Services { get; set; }
        public DbSet<ServicesRus> ServicesRus { get; set; }
        public DbSet<Contact> Contact { get; set; }
        #region Membership
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
        public DbSet<AppUserClaim> UserClaims { get; set; }
        public DbSet<AppRoleClaim> RoleClaims { get; set; }
        public DbSet<AppUserToken> UserTokes { get; set; }
        public DbSet<AppUserLogin> UserLogins { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Membership
            modelBuilder.Entity<AppUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });

            modelBuilder.Entity<AppUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });

            modelBuilder.Entity<AppUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");

            });

            modelBuilder.Entity<AppRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });

            modelBuilder.Entity<AppRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });

            modelBuilder.Entity<AppUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });

            modelBuilder.Entity<AppUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });
            #endregion
        }
    }
}
