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
using DatingSite_TermProject.Controllers;

namespace DatingSite_TermProject.Controllers
{

    public class MatchesController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/MatchUp";


        [HttpPost]
        public IActionResult AddDateRequest()
        {

            string savedUsername = Request.Cookies["Username"].ToString();
            DateRequestModel dateRequestModel = new DateRequestModel();
            dateRequestModel.RequesterUsername = savedUsername;
            dateRequestModel.RequesteeId = int.Parse(Request.Form["RequesteeID"].ToString());

            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(dateRequestModel);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddNewDateRequest");
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
                   

                    UserProfileModel userProfile = new UserProfileModel();
                    List<CardsModel> Cardslist1 = PopulateProfiles(savedUsername);
                    ViewBag.ProfileImage = GetUserImage();
                    return View("~/Views/Main/Matches.cshtml", Cardslist1);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            List<CardsModel> Cardslist = PopulateProfiles(savedUsername);
            ViewBag.ProfileImage = GetUserImage();
            return View("~/Views/Main/Matches.cshtml", Cardslist);
        }
      
     
    

    public IActionResult DeleteMatches()
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            MatchesModel matchesModel = new MatchesModel();
            matchesModel.MatcherUsername = savedUsername;
            matchesModel.MatcherID = int.Parse(Request.Form["MatcherID"].ToString());
            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(matchesModel);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/DeleteMatch");
                request.Method = "DELETE";
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
                    UserProfileModel userProfile = new UserProfileModel();
                    List<CardsModel> Cardslist1 = PopulateProfiles(savedUsername);
                    ViewBag.ProfileImage = GetUserImage();
                    return View("~/Views/Main/Matches.cshtml", Cardslist1);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            List<CardsModel> Cardslist = PopulateProfiles(savedUsername);
            ViewBag.ProfileImage = GetUserImage();
            return View("~/Views/Main/Matches.cshtml", Cardslist);
        }



        public IActionResult Matches()
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            List<CardsModel> Cardslist = PopulateProfiles(savedUsername);
            ViewBag.ProfileImage = GetUserImage();
            return View("~/Views/Main/Matches.cshtml", Cardslist);
        }
        public List<CardsModel> PopulateProfiles(string savedUsername2)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetMatches";

            SqlParameter inputParameter1 = new SqlParameter("@UserName", savedUsername2);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt2 = ds.Tables[0];

            foreach (DataRow dr in dt2.Rows)
            {
                cards = new CardsModel(
                    dr["FirstName"].ToString(),
                    dr["LastName"].ToString(),

                    dr["ProfilePhotoURL"].ToString(),
                    dr["City"].ToString(),
                    dr["State"].ToString(),
                    dr["Tagline"].ToString(),
                    dr["Occupation"].ToString(),
                    dr["Interests"].ToString(),
                    dr["FavouriteCuisine"].ToString(),
                    dr["FavouriteQuote"].ToString(),
                    dr["Goals"].ToString(),
                    dr["CommitmentType"].ToString(),
                    dr["FavouriteMovieGenre"].ToString(),
                    dr["FavouriteBookGenre"].ToString(),
                    dr["Address"].ToString(),
                    dr["PhoneNumber"].ToString(),
                    dr["FavouriteMovie"].ToString(),
                    dr["FavouriteBook"].ToString(),
                    dr["FavouriteRestaurant"].ToString(),
                    dr["Dislikes"].ToString(),
                    dr["AdditionalInterests"].ToString(),
                    dr["Dealbreaker"].ToString(),
                    dr["Biography"].ToString(),
                    int.Parse(dr["Age"].ToString()),

                    dr["Height"].ToString(),
                    dr["Weight"].ToString(),
                    dr["Image1"].ToString(),
                    dr["Image2"].ToString(),
                    dr["Image3"].ToString(),
                    dr["Image4"].ToString(),
                    dr["Image5"].ToString(),
                    int.Parse(dr["PrivateId"].ToString())
                );

                Cardslist.Add(cards);
            }
            return Cardslist;

        }
        private String GetUserImage()
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetProfileFromUsername";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            string picture = "";
            foreach (DataRow dr in dt.Rows)
            {
                picture = dr["ProfilePhotoURL"].ToString();
                ViewBag.FirstName = dr["FirstName"].ToString();
            }
            return picture;

        }
    }
}
