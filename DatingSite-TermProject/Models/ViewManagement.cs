using System.Data;
using Utilities;
using System.Data.SqlClient;
using Azure.Core;

namespace DatingSite_TermProject.Models
{
    public class ViewManagement
    {

        public ViewManagement() { }


        public string MaxAge()
        {

            string maxage = "";
            DBConnect DB = new DBConnect();
            SqlCommand Cmd = new SqlCommand();
            DataSet DS;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetAgeRange";
            DS = DB.GetDataSet(Cmd);
            if (DS.Tables[0].Rows.Count > 0)
            {
                //ViewBag.MinAge = DS.Tables[0].Rows[0]["MinAge"].ToString();
                maxage = DS.Tables[0].Rows[0]["MaxAge"].ToString();
            }
            else
            {
                //ViewBag.MinAge = 18;  // Default minimum age
                maxage = "100"; // Default maximum age
            }

            return maxage; 

        }

        public string MinAge()
        {
            string minage = "";
            DBConnect DB = new DBConnect();
            SqlCommand Cmd = new SqlCommand();
            DataSet DS;
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "TP_GetAgeRange";
            DS = DB.GetDataSet(Cmd);
            if (DS.Tables[0].Rows.Count > 0)
            {
                minage = DS.Tables[0].Rows[0]["MinAge"].ToString();
                //ViewBag.MaxAge = DS.Tables[0].Rows[0]["MaxAge"].ToString();
            }
            else
            {
                minage = "18";  // Default minimum age
                //ViewBag.MaxAge = 100; // Default maximum age
            }
            return minage;
        }

        public ProfileSecQuestionModel GetQuestions(string savedUsername)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetSecurityQuestionsForProfile";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            ProfileSecQuestionModel security = new ProfileSecQuestionModel();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                security.Question_One = dr["Question_One"].ToString();
                security.Question_Two = dr["Question_Two"].ToString();
                security.Question_Three = dr["Question_Three"].ToString();
                security.Answer_One = dr["Answer_One"].ToString();
                security.Answer_Two = dr["Answer_Two"].ToString();
                security.Answer_Three = dr["Answer_Three"].ToString();
                return security; // Assuming you want to return this model from a method
            }
            else
            {
                return null; // Or however you wish to handle cases where no profile data is returned
            }
        }

        public UserProfileModel GetProfile(string savedUsername)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetProfileFromUsername";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            UserProfileModel profile = new UserProfileModel();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                profile.Age = Convert.ToInt32(dr["Age"]);
                profile.Height = dr["Height"].ToString();
                profile.Weight = dr["Weight"].ToString();
                profile.ProfilePhotoURL = dr["ProfilePhotoURL"].ToString();
                profile.City = dr["City"].ToString();
                profile.State = dr["State"].ToString();
                profile.Tagline = dr["Tagline"].ToString();
                profile.Occupation = dr["Occupation"].ToString();
                profile.Interests = dr["Interests"].ToString();
                profile.FavoriteCuisine = dr["FavouriteCuisine"].ToString();
                profile.FavouriteQuote = dr["FavouriteQuote"].ToString();
                profile.Goals = dr["Goals"].ToString();
                profile.CommitmentType = dr["CommitmentType"].ToString();
                profile.IsVisible = Convert.ToBoolean(dr["IsVisible"]);
                profile.FavoriteMovieGenre = dr["FavouriteMovieGenre"].ToString();
                profile.FavoriteBookGenre = dr["FavouriteBookGenre"].ToString();
                profile.Address = dr["Address"].ToString();
                profile.PhoneNumber = dr["PhoneNumber"].ToString();
                profile.FavoriteMovie = dr["FavouriteMovie"].ToString();
                profile.FavoriteBook = dr["FavouriteBook"].ToString();
                profile.FavoriteRestaurant = dr["FavouriteRestaurant"].ToString();
                profile.Dislikes = dr["Dislikes"].ToString();
                profile.AdditionalInterests = dr["AdditionalInterests"].ToString();
                profile.Dealbreaker = dr["Dealbreaker"].ToString();
                profile.Biography = dr["Biography"].ToString();

                return profile; // Assuming you want to return this model from a method
            }
            else
            {
                return null; // Or however you wish to handle cases where no profile data is returned
            }
        }
        public ProfileImagesModel GetImageGallery(string savedUsername)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetImagesForProfile";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            ProfileImagesModel image = new ProfileImagesModel();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                image.Image1 = dr["Image1"].ToString();
                image.Image2 = dr["Image2"].ToString();
                image.Image3 = dr["Image3"].ToString();
                image.Image4 = dr["Image4"].ToString();
                image.Image5 = dr["Image5"].ToString();
                return image; // Assuming you want to return this model from a method
            }
            else
            {
                return null; // Or however you wish to handle cases where no profile data is returned
            }
        }
        public List<CardsModel> PopulateMatchesProfiles(string savedUsername2)
        {
            List<CardsModel> Cardslist = new List<CardsModel>();
            CardsModel cards;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetMatches";

            SqlParameter inputParameter1 = new SqlParameter("@UserName", savedUsername2);
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


        public List<string> PopulateStates()
        {
            //Populate States
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
               /* ViewBag.States*/
               return  uniqueStates;

        }


        public List<string> PopulateInterests()
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
            //ViewBag.Interests
            return uniqueInterests;
        }

        public List<string> PopulateCommitmentType()
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
            //ViewBag.Commitments
           return uniqueCommitments;

        }

        public String GetUserImage(string savedUsername)
        {
           // string savedUsername = Request.Cookies["Username"].ToString();
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
             
            }
            return picture;

        }

        public String GetUserFirstName(string savedUsername)
        {
            // string savedUsername = Request.Cookies["Username"].ToString();
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetProfileFromUsername";

            SqlParameter inputParameter1 = new SqlParameter("@Username", savedUsername);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            string firstname = "";
            foreach (DataRow dr in dt.Rows)
            {
                firstname = dr["FirstName"].ToString();
                //ViewBag.FirstName = dr["FirstName"].ToString();
            }
            return firstname;

        }



    }
}


//Populate Interests
//{
//    DBConnect DB = new DBConnect();
//    DataSet DS;
//    SqlCommand Cmd = new SqlCommand();
//    Cmd.CommandType = CommandType.StoredProcedure;
//    Cmd.CommandText = "TP_GetUniqueInterests";
//    DS = DB.GetDataSet(Cmd);

//    List<string> uniqueInterests = new List<string>();
//    foreach (DataRow row in DS.Tables[0].Rows)
//    {
//        string interestList = row["Interests"].ToString();
//        string[] interests = interestList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
//        foreach (string interest in interests)
//        {
//            string trimmedInterest = interest.Trim();
//            if (!uniqueInterests.Contains(trimmedInterest))
//            {
//                uniqueInterests.Add(trimmedInterest);
//            }
//        }
//    }
//    ViewBag.Interests = uniqueInterests;
//}
////Populate Commitment Types
//{
//    List<string> uniqueCommitments = new List<string>();
//    DBConnect DB = new DBConnect();
//    DataSet DS;
//    SqlCommand Cmd = new SqlCommand();
//    Cmd.CommandType = CommandType.StoredProcedure;
//    Cmd.CommandText = "TP_GetUniqueCommitmentTypes";
//    DS = DB.GetDataSet(Cmd);
//    foreach (DataRow row in DS.Tables[0].Rows)
//    {
//        string commitmentType = row["CommitmentType"].ToString();
//        uniqueCommitments.Add(commitmentType);
//    }
//    ViewBag.Commitments = uniqueCommitments;
//}
