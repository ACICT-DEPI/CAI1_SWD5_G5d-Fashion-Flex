﻿using Fashion_Flex.IRepositories;
using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
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

			//if user have any pending cart (add to it), or else create new one
			if (pendingOrder != null)
			{
				//if item exists, just change its quantity
				if (_orderItemRepository.OrderItemExist(pendingOrder.Id, selectedProduct.Id))
				{
					IncreaseSelectedItemQuantity(pendingOrder, selectedProduct.Id);
				}
				//else create new item
				else
				{
					CreateNewItem(pendingOrder, selectedProduct.Id);
				}

				//update total amount of the order price
				updateTotalAmount(pendingOrder, selectedProduct);
			}
			else
			{
				//Create new cart
				var newOrder = createNewCart(customer, selectedProduct);

				//add new item to that cart
				CreateNewItem(newOrder, selectedProduct.Id);

			}
			return Ok("Order is added successfuly!");
		}


		//---------
		private void IncreaseSelectedItemQuantity(Order pendingOrder, int selectedProductId)
		{
			var orderItem = _orderItemRepository.GetByProductAndOrderId(pendingOrder.Id, selectedProductId);
			orderItem.Quantity++;
			_orderItemRepository.Update(orderItem);
			_orderItemRepository.Save();
		}

		private void CreateNewItem(Order order, int selectedProductId)
		{
			var newOrder_Item = new Order_Item
			{
				Order_Id = order.Id,
				Product_Id = selectedProductId,
				Quantity = 1
			};

			_orderItemRepository.Add(newOrder_Item);
			_orderItemRepository.Save();
		}

		private Order createNewCart(Customer customer, Product selectedProduct)
		{
			var newOrder = new Order
			{
				Customer_Id = customer.Id,
				Order_Date = DateTime.Now,
				Order_Status = "Pending",
				Shipping_Address = customer.Street_Name + customer.Building_No + customer.City + customer.Governorate,
				Total_Amount = selectedProduct.Price,
			};
			_orderRepository.Add(newOrder);
			_orderItemRepository.Save();

			return newOrder;
		}

		private void updateTotalAmount(Order pendingOrder, Product selectedProduct)
		{
			pendingOrder.Total_Amount += selectedProduct.Price;
			_orderRepository.Update(pendingOrder);
			_orderItemRepository.Save();
		}
	}
}
