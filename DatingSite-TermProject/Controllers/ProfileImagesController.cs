using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DatingSiteCoreAPI;
using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies
using Utilities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DatingSite_TermProject.Controllers
{
    public class ProfileImagesController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        ViewManagement view = new ViewManagement();
        [HttpPost]
        public IActionResult ProfileImages()
        {
            ProfileImagesModel imageProfile = new ProfileImagesModel();
            UserProfileModel userProfile = new UserProfileModel();
            
            string savedUsername = Request.Cookies["Username"].ToString();
            // get the data from the form / model UserProfileModel  
            imageProfile.PrivateId = userProfile.getPrivateId(savedUsername); // get method in userprofilemodel --> get id by username ?cookie? response?

            imageProfile.Image1 = Request.Form["Image1"].ToString();
            imageProfile.Image2 = Request.Form["Image2"].ToString();
            imageProfile.Image3 = Request.Form["Image3"].ToString();
            imageProfile.Image4 = Request.Form["Image4"].ToString();
            imageProfile.Image5 = Request.Form["Image5"].ToString();
            

            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(imageProfile);
            try

            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddUserImages");
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
                    ProfileSecQuestionModel security = view.GetQuestions(savedUsername);
                    return View("~/Views/Profile/ProfileSecQuestion.cshtml", security);

                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Profile/ProfileImages.cshtml");
        }
    }
}
