using Fashion_Flex.IRepositories;
using Fashion_Flex.IRepositories.Repository;
using Fashion_Flex.Models;
using Fashion_Flex.Repositories;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add session and distributed memory cache services
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true; // Mark cookie as essential
			});

			// Add DbContext for EF Core with SQL Server
			builder.Services.AddDbContext<FFContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

			// Register Identity with ApplicationUser model and your custom DbContext (FFContext)
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				// Configure password, lockout, and user settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 4;
				options.Password.RequireNonAlphanumeric = false;
				options.Lockout.MaxFailedAccessAttempts = 3;
			})
			.AddEntityFrameworkStores<FFContext>() // Connect Identity to the EF Core DbContext
			.AddDefaultTokenProviders();

			// Configure the authentication cookie for Identity
			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true; // Make the cookie essential (GDPR compliance)
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the expiration time for authentication cookie
				options.LoginPath = "/Account/Login"; // Path to redirect to login page
				options.SlidingExpiration = true; // Ensure session is renewed when the user is active
			});

			// Add services to the container
			builder.Services.AddControllersWithViews();

            // Register Repositories
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IProductRepository ,ProductRepository>();
			builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();

			var app = builder.Build();

			// Configure the HTTP request pipeline
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseSession(); // Enable session middleware

			// Add authentication and authorization to the request pipeline
			app.UseAuthentication(); // Ensure authentication middleware is enabled
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
