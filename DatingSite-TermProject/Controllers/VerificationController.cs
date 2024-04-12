using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class VerificationController : Controller
    {
        public IActionResult Verification()
        {
            if (Request.Cookies.TryGetValue("VerCode", out string encryptedCookie))
            {
                string decryptedCode = EncryptionHelper.Decrypt(encryptedCookie);
                string userEnteredCode = Request.Form["VerificationCode"].ToString();
                if (decryptedCode == userEnteredCode)
                {
                    ViewBag.ErrorMessage = "The codes are the same.";
                    return View("~/Views/Main/Dashboard.cshtml");

                }
                else
                {
                    ViewBag.ErrorMessage = "The codes are not the same.";
                    return View("~/Views/Home/Verification.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the login process again.";
                return View("~/Views/Home/Verification.cshtml");
            }
        }
    }
}
