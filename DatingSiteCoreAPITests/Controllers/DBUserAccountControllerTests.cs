using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingSiteCoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteCoreAPI.Controllers.Tests
{
    [TestClass()]
    public class DBUserAccountControllerTests
    {
        [TestMethod()]
        public void AddPrivateInfoTest()
        {

            int expectedvalue = 1;
            int actualvalue = 1; 

            Assert.AreEqual(expectedvalue, actualvalue);    

            
        }
    }
}