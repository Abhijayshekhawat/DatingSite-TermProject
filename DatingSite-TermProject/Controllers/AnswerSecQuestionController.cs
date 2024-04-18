using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class AnswerSecQuestionController : Controller
    {
        public class SecurityQuestion
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        [HttpGet]
        public IActionResult AnswerSecQuestion()
        {
            ViewBag.Token = HttpContext.Request.Query["token"].ToString();
            if (HttpContext.Request.Cookies.TryGetValue("SecurityQuestion", out string encryptedQuestion))
            {
                var decryptedQuestion = EncryptionHelper.Decrypt(encryptedQuestion);
                var security = JsonSerializer.Deserialize<SecurityQuestion>(decryptedQuestion);
                ViewBag.Question = security.Question;
            }
            return View("~/Views/Home/AnswerSecQuestion.cshtml");
        }

        [HttpPost]
        public IActionResult AnswerSecQuestion(PrivateUserInfoModel model)
        {
            if (string.IsNullOrEmpty(Request.Form["Answer"].ToString()))
            {
                ViewBag.ErrorMessage = "Please enter your answer to the Security Question.";
                return View("~/Views/Home/AnswerSecQuestion.cshtml");
            }
            if (Request.Cookies.TryGetValue("Token", out string encryptedToken))
            {
                string decryptedToken = EncryptionHelper.Decrypt(encryptedToken);
                string tokenFromLink = Request.Form["token"];
                string decryptedTokenFromLink = EncryptionHelper.Decrypt(tokenFromLink);

                if (!string.IsNullOrEmpty(decryptedTokenFromLink))
                {
                    tokenFromLink = WebUtility.UrlDecode(decryptedTokenFromLink);
                }

                if (decryptedToken == decryptedTokenFromLink)
                {
                    if (HttpContext.Request.Cookies.TryGetValue("SecurityQuestion", out string encryptedQuestion))
                    {
                        var decryptedQuestion = EncryptionHelper.Decrypt(encryptedQuestion);
                        var security = JsonSerializer.Deserialize<SecurityQuestion>(decryptedQuestion);
                        if (security.Answer == Request.Form["Answer"].ToString())
                        {
                            return View("~/Views/Home/ResetPassword.cshtml");
                        } else
                        {
                            ViewBag.ErrorMessage = "Your answer was incorrect";
                            return View("~/Views/Home/AnswerSecQuestion.cshtml");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "A problem occurred Please go through the reset process again.";
                        return View("~/Views/Home/Login.cshtml");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "A problem occurred Please go through the reset process again.";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the reset process again.";
                return View("~/Views/Home/Login.cshtml");
            }
        }
    }
}
