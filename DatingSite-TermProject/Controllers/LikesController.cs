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
    public class LikesController : Controller
    {
        public IActionResult Likes()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            List<CardsModel> Cardslist = PopulateProfiles(privateid);
            ViewBag.ProfileImage = GetUserImage();
            return View("~/Views/Main/Likes.cshtml", Cardslist);
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
