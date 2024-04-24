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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.Serialization.Formatters.Binary;

namespace DatingSite_TermProject.Controllers
{
    public class ShowDatePlanController : Controller
    {
        ViewManagement view = new ViewManagement();
        public IActionResult ShowDatePlan()
        {
            var datePlan = new DatePlanModel();

            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
            return View("~/Views/Main/Dates/ShowDatePlan.cshtml", datePlan);
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
        private CardsModel GetProfile(int privateId)
        {
            string savedUsername = Request.Cookies["Username"].ToString();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetFullProfile";
            SqlParameter inputParameter1 = new SqlParameter("@userId", privateId);
            objCommand.Parameters.Add(inputParameter1);
            DataSet ds = objDB.GetDataSet(objCommand);
            DataTable dt = ds.Tables[0];
            CardsModel profile = new CardsModel();
            foreach (DataRow dr in dt.Rows)
            {
                profile = new CardsModel(
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
                    privateId
                );
            }
            return profile;
        }
        private int GetDateId(int privateId)
        {
            int id = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDateId";
            objCommand.Parameters.AddWithValue("@Username", Request.Cookies["Username"].ToString());
            objCommand.Parameters.AddWithValue("@otherid", privateId);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                id = int.Parse(dr["DateID"].ToString());
            }
            return id;
        }
        private DatePlanModel GetDatePlan(int dateID)
        {
            DatePlanModel plan = new DatePlanModel();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDatePlanByDateId";
            objCommand.Parameters.AddWithValue("@DateId", dateID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                byte[] byteArray = (byte[])dr["DatePlans"];
                BinaryFormatter serializer = new BinaryFormatter();
                MemoryStream memStream = new MemoryStream(byteArray);
                plan = (DatePlanModel)serializer.Deserialize(memStream);
            }
            return plan;
        }
    }
}
