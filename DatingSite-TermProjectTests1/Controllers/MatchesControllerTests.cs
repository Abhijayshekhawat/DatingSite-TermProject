using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSite_TermProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class MatchesControllerTests
    {
        [TestMethod()]
        public void AddDateRequestTest()
        {

            string username = "will_smith";
            ViewManagement view = new ViewManagement();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["RequesteeID"]).Returns("84");

            var controller = new MatchesController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.AddDateRequest() as ViewResult;

            // checked to see if the viewname is the same of what it is suppose to return 

            var Cardslist = result.ViewData.Model as List<CardsModel>;

            Assert.IsNotNull(Cardslist);


          
        }

        [TestMethod()]
        public void DeleteMatchesTest()
        {

            string username = "will_smith";
            ViewManagement view = new ViewManagement();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["MatcherID"]).Returns("84");

            var controller = new MatchesController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.DeleteMatches() as ViewResult;

            // checked to see if the viewname is the same of what it is suppose to return 

            var Cardslist = result.ViewData.Model as List<CardsModel>;

            Assert.IsNotNull(Cardslist);

            // similar to like controller testing to see if the controller returning the viewresult(View())
            // i will test the functionality in the web API for both.
           
        }
    }
}