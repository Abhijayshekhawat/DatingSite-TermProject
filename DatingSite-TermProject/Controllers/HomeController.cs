using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Request.Cookies.TryGetValue("fastLogin", out string encryptedCookie))
            {
                string decryptedCookie = EncryptionHelper.Decrypt(encryptedCookie);
                PrivateUserInfoModel userDetails = JsonSerializer.Deserialize<PrivateUserInfoModel>(decryptedCookie);
                ViewBag.Username = userDetails.PrivateUsername;
                ViewBag.Pwd = userDetails.Password;
            }
            return View("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CreateAccount()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
