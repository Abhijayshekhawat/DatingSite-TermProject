using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSiteCoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;





namespace DatingSiteCoreAPI.Controllers.Tests
{
    [TestClass()]
    public class DBUserAccountControllerTests
    {
        PrivateUserInfo acc = new PrivateUserInfo();
        PrivateUserInfo acc2 = new PrivateUserInfo();
        PrivateUserInfo acc3 = new PrivateUserInfo();
        PrivateUserInfo acc4 = new PrivateUserInfo();




        [TestMethod()]
        
        public void AddPrivateInfoTest()
        {
            
            // add the private user class to create a account variable 
            
            DBUserAccountController PrivateController = new DBUserAccountController();
            acc.FirstName = "John"; acc.LastName = "doe"; acc.Email = "1"; acc.PrivateUsername = "wonderworld";  acc.Password = "password";

            acc2.FirstName = "Jay"; acc2.LastName = "Bo"; acc2.Email = "2"; acc2.PrivateUsername = "SungJinwoo"; acc2.Password = "password2";

            acc3.FirstName = "Cat"; acc3.LastName = "Hat"; acc3.Email = "3"; acc3.PrivateUsername = "SungJinwoo"; acc3.Password = "password3";
            acc3.FirstName = "Bat"; acc3.LastName = "Man"; acc3.Email = "1"; acc3.PrivateUsername = "hello"; acc3.Password = "password4";
            // this method is bool method so i test to see if the return is true. if it is true then the method works
            bool ExpectedValue = true;
            bool ActualValue = PrivateController.AddPrivateInfo(acc);   
            Assert.AreEqual(ExpectedValue, ActualValue);

            // this method is adding private info into our private info database table.
            // so now i will check in the database to see if the values are the same. 

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            string email = "";
            string username = "";
            string firstname = "";


            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUserInfo";

            SqlParameter inputParameter1 = new SqlParameter("@Username", acc.PrivateUsername);
            objCommand.Parameters.Add(inputParameter1);



            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
           
            foreach (DataRow dr in dt.Rows)
            {

                email = dr["Email"].ToString();
                firstname = dr["FirstName"].ToString();
                username = dr["PrivateUsername"].ToString() ;

            }
            // check to see if email matches the account email that was inserted into the database
            string ExpectedValue2 = acc.Email;
            string ActualValue2 = email;

          
           
            Assert.AreEqual(ExpectedValue2, ActualValue2);
            // check to see if first name matches the account first name that was inserted into the database

            string ExpectedValue3 = acc.FirstName;
            string ActualValue3 = firstname;
            Assert.AreEqual(ExpectedValue3, ActualValue3);
            // check to see if username matches the account username that was inserted into the database
            string ExpectedValue4 = acc.PrivateUsername;
            string ActualValue4 = username;

            Assert.AreEqual (ExpectedValue4, ActualValue4);

            // I will add another user to make sure creating account can create multiple users instead of just one.

            bool ExpectedValue5 = true;
            bool ActualValue5 = PrivateController.AddPrivateInfo(acc2);

            Assert.AreEqual(ExpectedValue5, ActualValue5);


            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();
            string password = "";
            string username2 = "";
            string lastname = "";


            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetUserInfo";
           
            SqlParameter inputParameter2 = new SqlParameter("@Username", acc2.PrivateUsername);
            objCommand2.Parameters.Add(inputParameter2);



            DataSet ds2 = objDB2.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];

            foreach (DataRow dr in dt2.Rows)
            {

                password = dr["Password"].ToString();
                username2 = dr["PrivateUsername"].ToString();
                lastname = dr["LastName"].ToString();

            }
            // check data inside 

            string ExpectedValue6 = acc2.Password;
            string ActualValue6 = password;

            Assert.AreEqual(ExpectedValue6, ActualValue6);

            string ExpectedValue7 = acc2.PrivateUsername;
            string ActualValue7 = username2;
            Assert.AreEqual(ExpectedValue7, ActualValue7);


