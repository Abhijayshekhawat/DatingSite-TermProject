using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class Profile
    {
       private int profileprivateId;


        public Profile()
        {


        }

        public Profile(int profileprivateId)
        {

            this.profileprivateId = profileprivateId;   
        }

        public int ProfilePrivateId
        {
            get { return this.profileprivateId; }
            set { this.profileprivateId = value;}   
        
        }
        public List<Card> PopulateProfiles(int privateid)
        {


            List<Card> Cardslist = new List<Card>();
            Card cards;
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
                cards = new Card(
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



    }
}
