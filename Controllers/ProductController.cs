using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
	public class ProductController : Controller
	{

		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository _productRepository)
		{
			this._productRepository = _productRepository;
		}
		public IActionResult Index()
		{
			return View(_productRepository.GetAll());
		}

	}

}
