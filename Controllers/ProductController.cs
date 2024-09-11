using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class ProductController : Controller
    {

            private readonly ICustomerRepository _productRepository;

            public ProductController(ICustomerRepository _productRepository)
            {
                this._productRepository = _productRepository;
            }
            public IActionResult Index()
            {
                return View(_productRepository.GetAll());
            }

        }
    
}
