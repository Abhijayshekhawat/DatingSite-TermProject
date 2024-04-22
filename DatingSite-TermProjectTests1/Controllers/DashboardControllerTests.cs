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



namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        [TestMethod()]
        public void FilterActionTest()
        {


            // Arrange
            var controller = new DashboardController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

           // Set up mock data for the request

           //controller.HttpContext.Request.Cookies["UsernameTest"] = "TestUser";
           //controller.HttpContext.Request.Form["lessThanAge"] = "25";
           // controller.HttpContext.Request.Form["filterCity"] = "New York";
           // controller.HttpContext.Request.Form["filterState"] = "NY";
           // controller.HttpContext.Request.Form["filterOccupation"] = "Engineer";
           // controller.HttpContext.Request.Form["interests"] = "Reading, Sports";
           // controller.HttpContext.Request.Form["filterCommitmentType"] = "Long-term";

            // Act
            var result = controller.FilterAction() as ViewResult;






        }
    }
}