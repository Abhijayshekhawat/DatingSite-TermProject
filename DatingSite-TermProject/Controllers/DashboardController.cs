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
        public IActionResult Dashboard()
        {
            return View("~/Views/Main/Dashboard.cshtml");
        }

        [HttpPost]

        public IActionResult FilterAction()
        {

            // should this be a method or an IActionResult type of method ?

            // get the  dataset then redo the cardlist and send return the view again ?
            // should repopulate with new cardlist that has all the character
            // then reset filter is going to have the populateprofile() method to reset

            string username = Request.Cookies["Username"].ToString();
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
