using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using Microsoft.AspNetCore.Http;

namespace DatingSite_TermProject.Controllers
{
    public class ProfileController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";

        [HttpPost]
        public IActionResult Profile()
        {

           
            UserProfileModel userProfile = new UserProfileModel();

            string savedUsername = Request.Cookies["Username"].ToString() ;
            // get the data from the form / model UserProfileModel  
            userProfile.PrivateId = userProfile.getPrivateId(savedUsername); // get method in userprofilemodel --> get id by username ?cookie? response?
            userProfile.Age = Int32.Parse(Request.Form["Age"].ToString());
            userProfile.Height = Request.Form["Height"].ToString();
            userProfile.Weight = Request.Form["Weight"].ToString();
            userProfile.ProfilePhotoURL = Request.Form["ProfilePhotoURL"].ToString();
            userProfile.City = Request.Form["City"].ToString();
            userProfile.State = Request.Form["State"].ToString();
            userProfile.Description = Request.Form["Description"].ToString();
            userProfile.Occupation = Request.Form["Occupation"].ToString();

            userProfile.Interests = Request.Form["Interests"].ToString();

            userProfile.FavoriteCuisine = Request.Form["Cuisines"].ToString();

            userProfile.FavouriteQuote = Request.Form["FavouriteQuote"].ToString();
            userProfile.Goals = Request.Form["Goals"].ToString();
            userProfile.CommitmentType = Request.Form["Commitment"].ToString();

            userProfile.FavoriteMovieGenre = Request.Form["MovieGenres"].ToString();
            userProfile.FavoriteBookGenre = Request.Form["BookGenres"].ToString();
            userProfile.Address = Request.Form["Address"].ToString();
            userProfile.PhoneNumber = Request.Form["PhoneNumber"].ToString();
            userProfile.FavoriteMovie = Request.Form["FavouriteMovie"].ToString();
            userProfile.FavoriteBook = Request.Form["FavouriteBook"].ToString();
            userProfile.FavoriteRestaurant = Request.Form["FavouriteRestaurant"].ToString();
            userProfile.Dislikes = Request.Form["Dislikes"].ToString();
            userProfile.IsVisible = Request.Form["ProfileVisibility"].ToString() == "Yes";


            // combine these into one since having two seperate table is hard to insert with
            // web api --> mvc core




            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(userProfile);
            try

            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddUserInfo");
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
                    ViewBag.ErrorMessage = "User's Information was successfully added";
                    return View("~/Views/Profile/ProfileImages.cshtml");

                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View();
        }
    }
}
