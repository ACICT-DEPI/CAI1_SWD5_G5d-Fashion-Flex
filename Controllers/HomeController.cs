using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Fashion_Flex.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository _productRepository)
		{
			_logger = logger;
            this._productRepository = _productRepository;
        }

		public IActionResult Index(int pageIndex = 1, int pageSize = 8, string sortOrder = "", string category = "", string type = "", string searchString = "")
		{

			// Track the current sorting order
			ViewData["CurrentSort"] = sortOrder;
			// Keep track of the selected category
			ViewData["CurrentCategory"] = category;
			// Track the selected type (Women, Men, Watches, etc.)
			ViewData["CurrentType"] = type;

			// List of current Categories
			var categories = _productRepository.GetCategories();
			ViewData["Categories"] = categories;

			// List of current Types
			var types = _productRepository.GetTypes();
			ViewData["Types"] = types;

			// Track the current search string
			ViewData["CurrentSearch"] = searchString;



			// Assign sorting parameters for the view (already done previously)
			ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";
			ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";


			List<Product> products = _productRepository.GetAll();


            return View(products);
		}

		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
