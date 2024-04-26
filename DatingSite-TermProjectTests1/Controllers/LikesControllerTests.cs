using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSite_TermProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class LikesControllerTests
    {
        [TestMethod()]
        public void DeleteLikeTest()
        {
            string username = "will_smith";
            ViewManagement view = new ViewManagement();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["Username"]).Returns("will_smith");
            mockHttpContext.SetupGet(context => context.Request.Form["DislikeeID"]).Returns("84");

            var controller = new LikesController();
            controller.ViewBag.ProfileImage = view.GetUserImage(username);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object


            };
            var result = controller.DeleteLike() as ViewResult;

            // checked to see if the viewname is the same of what it is suppose to return 
           
             var likeCards = result.ViewData.Model as LikeCardsModel;

            Assert.IsNotNull(likeCards);
                 

            // testing to see if the likecards list that is being delievered to the razor view is not null
            // the functionality mostly tested in our web api unit test.


          
        }
    }
}