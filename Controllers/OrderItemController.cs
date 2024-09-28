using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Fashion_Flex.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

       /* public IActionResult Index()
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

            _orderItemRepository.Delete(id);
            _orderItemRepository.Save();

            return RedirectToAction("Index");
        }*/
    }
}
