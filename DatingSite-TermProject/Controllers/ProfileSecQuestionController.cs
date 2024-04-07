using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DatingSiteCoreAPI;
using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http; // need for cookies

namespace DatingSite_TermProject.Controllers
{
    public class ProfileSecQuestionController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        // string CreateAccountAPI_Url = "" ;     // have your URL then we comment and uncomment off whenever who uses it.

        [HttpPost]
        public IActionResult ProfileSecQuestion()
        {
            ProfileSecQuestionModel secQuestion = new ProfileSecQuestionModel();
            UserProfileModel userProfile = new UserProfileModel();

            string savedUsername = Request.Cookies["Username"].ToString();
            // get the data from the form / model UserProfileModel  
            secQuestion.PrivateId = userProfile.getPrivateId(savedUsername); // get method in userprofilemodel --> get id by username ?cookie? response?

            secQuestion.Question_One = Request.Form["Question_One"].ToString();
            secQuestion.Answer_One = Request.Form["Answer_One"].ToString();
            secQuestion.Question_Two = Request.Form["Question_Two"].ToString();
            secQuestion.Answer_Two = Request.Form["Answer_Two"].ToString();
            secQuestion.Question_Three = Request.Form["Question_Three"].ToString();
            secQuestion.Answer_Three = Request.Form["Answer_Three"].ToString();


            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(secQuestion);
            try

            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddUserSecurityQuestions");
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
                    ViewBag.ErrorMessage = "UserInfo was successfully added";
                    return View("~/Views/Main/Dashboard.cshtml");

                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Profile/ProfileSecQuestion.cshtml");
        }
    }
}
