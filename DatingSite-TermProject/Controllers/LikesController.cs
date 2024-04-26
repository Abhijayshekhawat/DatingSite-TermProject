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

    public class LikesController : Controller
    {
        string CreateAccountAPI_Url = "https://cis-iis2.temple.edu/Spring2024/CIS3342_tuh18229/WebAPITest/api/MatchUp";
        ViewManagement view = new ViewManagement();
        DbLikeManagement LikeManager = new DbLikeManagement();
        public IActionResult DeleteLike()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername = Request.Cookies["Username"].ToString();
                    LikeRequestModel like = new LikeRequestModel();
                    // get the data from the form / model PrivateUserInfoModel  
                    like.LikerUsername = savedUsername;
                    like.LIkeeId = int.Parse(Request.Form["DislikeeID"].ToString());
                    // Serialize an Account object into a JSON string.
                    var jsonPayload = JsonSerializer.Serialize(like);
                    try
                    {
                        // Send the account object to the Web API that will be used to store a new account record in the database.
                        // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                        WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/DeleteLikes");
                        request.Method = "DELETE";
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
                        if (data == "true")
                        {
                            string savedUsername2 = Request.Cookies["Username"].ToString();
                            ViewBag.ProfileImage = view.GetUserImage(savedUsername2);

                            var likeCards = new LikeCardsModel
                            {
                                PeopleWhoLikedYou = LikeManager.PopulatePeopleWhoLikedYou(savedUsername2),
                                PeopleYouLiked = LikeManager.PopulatePeopleYouLiked(savedUsername2),
                            };
                            return View("~/Views/Main/Likes.cshtml", likeCards);
                        }
                        else
                            ViewBag.ErrorMessage = "A problem occurred while adding the customer to the database. The data wasn't recorded.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Error: " + ex.Message;
                    }

                    string savedUsername3 = Request.Cookies["Username"].ToString();

                    ViewBag.ProfileImage = view.GetUserImage(savedUsername3);

                    var likeCards1 = new LikeCardsModel
                    {
                        PeopleWhoLikedYou = LikeManager.PopulatePeopleWhoLikedYou(savedUsername3),
                        PeopleYouLiked = LikeManager.PopulatePeopleYouLiked(savedUsername3)
                    };
                    return View("~/Views/Main/Likes.cshtml", likeCards1);
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
        public IActionResult Likes()
        {
            if (HttpContext.Request.Cookies.TryGetValue("isValid", out string encryptedAuth))
            {
                var decryptedAuth = EncryptionHelper.Decrypt(encryptedAuth);
                if (decryptedAuth == "Valid")
                {
                    string savedUsername2 = Request.Cookies["Username"].ToString();
                    UserProfileModel userProfile = new UserProfileModel();
                    int privateid = userProfile.getPrivateId(savedUsername2);
                    var likeCards = new LikeCardsModel
                    {
                        PeopleWhoLikedYou = LikeManager.PopulatePeopleWhoLikedYou(savedUsername2),
                        PeopleYouLiked = LikeManager.PopulatePeopleYouLiked(savedUsername2)
                    };

                    ViewBag.ProfileImage = view.GetUserImage(savedUsername2);
                    return View("~/Views/Main/Likes.cshtml", likeCards);
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
