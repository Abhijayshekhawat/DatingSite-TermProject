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
using Microsoft.IdentityModel.Tokens;

namespace DatingSite_TermProject.Controllers
{

    public class MatchesController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/MatchUp";
        ViewManagement view = new ViewManagement();
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
                    List<CardsModel> Cardslist1 = view.PopulateMatchesProfiles(savedUsername);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername);
                    return View("~/Views/Main/Matches.cshtml", Cardslist1);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            List<CardsModel> Cardslist = view.PopulateMatchesProfiles(savedUsername);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername);
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
                    List<CardsModel> Cardslist1 = view.PopulateMatchesProfiles(savedUsername);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername);
                    return View("~/Views/Main/Matches.cshtml", Cardslist1);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            List<CardsModel> Cardslist = view.PopulateMatchesProfiles(savedUsername);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername);
            return View("~/Views/Main/Matches.cshtml", Cardslist);
        }

        [HttpGet]
        public IActionResult Matches()
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            MatchesModel matchesModel = new MatchesModel();
            matchesModel.MatcherUsername = savedUsername;
            var jsonPayload = JsonSerializer.Serialize(matchesModel);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/GetMatches");
                request.Method = "GET";
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
                List<CardsModel> Cardslist2 = new List<CardsModel>(); // Initialize the list

                if (!string.IsNullOrEmpty(data))
                {
                    // Deserialize the JSON response into a list of CardsModel objects
                    Cardslist2 = JsonSerializer.Deserialize<List<CardsModel>>(data);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername);
                    return View("~/Views/Main/Matches.cshtml", Cardslist2);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            List<CardsModel> Cardslist = view.PopulateMatchesProfiles(savedUsername);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername);
            return View("~/Views/Main/Matches.cshtml", Cardslist);
        }



        

        }
    }

