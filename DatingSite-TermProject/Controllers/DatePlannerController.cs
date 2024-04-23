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
            if (Request.Cookies.TryGetValue("DatePersonId", out string dateIDCookie))
            {
                plan.DateId = GetDateId(int.Parse(dateIDCookie));
            }
            else
            {
                return View("~/Views/Main/Dates/DatePlanner.cshtml");
            }
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();

            Byte[] byteArray;
            #pragma warning disable SYSLIB0011
            serializer.Serialize(memStream, plan);

            byteArray = memStream.ToArray();





            // Update the account to store the serialized object (binary data) in the database

            objCommand.CommandType = CommandType.StoredProcedure;

            objCommand.CommandText = "TP_InsertBinaryDatePlan";



            objCommand.Parameters.AddWithValue("@DateID", plan.DateId);

            objCommand.Parameters.AddWithValue("@DatePlan", byteArray);



            int retVal = objDB.DoUpdateUsingCmdObj(objCommand);







            return View("~/Views/Main/Dates/DatePlanner.cshtml");



        }
        public IActionResult DatePlan()
        {
            return View("~/Views/Main/Dates/DatePlanner.cshtml");
        }
        public IActionResult EditDatePlan()
        {
            return View("~/Views/Main/Dates/DatePlanner.cshtml");
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
    }
}
