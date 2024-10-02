using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fashion_Flex.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;

		}

        public IActionResult Index()
        {
            var orderItems = _orderItemRepository.GetAll();
            return View(orderItems);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Order_Item orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemRepository.Add(orderItem);
                _orderItemRepository.Save();
                return RedirectToAction("Index");
            }

            return View("New", orderItem);
        }

        public IActionResult Edit(int id)
        {
            var orderItem = _orderItemRepository.GetById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return View(orderItem);
        }

        [HttpPost]
        public IActionResult Edit(Order_Item orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemRepository.Update(orderItem);
                _orderItemRepository.Save();
                return RedirectToAction("Index");
            }

            return View("Edit", orderItem);
        }

        public IActionResult Delete(int id)
        {
            var orderItem = _orderItemRepository.GetById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.Order.Total_Amount -= orderItem.Product.Price * orderItem.Quantity;
            if(orderItem.Order.Total_Amount == 0)
            {
                _orderRepository.Delete(orderItem.Order.Id);
            }
            _orderItemRepository.Delete(id);
            _orderItemRepository.Save();

            return RedirectToAction("Index","Product");
        }

		[HttpPost]
		public IActionResult IncreaseQuantity(int orderItemId)
		{
			var orderItem = _orderItemRepository.GetById(orderItemId);
			if (orderItem != null)
			{
				orderItem.Quantity++;
				orderItem.Order.Total_Amount += orderItem.Product.Price;
				_orderItemRepository.Save();
			}
			return RedirectToAction("CheckOut", "Payment");
		}

		[HttpPost]
		public IActionResult DecreaseQuantity(int orderItemId)
		{
			var orderItem = _orderItemRepository.GetById(orderItemId);
			if (orderItem != null)
			{
                if (orderItem.Quantity > 1)
                {
					orderItem.Quantity--;
                    orderItem.Order.Total_Amount -= orderItem.Product.Price; 
					_orderItemRepository.Save();
				}
                else
                {
					orderItem.Order.Total_Amount -= orderItem.Product.Price;
					_orderItemRepository.Delete(orderItemId);
                    _orderItemRepository.Save();
                }
			}
			return RedirectToAction("CheckOut","Payment");
		}
	}
}
