using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies
using DatingSite_TermProject.Controllers;
using System.Runtime.Serialization.Formatters.Binary;
using Utilities;

namespace DatingSite_TermProject.Controllers
{
    public class PlanDateController : Controller
    {
        ViewManagement view = new ViewManagement();
        public IActionResult CreateDate() {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername4 = Request.Cookies["Username"].ToString();
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername4);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername4);
                    string cookieObject = Request.Form["PrivateId"].ToString();
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddMinutes(5);
                    HttpContext.Response.Cookies.Append("DatePersonId", cookieObject, options);
                    ViewBag.FavouriteCuisine = GetFavouriteCuisine(Request.Cookies["Username"].ToString(), int.Parse(Request.Form["PrivateId"].ToString()));
                    ViewBag.UserCity = GetCity(Request.Cookies["Username"].ToString());
                    return View("~/Views/Main/Dates/DatePlanner.cshtml");
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }
            
        }
        public IActionResult PlanDate()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername4 = Request.Cookies["Username"].ToString();
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername4);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername4);
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
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }
            
        }
        public IActionResult ShowDatePlan()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
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
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }
            
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
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername4 = Request.Cookies["Username"].ToString();
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername4);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername4);
                    DatePlanModel plan = new DatePlanModel();
                    int dateId = GetDateId(int.Parse(Request.Form["PrivateId"].ToString()));
                    plan = GetDatePlan(dateId);
                    string cookieObject = plan.DateId.ToString();
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddSeconds(15);
                    HttpContext.Response.Cookies.Append("EditDateId", cookieObject, options);
                    ViewBag.FavouriteCuisine = GetFavouriteCuisine(Request.Cookies["Username"].ToString(), int.Parse(Request.Form["PrivateId"].ToString()));
                    ViewBag.UserCity = GetCity(Request.Cookies["Username"].ToString());
                    return View("~/Views/Main/Dates/DatePlanner.cshtml", plan);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }
            
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
        private string GetFavouriteCuisine(string username, int privateId)
        {
            string cuisine = "romantic";
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetFavoriteCuisines";
            objCommand.Parameters.AddWithValue("@Username", username);
            objCommand.Parameters.AddWithValue("@OtherUserPrivateId", privateId);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                var userOneCuisines = dt.Rows[0]["UserOneCuisine"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var userTwoCuisines = dt.Rows[0]["UserTwoCuisine"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var commonCuisines = userOneCuisines.Intersect(userTwoCuisines).ToList();
                if (commonCuisines.Any())
                {
                    cuisine = commonCuisines.First();
                }
            }

            return cuisine;
        }
        private string GetCity(string username)
        {
            string city = "Philadelphia";
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetCityByUsername";
            objCommand.Parameters.AddWithValue("@Username", username);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                city = dt.Rows[0]["City"].ToString();
            }

            return city;
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
