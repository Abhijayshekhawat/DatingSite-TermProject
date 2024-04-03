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



// creating cookies using Microsoft.AspNetCore.Http class ---> use request.cookie to create one
// 

namespace DatingSite_TermProject.Controllers
{
    public class CreateAccountController : Controller
    {

        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        // string CreateAccountAPI_Url = "" ;     // have your URL then we comment and uncomment off whenever who uses it.

        [HttpPost]
        public IActionResult CreateAccount()
        {
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();  
            // get the data from the form / model PrivateUserInfoModel  
            privateinfo.FirstName = Request.Form["FirstName"].ToString();
            privateinfo.LastName = Request.Form["LastName"].ToString();
            privateinfo.Email = Request.Form["Email"].ToString();
            privateinfo.PrivateUsername = Request.Form["Username"].ToString();
            privateinfo.Password = Request.Form["Password"].ToString();
            
            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(privateinfo);
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
                    ViewBag.ErrorMessage = "The customer was successfully saved to the database.";
                    return View("~/Views/Profile/Profile.cshtml");

                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Home/CreateAccount.cshtml");
        }
    }
}
