using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.Models
{
    public class FFContext : IdentityDbContext<ApplicationUser>
    {
        public FFContext(DbContextOptions<FFContext> options) : base(options)
        {
        }       

        public DbSet<Customer> Customers { get; set; }       
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<FavoriteList> FavoriteList { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
