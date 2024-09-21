using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

//Another comment to know what is happening
//After merge with the final test version
namespace Fashion_Flex.Controllers
{
    //public class ProductController : Controller
    //{

    //        private readonly IProductRepository _productRepository;

    //        public ProductController(IProductRepository _productRepository)
    //        {
    //            this._productRepository = _productRepository;
    //        }
    //        public IActionResult Index()
    //        {
    //            return View(_productRepository.GetAll());
    //        }

    //}


    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
