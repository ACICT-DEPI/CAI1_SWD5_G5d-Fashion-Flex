using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.Models
{
    public class FFContext : DbContext
    {
        DbSet<Brand> Brands { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Favorite_Brand> Favorite_Brands { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Order_Item> Order_Items { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Payment> Payment { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FashionFlex_DB;Integrated Security=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
