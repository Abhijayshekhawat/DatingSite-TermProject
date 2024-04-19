﻿namespace DatingSite_TermProject.Models
{
    public class CardsModel
    {
        private int age;
        private string height;
        private string weight;
        private string profilePhotoUrl;
        private string city;
        private string state;
        private string tagline;
        private string occupation;
        private string interests;
        private string favouriteCuisine;
        private string favouriteQuote;
        private string goals;
        private string commitmentType;
     
        private string favouriteMovieGenre;
        private string favouriteBookGenre;
        private string address;
        private string phoneNumber;
        private string favouriteMovie;
        private string favouriteBook;
        private string favouriteRestaurant;
        private string dislikes;
        private string additionalInterests;
        private string dealbreaker;
        private string biography;
        private string firstname;
        private string lastname;

        public CardsModel() { }
        public CardsModel(
            string firstname,
            string lastname,
            string profilePhotoUrl,
            string city,
            string state,
            string tagline,
            string occupation,
            string interests,
            string favouriteCuisine,
            string favouriteQuote,
            string goals,
            string commitmentType,
            string favouriteMovieGenre,
            string favouriteBookGenre,
            string address,
            string phoneNumber,
            string favouriteMovie,
            string favouriteBook,
            string favouriteRestaurant,
            string dislikes,
            string additionalInterests,
            string dealbreaker,
            string biography,
            int age,
            string height,
            string weight
            )
        {
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.profilePhotoUrl = profilePhotoUrl;
            this.city = city;
            this.state = state;
            this.tagline = tagline;
            this.occupation = occupation;
            this.interests = interests;
            this.favouriteCuisine = favouriteCuisine;
            this.favouriteQuote = favouriteQuote;
            this.goals = goals;
            this.commitmentType = commitmentType;
            this.favouriteMovieGenre = favouriteMovieGenre;
            this.favouriteBookGenre = favouriteBookGenre;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.favouriteMovie = favouriteMovie;
            this.favouriteBook = favouriteBook;
            this.favouriteRestaurant = favouriteRestaurant;
            this.dislikes = dislikes;
            this.additionalInterests = additionalInterests;
            this.dealbreaker = dealbreaker;
            this.biography = biography;
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
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
            get { return profilePhotoUrl; }
            set { profilePhotoUrl = value; }
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


