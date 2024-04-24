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
using System.Runtime.Serialization.Formatters.Binary;

namespace DatingSite_TermProject.Controllers
{
    public class PlanDateController : Controller
    {
        public IActionResult CreateDate() {
            string cookieObject = Request.Form["PrivateId"].ToString();
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(5);
            HttpContext.Response.Cookies.Append("DatePersonId", cookieObject, options);
            return View("~/Views/Main/Dates/DatePlanner.cshtml");
        }
        public IActionResult PlanDate()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            var datePlanCards = new PlanDateCardModel
            {
                DatesNotYetPlanned = DatesNotYetPlanned(privateid),
                PlannedDates = PlannedDates(privateid)
            };
            ViewBag.ProfileImage = GetUserImage();
            return View("~/Views/Main/Dates/PlanDate.cshtml", datePlanCards);
        }
        public IActionResult ShowDatePlan()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            ViewBag.ProfileImage = GetUserImage();
            DatePlanModel plan = new DatePlanModel();
            int dateId = GetDateId(int.Parse(Request.Form["PrivateId"].ToString()));
            plan = GetDatePlan(dateId);
            plan.YourProfile = GetProfile(privateid);
            plan.OtherProfile = GetProfile(int.Parse(Request.Form["PrivateId"].ToString()));
            return View("~/Views/Main/Dates/ShowDatePlan.cshtml", plan);
        }
        public List<CardsModel> DatesNotYetPlanned(int privateid)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDatesForUser_byStatus";
            string savedUsername = Request.Cookies["Username"].ToString();

            SqlParameter inputParameter = new SqlParameter("@UserName", savedUsername);
            objCommand.Parameters.Add(inputParameter);

            SqlParameter inputParameter1 = new SqlParameter("@DateStatus", "approved");
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
        public List<CardsModel> PlannedDates(int privateid)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDatesForUser_byStatus";
            string savedUsername = Request.Cookies["Username"].ToString();

            SqlParameter inputParameter = new SqlParameter("@UserName", savedUsername);
            objCommand.Parameters.Add(inputParameter);

            SqlParameter inputParameter1 = new SqlParameter("@DateStatus", "planned");
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
        public IActionResult EditDate()
        {
            DatePlanModel plan = new DatePlanModel();
            int dateId = GetDateId(int.Parse(Request.Form["PrivateId"].ToString()));
            plan = GetDatePlan(dateId);
            string cookieObject = plan.DateId.ToString();
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(15);
            HttpContext.Response.Cookies.Append("EditDateId", cookieObject, options);
            return View("~/Views/Main/Dates/DatePlanner.cshtml", plan);
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
    }
}
