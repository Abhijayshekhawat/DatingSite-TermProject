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
using System.Data.Common;
using System.Numerics;

namespace DatingSite_TermProject.Controllers
{
    public class DatePlannerController : Controller
    {
        public IActionResult SaveDatePlan()
        {
            
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            DatePlanModel plan = new DatePlanModel();
            plan.Date = Convert.ToDateTime(Request.Form["Date"].ToString());
            plan.Time = TimeSpan.Parse(Request.Form["Time"].ToString());
            plan.Location = Request.Form["Location"].ToString();
            plan.Description = Request.Form["Description"].ToString();
            if (!string.IsNullOrEmpty(Request.Form["DateId"]))
            {
                plan.DateId = int.Parse(Request.Form["DateId"].ToString());
            }
            else
            {
                if (Request.Cookies.TryGetValue("DatePersonId", out string dateIDCookie))
                {
                    plan.DateId = GetDateId(int.Parse(dateIDCookie));
                }
            }
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();

            Byte[] byteArray;
            #pragma warning disable SYSLIB0011
            serializer.Serialize(memStream, plan);

            byteArray = memStream.ToArray();

            objCommand.CommandType = CommandType.StoredProcedure;
            int retVal = 0;
            int check = CheckForDatePlan(plan.DateId);
            if (check > 0)
            {
                objCommand.CommandText = "TP_UpdateBinaryDatePlan";
                objCommand.Parameters.AddWithValue("@DateID", plan.DateId);
                objCommand.Parameters.AddWithValue("@DatePlan", byteArray);
                retVal = objDB.DoUpdateUsingCmdObj(objCommand);
            }
            else
            {
                objCommand.CommandText = "TP_InsertBinaryDatePlan";
                objCommand.Parameters.AddWithValue("@DateID", plan.DateId);
                objCommand.Parameters.AddWithValue("@DatePlan", byteArray);
                retVal = objDB.DoUpdateUsingCmdObj(objCommand);
                UpdateDateStatus();
            }

            if (retVal > 0)
            {
                string savedUsername2 = Request.Cookies["Username"].ToString();
                UserProfileModel userProfile = new UserProfileModel();
                int privateid = userProfile.getPrivateId(savedUsername2);
                var datePlanCards = new PlanDateCardModel
                {
                    DatesNotYetPlanned = DatesNotYetPlanned(privateid),
                    PlannedDates = PlannedDates(privateid)
                };
                return View("~/Views/Main/Dates/PlanDate.cshtml", datePlanCards);
            }
            else
            {
                return View("~/Views/Main/Dates/DatePlanner.cshtml");
            }

        }
        public IActionResult DatePlan()
        {
            if (Request.Cookies.TryGetValue("EditDatePersonId", out string cookie))
            {
                DatePlanModel plan = new DatePlanModel();
                int dateId = GetDateId(int.Parse(cookie));
                plan = GetDatePlan(dateId);
                return View("~/Views/Main/Dates/DatePlanner.cshtml", plan);
            }
            else
            {
                return View("~/Views/Main/Dates/DatePlanner.cshtml");
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
        private void UpdateDateStatus()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpdateDateStatus";
            SqlParameter resultParameter = new SqlParameter("@Result", SqlDbType.Int);
            resultParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(resultParameter);
            objCommand.Parameters.AddWithValue("@UserName", Request.Cookies["Username"].ToString());
            objCommand.Parameters.AddWithValue("@OtherID", Request.Cookies["DatePersonId"].ToString());
            objCommand.Parameters.AddWithValue("@NewStatus", "planned");
            objDB.DoUpdateUsingCmdObj(objCommand);
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
        private int CheckForDatePlan(int  id)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_CheckDatePlanExists";
            objCommand.Parameters.AddWithValue("@DateID", id);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            int retVal=0;
            if (ds.Tables.Count == 0)
            {
                retVal = -1;
            }
            else
            {
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    retVal =0; // Set retVal to 0 if rows are returned
                }
            }
            
            return retVal;
        }
    }

}
