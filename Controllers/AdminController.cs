using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
	public class AdminController : Controller
	{
		private readonly ICustomerRepository _customerRepository;
		public AdminController(ICustomerRepository _customerRepository)
		{
			this._customerRepository = _customerRepository;
		}
		public IActionResult Index()
		{
			ViewData["currTab"] = "dashboard";
			return View();
		}

		public IActionResult Customers()
		{
			ViewData["currTab"] = "customers";
			var customers = _customerRepository.GetAll();
			return View(customers);
		}
		public IActionResult Products()
		{
			ViewData["currTab"] = "products";
			return View();
		}
		public IActionResult Orders()
		{
			ViewData["currTab"] = "orders";
			return View();
		}
	}
}
