using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Fashion_Flex.Controllers
{

    public class StripeController : Controller
    {
        private readonly IConfiguration _configuration;

        public StripeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


		//When you hit submit button in frontend
		//This Function is fired up and calls the request to stripe
		[HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentIntentCreateRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(request.Amount * 100), // Amount in cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

			//the Stripe SDK is used to create the PaymentIntent
			var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);


            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }
    }

    public class PaymentIntentCreateRequest
    {
        public decimal Amount { get; set; }
    }

}