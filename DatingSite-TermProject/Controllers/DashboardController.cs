﻿using Microsoft.AspNetCore.Mvc;
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
                Convert.ToInt32(dr["Age"]),
                dr["Height"].ToString(),
                dr["Weight"].ToString(),
                dr["FirstName"].ToString(),
                dr["LastName"].ToString()
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
                   Convert.ToInt32(dr["Age"]),
                   dr["Height"].ToString(),
                   dr["Weight"].ToString(),
                   dr["FirstName"].ToString(),
                   dr["LastName"].ToString()
                   );

                Cardslist.Add(cards);
            }
           

            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
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
                    dr["Tagline"].ToString(),
                    dr["City"].ToString(),
                    dr["State"].ToString()
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
    }


}

