using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Fashion_Flex.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;
		private readonly ICustomerRepository _customerRepository;

		public ProductController(IProductRepository _productRepository, IOrderRepository _orderRepository, IOrderItemRepository _orderItemRepository, ICustomerRepository _customerRepository)
		{
			this._productRepository = _productRepository;
			this._orderRepository = _orderRepository;
			this._orderItemRepository = _orderItemRepository;
			this._customerRepository = _customerRepository;
		}
		public IActionResult Index(int pageIndex = 1, int pageSize = 8, string sortOrder = "", string category = "")
		{
			// Track the current sorting order
			ViewData["CurrentSort"] = sortOrder;
			// Keep track of the selected category
			ViewData["CurrentCategory"] = category;
			// List of current Categories
			var categories = _productRepository.GetCategories();
			ViewData["Categories"] = categories;

			// Assign sorting parameters for the view (already done previously)
			ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";
			ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";

			var paginatedProducts = _productRepository.GetAllPaginated(pageIndex, pageSize, sortOrder, category);
			return View(paginatedProducts);
		}


		[HttpGet]
		public IActionResult AddToCart(int selectedProductId)
		{
			//if the user isnt loged in redirecting him to login page
			if (!User.Identity.IsAuthenticated)
			{
				return BadRequest();
			}

			var selectedProduct = _productRepository.GetById(selectedProductId);
			var customer = _customerRepository.GetByApplicationUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var pendingOrder = _orderRepository.GetCustomerCurrOrder(customer.Id);
			//if user have any order else create one
			if (pendingOrder != null)
			{
				//if user added this item before, change only its quantity
				if (_orderItemRepository.OrderItemExist(pendingOrder.Id, selectedProduct.Id))
				{
					var orderItem = _orderItemRepository.GetByProductAndOrderId(pendingOrder.Id, selectedProduct.Id);
					orderItem.Quantity++;
					_orderItemRepository.Update(orderItem);
					_orderItemRepository.Save();
				}
				//else create new order item
				else
				{
					var newOrder_Item = new Order_Item
					{
						Order_Id = pendingOrder.Id,
						Product_Id = selectedProduct.Id,
						Quantity = 1
					};

					_orderItemRepository.Add(newOrder_Item);
					_orderItemRepository.Save();
				}

				//update total amount of the order price
				pendingOrder.Total_Amount += selectedProduct.Price;
				_orderRepository.Update(pendingOrder);
				_orderItemRepository.Save();
			}
			else
			{
				var newOrder = new Order
				{
					Customer_Id = customer.Id,
					Order_Date = DateTime.Now,
					Order_Status = "Pending",
					Shipping_Address = customer.Address,
					Total_Amount = selectedProduct.Price,
					Tracking_Number = 0
				};
				_orderRepository.Add(newOrder);
				_orderItemRepository.Save();

				//if user added this item before change only its quantity
				if (_orderItemRepository.OrderItemExist(newOrder.Id, selectedProduct.Id))
				{
					var orderItem = _orderItemRepository.GetByProductAndOrderId(newOrder.Id, selectedProduct.Id);
					orderItem.Quantity++;
					_orderItemRepository.Update(orderItem);
					_orderItemRepository.Save();
				}
				//else create new order item
				else
				{
					var newOrder_Item = new Order_Item
					{
						Order_Id = newOrder.Id,
						Product_Id = selectedProduct.Id,
						Quantity = 1
					};

					_orderItemRepository.Add(newOrder_Item);
					_orderItemRepository.Save();
				}
			}
			return RedirectToAction("Index", "Product");
		}
	}
}
