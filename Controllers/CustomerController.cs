using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class CustomerController : Controller
    {        
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository _customerRepository)
        {
            this._customerRepository = _customerRepository;
        }
        public IActionResult Index()
        {
            return View(_customerRepository.GetAll());
        }

    }
}
