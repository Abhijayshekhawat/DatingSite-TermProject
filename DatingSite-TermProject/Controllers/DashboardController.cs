using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

using DatingSite_TermProject.Models;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using Microsoft.AspNetCore.Http; // need for cookies
using Utilities;
using DatingSite_TermProject.Controllers;

namespace DatingSite_TermProject.Controllers
{
    public class DashboardController : Controller
    {
        string CreateAccountAPI_Url = "https://cis-iis2.temple.edu/Spring2024/CIS3342_tuh18229/WebAPITest/api/MatchUp";
        ViewManagement view = new ViewManagement();
        DbFilterManagement filter = new DbFilterManagement();
        DbUpdateMatch updateMatch = new DbUpdateMatch();
        public IActionResult Dashboard()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
            string savedUsername2 = Request.Cookies["Username"].ToString();
            UserProfileModel userProfile = new UserProfileModel();
            userProfile.PrivateId = userProfile.getPrivateId(savedUsername2);


            var jsonPayload = JsonSerializer.Serialize(userProfile);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/GetProfiles");
                request.Method = "GET";
                request.ContentLength = jsonPayload.Length;
                request.ContentType = "application/json";
                // Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonPayload);
                writer.Flush();
                writer.Close();
                // Read the data from the Web Response, which requires working with streams.
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                List<CardsModel> Cardslist3 = new List<CardsModel>(); 

