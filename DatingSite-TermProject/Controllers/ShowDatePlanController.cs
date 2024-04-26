using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies
using DatingSite_TermProject.Controllers;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.Serialization.Formatters.Binary;
using Utilities;

namespace DatingSite_TermProject.Controllers
{
    public class ShowDatePlanController : Controller
    {
        ViewManagement view = new ViewManagement();
        public IActionResult ShowDatePlan()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    var datePlan = new DatePlanModel();

                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    int privateid = userProfile.getPrivateId(savedUsername2);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    return View("~/Views/Main/Dates/ShowDatePlan.cshtml", datePlan);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please Login First";
                    return View("~/Views/Home/Login.cshtml");
                }
            }   
            else
            {
                ViewBag.ErrorMessage = "Please Login First";
                return View("~/Views/Home/Login.cshtml");
            }

        }  
    }
}
// every date method was move to datemanagement class model