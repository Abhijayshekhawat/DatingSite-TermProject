using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace DatingSite_TermProject.Controllers
{
    public class ProfileController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        ViewManagement view = new ViewManagement();
        [HttpGet]
        public IActionResult Profile()
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            UserProfileModel profile = view.GetProfile(savedUsername);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername);
            ViewBag.CommitmentTypes = new List<string> { "Friends", "Short-Term Relationship", "Long-Term Relationship", "Open-Relationship", "Marriage" };
            ViewBag.BookGenres = new List<string> { "Fiction", "Non-Fiction", "Mystery", "Sci-Fi", "Biography" };
            ViewBag.MovieGenres = new List<string> { "Action", "Comedy", "Drama", "Fantasy", "Horror" };
            if (profile != null)
            {
                return View("~/Views/Profile/Profile.cshtml", profile);
            }
            else
            {
                // Handle case where no profile is found
                return View("~/Views/Profile/Profile.cshtml", new UserProfileModel());
            }
        }
        [HttpPost]
        public IActionResult Profile(UserProfileModel profileModel)
        {
            ViewBag.CommitmentTypes = new List<string> { "Friends", "Short-Term Relationship", "Long-Term Relationship", "Open-Relationship", "Marriage" };
            ViewBag.BookGenres = new List<string> { "Fiction", "Non-Fiction", "Mystery", "Sci-Fi", "Biography" };
            ViewBag.MovieGenres = new List<string> { "Action", "Comedy", "Drama", "Fantasy", "Horror" };
            UserProfileModel userProfile = new UserProfileModel();

            string savedUsername = Request.Cookies["Username"].ToString();
            // get the data from the form / model UserProfileModel  
            userProfile.PrivateId = userProfile.getPrivateId(savedUsername); // get method in userprofilemodel --> get id by username ?cookie? response?
            userProfile.Age = Int32.Parse(Request.Form["Age"].ToString());
            userProfile.Height = Request.Form["Height"].ToString();
            userProfile.Weight = Request.Form["Weight"].ToString();
            userProfile.ProfilePhotoURL = Request.Form["ProfilePhotoURL"].ToString();
            userProfile.City = Request.Form["City"].ToString();
            userProfile.State = Request.Form["State"].ToString();
            userProfile.Tagline = Request.Form["Tagline"].ToString();
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
            userProfile.AdditionalInterests = Request.Form["AdditionalInterests"].ToString();
            userProfile.Dealbreaker = Request.Form["Dealbreaker"].ToString();
            userProfile.Biography = Request.Form["Biography"].ToString();

            var jsonPayload = JsonSerializer.Serialize(userProfile);
            try

            {
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
                    ProfileImagesModel imageProfile = view.GetImageGallery(savedUsername);
                    return View("~/Views/Profile/ProfileImages.cshtml", imageProfile);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the profile to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Profile/Profile.cshtml", userProfile);
        }
   
    }
}