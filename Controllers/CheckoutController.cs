using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace shoppingstore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public CheckoutController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        private readonly IOrderItemRepository _orderItemRepository;
        public CheckoutController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool isCheckedOut = await _orderItemRepository.DoCheckout(model);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(Order order)
        {
            try
            {
                    order.Customer.First_Name = User.Identity.Name;
                    order.Order_Date = DateTime.Now;


                    _orderRepository.Add(order);
                    _orderRepository.Save();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);
                _orderRepository.Add(order)

                    return RedirectToAction("Complete",
                        new { id = order.Id });
            }
            catch
            {

                return View(order);
            }
        }
       
        public IActionResult Complete(int id)
        {
            bool isValid = true;
            Order order = _orderRepository.GetById(id);
            if (order == null)
            {
                isValid = false;
            }

            if (isValid)
            {
                ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}