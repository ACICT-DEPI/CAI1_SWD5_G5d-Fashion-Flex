using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fashion_Flex.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
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
        public ActionResult Checkout(decimal amount)
        {
            var payment = new Payment
            {
                Amount = amount
            };
            return View(payment);
        }

       
    }
}

