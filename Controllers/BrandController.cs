using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
