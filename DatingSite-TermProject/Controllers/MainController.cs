using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }
        }
    }
}
