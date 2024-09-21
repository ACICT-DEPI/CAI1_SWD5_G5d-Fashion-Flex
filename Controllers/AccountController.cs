using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Fashion_Flex.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create new ApplicationUser object
                var newUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                // Save new user in the database using Identity
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    // Create new Customer and link it to the ApplicationUser
                    var newCustomer = new Customer
                    {
                        Account_Creation_Date = DateTime.Now,
                        Address = model.Address,
                        Date_Of_Birth = model.Date_Of_Birth,
                        Email = model.Email,
                        Phone_Number = model.PhoneNumber,
                        First_Name = model.First_Name.Trim(),
                        Last_Name = model.Last_Name.Trim(),
                        Phone_Country_Code = model.Phone_Country_Code,
                        Is_Active = model.Is_Active,
                        ApplicationUserId = newUser.Id // Link to ApplicationUser
                    };

                    // Save the Customer in the database via the repository
                    _customerRepository.Add(newCustomer);
                    _customerRepository.Save();

                    // Automatically sign the user in after registration
                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    return RedirectToAction("Index", "Home"); // Redirect to the home page upon successful registration
                }

                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // Redirect to the home page upon successful login
                }

                //handel error
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Redirect to home page or another page
            return RedirectToAction("Index", "Home");
        }

    }
}
