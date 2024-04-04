﻿namespace DatingSiteCoreAPI
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
        private string description;
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

    


        public UserProfile()
        {

        }

        public UserProfile(int privateid, int age, string height, string weight, string profilePhotoURL, string city, string state, string description, string occupation, string interests, string favoritecusine, string favouritequote, string goals, string commitmentType,  string favoriteMovieGenre, string favoriteBookGenre, string address, string phonenumber, string favoritemovie, string favoritebook, string favoriterestaurant, string dislikes)
        {
            this.privateid = privateid;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.profilePhotoURL = profilePhotoURL;
            this.city = city;
            this.state = state; 
            this.description = description;
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




    }
}