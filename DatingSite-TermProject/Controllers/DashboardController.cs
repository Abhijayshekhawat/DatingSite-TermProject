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
    public class DashboardController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/MatchUp";
        ViewManagement view = new ViewManagement();
        DbFilterManagement filter = new DbFilterManagement();
        DbUpdateMatch updateMatch = new DbUpdateMatch();
        public IActionResult Dashboard()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();

            List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
        }

        [HttpPost]
        public IActionResult AddLikes()
        {
            UserProfileModel userProfile = new UserProfileModel();

            string savedUsername = Request.Cookies["Username"].ToString();
            LikeRequestModel like = new LikeRequestModel();

            like.LikerUsername = savedUsername;
            like.LIkeeId = Int32.Parse(Request.Form["LikeeID"].ToString());
            var jsonPayload = JsonSerializer.Serialize(like);
            try
            {
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddLikes");
                request.Method = "POST";
                request.ContentLength = jsonPayload.Length;
                request.ContentType = "application/json";
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonPayload);
                writer.Flush();
                writer.Close();
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                if (data == "true")
                {
                    updateMatch.UpdateMatch();
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            string savedUsername3 = Request.Cookies["Username"].ToString();
            List<CardsModel> Cardslist2 = view.PopulateProfiles(userProfile.getPrivateId(savedUsername3));
            ViewBag.ProfileImage = view.GetUserImage(savedUsername3);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername3);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge();
            return View("~/Views/Main/Dashboard.cshtml", Cardslist2);
        }

        [HttpPost]
        public IActionResult ResetFilters()
        {
            // Call methods to repopulate ViewBag properties


            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            List<CardsModel> Cardslist = new List<CardsModel>();
            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge();
            Cardslist = filter.ResetFilter(userProfile.getPrivateId(savedUsername2));


            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
        }


        [HttpPost]
        public IActionResult FilterAction()
        {
            // get username
            filter.Username = Request.Cookies["Username"].ToString();
            // populate all the viewbags
            ViewBag.ProfileImage = view.GetUserImage(filter.Username);
            ViewBag.FirstName = view.GetUserFirstName(filter.Username);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge(); 
            // properties with request form data 
            filter.MaxAge = Request.Form["ageRangeMax"].ToString();
            filter.MinAge = Request.Form["ageRangeMin"].ToString();
            filter.FilterCity = Request.Form["filterCity"].ToString();
            filter.FilterState = Request.Form["filterState"].ToString();
            filter.FilterOccupation = Request.Form["filterOccupation"].ToString();
            filter.InterestsString = Request.Form["interests"].ToString();

            filter.FilterCommitmentType = Request.Form["filterCommitmentType"].ToString();

            List<CardsModel> Cardslist = new List<CardsModel>();

            Cardslist = filter.ApplyFilter(filter.Username, filter.MaxAge, filter.MinAge, filter.FilterCity, filter.FilterState, filter.FilterOccupation, filter.InterestsString, filter.FilterCommitmentType);


            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
        }











    }
}