                if (!string.IsNullOrEmpty(data))
                {
                   
                    Cardslist3 = JsonSerializer.Deserialize<List<CardsModel>>(data);
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist3);
                }
                else
                    ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            string savedUsername3 = Request.Cookies["Username"].ToString();
            List<CardsModel> Cardslist2 = view.PopulateProfiles(userProfile.getPrivateId(savedUsername3));
            ViewBag.ProfileImage = view.GetUserImage(savedUsername3);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername3);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge();
            return View("~/Views/Main/Dashboard.cshtml", Cardslist2);
        }
                    List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
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

        [HttpPost]
        public IActionResult AddLikes()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    UserProfileModel userProfile = new UserProfileModel();

                    string savedUsername = Request.Cookies["Username"].ToString();
                    LikeRequestModel like = new LikeRequestModel();

            like.LikerUsername = savedUsername;
            like.LIkeeId = Int32.Parse(Request.Form["LikeeID"].ToString());
            var jsonPayload = JsonSerializer.Serialize(like);
            try
            {
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddLikes");
                request.Method = "POST";
                request.ContentLength = jsonPayload.Length;
                request.ContentType = "application/json";
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonPayload);
                writer.Flush();
                writer.Close();
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                String data = reader.ReadToEnd();
                reader.Close();
                response.Close();
                if (data == "true")
                {
                    var jsonPayload2 = JsonSerializer.Serialize(userProfile);
                    try
                    {
                        // Send the account object to the Web API that will be used to store a new account record in the database.
                        // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                        WebRequest request2 = WebRequest.Create(CreateAccountAPI_Url + "/UpdateMatch");
                        request2.Method = "PUT";
                        request2.ContentLength = jsonPayload2.Length;
                        request2.ContentType = "application/json";
                        // Write the JSON data to the Web Request
                        StreamWriter writer2 = new StreamWriter(request2.GetRequestStream());
                        writer2.Write(jsonPayload2);
                        writer2.Flush();
                        writer2.Close();
                        // Read the data from the Web Response, which requires working with streams.
                        WebResponse response2 = request2.GetResponse();
                        Stream theDataStream2 = response2.GetResponseStream();
                        StreamReader reader2 = new StreamReader(theDataStream2);
                        String data2 = reader2.ReadToEnd();
                        reader2.Close();
                        response2.Close();



                        string savedUsername4 = Request.Cookies["Username"].ToString();
                        List<CardsModel> Cardslist4 = view.PopulateProfiles(userProfile.getPrivateId(savedUsername4));
                        ViewBag.ProfileImage = view.GetUserImage(savedUsername4);
                        ViewBag.FirstName = view.GetUserFirstName(savedUsername4);
                        ViewBag.States = view.PopulateStates();
                        ViewBag.Interests = view.PopulateInterests();
                        ViewBag.Commitments = view.PopulateCommitmentType();
                        ViewBag.MaxAge = view.MaxAge();
                        ViewBag.MinAge = view.MinAge();
                        return View("~/Views/Main/Dashboard.cshtml", Cardslist4);





                    }

                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Error: " + ex.Message;
                    }
                }



                //updateMatch.UpdateMatch();
                string savedUsername2 = Request.Cookies["Username"].ToString();
                    List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                return View("~/Views/Main/Dashboard.cshtml", Cardslist);
                
               
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            string savedUsername3 = Request.Cookies["Username"].ToString();
            List<CardsModel> Cardslist2 = view.PopulateProfiles(userProfile.getPrivateId(savedUsername3));
            ViewBag.ProfileImage = view.GetUserImage(savedUsername3);
            ViewBag.FirstName = view.GetUserFirstName(savedUsername3);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge();
            return View("~/Views/Main/Dashboard.cshtml", Cardslist2);
        }
                    like.LikerUsername = savedUsername;
                    like.LIkeeId = Int32.Parse(Request.Form["LikeeID"].ToString());
                    var jsonPayload = JsonSerializer.Serialize(like);
                    try
                    {
                        WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/AddLikes");
                        request.Method = "POST";
                        request.ContentLength = jsonPayload.Length;
                        request.ContentType = "application/json";
                        StreamWriter writer = new StreamWriter(request.GetRequestStream());
                        writer.Write(jsonPayload);
                        writer.Flush();
                        writer.Close();
                        WebResponse response = request.GetResponse();
                        Stream theDataStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(theDataStream);
                        String data = reader.ReadToEnd();
                        reader.Close();
                        response.Close();
                        if (data == "true")
                        {
                            updateMatch.UpdateMatch();
                            string savedUsername2 = Request.Cookies["Username"].ToString();
                            List<CardsModel> Cardslist = view.PopulateProfiles(userProfile.getPrivateId(savedUsername2));
                            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                            ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                            ViewBag.States = view.PopulateStates();
                            ViewBag.Interests = view.PopulateInterests();
                            ViewBag.Commitments = view.PopulateCommitmentType();
                            ViewBag.MaxAge = view.MaxAge();
                            ViewBag.MinAge = view.MinAge();
                            return View("~/Views/Main/Dashboard.cshtml", Cardslist);
                        }
                        else
                            ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Error: " + ex.Message;
                    }
                    string savedUsername3 = Request.Cookies["Username"].ToString();
                    List<CardsModel> Cardslist2 = view.PopulateProfiles(userProfile.getPrivateId(savedUsername3));
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername3);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername3);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    return View("~/Views/Main/Dashboard.cshtml", Cardslist2);
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

        [HttpPost]
        public IActionResult ResetFilters()
        {
            // Call methods to repopulate ViewBag properties
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    List<CardsModel> Cardslist = new List<CardsModel>();
                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    ViewBag.FirstName = view.GetUserFirstName(savedUsername2);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    Cardslist = filter.ResetFilter(userProfile.getPrivateId(savedUsername2));


                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
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


        [HttpPost]
        public IActionResult FilterAction()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    // get username
                    filter.Username = Request.Cookies["Username"].ToString();
                    // populate all the viewbags
                    ViewBag.ProfileImage = view.GetUserImage(filter.Username);
                    ViewBag.FirstName = view.GetUserFirstName(filter.Username);
                    ViewBag.States = view.PopulateStates();
                    ViewBag.Interests = view.PopulateInterests();
                    ViewBag.Commitments = view.PopulateCommitmentType();
                    ViewBag.MaxAge = view.MaxAge();
                    ViewBag.MinAge = view.MinAge();
                    // properties with request form data 
                    filter.MaxAge = Request.Form["ageRangeMax"].ToString();
                    filter.MinAge = Request.Form["ageRangeMin"].ToString();
                    filter.FilterCity = Request.Form["filterCity"].ToString();
                    filter.FilterState = Request.Form["filterState"].ToString();
                    filter.FilterOccupation = Request.Form["filterOccupation"].ToString();
                    filter.InterestsString = Request.Form["interests"].ToString();
            // get username
            filter.Username = Request.Cookies["Username"].ToString();
            //populate all the viewbags
            ViewBag.ProfileImage = view.GetUserImage(filter.Username);
            ViewBag.FirstName = view.GetUserFirstName(filter.Username);
            ViewBag.States = view.PopulateStates();
            ViewBag.Interests = view.PopulateInterests();
            ViewBag.Commitments = view.PopulateCommitmentType();
            ViewBag.MaxAge = view.MaxAge();
            ViewBag.MinAge = view.MinAge();
            // properties with request form data 
            filter.MaxAge = Request.Form["ageRangeMax"].ToString();
            filter.MinAge = Request.Form["ageRangeMin"].ToString();
            filter.FilterCity = Request.Form["filterCity"].ToString();
            filter.FilterState = Request.Form["filterState"].ToString();
            filter.FilterOccupation = Request.Form["filterOccupation"].ToString();
            filter.InterestsString = Request.Form["interests"].ToString();

            filter.FilterCommitmentType = Request.Form["filterCommitmentType"].ToString();

            List<CardsModel> Cardslist = new List<CardsModel>();

            Cardslist = filter.ApplyFilter(filter.Username, filter.MaxAge, filter.MinAge, filter.FilterCity, filter.FilterState, filter.FilterOccupation, filter.InterestsString, filter.FilterCommitmentType);


                    return View("~/Views/Main/Dashboard.cshtml", Cardslist);
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

