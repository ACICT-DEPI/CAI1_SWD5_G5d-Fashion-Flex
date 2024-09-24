using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        

        public IActionResult OrderConfirmation(int paymentid, int orderid)
        {
            _paymentRepository.updateOrderStates(paymentid);
            _orderRepository.updateOrderStates(orderid);
            Order order = _orderRepository.GetOrderById(orderid);
            Payment payment = _paymentRepository.GetById(paymentid);
            _paymentRepository.Save();
            _orderRepository.Save();
            if(order.Order_Status.ToLower() == "complete" && payment.Payment_Status.ToLower() == "paid")
            {
                return View("Successful", orderid);
            }
            else
            {
                return View("Faild", orderid);
            }
        }



    }
}

