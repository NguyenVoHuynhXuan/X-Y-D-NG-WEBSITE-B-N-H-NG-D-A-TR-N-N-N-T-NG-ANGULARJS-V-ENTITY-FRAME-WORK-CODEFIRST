using Microsoft.AspNet.Identity.EntityFramework;
using uStora.Model.Models;
using System.Data.Entity;

namespace uStora.Data
{
    public class uStoraDbContext : IdentityDbContext<ApplicationUser>
    {
        public uStoraDbContext()
            : base("uStoraConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TrackOrder> TrackOrders { get; set; }

        public static uStoraDbContext Create()
        {
            return new uStoraDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(i => i.UserId).ToTable("ApplicationUserLogins");

            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");

            modelBuilder.Entity<IdentityUserClaim>()
                .HasKey(i => i.UserId).ToTable("ApplicationUserClaims");

        }
    }
}