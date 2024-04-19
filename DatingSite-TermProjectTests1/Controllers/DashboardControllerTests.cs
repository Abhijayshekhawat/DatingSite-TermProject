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

            //Random Verfication = new Random();
            //string code = "";
            //code = Verfication.Next(100000, 1000000).ToString();
            //string cookieToken = code;
            //string secretToken = EncryptionHelper.Encrypt(cookieToken);
            //CookieOptions tokenOptions = new CookieOptions();
            //tokenOptions.Expires = DateTime.Now.AddMinutes(2);
            //HttpContext.Response.Cookies.Append("Token", secretToken, tokenOptions);
            DashboardController controller = new DashboardController();
           
           
            ViewResult v = controller.View();

            Assert.IsNotNull(v.Model); 

           
            List<CardsModel> cardslist = v.Model as List<CardsModel>;
            Assert.IsNotNull(cardslist);



        }
    }
}