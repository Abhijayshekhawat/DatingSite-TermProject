using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSite_TermProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Utilities;
using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        [TestMethod()]

        public void FilterActionTest()
        {
            ViewManagement view = new ViewManagement();
            DbFilterManagement filter = new DbFilterManagement();

            // Arrange
            // in my testing I need to create a mock data 
            // the reason is because I need data for the cookies/form data getting using post method
            // so basically im getting data by requesting/responding from client/server side
            // however that is done by runtime from clicking a submit button or dropdownlist / textbox.. etc
            // we cant do it here for unit testing so i download a package call Moq which is a class library
            // to crreate a mock context of what you want it to be. so i am able to create the cookies
            // and post method data from form using Moq. 

            string username = "john_doe";
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterState"]).Returns("AZ");
            mockHttpContext.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Friends");



            // created controller to call the method and fill up the view bags that populate the filter data

            var controller = new DashboardController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ViewBag.FirstName = view.GetUserFirstName(username);
            controller.ViewBag.States = view.PopulateStates();
            controller.ViewBag.Interests = view.PopulateInterests();
            controller.ViewBag.Commitments = view.PopulateCommitmentType();
            controller.ViewBag.MaxAge = view.MaxAge();
            controller.ViewBag.MinAge = view.MinAge();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.FilterAction() as ViewResult;

            // checked to see if the viewname is the same of what it is suppose to return 
            Assert.AreEqual("~/Views/Main/Dashboard.cshtml", result.ViewName);
            var cardsList = result.ViewData.Model as List<CardsModel>;

            //var cardsList = filter.ApplyFilter("john_doe", "40", "22", "", "AZ", "", "", "Friends");
            // check to see if cardslist is null or not
            // the filter is logged in as will smith so basivally the filter will have someone between
            // 22-40 & State being AZ & commmitment type being Friends
            // and the only card testing locally will be 
            Assert.IsNotNull(cardsList);
            var card = cardsList.First();
            // check if the data is correctly populated into the filter cards
            // check first name
            string ActualValue = card.FirstName;
            string ExpectedValue = "Mike";
            Assert.AreEqual(ExpectedValue, ActualValue);
            string ExpectedValue2 = "Smith";
            string ActualValue2 = card.LastName;
            Assert.AreEqual(ExpectedValue2, ActualValue2);
            int ExpectedValue3 = 28;
            int ActualValue3 = card.Age;

            Assert.AreEqual(ExpectedValue3, ActualValue3);






            string username2 = "will_smith";
            var mockHttpContext2 = new Mock<HttpContext>();
            mockHttpContext2.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext2.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext2.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext2.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext2.SetupGet(context => context.Request.Form["filterState"]).Returns("PA");
            mockHttpContext2.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext2.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext2.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Marriage");
            //



            var controller2 = new DashboardController();
            controller2.ViewBag.ProfileImage = view.GetUserImage(username2);
            controller2.ViewBag.FirstName = view.GetUserFirstName(username2);
            controller2.ViewBag.States = view.PopulateStates();
            controller2.ViewBag.Interests = view.PopulateInterests();
            controller2.ViewBag.Commitments = view.PopulateCommitmentType();
            controller2.ViewBag.MaxAge = view.MaxAge();
            controller2.ViewBag.MinAge = view.MinAge();
            controller2.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext2.Object


            };



            var result2 = controller2.FilterAction() as ViewResult;
            var cardsList2 = result2.ViewData.Model as List<CardsModel>;
            // check to see if its null or not
            Assert.IsNotNull(cardsList2);

            // check to see if the data is populated in the cardlist2 this filter have five cards this time instead of 1
            // so ima check first one 
            var card2 = cardsList2.ElementAt(0);
            string ExpectedValue4 = "Jessica";
            string ActualValue4 = card2.FirstName;
            Assert.AreEqual(ExpectedValue4, ActualValue4);
            string ExpectedValue5 = "Jones";
            string ActualValue5 = card2.LastName;
            Assert.AreEqual(ExpectedValue5, ActualValue5);
            string ExpectedValue6 = "165";
            string ActualValue6 = card2.Weight.Trim();
            Assert.AreEqual(ExpectedValue6, ActualValue6);

            // testing different card in the same list
            var card3 = cardsList2.ElementAt(2);
            string ExpectedValue7 = "Vision";
            string ActualValue7 = card3.FirstName.Trim();
            Assert.AreEqual(ExpectedValue7, ActualValue7);
            string ExpectedValue8 = "Film maker and story teller.";
            string ActualValue8 = card3.Tagline.TrimEnd();
            Assert.AreEqual(ExpectedValue8, ActualValue8);
            string ExpectedValue9 = "Direct a feature film.";
            string ActualValue9 = card3.Goals.TrimEnd();
            Assert.AreEqual(ExpectedValue9, ActualValue9);



            // going to test one more time for the filter.

            string username3 = "will_smith";
            var mockHttpContext3 = new Mock<HttpContext>();
            mockHttpContext3.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext3.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext3.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterState"]).Returns("PA");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Relationship");
            //



            var controller3 = new DashboardController();
            controller3.ViewBag.ProfileImage = view.GetUserImage(username2);
            controller3.ViewBag.FirstName = view.GetUserFirstName(username2);
            controller3.ViewBag.States = view.PopulateStates();
            controller3.ViewBag.Interests = view.PopulateInterests();
            controller3.ViewBag.Commitments = view.PopulateCommitmentType();
            controller3.ViewBag.MaxAge = view.MaxAge();
            controller3.ViewBag.MinAge = view.MinAge();
            controller3.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext3.Object


            };



            var result3 = controller3.FilterAction() as ViewResult;
            var cardsList3 = result3.ViewData.Model as List<CardsModel>;
            // check to see if its null or not
            Assert.IsNotNull(cardsList3);
            // testing two cards from this 3rd filter cardlist 

            var card4 = cardsList3.ElementAt(2);
            string ExpectedValue10 = "Peter";
            string ActualValue10 = card4.FirstName;
            Assert.AreEqual(ExpectedValue10, ActualValue10);
            string ExpectedValue11 = "5ft 9in";
            string ActualValue11 = card4.Height.TrimEnd();
            var card5 = cardsList3.ElementAt(5);

            string ExpectedValue12 = "Thor";

            string ActualValue12 = card5.FirstName.TrimEnd();

            Assert.AreEqual(ExpectedValue12, ActualValue12);

            string ExpectedValue13 = "Drama";
            string ActualValue13 = card5.FavouriteMovieGenre.TrimEnd();

            Assert.AreEqual(ExpectedValue13, ActualValue13);

        }


        [TestMethod]

        public void ResetFilterActionTest()
        {
            ViewManagement view = new ViewManagement();
            string username = "will_smith";

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterState"]).Returns("AZ");
            mockHttpContext.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Friends");



            // created controller to call the method and fill up the view bags that populate the filter data

            var controller = new DashboardController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ViewBag.FirstName = view.GetUserFirstName(username);
            controller.ViewBag.States = view.PopulateStates();
            controller.ViewBag.Interests = view.PopulateInterests();
            controller.ViewBag.Commitments = view.PopulateCommitmentType();
            controller.ViewBag.MaxAge = view.MaxAge();
            controller.ViewBag.MinAge = view.MinAge();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.FilterAction() as ViewResult;

            // checked to see if the viewname is the same of what it is suppose to return 
            Assert.AreEqual("~/Views/Main/Dashboard.cshtml", result.ViewName);
            var cardsList = result.ViewData.Model as List<CardsModel>;

            var card1 = cardsList.FirstOrDefault();

            string ExpectedValue1 = "Mike";
            string ActualValue1 = card1.FirstName;

            Assert.AreEqual(ExpectedValue1, ActualValue1);

            int ExpectedValue2 = 71;
            int ActualValue2 = card1.PrivateId;
            Assert.AreEqual(ExpectedValue2, ActualValue2);


            // for the reset method . i will test this method by checking the first card(profile) that is grab from our database
            // with our filter then check to see the first card from our reset filter method. since it will populate all the profiles that has yet been liked by the user.
            // the user i am using is will smith(will_smith)

            var mockHttpContext2 = new Mock<HttpContext>();
            mockHttpContext2.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");

            var controller2 = new DashboardController();
            controller2.ViewBag.ProfileImage = view.GetUserImage(username);
            controller2.ViewBag.FirstName = view.GetUserFirstName(username);
            controller2.ViewBag.States = view.PopulateStates();
            controller2.ViewBag.Interests = view.PopulateInterests();
            controller2.ViewBag.Commitments = view.PopulateCommitmentType();
            controller2.ViewBag.MaxAge = view.MaxAge();
            controller2.ViewBag.MinAge = view.MinAge();
            controller2.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext2.Object


            };
            var result2 = controller2.ResetFilters() as ViewResult;
            var cardsList2 = result2.ViewData.Model as List<CardsModel>;

            Assert.IsNotNull(cardsList2);
            // testing the first card(profile)

            var card3 = cardsList2.FirstOrDefault();

            string ExpectedValue3 = "Jane";
            string ActualValue3 = card3.FirstName;
            Assert.AreEqual(ExpectedValue3, ActualValue3);

            string ExpectedValue4 = "Writer";
            string ActualValue4 = card3.Occupation.TrimEnd();

            Assert.AreEqual(ExpectedValue4, ActualValue4);


            // second apply filter 

            string username2 = "will_smith";
            var mockHttpContext3 = new Mock<HttpContext>();
            mockHttpContext3.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext3.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext3.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterState"]).Returns("PA");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext3.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Marriage");
            //



            var controller3 = new DashboardController();
            controller3.ViewBag.ProfileImage = view.GetUserImage(username2);
            controller3.ViewBag.FirstName = view.GetUserFirstName(username2);
            controller3.ViewBag.States = view.PopulateStates();
            controller3.ViewBag.Interests = view.PopulateInterests();
            controller3.ViewBag.Commitments = view.PopulateCommitmentType();
            controller3.ViewBag.MaxAge = view.MaxAge();
            controller3.ViewBag.MinAge = view.MinAge();
            controller3.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext3.Object


            };



            var result3 = controller3.FilterAction() as ViewResult;
            var cardsList3 = result3.ViewData.Model as List<CardsModel>;
            var card4 = cardsList3.First();
            Assert.IsNotNull(cardsList3);
            // first one should be jessica when filteraction is apply
            string ExpectedValue5 = "Jessica";
            string ActualValue5 = card4.FirstName;
            Assert.AreEqual(ExpectedValue5, ActualValue5);


            string ExpectedValue6 = "Catch-22";
            string ActualValue6 = card4.FavouriteBook;

            Assert.AreEqual(ExpectedValue6, ActualValue6);

            // now we reset filter and check if the cardlist repopulate therefore the first should be Jane

            var mockHttpContext4 = new Mock<HttpContext>();
            mockHttpContext4.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");

            var controller4 = new DashboardController();
            controller4.ViewBag.ProfileImage = view.GetUserImage(username);
            controller4.ViewBag.FirstName = view.GetUserFirstName(username);
            controller4.ViewBag.States = view.PopulateStates();
            controller4.ViewBag.Interests = view.PopulateInterests();
            controller4.ViewBag.Commitments = view.PopulateCommitmentType();
            controller4.ViewBag.MaxAge = view.MaxAge();
            controller4.ViewBag.MinAge = view.MinAge();
            controller4.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext2.Object


            };
            var result4 = controller4.ResetFilters() as ViewResult;
            var cardsList4 = result4.ViewData.Model as List<CardsModel>;

            Assert.IsNotNull(cardsList4);
            // testing the first card(profile)

            var card5 = cardsList4.FirstOrDefault();

            string ExpectedValue7 = "Jane";
            string ActualValue7 = card5.FirstName;
            Assert.AreEqual(ExpectedValue3, ActualValue3);

            string ExpectedValue8 = "The Martian";
            string ActualValue8 = card3.FavouriteMovie.TrimEnd();

            Assert.AreEqual(ExpectedValue8, ActualValue8);

            // 3rd filter apply

            string username3 = "will_smith";
            var mockHttpContext5 = new Mock<HttpContext>();
            mockHttpContext5.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext5.SetupGet(context => context.Request.Form["ageRangeMax"]).Returns("40");
            mockHttpContext5.SetupGet(context => context.Request.Form["ageRangeMin"]).Returns("22");
            mockHttpContext5.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext5.SetupGet(context => context.Request.Form["filterState"]).Returns("PA");
            mockHttpContext5.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext5.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext5.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("Relationship");
            //



            var controller5 = new DashboardController();
            controller5.ViewBag.ProfileImage = view.GetUserImage(username2);
            controller5.ViewBag.FirstName = view.GetUserFirstName(username2);
            controller5.ViewBag.States = view.PopulateStates();
            controller5.ViewBag.Interests = view.PopulateInterests();
            controller5.ViewBag.Commitments = view.PopulateCommitmentType();
            controller5.ViewBag.MaxAge = view.MaxAge();
            controller5.ViewBag.MinAge = view.MinAge();
            controller5.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext5.Object


            };



            var result5 = controller5.FilterAction() as ViewResult;
            var cardsList5 = result5.ViewData.Model as List<CardsModel>;
            // check to see if its null or not
            Assert.IsNotNull(cardsList5);

            var card6 = cardsList5.First();
            string ExpectedValue9 = "Sara";
            string ActualValue9 = card6.FirstName;
            Assert.AreEqual(ExpectedValue9, ActualValue9);

            /// test age
            int ExpectedValue10 = 30;
            int ActualValue10 = card6.Age;

            Assert.AreEqual(ExpectedValue10, ActualValue10);
            // reset 

            var mockHttpContext6 = new Mock<HttpContext>();
            mockHttpContext6.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");

            var controller6 = new DashboardController();
            controller6.ViewBag.ProfileImage = view.GetUserImage(username);
            controller6.ViewBag.FirstName = view.GetUserFirstName(username);
            controller6.ViewBag.States = view.PopulateStates();
            controller6.ViewBag.Interests = view.PopulateInterests();
            controller6.ViewBag.Commitments = view.PopulateCommitmentType();
            controller6.ViewBag.MaxAge = view.MaxAge();
            controller6.ViewBag.MinAge = view.MinAge();
            controller6.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext2.Object


            };
            var result6 = controller4.ResetFilters() as ViewResult;
            var cardsList6 = result4.ViewData.Model as List<CardsModel>;

            var card7 = cardsList6.First();
            string ExpectedValue11 = "Jane";
            string ActualValue11 = card7.FirstName;

            Assert.AreEqual(ExpectedValue11, ActualValue11);

            // overalll i tested 3 cases of filteraction and it worked each of the first profile of every list was different
            // while every resetfilter method was all the same profile.
        }

        [TestMethod]
        public void AddLikesTest()
        {
            string username = "will_smith";
            ViewManagement view = new ViewManagement();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["LikeeID"]).Returns("84");
            var controller = new DashboardController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ViewBag.FirstName = view.GetUserFirstName(username);
            controller.ViewBag.States = view.PopulateStates();
            controller.ViewBag.Interests = view.PopulateInterests();
            controller.ViewBag.Commitments = view.PopulateCommitmentType();
            controller.ViewBag.MaxAge = view.MaxAge();
            controller.ViewBag.MinAge = view.MinAge();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.AddLikes() as ViewResult;
            var cardsList = result.ViewData.Model as List<CardsModel>;
            // test to see the cardsList is not null. if it is not null it the like was successfully added
            Assert.IsNotNull(cardsList);
            // 73 is the private id of will smith the profile im using so it will exclude him in the dashboard
            var cardsListDemo = view.PopulateProfiles(1);

            // I will use populatedprofile method to get the dashboard list. then if i use the add like method it should update the dashboard and make the person i like disapppear from the dashboard
            // test the count of the list too to see if it goes down
            // thor is 13
            int ExpectedBeforeListCount = 31;
            int ActualBeforeListCount = cardsListDemo.Count;
            Assert.AreEqual(ExpectedBeforeListCount,ActualBeforeListCount);
            // so we now know the populated cardlist without any likes lets see the count if the person likes few people
            // the likes make it so the next time he go to the dashboard the dashboards goes lower.
            int ExpectedAfterLikeList = 26;
            int AfterLikeListCount = cardsList.Count;
            Assert.AreEqual(ExpectedAfterLikeList,AfterLikeListCount);












        }



    }
}




