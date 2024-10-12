using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Microsoft.AspNetCore.Mvc;
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

		public IActionResult Index()
		{

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
