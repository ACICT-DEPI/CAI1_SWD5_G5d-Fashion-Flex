using Microsoft.AspNetCore.Identity;
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

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Optionally, you can add some default roles directly in the database using the ModelBuilder
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
				new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
			);
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//    optionsBuilder.UseSqlServer("ConnectionString");
		//    base.OnConfiguring(optionsBuilder);
		//}
	}
}
