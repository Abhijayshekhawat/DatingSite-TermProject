using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace DatingSiteCoreAPI
{
    public class UserProfile
    {
        private int privateid;
        private int age;
        private string height;
        private string weight;
        private string profilePhotoURL;
        private string city;
        private string state;
        private string tagline;
        private string occupation;
        private string interests;
        private string favoritecusine;
        private string favouritequote;
        private string goals;
        private string commitmentType;
        private string favoriteMovieGenre;
        private string favoriteBookGenre;
        private string address;
        private string phonenumber;
        private string favoritemovie;
        private string favoritebook;
        private string favoriterestaurant;
        private string dislikes;
        private bool isVisible;
        private string additionalInterests;
        private string dealbreaker;
        private string biography;

        public int getPrivateId(string username)
        {
            int result = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUserID";

            SqlParameter inputParameter1 = new SqlParameter("@Username", username);
            objCommand.Parameters.Add(inputParameter1);


            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                result = Int32.Parse(dr["PrivateId"].ToString());

            }

            return result;

        }

            public int AddUserInfo(int privateid, int age, string height, string weight, string profilePhotoURL, string city, string state, string tagline, string occupation, string interests, string favoritecusine, string favouritequote, string goals, string commitmentType, string favoriteMovieGenre, string favoriteBookGenre, string address, string phonenumber, string favoritemovie, string favoritebook, string favoriterestaurant, string dislikes, bool isVisible, string additionalInterests, string dealbreaker, string biography)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_UpdateProfileByPrivateId";

            SqlParameter inputParameter1 = new SqlParameter("@PrivateId", privateid);
            objCommand.Parameters.Add(inputParameter1);

            SqlParameter inputParameter2 = new SqlParameter("@Age", age);
            objCommand.Parameters.Add(inputParameter2);

            SqlParameter inputParameter3 = new SqlParameter("@Height", height);
            objCommand.Parameters.Add(inputParameter3);

            SqlParameter inputParameter4 = new SqlParameter("@Weight", weight);
            objCommand.Parameters.Add(inputParameter4);

            SqlParameter inputParameter5 = new SqlParameter("@ProfilePhotoURL", profilePhotoURL);
            objCommand.Parameters.Add(inputParameter5);

            SqlParameter inputParameter6 = new SqlParameter("@City", city);
            objCommand.Parameters.Add(inputParameter6);

            SqlParameter inputParameter7 = new SqlParameter("@State", state);
            objCommand.Parameters.Add(inputParameter7);

            SqlParameter inputParameter8 = new SqlParameter("@Tagline", tagline);
            objCommand.Parameters.Add(inputParameter8);

            SqlParameter inputParameter9 = new SqlParameter("@Occupation", occupation);
            objCommand.Parameters.Add(inputParameter9);

            SqlParameter inputParameter10 = new SqlParameter("@Interests", interests);
            objCommand.Parameters.Add(inputParameter10);

            SqlParameter inputParameter11 = new SqlParameter("@FavouriteCuisine", favoritecusine);
            objCommand.Parameters.Add(inputParameter11);

            SqlParameter inputParameter12 = new SqlParameter("@FavouriteQuote", favouritequote);
            objCommand.Parameters.Add(inputParameter12);

            SqlParameter inputParameter13 = new SqlParameter("@Goals", goals);
            objCommand.Parameters.Add(inputParameter13);

            SqlParameter inputParameter14 = new SqlParameter("@CommitmentType", commitmentType);
            objCommand.Parameters.Add(inputParameter14);

            SqlParameter inputParameter15 = new SqlParameter("@FavouriteMovieGenre", favoriteMovieGenre);
            objCommand.Parameters.Add(inputParameter15);

            SqlParameter inputParameter16 = new SqlParameter("@FavouriteBookGenre", favoriteBookGenre);
            objCommand.Parameters.Add(inputParameter16);

            SqlParameter inputParameter17 = new SqlParameter("@Address", address);
            objCommand.Parameters.Add(inputParameter17);

            SqlParameter inputParameter18 = new SqlParameter("@PhoneNumber", phonenumber);
            objCommand.Parameters.Add(inputParameter18);

            SqlParameter inputParameter19 = new SqlParameter("@FavouriteMovie", favoritemovie);
            objCommand.Parameters.Add(inputParameter19);

            SqlParameter inputParameter20 = new SqlParameter("@FavouriteBook", favoritebook);
            objCommand.Parameters.Add(inputParameter20);

            SqlParameter inputParameter21 = new SqlParameter("@FavouriteRestaurant", favoriterestaurant);
            objCommand.Parameters.Add(inputParameter21);

            SqlParameter inputParameter22 = new SqlParameter("@Dislikes", dislikes);
            objCommand.Parameters.Add(inputParameter22);

            SqlParameter inputParameter23 = new SqlParameter("@IsVisible", isVisible);
            objCommand.Parameters.Add(inputParameter23);

            SqlParameter inputParameter24 = new SqlParameter("@AdditionalInterests", additionalInterests);
            objCommand.Parameters.Add(inputParameter24);

            SqlParameter inputParameter25 = new SqlParameter("@Dealbreaker", dealbreaker);
            objCommand.Parameters.Add(inputParameter25);

            SqlParameter inputParameter26 = new SqlParameter("@Biography", biography);
            objCommand.Parameters.Add(inputParameter26);

            SqlParameter returnParameter = new SqlParameter("@OperationResult", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(returnParameter);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);

            return result;

        }

        public UserProfile()
        {

        }

        public UserProfile(int privateid, int age, string height, string weight, string profilePhotoURL, string city, string state, string tagline, string occupation, string interests, string favoritecusine, string favouritequote, string goals, string commitmentType,  string favoriteMovieGenre, string favoriteBookGenre, string address, string phonenumber, string favoritemovie, string favoritebook, string favoriterestaurant, string dislikes, bool isVisible, string additionalInterests, string dealbreaker, string biography)
        {
            this.privateid = privateid;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.profilePhotoURL = profilePhotoURL;
            this.city = city;
            this.state = state;
            this.tagline = tagline;
            this.occupation = occupation;
            this.interests = interests;
            this.favoritecusine = favoritecusine;
            this.favouritequote = favouritequote;
            this.goals = goals;
            this.commitmentType = commitmentType;
            this.favoriteMovieGenre = favoriteMovieGenre;
            this.address = address;
            this.favoriteBookGenre = favoriteBookGenre;
            this.phonenumber = phonenumber;
            this.favoritemovie = favoritemovie;
            this.favoritebook = favoritebook;
            this.favoriterestaurant = favoriterestaurant;
            this.dislikes = dislikes;
            this.isVisible = isVisible;
            this.additionalInterests = additionalInterests;
            this.dealbreaker = dealbreaker;
            this.biography = biography;
        }


        public int PrivateId
        {
            get { return privateid; }
            set { privateid = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }

        }

        public string Height
        {
            get { return height; }
            set { height = value; }

        }

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }


        public string ProfilePhotoURL
        {
            get { return profilePhotoURL; }
            set { profilePhotoURL = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Tagline
        {
            get { return tagline; }
            set { tagline = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public string Interests
        {
            get { return interests; }
            set { interests = value; }
        }

        public string FavoriteCuisine
        {
            get { return favoritecusine; }
            set { favoritecusine = value; }
        }

        public string FavouriteQuote
        {
            get { return favouritequote; }
            set { favouritequote = value; }
        }

        public string Goals
        {
            get { return goals; }
            set { goals = value; }
        }

        public string CommitmentType
        {
            get { return commitmentType; }
            set { commitmentType = value; }
        }

        public string FavoriteMovieGenre
        {
            get { return favoriteMovieGenre; }
            set { favoriteMovieGenre = value; }
        }

        public string FavoriteBookGenre
        {
            get { return favoriteBookGenre; }
            set { favoriteBookGenre = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string PhoneNumber
        {
            get { return phonenumber; }
            set { phonenumber = value; }
        }

        public string FavoriteMovie
        {
            get { return favoritemovie; }
            set { favoritemovie = value; }  

        }

        public string FavoriteBook
        {
            get { return favoritebook; }
            set { favoritebook = value; }

        }

        public string FavoriteRestaurant
        {
            get { return favoriterestaurant; }
            set { favoriterestaurant = value; }
        }

        public string Dislikes
        {
            get { return dislikes; }
            set { dislikes = value; }

        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public string AdditionalInterests
        {
            get { return additionalInterests; }
            set { additionalInterests = value; }
        }
        public string Dealbreaker
        {
            get { return dealbreaker; }
            set { dealbreaker = value; }
        }
        public string Biography
        {
            get { return biography; }
            set { biography = value; }
        }
    }
}
