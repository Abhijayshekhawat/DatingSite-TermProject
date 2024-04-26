using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Logout()
        {
            if (Request.Cookies.TryGetValue("fastLogin", out string encryptedCookie))
            {
                string decryptedCookie = EncryptionHelper.Decrypt(encryptedCookie);
                PrivateUserInfoModel userDetails = JsonSerializer.Deserialize<PrivateUserInfoModel>(decryptedCookie);
                ViewBag.Username = userDetails.PrivateUsername;
                ViewBag.Pwd = userDetails.Password;
            }
            var decryptedAuth = EncryptionHelper.Decrypt(Request.Cookies["isValid"].ToString());
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMilliseconds(1);
            string SecretCookie = EncryptionHelper.Encrypt(decryptedAuth);
            HttpContext.Response.Cookies.Append("isValid", SecretCookie, options);
            return View("~/Views/Home/Login.cshtml");

        }

    }

}
