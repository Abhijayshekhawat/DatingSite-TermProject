using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DatingSiteCoreAPI;
using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers

using System.IO;    // needed for Stream and Stream Reader

using System.Net;




namespace DatingSite_TermProject.Controllers
{
    public class CreateAccountController : Controller
    {

        string CreateAccountAPI_Url = "http://localhost:5046/";




        [HttpPost]
        public IActionResult CreateAccount()
        {

            
            


            return View();
        }
    }
}
