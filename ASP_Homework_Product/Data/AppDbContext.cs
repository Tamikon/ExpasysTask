using ASP_Homework_Product.Areas.Admin.Models;
using ASP_Homework_Product.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Homework_Product.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserAccount> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDeliveryInfo> UserDeliveryInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne()
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(ci => ci.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasData(
                new Product("ASUS ROG Zephyrus Duo 16", "2560х1600, AMD Ryzen 9 7945HX 2.5 ГГц, NVIDIA GeForce RTX 4090", 499990, "/Images/image1.webp") { Id = 1 },
                new Product("GIGABYTE G6", "1920х1200, Intel Core i7 12650H 2.3 ГГц, NVIDIA GeForce RTX 4050", 105990, "/Images/image2.webp") { Id = 2 },
                new Product("MSI Sword 17 A12VF-810XRU", "1920х1080, Intel Core i7 12650H 2.3 ГГц, NVIDIA GeForce RTX 4060", 170990, "/Images/image3.webp") { Id = 3 }
            );

            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount { Name = "admin@gmail.com", Password = "admin", Phone = "1234567890", Role = UserRole.Admin },
                new UserAccount { Name = "user@gmail.com", Password = "user", Phone = "0987654321", Role = UserRole.Customer }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}