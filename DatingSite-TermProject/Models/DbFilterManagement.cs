using System.Data;
using Utilities;
using System.Data.SqlClient;
using Azure.Core;

namespace DatingSite_TermProject.Models
{
    //string username = Request.Cookies["Username"].ToString();
    //ViewBag.ProfileImage = GetUserImage();
    //UserProfileModel userProfile = new UserProfileModel();

    //string lessThanAge = Request.Form["lessThanAge"].ToString();
    //string filterCity = Request.Form["filterCity"].ToString();
    //string filterState = Request.Form["filterState"].ToString();
    //string filterOccupation = Request.Form["filterOccupation"].ToString();
    //string interestsString = Request.Form["interests"].ToString();

    //string filterCommitmentType = Request.Form["filterCommitmentType"].ToString();

    public class DbFilterManagement
    {
        private string username;
        private string lessthanage;
        private string filtercity;
        private string filterstate;
        private string filteroccupation;
        private string interestsString;
        private string filtercommitmenttype;
        private string agegreaterthan;


        public DbFilterManagement() { }

        public DbFilterManagement(string username, string lessthanage, string filtercity, string filterstate, string filteroccupation, string interestsString, string filtercommitmentType, string agegreaterthan)
        {
            this.username = username;
            this.lessthanage = lessthanage;
            this.filtercity = filtercity;
            this.filterstate = filterstate;
            this.filteroccupation = filteroccupation;
            this.interestsString = interestsString;
            this.filtercommitmenttype = filtercommitmentType;
            this.agegreaterthan = agegreaterthan; 


        }
        public List<CardsModel> ResetFilter(int privateid)
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

        public List<CardsModel> ApplyFilter(string username, string lessThanAge, string filterCity, string filterState, string filterOccupation, string interestsString, string filterCommitmentType)
        {

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

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }

        }
        public string LessThanAge
        {
            get { return this.lessthanage; }
            set { this.lessthanage = value; }

        }

        public string FilterCity
        {
            get { return this.filtercity; }
            set { this.filtercity = value; }

        }

        public string FilterState
        {

            get { return this.filterstate; }
            set { this.filterstate = value; }



        }

        public string FilterOccupation
        {


            get { return this.filteroccupation; }

            set { this.filteroccupation = value; }




        }

        public string FilterCommitmentType
        {
            get { return this.filtercommitmenttype; }
            set { this.filtercommitmenttype = value; }
        }

        public string InterestsString
        {
            get { return this.interestsString; }
            set { this.interestsString = value; }
        }











    }
}
