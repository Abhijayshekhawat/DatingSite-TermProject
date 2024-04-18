using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSite_TermProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Identity.Client;
using DatingSite_TermProject.Models;





namespace DatingSite_TermProject.Controllers.Tests
{
    [TestClass()]
    public class VerificationControllerTests
    {


        [TestMethod()]
        public void actionresultTest()
        {
            VerificationController controller = new VerificationController();

            
            var result = controller.actionresult() as ViewResult;

            Assert.IsNotNull(result);
            string ExpectedValue = "~/Views/Main/Dashboard.cshtml";
            string ActualValue = result.ViewName;
            Assert.AreEqual(ExpectedValue, ActualValue);



            // 



            StringWriter sw = new StringWriter();
            {
                //var viewResult = result.ViewEngine.FindView(controller.ControllerContext, "~/Views/Main/Dashboard.cshtml", false);

                // saying this null reference/ value
                var viewEngineResult = result.ViewEngine.FindView(controller.ControllerContext, "~/Views/Main/Dashboard.cshtml", true);



                var viewContext = new ViewContext(
                            controller.ControllerContext,
                            viewEngineResult.View,
                            controller.ViewData,
                            controller.TempData,
                            sw,
                            new HtmlHelperOptions()
                        );
                viewEngineResult.View.RenderAsync(viewContext);


                var test = sw.GetStringBuilder().ToString();

                bool ActualValue2 = test.Contains("<h1>Dashboard</h1>");

                bool ExpectedValue2 = true;

                Assert.AreEqual(ExpectedValue2, ActualValue2);

            }

    }


        [TestMethod()]  
        public void PopulateProfilesTest()
        {
            VerificationController controller = new VerificationController();

            int privateid = 70;
            List<CardsModel> list = new List<CardsModel>();

            CardsModel card1 = new CardsModel(); 
            list = controller.PopulateProfiles( privateid );
            //in this method we are able to get profiles from our database
            // inner join creates a table where both id lies PK --> FK
            
            // returns a list of the profiles
            // i create individual profile object to test if the data we are getting is accurate

            card1 = list.ElementAt(0);
            string ExpectedValue = "John";
            string ActualValue = card1.FirstName;
            Assert.AreEqual (ExpectedValue, ActualValue);   

            // Since i tested a value from the privateinfo table
            // I will do one from profile table

            string ExpectedValue2 = "CA";
            string ActualValue2 = card1.State;

            Assert.AreEqual(ExpectedValue2, ActualValue2);

            // this method also exclude the user that is currently logged in
            // I excluded private id 70 so it should skip over it and go to the next profile
            // instead of jane doe it will be Mike smith 

            CardsModel card2 = new CardsModel();
            card2 = list.ElementAt(1);

            string ExpectedValue3 = "Mike";
            string ActualValue3 = card2.FirstName;

            Assert.AreEqual(ExpectedValue3 , ActualValue3);

            CardsModel card3 = new CardsModel();

            card3 = list.ElementAt(2);

            string ExpectedValue4 = "Connor";
            string ActualValue4 = card3.LastName;  









        }




    }
}
