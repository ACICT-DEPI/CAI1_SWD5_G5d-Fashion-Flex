using Fashion_Flex.Models;
using Fashion_Flex.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
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
				//save new user in database
				var newUser = new ApplicationUser
				{
					UserName = model.Email,
					Email = model.Email,
					PasswordHash = model.Password
				};

				var result = await _userManager.CreateAsync(newUser, model.Password);

				//save the register cookies in database
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(newUser, isPersistent: false);
					return RedirectToAction("Index"); //if registered succssfuly redirect to home page
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

			}
			return View(model);
		}
	}
}
