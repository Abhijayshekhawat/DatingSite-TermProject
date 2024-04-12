﻿using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSite_TermProject.Controllers
{
    public class VerificationController : Controller
    {
        public IActionResult Verification()
        {



            string savedUsername2 = Request.Cookies["Username"].ToString();
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
                    dr["Description"].ToString(),
                    dr["City"].ToString(),
                    dr["State"].ToString()
                );

                Cardslist.Add(cards);
            }

            return View("~/Views/Main/Dashboard.cshtml", Cardslist);






           
        }
    }
}
