using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult PayPage()
        {
            ViewBag.Total = 20.00m; 
            ViewBag.PublishableKey = _configuration["Stripe:PublishableKey"];
            return View();
        }
       
        [HttpPost]
        public IActionResult Pay([FromForm] PaymentCreateVm vm)
        {
            if (vm == null || string.IsNullOrEmpty(vm.StripeToken) || vm.Amount <= 0)
            {
                return BadRequest(new { Message = "Geçersiz ödeme verisi." });
            }

            var payment = _paymentService.ProcessStripePayment(vm);

            if (payment.PaymentStatus == "Success")
            {
                return RedirectToAction(nameof(Success));
            }
            else
            {
                return RedirectToAction(nameof(Failed));
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PaymentSuccess(string? paymentIntentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

          
            var success = await _paymentService.ConfirmPaymentAndAddPaidBooks(userId, paymentIntentId);

            if (success)
            {
                TempData["SuccessMessage"] = "Ödəniş uğurla tamamlandı! Aldığınız kitabları profilinizdə görə bilərsiniz.";
                return RedirectToAction("Orders", "Account");
            }
            else
            {
                TempData["ErrorMessage"] = "Ödəniş təsdiqi alınmadı və ya bir səhv baş verdi.";
                return RedirectToAction("ViewCart", "Cart");
            }
        }

    }
}
