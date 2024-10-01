using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Fashion_Flex.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fashion_Flex.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ICustomerRepository _customerRepository;
		List<SelectListItem> countryPhoneCodes = new List<SelectListItem>
			{
				new SelectListItem { Value = "+1", Text = "United States +1" },
				new SelectListItem { Value = "+44", Text = "United Kingdom +44" },
				new SelectListItem { Value = "+93", Text = "Afghanistan +93" },
				new SelectListItem { Value = "+355", Text = "Albania +355" },
				new SelectListItem { Value = "+213", Text = "Algeria +213" },
				new SelectListItem { Value = "+376", Text = "Andorra +376" },
				new SelectListItem { Value = "+244", Text = "Angola +244" },
				new SelectListItem { Value = "+54", Text = "Argentina +54" },
				new SelectListItem { Value = "+374", Text = "Armenia +374" },
				new SelectListItem { Value = "+61", Text = "Australia +61" },
				new SelectListItem { Value = "+43", Text = "Austria +43" },
				new SelectListItem { Value = "+994", Text = "Azerbaijan +994" },
				new SelectListItem { Value = "+973", Text = "Bahrain +973" },
				new SelectListItem { Value = "+880", Text = "Bangladesh +880" },
				new SelectListItem { Value = "+375", Text = "Belarus +375" },
				new SelectListItem { Value = "+32", Text = "Belgium +32" },
				new SelectListItem { Value = "+501", Text = "Belize +501" },
				new SelectListItem { Value = "+229", Text = "Benin +229" },
				new SelectListItem { Value = "+975", Text = "Bhutan +975" },
				new SelectListItem { Value = "+591", Text = "Bolivia +591" },
				new SelectListItem { Value = "+387", Text = "Bosnia and Herzegovina +387" },
				new SelectListItem { Value = "+267", Text = "Botswana +267" },
				new SelectListItem { Value = "+55", Text = "Brazil +55" },
				new SelectListItem { Value = "+359", Text = "Bulgaria +359" },
				new SelectListItem { Value = "+226", Text = "Burkina Faso +226" },
				new SelectListItem { Value = "+257", Text = "Burundi +257" },
				new SelectListItem { Value = "+855", Text = "Cambodia +855" },
				new SelectListItem { Value = "+237", Text = "Cameroon +237" },
				new SelectListItem { Value = "+1", Text = "Canada +1" },
				new SelectListItem { Value = "+238", Text = "Cape Verde +238" },
				new SelectListItem { Value = "+236", Text = "Central African Republic +236" },
				new SelectListItem { Value = "+235", Text = "Chad +235" },
				new SelectListItem { Value = "+56", Text = "Chile +56" },
				new SelectListItem { Value = "+86", Text = "China +86" },
				new SelectListItem { Value = "+57", Text = "Colombia +57" },
				new SelectListItem { Value = "+269", Text = "Comoros +269" },
				new SelectListItem { Value = "+243", Text = "Congo (DRC) +243" },
				new SelectListItem { Value = "+242", Text = "Congo (Republic) +242" },
				new SelectListItem { Value = "+506", Text = "Costa Rica +506" },
				new SelectListItem { Value = "+385", Text = "Croatia +385" },
				new SelectListItem { Value = "+53", Text = "Cuba +53" },
				new SelectListItem { Value = "+357", Text = "Cyprus +357" },
				new SelectListItem { Value = "+420", Text = "Czech Republic +420" },
				new SelectListItem { Value = "+45", Text = "Denmark +45" },
				new SelectListItem { Value = "+253", Text = "Djibouti +253" },
				new SelectListItem { Value = "+1", Text = "Dominica +1" },
				new SelectListItem { Value = "+1", Text = "Dominican Republic +1" },
				new SelectListItem { Value = "+593", Text = "Ecuador +593" },
				new SelectListItem { Value = "+20", Text = "Egypt +20" },
				new SelectListItem { Value = "+503", Text = "El Salvador +503" },
				new SelectListItem { Value = "+240", Text = "Equatorial Guinea +240" },
				new SelectListItem { Value = "+291", Text = "Eritrea +291" },
				new SelectListItem { Value = "+372", Text = "Estonia +372" },
				new SelectListItem { Value = "+251", Text = "Ethiopia +251" },
				new SelectListItem { Value = "+358", Text = "Finland +358" },
				new SelectListItem { Value = "+33", Text = "France +33" },
				new SelectListItem { Value = "+241", Text = "Gabon +241" },
				new SelectListItem { Value = "+220", Text = "Gambia +220" },
				new SelectListItem { Value = "+995", Text = "Georgia +995" },
				new SelectListItem { Value = "+49", Text = "Germany +49" },
				new SelectListItem { Value = "+233", Text = "Ghana +233" },
				new SelectListItem { Value = "+350", Text = "Gibraltar +350" },
				new SelectListItem { Value = "+30", Text = "Greece +30" },
				new SelectListItem { Value = "+502", Text = "Guatemala +502" },
				new SelectListItem { Value = "+224", Text = "Guinea +224" },
				new SelectListItem { Value = "+245", Text = "Guinea-Bissau +245" },
				new SelectListItem { Value = "+592", Text = "Guyana +592" },
				new SelectListItem { Value = "+509", Text = "Haiti +509" },
				new SelectListItem { Value = "+504", Text = "Honduras +504" },
				new SelectListItem { Value = "+852", Text = "Hong Kong +852" },
				new SelectListItem { Value = "+36", Text = "Hungary +36" },
				new SelectListItem { Value = "+354", Text = "Iceland +354" },
				new SelectListItem { Value = "+91", Text = "India +91" },
				new SelectListItem { Value = "+62", Text = "Indonesia +62" },
				new SelectListItem { Value = "+98", Text = "Iran +98" },
				new SelectListItem { Value = "+964", Text = "Iraq +964" },
				new SelectListItem { Value = "+353", Text = "Ireland +353" },
				new SelectListItem { Value = "+972", Text = "Israel +972" },
				new SelectListItem { Value = "+39", Text = "Italy +39" },
				new SelectListItem { Value = "+81", Text = "Japan +81" },
				new SelectListItem { Value = "+962", Text = "Jordan +962" },
				new SelectListItem { Value = "+7", Text = "Kazakhstan +7" },
				new SelectListItem { Value = "+254", Text = "Kenya +254" },
				new SelectListItem { Value = "+686", Text = "Kiribati +686" },
				new SelectListItem { Value = "+383", Text = "Kosovo +383" },
				new SelectListItem { Value = "+965", Text = "Kuwait +965" },
				new SelectListItem { Value = "+996", Text = "Kyrgyzstan +996" },
				new SelectListItem { Value = "+856", Text = "Laos +856" },
				new SelectListItem { Value = "+371", Text = "Latvia +371" },
				new SelectListItem { Value = "+961", Text = "Lebanon +961" },
				new SelectListItem { Value = "+231", Text = "Liberia +231" },
				new SelectListItem { Value = "+218", Text = "Libya +218" },
				new SelectListItem { Value = "+423", Text = "Liechtenstein +423" },
				new SelectListItem { Value = "+370", Text = "Lithuania +370" },
				new SelectListItem { Value = "+352", Text = "Luxembourg +352" },
				// Add more countries as needed...
			};
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
			ViewBag.CountryPhoneCodes = countryPhoneCodes;

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
						Phone_Country_Code = model.Phone_Country_Code,
						First_Name = model.First_Name.Trim(),
						Last_Name = model.Last_Name.Trim(),
						Is_Active = model.Is_Active,
						ApplicationUserId = newUser.Id // Link to ApplicationUser
					};

					// Save the Customer in the database via the repository
					_customerRepository.Add(newCustomer);
					_customerRepository.Save();

					//Assign Role for the new Customer
					await _userManager.AddToRoleAsync(newUser, "Admin");
					// Automatically sign the user in after registration
					await _signInManager.SignInAsync(newUser, isPersistent: false);

					return RedirectToAction("Index", "Home"); // Redirect to the home page upon successful registration
				}

				// Handle errors
				foreach (var error in result.Errors)
				{
					switch (error.Code)
					{
						case "DuplicateUserName":
						case "InvalidUserName":
							ModelState.AddModelError(nameof(model.Email), error.Description);
							break;

						case "PasswordTooShort":
						case "PasswordRequiresNonAlphanumeric":
						case "PasswordRequiresDigit":
						case "PasswordRequiresUpper":
							ModelState.AddModelError(nameof(model.Password), error.Description);
							break;

						default:
							// If the error doesn't map to a specific field, add it as a general error
							ModelState.AddModelError("", error.Description);
							break;
					}
				}
			}

			ViewBag.CountryPhoneCodes = countryPhoneCodes;
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
