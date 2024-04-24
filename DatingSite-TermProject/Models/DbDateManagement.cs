using Azure.Core;
using System.Data;
using Utilities;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using DatingSite_TermProject.Models;

namespace DatingSite_TermProject.Models
{
    public class DbDateManagement
    {

        public DbDateManagement() { }

        public List<CardsModel> DatesYouSent(int privateid, string savedUsername)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDateRequestsFromUser";
            //savedUsername = Request.Cookies["Username"].ToString();

            SqlParameter inputParameter1 = new SqlParameter("@UserName", savedUsername);
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

        public List<CardsModel> DatesYouReceived(int privateid, string savedUsername)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetDateRequestsToUser";
           // string savedUsername = Request.Cookies["Username"].ToString();

            SqlParameter inputParameter1 = new SqlParameter("@UserName", savedUsername);
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

        public DatesCardModel ApproveDate(int privateid, string savedUsername)
        {

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpdateDateStatus";
            SqlParameter resultParameter = new SqlParameter("@Result", SqlDbType.Int);
            resultParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(resultParameter);
            objCommand.Parameters.AddWithValue("@UserName", savedUsername);
            objCommand.Parameters.AddWithValue("@OtherID", privateid);
            objCommand.Parameters.AddWithValue("@NewStatus", "approved");
            objDB.DoUpdateUsingCmdObj(objCommand);
            DatesCardModel dateCards = new DatesCardModel
            {
                DatesYouSent = DatesYouSent(privateid, savedUsername),
                DatesYouReceived = DatesYouReceived(privateid, savedUsername)
            };




            return dateCards;


        }

        public DatesCardModel DenyDate(int privateid, string savedUsername) 
        
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_DeleteDateRequest";
            objCommand.Parameters.AddWithValue("@UserName", savedUsername);
            objCommand.Parameters.AddWithValue("@OtherUserID", privateid);
            objDB.DoUpdateUsingCmdObj(objCommand);
            DatesCardModel dateCards = new DatesCardModel
            {
                DatesYouSent = DatesYouSent(privateid, savedUsername),
                DatesYouReceived = DatesYouReceived(privateid, savedUsername)
            };


            return dateCards;


        }







    }
}
