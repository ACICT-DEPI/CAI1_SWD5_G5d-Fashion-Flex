using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class FavoriteBrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