            string ExpectedValue8 = acc2.LastName;
            string ActualValue8 = lastname;
            Assert.AreEqual(ExpectedValue8, ActualValue8);






            // check if we can add account3 even tho it has same username as account 2.(it is unique so it should be able to . expected is false.
            bool ExpectedValue9 = false;
            bool ActualValue9 = PrivateController.AddPrivateInfo(acc3);
            Assert.AreEqual(ExpectedValue9, ActualValue9);  

            // check if we can add account 4 even tho it has same email as account 1

            bool ExpectedValue10 = false;
            bool ActualValue10 = PrivateController.AddPrivateInfo(acc4);



            

















        }



        [TestMethod()]
        public void AddUserInfoTest()
         {
            // created user info 
            string username1 = "wonderworld";
            
            UserProfile profile = new UserProfile();
            UserProfile profile2= new UserProfile();
            UserProfile profile3 = new UserProfile();
            DBUserAccountController controller = new DBUserAccountController();
            profile.PrivateId = profile.getPrivateId(username1); profile.Age = 25; profile.Height = "6'2";
            profile.Weight = "180 lbs"; profile.ProfilePhotoURL = "aaa";
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

            //profile 2
            string username2 = "SungJinwoo";
            profile2.PrivateId = profile2.getPrivateId(username2); profile2.Age = 30; profile2.Height = "5'10"; profile2.Weight = "160 lbs";
            profile2.ProfilePhotoURL = "bbb"; profile2.City = "Los Angeles"; profile2.State = "CA"; profile2.Description = "I work in marketing."; profile2.Occupation = "Marketing Manager";
            profile2.Interests = "Cooking, painting, traveling"; profile2.FavoriteCuisine = "Mexican";  profile2.FavouriteQuote = "The only way to do great work is to love what you do.";
            profile2.Goals = "Start my own business";     profile2.CommitmentType = "Open Relationship";   profile2.FavoriteMovieGenre = "Comedy"; profile2.FavoriteBookGenre = "Mystery";
            profile2.Address = "456 Elm St"; profile2.PhoneNumber = "555-987-6543";  profile2.FavoriteMovie = "The Hangover";     profile2.FavoriteBook = "Gone Girl";
            profile2.FavoriteRestaurant = "Taco Haven";    profile2.Dislikes = "Traffic jams"; profile2.IsVisible = true;

            //profile3

            string username3 = "SungJinwoo";
            profile3.PrivateId = profile3.getPrivateId(username3); profile3.Age = 50; profile3.Height = "6'10"; profile3.Weight = "130 lbs";
            profile3.ProfilePhotoURL = "bbb"; profile3.City = "Los Angeles"; profile3.State = "PA"; profile3.Description = "I work in marketing."; profile3.Occupation = "Marketing Manager";
            profile3.Interests = "Cooking, painting, traveling"; profile3.FavoriteCuisine = "Chinese"; profile3.FavouriteQuote = "The only way to do great work is to love what you do.";
            profile3.Goals = "Start my own business"; profile3.CommitmentType = "Close Relationship"; profile3.FavoriteMovieGenre = "Comedy"; profile3.FavoriteBookGenre = "Mystery";
            profile3.Address = "456 Elm St"; profile3.PhoneNumber = "555-987-6543"; profile3.FavoriteMovie = "The Hangover"; profile3.FavoriteBook = "How Not to Age";
            profile3.FavoriteRestaurant = "Taco Haven"; profile3.Dislikes = "Traffic jams"; profile3.IsVisible = true;

            // it is a bool method so we will check if method return true. if it does. then it works
            bool ExpectedValue = true;
            bool ActualValue = controller.AddUserInfo(profile);   

            Assert.AreEqual(ExpectedValue, ActualValue);


            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            string goals = "";
            string favoritebook = "";
            string phonenumber = "";


            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetUserInfo_Test";

            SqlParameter inputParameter1 = new SqlParameter("@PrivateId", profile.PrivateId);
            objCommand.Parameters.Add(inputParameter1);



            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {

                goals = dr["Goals"].ToString();
                favoritebook = dr["FavouriteBook"].ToString();
                phonenumber = dr["PhoneNumber"].ToString();

            }
            // check to see if the data is inserted
            string ExpectedValue2 = profile.Goals;
            string ActualValue2 = goals;
            Assert.AreEqual(ExpectedValue2,ActualValue2);

            string ExpectedValue3 = profile.FavoriteBook;
            string ActualValue3 = favoritebook;
            Assert.AreEqual(ExpectedValue3,ActualValue3);

            string ExpectedValue4 = profile.PhoneNumber;

            string ActualValue4 = phonenumber;


            Assert.AreEqual(ExpectedValue4,ActualValue4);

            // add information the other user to see if it works for others than just one

            bool ExpectedValue5 = true;
            bool ActualValue5 = controller.AddUserInfo(profile2);

            Assert.AreEqual(ExpectedValue5, ActualValue5);

            // check data for profile 2 

            DBConnect objDB2 = new DBConnect();
            SqlCommand objCommand2 = new SqlCommand();
            string interests = "";
            string  favbook = "";
            string dislikes = "";


            objCommand2.CommandType = CommandType.StoredProcedure;
            objCommand2.CommandText = "TP_GetUserInfo_Test";

            SqlParameter inputParameter2 = new SqlParameter("@PrivateId", profile2.PrivateId);
            objCommand2.Parameters.Add(inputParameter2);



            DataSet ds2 = objDB.GetDataSet(objCommand2);

            DataTable dt2 = ds2.Tables[0];

            foreach (DataRow dr in dt2.Rows)
            {

                interests = dr["Interests"].ToString();
                favbook = dr["FavouriteBook"].ToString();
                dislikes = dr["Dislikes"].ToString();

            }

            string ExpectedValue6 = profile2.Interests;
            string ActualValue6 = interests;
            Assert.AreEqual(ExpectedValue6, ActualValue6);

            string ExpectedValue7 = profile2.FavoriteBook;
            string ActualValue7 = favbook;
            Assert.AreEqual(ExpectedValue7, ActualValue7);

            string ExpectedValue8 = profile2.Dislikes;
            string ActualValue8 = dislikes;
            Assert.AreEqual(ExpectedValue8, ActualValue8);


            // check to see if i can add the third user even if they have the same id
            // should be a no since the private id is a foreign key that's linked to a primary key in another table.
            // and PKs are unique so we shouldn't able to add add a new profile that has the same private id

            bool ExpectedValue9 = false;
            bool ActualValue9 = controller.AddUserInfo(profile3);




        }


