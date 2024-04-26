using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies



// creating cookies using Microsoft.AspNetCore.Http class ---> use request.cookie to create one
// 

namespace DatingSite_TermProject.Controllers
{
    public class CreateAccountController : Controller
    {

        string CreateAccountAPI_Url = "https://cis-iis2.temple.edu/Spring2024/CIS3342_tuh18229/WebAPITest/api/CreateAccount";
        
        [HttpPost]
        public IActionResult CreateAccount(PrivateUserInfoModel pUInfo)
        {
            if (Request.Form["Password"].ToString() != Request.Form["ConfirmPassword"].ToString())
            {
                ViewBag.ErrorMessage = "Passwords do not match";
                return View("~/Views/Home/CreateAccount.cshtml");
            }
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"{entry.Key} errors:");
                        foreach (var error in entry.Value.Errors)
                        {
                            Console.WriteLine($" - {error.ErrorMessage}");
                        }
                    }
                }
                return View("~/Views/Home/CreateAccount.cshtml", pUInfo);
            }
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();  
            // get the data from the form / model PrivateUserInfoModel  
            privateinfo.FirstName = Request.Form["FirstName"].ToString();
            privateinfo.LastName = Request.Form["LastName"].ToString();
            privateinfo.Email = Request.Form["Email"].ToString();
            privateinfo.PrivateUsername = Request.Form["PrivateUsername"].ToString();
            privateinfo.Password = EncryptionHelper.ComputeHash(Request.Form["Password"].ToString());
            
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
                    string auth = "Valid";
                    CookieOptions options = new CookieOptions();
                    string SecretCookie = EncryptionHelper.Encrypt(auth);
                    HttpContext.Response.Cookies.Append("isValid", SecretCookie, options);

                    Response.Cookies.Append("Username", privateinfo.PrivateUsername);
                    ViewBag.ErrorMessage = "The customer was successfully saved to the database.";
                    ViewBag.CommitmentTypes = new List<string> { "Friends", "Short-Term Relationship", "Long-Term Relationship", "Open-Relationship", "Marriage" };
                    ViewBag.BookGenres = new List<string> { "Fiction", "Non-Fiction", "Mystery", "Sci-Fi", "Biography" };
                    ViewBag.MovieGenres = new List<string> { "Action", "Comedy", "Drama", "Fantasy", "Horror" };
                    UserProfileModel profile = new UserProfileModel();
                    return View("~/Views/Profile/Profile.cshtml",profile);

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
