using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class ResetPasswordController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";

        public IActionResult ResetPassword()
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

                }
                else
                {
                    ViewBag.ErrorMessage = "A problem occurred while resetting your password. The data wasn't recorded.";

                }
                return View("~/Views/Home/ResetPassword.cshtml");

                // Now you have access to the user details
                // Do whatever you need with them
            }
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the reset process again.";
                return View("~/Views/Home/Login.cshtml");
                // Cookie not found or decryption failed
                // Handle the case accordingly
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
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddPrivateInfo");
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
