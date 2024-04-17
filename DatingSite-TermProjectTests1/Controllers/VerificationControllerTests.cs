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



        public void PopulateProfiles()
        {
            VerificationController controller = new VerificationController();








        }




    }
}
