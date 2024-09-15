using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Fashion_Flex.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class CustomerController : Controller
    {        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(UserManager<ApplicationUser> userManager, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var userId = _userManager.GetUserId(User);
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Id = customer.Id,
                First_Name = customer.First_Name,
                Last_Name = customer.Last_Name,
                Email = customer.Email,
                Address = customer.Address,
                Date_Of_Birth = customer.Date_Of_Birth,
                Phone_Number = customer.Phone_Number,
                Phone_Country_Code = customer.Phone_Country_Code,
                Is_Active = customer.Is_Active
            };

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(User);
            var customer = await _customerRepository.GetByUserIdAsync(userId);

            if (customer == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Id = customer.Id,
                First_Name = customer.First_Name,
                Last_Name = customer.Last_Name,
                Email = customer.Email,
                Address = customer.Address,
                Date_Of_Birth = customer.Date_Of_Birth,
                Phone_Number = customer.Phone_Number,
                Phone_Country_Code = customer.Phone_Country_Code,
                Is_Active = customer.Is_Active
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var customer = await _customerRepository.GetByUserIdAsync(userId);

                if (customer == null)
                {
                    return NotFound();
                }

                // Update customer details
                customer.First_Name = model.First_Name;
                customer.Last_Name = model.Last_Name;
                customer.Email = model.Email;
                customer.Address = model.Address;
                customer.Date_Of_Birth = model.Date_Of_Birth;
                customer.Phone_Number = model.Phone_Number;
                customer.Phone_Country_Code = model.Phone_Country_Code;
                customer.Is_Active = model.Is_Active;

                _customerRepository.Update(customer);
                _customerRepository.Save();

                return RedirectToAction("Details");
            }

            return View(model);
        }

    }
}
