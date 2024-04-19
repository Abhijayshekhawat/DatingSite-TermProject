using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using Utilities;


namespace DatingSite_TermProject.Controllers
{
    public class VerificationController : Controller
    {

        public ActionResult actionresult()
        {
            return View("~/Views/Main/Dashboard.cshtml");
        }
        public IActionResult Verification()
        {
            if (Request.Cookies.TryGetValue("VerCode", out string encryptedCookie))
            {
                string decryptedCode = EncryptionHelper.Decrypt(encryptedCookie);
                string userEnteredCode = Request.Form["VerificationCode"].ToString();
                if (decryptedCode == userEnteredCode)
                {

                    ViewBag.ErrorMessage = "The codes are the same.";
                    // get id 
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    int privateid = userProfile.getPrivateId(savedUsername2);
                    List<CardsModel> Cardslist = PopulateProfiles(privateid);
                    ViewBag.ProfileImage = GetUserImage();
                    PopulateFilters();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
                }
                else
                {
                    ViewBag.ErrorMessage = "The codes are not the same.";
                    return View("~/Views/Home/Verification.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the login process again.";
                return View("~/Views/Home/Verification.cshtml");
            }
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
                    int.Parse(dr["PrivateId"].ToString()),
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
            string savedUsername2 = Request.Cookies["Username"].ToString();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetProfileFromUsername";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername2);
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





    }
}
