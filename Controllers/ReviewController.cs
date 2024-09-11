using Fashion_Flex.Models;
using Fashion_Flex.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fashion_Flex.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewController (IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public IActionResult Index()
        {
            var review = _reviewRepository.GetAll();
            return View(review);
        }

        public IActionResult NewReview()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveNewReview(Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewRepository.Add(review);
                _reviewRepository.Save();
                return RedirectToAction("Index");
            }

            return View("New", review);
        }
        public IActionResult Details(int id)
        {
            var review = _reviewRepository.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }
        [HttpPost]
        public IActionResult SaveEditedReview(Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewRepository.Update(review);
                _reviewRepository.Save();
                return RedirectToAction("Index");
            }

            return View("Edit", review);
        }
        public IActionResult Delete(int id)
        {
            var review = _reviewRepository.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.Delete(id);
            _reviewRepository.Save();

            return RedirectToAction("Index");
        }


    }
}

