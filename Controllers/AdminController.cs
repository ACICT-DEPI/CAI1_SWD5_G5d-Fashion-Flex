using Fashion_Flex.IRepositories;
using Fashion_Flex.IRepositories.Repository;
using Fashion_Flex.Models;
using Fashion_Flex.Repositories;
using Fashion_Flex.Repository;
using Fashion_Flex.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Product = Fashion_Flex.Models.Product;

namespace Fashion_Flex.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IProductRepository _productRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
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
		public AdminController(ICustomerRepository _customerRepository, UserManager<ApplicationUser> _userManager,
			   SignInManager<ApplicationUser> _signInManager, IProductRepository _productRepository, IOrderRepository _orderRepository)
		{
			this._customerRepository = _customerRepository;
			this._userManager = _userManager;
			this._signInManager = _signInManager;
			this._productRepository = _productRepository;
			this._orderRepository = _orderRepository;
		}
		//Dashboard
		public IActionResult Index()
		{
			ViewData["currTab"] = "dashboard";
			//Customer Stats
			ViewData["totalCustomers"] = _customerRepository.GetAll().Count();
			ViewData["newCustomerMonthly"] = _customerRepository.GetAll().Count(o => o.Account_Creation_Date.Month == DateTime.Now.Month &&
																				 o.Account_Creation_Date.Year == DateTime.Now.Year);
			ViewData["CustomerDemographics"] = _customerRepository.GetAll()
																.GroupBy(c => c.City)
																.Select(g => new { City = g.Key, Count = g.Count() })
																.ToList();

			//Product Stats
			ViewData["totalProducts"] = _productRepository.GetAll().Count();

			//Order Stats
			ViewData["totalOrders"] = _orderRepository.GetAll().Count();
			ViewData["totalRevenue"] = _orderRepository.GetAll().Sum(o => o.Total_Amount);
			var pendingOrders = _orderRepository.GetAll().Count(o => o.Order_Status == "Pending");
			var shippedOrders = _orderRepository.GetAll().Count(o => o.Order_Status == "Completed");
			var deliveredOrders = _orderRepository.GetAll().Count(o => o.Order_Status == "Delivered");

			ViewData["OrderStatusData"] = new int[] { pendingOrders, shippedOrders, deliveredOrders };
			return View();
		}


		#region customer actions
		//Customer Actions
		[HttpGet]
		public IActionResult Customers()
		{
			ViewData["currTab"] = "customers";
			var customers = _customerRepository.GetAll();
			return View(customers);
		}

		[HttpGet]
		public IActionResult CreateCustomer()
		{
			ViewData["currTab"] = "customers";
			ViewData["Roles"] = new List<string> { "Admin", "Customer" };
			ViewBag.CountryPhoneCodes = countryPhoneCodes;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(RegisterViewModel model, string role)
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
					var newCustomer = new Models.Customer
					{
						Account_Creation_Date = DateTime.Now,
						Street_Name = model.Street_Name,
						Building_No = model.Building_No,
						City = model.City,
						Governorate = model.Governorate,
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
					await _userManager.AddToRoleAsync(newUser, role);

					return RedirectToAction("Customers", "Admin");
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

			ViewData["currTab"] = "customers";
			ViewBag.CountryPhoneCodes = countryPhoneCodes;
			ViewData["Roles"] = new List<string> { "Admin", "Customer" };
			return View(model);
		}

		[HttpGet]
		public IActionResult EditCustomer(int id)
		{
			ViewData["currTab"] = "customers";
			ViewBag.CountryPhoneCodes = countryPhoneCodes;
			var customer = _customerRepository.GetById(id);

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
				Street_Name = customer.Street_Name,
				Building_No = customer.Building_No,
				City = customer.City,
				Governorate = customer.Governorate,
				Date_Of_Birth = customer.Date_Of_Birth,
				Phone_Country_Code = customer.Phone_Country_Code,
				Phone_Number = customer.Phone_Number,
				Is_Active = customer.Is_Active
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCustomer(ProfileViewModel model)
		{
			if (ModelState.IsValid)
			{
				var customer = _customerRepository.GetById(model.Id);

				if (customer == null)
				{
					return NotFound();
				}

				// Update customer details
				customer.First_Name = model.First_Name;
				customer.Last_Name = model.Last_Name;
				customer.Email = model.Email;
				customer.Street_Name = model.Street_Name;
				customer.Building_No = model.Building_No;
				customer.City = model.City;
				customer.Governorate = model.Governorate;
				customer.Date_Of_Birth = model.Date_Of_Birth;
				customer.Phone_Country_Code = model.Phone_Country_Code;
				customer.Phone_Number = model.Phone_Number;
				customer.Is_Active = model.Is_Active;

				_customerRepository.Update(customer);
				_customerRepository.Save();

				return RedirectToAction("Customers");
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var customer = _customerRepository.GetById(id);
			var user = await _userManager.FindByEmailAsync(customer.Email);
			_customerRepository.Delete(id);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
			}
			_customerRepository.Save();
			return RedirectToAction("Customers");
		}
		#endregion

		#region Product Actions
		//Product Actions
		[HttpGet]
		public IActionResult Products()
		{
			ViewData["currTab"] = "products";
			var products = _productRepository.GetAll();

			return View(products);
		}
		[HttpGet]
		public IActionResult CreateProduct()
		{
			ViewData["currTab"] = "products";
			return View();
		}
		[HttpPost]
		public IActionResult CreateProduct(Product model, IFormFile image)
		{
			// Clear the ModelState entry for the Image & Color
			ModelState.Remove(nameof(Product.Image));
			ModelState.Remove(nameof(Product.Color));

			if (ModelState.IsValid)
			{
				if (image != null && image.Length > 0)
				{
					// Save the image to the server
					var imagePath = Path.Combine("wwwroot/images/", image.FileName);

					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						image.CopyTo(stream);
					}

					// Update the image filename in the database					
					model.Image = image.FileName;
				}

				model.Added_Date = DateTime.Now;
				model.Color = "defualt";
				model.Discount = 0;
				_productRepository.Add(model);
				_productRepository.Save();
				ViewData["currTab"] = "products";
				return RedirectToAction("Products", "Admin");
			}

			ViewData["currTab"] = "products";
			return View("CreateProduct", model);
		}
		[HttpGet]
		public IActionResult EditProduct(int id)
		{
			ViewData["currTab"] = "products";
			var product = _productRepository.GetById(id);

			return View(product);
		}
		[HttpPost]
		public IActionResult EditProduct(Product editedProduct, IFormFile image)
		{
			var oldProduct = _productRepository.GetById(editedProduct.Id);
			if (ModelState.IsValid)
			{
				if (image != null && image.Length > 0)
				{
					// Save the image to the server
					var imagePath = Path.Combine("wwwroot/images/", image.FileName);

					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						image.CopyTo(stream);
					}

					oldProduct.Image = image.FileName;
				}

				oldProduct.Name = editedProduct.Name;
				oldProduct.Description = editedProduct.Description;
				oldProduct.Price = editedProduct.Price;
				oldProduct.Available_Quantity = editedProduct.Available_Quantity;
				oldProduct.Category = editedProduct.Category;
				oldProduct.type = editedProduct.type;
				oldProduct.Discount = editedProduct.Discount;
				oldProduct.Color = editedProduct.Color;

				_productRepository.Update(oldProduct);
				_productRepository.Save();
				ViewData["currTab"] = "products";
				return RedirectToAction("Products");
			}

			ViewData["currTab"] = "products";
			return View("EditProduct", editedProduct);
		}
		[HttpGet]
		public IActionResult DeleteProduct(int id)
		{
			_productRepository.Delete(id);
			_productRepository.Save();
			return RedirectToAction("Products");
		}
		#endregion


		#region Orders
		//Orders Actions
		[HttpGet]
		public IActionResult Orders(string status = "All")
		{
			// Retrieve all orders from the service
			var orders = _orderRepository.GetAll();

			// Filter orders by status if not "All"
			if (status != "All")
			{
				orders = orders.Where(o => o.Order_Status == status).ToList();
			}

			// Calculate total amount of filtered orders
			ViewBag.OrdersTotalAmount = orders.Sum(o => o.Total_Amount);

			// Pass the selected status to the view for keeping the dropdown value selected
			ViewBag.SelectedStatus = status;

			return View(orders);
		}

		[HttpGet]
		public IActionResult ViewOrder(int id)
		{
			ViewData["currTab"] = "orders";

            Order order = _orderRepository.GetOrderById(id);

            return View(order);
		}
		#endregion
	}
}
