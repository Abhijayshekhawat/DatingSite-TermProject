using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class AnswerSecQuestionController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        [HttpGet]
        public IActionResult AnswerSecQuestion()
        {
            ViewBag.Token = HttpContext.Request.Query["token"].ToString();
            return View("~/Views/Home/ResetPassword.cshtml");
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
                    if (Request.Cookies.TryGetValue("userDetails", out string encryptedCookie))
                    {
                        string decryptedCookie = EncryptionHelper.Decrypt(encryptedCookie);
                        // Deserialize the decryptedCookie to get back the user details
                        PrivateUserInfoModel userDetails = JsonSerializer.Deserialize<PrivateUserInfoModel>(decryptedCookie);
                        userDetails.Password = Request.Form["Password"].ToString();
                        bool result = SaveAccount(userDetails);
                        if (result)
                        {
                            ViewBag.ErrorMessage = "The details were successfully saved to the database.";
                            return View("~/Views/Home/Login.cshtml");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = "A problem occurred while resetting your password. The data wasn't recorded.";
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
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the reset process again.";
                return View("~/Views/Home/Login.cshtml");
            }
        }

        private bool SaveAccount(PrivateUserInfoModel user)
        {

            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(user);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/ResetPassword");
                request.Method = "POST";
                request.ContentLength = jsonPayload.Length;
                request.ContentType = "application/json";
                // Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonPayload);
                writer.Flush();
                writer.Close();
                // Read the data from the Web Response, which requires working with streams.
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                if (data == "true")
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                return false;
            }
        }
    }
}