        [TestMethod()]
        public void LoginTest()
        {
            // here i take the information of account 1 and account 2 
            // these two accounts are the profiles i added from above
            acc.FirstName = "John"; acc.LastName = "doe"; acc.Email = "1"; acc.PrivateUsername = "wonderworld"; acc.Password = "password";

            acc2.FirstName = "Jay"; acc2.LastName = "Bo"; acc2.Email = "2"; acc2.PrivateUsername = "SungJinwoo"; acc2.Password = "password2";

            acc3.FirstName = "Cat"; acc3.LastName = "Hat"; acc3.Email = "3"; acc3.PrivateUsername = "SungJinwoo"; acc3.Password = "password3";
            DBUserAccountController PrivateController = new DBUserAccountController();
            // should return true since the account 1 i added to the database
            // 

            bool ExpectedValue1 = true;
            bool ActualValue1 = PrivateController.Login(acc);

            Assert.AreEqual(ExpectedValue1, ActualValue1);

            // check to see account 2 can also log in

            bool ExpectedValue2 = true;
            bool ActualValue2 = PrivateController.Login(acc2);
            Assert.AreEqual(ExpectedValue2 , ActualValue2);


            //  account 3 was not able to create its account because it did not have a unique username.
            // account 2 already have the username

            bool ExpectedValue3 = false;
            bool ActualValue3 =  PrivateController.Login(acc3); 







            






        }


    }
}