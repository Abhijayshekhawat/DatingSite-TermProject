namespace DatingSite_TermProject.Models
{
    public class ProfileModel
    {
        public class UserProfile
        {
            private int profileID;
            private int privateID;
            private int age;
            private string height;
            private string weight;
            private string profilePhotoURL;
            private string city;
            private string state;
            private string description;
            private string occupation;
            private string interests;
            private string favouriteCuisine;
            private string favouriteQuote;
            private string goals;
            private string commitmentType;
            private bool isVisible;
            private string favouriteMovieGenre;
            private string favouriteBookGenre;
            private string address;
            private string phoneNumber;
            private string favouriteMovie;
            private string favouriteBook;
            private string favouriteRestaurant;
            private string dislikes;

            public UserProfile()
            {
            }

            public UserProfile(int profileID, int privateID, int age, string height, string weight, string profilePhotoURL,
                       string city, string state, string description, string occupation, string interests,
                       string favouriteCuisine, string favouriteQuote, string goals, string commitmentType,
                       bool isVisible, string favouriteMovieGenre, string favouriteBookGenre, string address,
                       string phoneNumber, string favouriteMovie, string favouriteBook, string favouriteRestaurant,
                       string dislikes)
            {
                this.profileID = profileID;
                this.privateID = privateID;
                this.age = age;
                this.height = height;
                this.weight = weight;
                this.profilePhotoURL = profilePhotoURL;
                this.city = city;
                this.state = state;
                this.description = description;
                this.occupation = occupation;
                this.interests = interests;
                this.favouriteCuisine = favouriteCuisine;
                this.favouriteQuote = favouriteQuote;
                this.goals = goals;
                this.commitmentType = commitmentType;
                this.isVisible = isVisible;
                this.favouriteMovieGenre = favouriteMovieGenre;
                this.favouriteBookGenre = favouriteBookGenre;
                this.address = address;
                this.phoneNumber = phoneNumber;
                this.favouriteMovie = favouriteMovie;
                this.favouriteBook = favouriteBook;
                this.favouriteRestaurant = favouriteRestaurant;
                this.dislikes = dislikes;
            }

            public int ProfileID
            {
                get { return profileID; }
                set { profileID = value; }
            }
            public int PrivateID
            {
                get { return privateID; }
                set { privateID = value; }
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
            public string Description
            {
                get { return description; }
                set { description = value; }
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
            public string FavouriteCuisine
            {
                get { return favouriteCuisine; }
                set { favouriteCuisine = value; }
            }
            public string FavouriteQuote
            {
                get { return favouriteQuote; }
                set { favouriteQuote = value; }
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
            public bool IsVisible
            {
                get { return isVisible; }
                set { isVisible = value; }
            }
            public string FavouriteMovieGenre
            {
                get { return favouriteMovieGenre; }
                set { favouriteMovieGenre = value; }
            }
            public string FavouriteBookGenre
            {
                get { return favouriteBookGenre; }
                set { favouriteBookGenre = value; }
            }
            public string Address
            {
                get { return address; }
                set { address = value; }
            }
            public string PhoneNumber
            {
                get { return phoneNumber; }
                set { phoneNumber = value; }
            }
            public string FavouriteMovie
            {
                get { return favouriteMovie; }
                set { favouriteMovie = value; }
            }
            public string FavouriteBook
            {
                get { return favouriteBook; }
                set { favouriteBook = value; }
            }
            public string FavouriteRestaurant
            {
                get { return favouriteRestaurant; }
                set { favouriteRestaurant = value; }
            }
            public string Dislikes
            {
                get { return dislikes; }
                set { dislikes = value; }
            }
        }
    }
}
