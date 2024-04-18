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


namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class DashboardControllerTests
    {
        [TestMethod()]
        public void FilterActionTest()
        {
            DashboardController controller = new DashboardController();
            string username = "john_doe";
            string Lessthanage = "";
            string filtercity = "";
            string filterstate = "AZ";
            string filteroccupation = "";
            string interests = "";
            string filterCommitmentType = "";

            controller.FilterAction();
           
            ViewResult v = controller.View();

            Assert.IsNotNull(v.Model); 

           
            List<CardsModel> cardslist = v.Model as List<CardsModel>;
            Assert.IsNotNull(cardslist);



        }
    }
}