using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using Utilities;


namespace DatingSite_TermProject.Controllers
{
    public class VerificationController : Controller
    {
        ViewManagement view = new ViewManagement();
        public ActionResult actionresult()
        {
            return View("~/Views/Main/Dashboard.cshtml");
        }
        public IActionResult Verification()
        {
            if (Request.Cookies.TryGetValue("VerCode", out string encryptedCookie))
            {
                string decryptedCode = EncryptionHelper.Decrypt(encryptedCookie);
                string userEnteredCode = Request.Form["VerificationCode"].ToString();
                if (decryptedCode == userEnteredCode)
                {
                    ViewBag.ErrorMessage = "The codes are the same.";
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
                }
                else
                {
                    ViewBag.ErrorMessage = "The codes are not the same.";
                    return View("~/Views/Home/Verification.cshtml");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "A problem occurred Please go through the login process again.";
                return View("~/Views/Home/Verification.cshtml");
            }
        }
      
       
       


        





    }
}
