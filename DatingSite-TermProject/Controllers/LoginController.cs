using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using DatingSiteCoreAPI;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Newtonsoft.Json.Linq;

namespace DatingSite_TermProject.Controllers
{
    public class LoginController : Controller
    {

        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";

        public IActionResult Login()
        {
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();

            privateinfo.PrivateUsername = Request.Form["Username"].ToString();
            privateinfo.Password = Request.Form["Password"].ToString();

            // Serialize an Account object into a JSON string.
            var jsonPayload =  JsonSerializer.Serialize(privateinfo);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/Login");
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

                    // **
                    // need to change this to going into the view with two step verification
                    //**
                    ViewBag.ErrorMessage = "The customer was successfully saved to the database.";
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            return View("~/Views/Home/Login.cshtml");
        }
    }
}
