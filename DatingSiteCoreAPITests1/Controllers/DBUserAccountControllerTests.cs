using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSiteCoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DatingSiteCoreAPI.Controllers.Tests
{
    [TestClass()]
    public class DBUserAccountControllerTests
    {





        [TestMethod()]
        
        public void AddPrivateInfoTest()
        {
            PrivateUserInfo acc = new PrivateUserInfo();
            DBUserAccountController PrivateController = new DBUserAccountController();
             acc.FirstName = "John"; acc.LastName = "doe"; acc.Email = "1"; acc.PrivateUsername = "wonderworld"; acc.Password = "password";
             
            int ExpectedValue = 1;
            int ActualValue = acc.CreateAccount(acc.FirstName, acc.LastName, acc.Email, acc.PrivateUsername, acc.Password);
            Assert.AreEqual(ExpectedValue, ActualValue);


        }



        [TestMethod()]
        public void AddUserInfoTest()
         {
            // created user info 
            UserProfile profile = new UserProfile();
            DBUserAccountController controller = new DBUserAccountController();
            profile.PrivateId = 30; profile.Age = 25; profile.Height = "6'2\"";
            profile.Weight = "180 lbs"; profile.ProfilePhotoURL = "https://example.com/profile.jpg";
            profile.City = "New York"; profile.State = "NY";
            profile.Description = "I am a software engineer."; profile.Occupation = "Software Engineer";
            profile.Interests = "Reading, hiking, programming"; profile.FavoriteCuisine = "Italian";
            profile.FavouriteQuote = "Success is not final, failure is not fatal: It is the courage to continue that counts.";
            profile.Goals = "Become a senior developer"; profile.CommitmentType = "Serious";
            profile.FavoriteMovieGenre = "Action"; profile.FavoriteBookGenre = "Fantasy";
            profile.Address = "123 Main St"; profile.PhoneNumber = "555-123-4567";
            profile.FavoriteMovie = "Inception"; profile.FavoriteBook = "The Lord of the Rings";
            profile.FavoriteRestaurant = "Sushi Palace"; profile.Dislikes = "Cold weather";
            profile.IsVisible = true;

            // it is a bool method so we will check if method return true. if it does. then it works
            bool ExpectedValue = true;
           bool ActualValue = controller.AddUserInfo(profile);   

            Assert.AreEqual(ExpectedValue, ActualValue);    
           



            
        
        
        }    
    }
}