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



namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        [TestMethod()]

        public void FilterActionTest()
        {


            // Arrange
            // in my testing I need to create a mock data 
            // the reason is because I need data for the cookies/form data getting using post method
            // 
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(context => context.Request.Cookies["UsernameTest"]).Returns("TestUser");
            mockHttpContext.SetupGet(context => context.Request.Form["lessThanAge"]).Returns("");

            mockHttpContext.SetupGet(context => context.Request.Form["filterCity"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterState"]).Returns("AZ");
            mockHttpContext.SetupGet(context => context.Request.Form["filterOccupation"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["interests"]).Returns("");
            mockHttpContext.SetupGet(context => context.Request.Form["filterCommitmentType"]).Returns("");

            var controller = new DashboardController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = controller.FilterAction() as ViewResult;






        }
    }
}

//controller.HttpContext.Request.Cookies["UsernameTest"] = "TestUser";
//controller.HttpContext.Request.Form["lessThanAge"] = "25";
//controller.HttpContext.Request.Form["filterCity"] = "New York";
//controller.HttpContext.Request.Form["filterState"] = "NY";
//controller.HttpContext.Request.Form["filterOccupation"] = "Engineer";
//controller.HttpContext.Request.Form["interests"] = "Reading, Sports";
//controller.HttpContext.Request.Form["filterCommitmentType"] = "Long-term";
