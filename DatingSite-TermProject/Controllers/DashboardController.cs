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
        public IActionResult Dashboard()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            List<CardsModel> Cardslist = PopulateProfiles(privateid);
            ViewBag.ProfileImage = GetUserImage();
            PopulateFilters();
            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
        }

        [HttpPost]
        public IActionResult AddLikes()
        {

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
                    UpdateMatch();
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    int privateid = userProfile.getPrivateId(savedUsername2);
                    List<CardsModel> Cardslist = PopulateProfiles(privateid);
                    ViewBag.ProfileImage = GetUserImage();
                    PopulateFilters();
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
            UserProfileModel userProfile2 = new UserProfileModel();
            int privateid2 = userProfile2.getPrivateId(savedUsername3);
            List<CardsModel> Cardslist2 = PopulateProfiles(privateid2);
            ViewBag.ProfileImage = GetUserImage();
            PopulateFilters();  
            return View("~/Views/Main/Dashboard.cshtml", Cardslist2);
        }

        [HttpPost]
        public IActionResult ResetFilters()
        {
            // Call methods to repopulate ViewBag properties
            PopulateStates();
            PopulateInterests();
            PopulateCommitmentTypes();

            string savedUsername2 = Request.Cookies["Username"].ToString();
            ViewBag.ProfileImage = GetUserImage();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUsersCards";

            SqlParameter inputParameter1 = new SqlParameter("@ExcludedUserId", privateid);
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

            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
        }

        [HttpPost]
        public IActionResult FilterAction()
        {

            // should this be a method or an IActionResult type of method ?

            // get the  dataset then redo the cardlist and send return the view again ?
            // should repopulate with new cardlist that has all the character
            // then reset filter is going to have the populateprofile() method to
            //PopulateStates();
            //PopulateInterests();
            //PopulateCommitmentTypes();
            PopulateFilters();



            string username = Request.Cookies["Username"].ToString();
            ViewBag.ProfileImage = GetUserImage();
            UserProfileModel userProfile = new UserProfileModel();

            string lessThanAge = Request.Form["lessThanAge"].ToString();
            string filterCity = Request.Form["filterCity"].ToString();
            string filterState = Request.Form["filterState"].ToString();
            string filterOccupation = Request.Form["filterOccupation"].ToString();
            string interestsString = Request.Form["interests"].ToString();

            string filterCommitmentType = Request.Form["filterCommitmentType"].ToString();

            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;

            DBConnect DB = new DBConnect();
            DataSet DS = new DataSet();
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetFilteredProfiles";

            Cmd.Parameters.AddWithValue("@UserName", username);
            Cmd.Parameters.AddWithValue("@AgeLessThan", string.IsNullOrWhiteSpace(lessThanAge) ? DBNull.Value : (object)Convert.ToInt32(lessThanAge));
            Cmd.Parameters.AddWithValue("@City", string.IsNullOrWhiteSpace(filterCity) ? DBNull.Value : (object)filterCity);
            Cmd.Parameters.AddWithValue("@State", filterState == "" ? DBNull.Value : (object)filterState);
            Cmd.Parameters.AddWithValue("@Occupation", string.IsNullOrWhiteSpace(filterOccupation) ? DBNull.Value : (object)filterOccupation);
            //string selectedInterests = string.Join(",", chkInterests.Items.Cast<ListItem>().Where(i => i.Selected).Select(i => i.Value));
            Cmd.Parameters.AddWithValue("@Interests", string.IsNullOrWhiteSpace(interestsString) ? DBNull.Value : (object)interestsString);
            Cmd.Parameters.AddWithValue("@CommitmentType", filterCommitmentType == "" ? DBNull.Value : (object)filterCommitmentType);

            DS = DB.GetDataSet(Cmd);

            DataTable dt2 = DS.Tables[0];

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


            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
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
        public List<CardsModel> PopulateProfiles(int privateid)
        {


            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUsersCards";

            SqlParameter inputParameter1 = new SqlParameter("@ExcludedUserId", privateid);
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
        private void PopulateStates()
        {
            // Code for populating ViewBag.States
            List<string> uniqueStates = new List<string>();
            DBConnect DB = new DBConnect();
            DataSet DS;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetUniqueStates";
            DS = DB.GetDataSet(Cmd);
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                string state = row["State"].ToString();
                uniqueStates.Add(state);
            }
            ViewBag.States = uniqueStates;
        }
        public void PopulateFilters()
        {
            //Populate States
            {
                List<string> uniqueStates = new List<string>();
                DBConnect DB = new DBConnect();
                DataSet DS;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "TP_GetUniqueStates";
                DS = DB.GetDataSet(Cmd);
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    string state = row["State"].ToString();
                    uniqueStates.Add(state);
                }
                ViewBag.States = uniqueStates;
            }
            //Populate Interests
            {
                DBConnect DB = new DBConnect();
                DataSet DS;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "TP_GetUniqueInterests";
                DS = DB.GetDataSet(Cmd);

                List<string> uniqueInterests = new List<string>();
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    string interestList = row["Interests"].ToString();
                    string[] interests = interestList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string interest in interests)
                    {
                        string trimmedInterest = interest.Trim();
                        if (!uniqueInterests.Contains(trimmedInterest))
                        {
                            uniqueInterests.Add(trimmedInterest);
                        }
                    }
                }
                ViewBag.Interests = uniqueInterests;
            }
            //Populate Commitment Types
            {
                List<string> uniqueCommitments = new List<string>();
                DBConnect DB = new DBConnect();
                DataSet DS;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "TP_GetUniqueCommitmentTypes";
                DS = DB.GetDataSet(Cmd);
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    string commitmentType = row["CommitmentType"].ToString();
                    uniqueCommitments.Add(commitmentType);
                }
                ViewBag.Commitments = uniqueCommitments;
            }



        }
        private void PopulateInterests()
        {
            DBConnect DB = new DBConnect();
            DataSet DS;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetUniqueInterests";
            DS = DB.GetDataSet(Cmd);

            List<string> uniqueInterests = new List<string>();
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                string interestList = row["Interests"].ToString();
                string[] interests = interestList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string interest in interests)
                {
                    string trimmedInterest = interest.Trim();
                    if (!uniqueInterests.Contains(trimmedInterest))
                    {
                        uniqueInterests.Add(trimmedInterest);
                    }
                }
            }
            ViewBag.Interests = uniqueInterests;
        }
        private void PopulateCommitmentTypes()
        {
            List<string> uniqueCommitments = new List<string>();
            DBConnect DB = new DBConnect();
            DataSet DS;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetUniqueCommitmentTypes";
            DS = DB.GetDataSet(Cmd);
            foreach (DataRow row in DS.Tables[0].Rows)
            {
                string commitmentType = row["CommitmentType"].ToString();
                uniqueCommitments.Add(commitmentType);
            }
            ViewBag.Commitments = uniqueCommitments;
        }
        private DataSet GetMutualLikes()
        {
            DBConnect DB = new DBConnect();
            DataSet DS;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetMutualLikes";
            DS = DB.GetDataSet(Cmd);
            return DS;
        }
        private void UpdateMatch()
        {
            DataSet dsMutualLikes = GetMutualLikes();
            if (dsMutualLikes != null && dsMutualLikes.Tables.Count > 0)
            {
                foreach (DataRow row in dsMutualLikes.Tables[0].Rows)
                {
                    int user1ID = (int)row["User1ID"];
                    int user2ID = (int)row["User2ID"];
                    DateTime user1LikesUser2Time = (DateTime)row["User1LikesUser2Time"];
                    DateTime user2LikesUser1Time = (DateTime)row["User2LikesUser1Time"];
                    DateTime matchTime = user1LikesUser2Time > user2LikesUser1Time ? user1LikesUser2Time : user2LikesUser1Time;
                    if (!IsMatchExist(user1ID, user2ID))
                    {
                        InsertMatch(user1ID, user2ID, matchTime);
                    }
                }
            }

        }
        private bool IsMatchExist(int user1ID, int user2ID)
        {
            DBConnect DB = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CheckIfMatchExists";
            cmd.Parameters.AddWithValue("@MatcherID1", user1ID);
            cmd.Parameters.AddWithValue("@MatcherID2", user2ID);

            SqlParameter outputParam = new SqlParameter("@IsExist", SqlDbType.Bit);
            outputParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outputParam);

            DB.GetDataSetUsingCmdObj(cmd);
            bool isExist = (bool)cmd.Parameters["@IsExist"].Value;
            return isExist;
        }
        private void InsertMatch(int matcherID1, int matcherID2, DateTime matchTime)
        {
            DBConnect DB = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_InsertMatch";
            cmd.Parameters.AddWithValue("@MatcherID1", matcherID1);
            cmd.Parameters.AddWithValue("@MatcherID2", matcherID2);
            cmd.Parameters.AddWithValue("@MatchTime", matchTime);

            DB.DoUpdateUsingCmdObj(cmd);
            ViewBag.ErrorMessage = "You have a new match!";
        }
    }


}

