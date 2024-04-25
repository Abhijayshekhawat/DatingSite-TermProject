using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DatingSiteCoreAPI;
using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies
using Utilities;
using DatingSite_TermProject.Controllers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Common;
using Azure.Core;

namespace DatingSite_TermProject.Controllers
{
    public class DatesController : Controller
    {
        ViewManagement view = new ViewManagement();
        DbDateManagement DateManager = new DbDateManagement();
        public IActionResult Dates()
        {
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            int privateid = userProfile.getPrivateId(savedUsername2);
            var dateCards = new DatesCardModel
            {
                DatesYouSent = DateManager.DatesYouSent(privateid, savedUsername2),
                DatesYouReceived = DateManager.DatesYouReceived(privateid, savedUsername2)
            };
            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
            return View("~/Views/Main/Dates/Dates.cshtml", dateCards);
        }
        public IActionResult AcceptDate()
        {
            DatesCardModel dateCards = new DatesCardModel();
            int privateid = int.Parse(Request.Form["PrivateId"].ToString());
            string savedUsername = Request.Cookies["Username"].ToString();
            dateCards = DateManager.ApproveDate(privateid, savedUsername);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername);

            return View("~/Views/Main/Dates/Dates.cshtml", dateCards);
        }
        public IActionResult RejectDate()
        {
            DatesCardModel dateCards = new DatesCardModel();
            int privateid = int.Parse(Request.Form["PrivateId"].ToString());
            string savedUsername = Request.Cookies["Username"].ToString();
            dateCards = DateManager.DenyDate(privateid, savedUsername);
            ViewBag.ProfileImage = view.GetUserImage(savedUsername);
            return View("~/Views/Main/Dates/Dates.cshtml", dateCards);
        }
    }
}
