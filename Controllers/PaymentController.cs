using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Fashion_Flex.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        public PaymentController(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }
        
        public IActionResult Index()
        {
            var payment = _paymentRepository.GetAll();
            return View(payment);
        }
        public IActionResult NewPayment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveNewPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.Add(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index");
            }

            return View("New", payment);
        }
        public IActionResult Details(int id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }
        [HttpPost]
        public IActionResult SaveEditedPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _paymentRepository.Update(payment);
                _paymentRepository.Save();
                return RedirectToAction("Index");
            }

            return View("Edit", payment);
        }
        public IActionResult Delete(int id)
        {
            var payment = _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            _paymentRepository.Delete(id);
            _paymentRepository.Save();

            return RedirectToAction("Index");
        }

        public IActionResult ChangeOrderStatus(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order with id:{orderId} is not found");
            }
            order.Order_Status = "Paid/Completed";
            _orderRepository.Save();
            return View(order);
        }



    }
}

