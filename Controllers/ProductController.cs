using Fashion_Flex.IRepositories;
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
		public IActionResult Index()
		{
			return View(_productRepository.GetAll());
		}

		[HttpPost]
		public IActionResult AddToCart(int selectedProductId)
		{			
			//if the user isnt loged in redirecting him to login page
			if (!User.Identity.IsAuthenticated)
			{
				return BadRequest();
			}

			var customer = _customerRepository.GetByApplicationUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var pendingOrder = _orderRepository.GetCustomerCurrOrder(customer.Id);
			//if user have any order else create one
			if (pendingOrder != null)
			{
				//if user added this item before change only its quantity
				if (_orderItemRepository.OrderItemExist(pendingOrder.Id, selectedProductId))
				{
					var orderItem = _orderItemRepository.GetByProductAndOrderId(pendingOrder.Id, selectedProductId);
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
						Product_Id = selectedProductId,
						Quantity = 1
					};

					_orderItemRepository.Add(newOrder_Item);
					_orderItemRepository.Save();
				}
			}
			else
			{
				var newOrder = new Order
				{
					Customer_Id = customer.Id,					
					Order_Date = DateTime.Now,
					Order_Status = "Pending",
					Shipping_Address = customer.Address,
					Total_Amount = 0,
					Tracking_Number = 0
				};
				_orderRepository.Add(newOrder);
				_orderItemRepository.Save();

				//if user added this item before change only its quantity
				if (_orderItemRepository.OrderItemExist(newOrder.Id, selectedProductId))
				{
					var orderItem = _orderItemRepository.GetByProductAndOrderId(newOrder.Id, selectedProductId);
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
						Product_Id = selectedProductId,
						Quantity = 1
					};

					_orderItemRepository.Add(newOrder_Item);
					_orderItemRepository.Save();
				}
			}
			return Ok();
		}
	}
}
