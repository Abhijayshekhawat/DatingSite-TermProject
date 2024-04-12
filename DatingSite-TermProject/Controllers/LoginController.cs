using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using DatingSiteCoreAPI;
using System.Text.Json;  // needed for JSON serializers
using System.IO;    // needed for Stream and Stream Reader
using System.Net;
using System.Data;
using Microsoft.AspNetCore.Identity;
using static DatingSite_TermProject.Models.ProfileModel;


namespace DatingSite_TermProject.Controllers
{
    public class LoginController : Controller
    {

        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";

        public IActionResult Login()
        {
            if (Request.Cookies.TryGetValue("fastLogin", out string encryptedCookie))
            {
                string decryptedCookie = EncryptionHelper.Decrypt(encryptedCookie);
                PrivateUserInfoModel userDetails = JsonSerializer.Deserialize<PrivateUserInfoModel>(decryptedCookie);
                ViewBag.Username = userDetails.PrivateUsername;
                ViewBag.Pwd = userDetails.Password;
            }
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();
            privateinfo.FirstName = "NoValue";
            privateinfo.LastName = "NoValue";
            privateinfo.Email = "NoValue";
            privateinfo.PrivateUsername = Request.Form["Username"].ToString();
            privateinfo.Password = Request.Form["Password"].ToString();
            //email
            string UserEmail = "";
            // for email
            string FirstName = "";
            string LastName = "";
            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(privateinfo);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/Login");
                request.Method = "POST";
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
                    SaveLogin();
                    //Create a random number for the verification code
                    Random Verfication = new Random();
                    string code = "";
                    code = Verfication.Next(100000, 1000000).ToString();

                    DataSet mydata = privateinfo.GetUserInfo(privateinfo.PrivateUsername,"");
                    DataTable dt = mydata.Tables[0];

                    //Get the email and first name of the user and send the email
                    foreach (DataRow dr in dt.Rows)
                    {

                        UserEmail = dr["Email"].ToString();
                        FirstName = dr["FirstName"].ToString();
                       
                    }
                    EmailModel objEmail = new EmailModel();
                    String strTO = UserEmail;
                    String strFROM = "Verification-Matchup@gmail.com";
                    String strSubject = "Verification Code for MatchUp";
                    String strMessage = "Hi "+ FirstName + "! Here is your verification code: " + code;

                    //**** Uncomment everything before return view  when you want to send the email 

                    try
                    {
                        objEmail.SendMail(strTO, strFROM, strSubject, strMessage);
                        string cookieObject = code;
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddSeconds(30);
                        string SecretCookie = EncryptionHelper.Encrypt(cookieObject);
                        HttpContext.Response.Cookies.Append("VerCode", SecretCookie, options);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send verification email: {ex.Message}");
                        ViewBag.ErrorMessage = "The email wasn't sent because: " + ex.Message;
                    }
                    ViewBag.ErrorMessage = "The customer was successfully loggedin.";
                    return View("~/Views/Home/Verification.cshtml");

                }
                // **
                // need to change this to going into the view with two step verification
                //**

                else { 
                    ViewBag.ErrorMessage = "A problem occurred while logging in.";
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Home/Login.cshtml");

        }
        private void SaveLogin()
        {
            if (Request.Form["SaveDetails"].ToString() == "Yes")
            {
                PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();
                privateinfo.FirstName = "NoValue";
                privateinfo.LastName = "NoValue";
                privateinfo.Email = "NoValue";
                privateinfo.PrivateUsername = Request.Form["Username"].ToString();
                privateinfo.Password = Request.Form["Password"].ToString();
                string cookieObject = JsonSerializer.Serialize(privateinfo);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddYears(1);
                string SecretCookie = EncryptionHelper.Encrypt(cookieObject);
                HttpContext.Response.Cookies.Append("fastLogin", SecretCookie, options);
            }
            else if (Request.Form["SaveDetails"].ToString() == "No")
            {
                PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();
                privateinfo.FirstName = "NoValue";
                privateinfo.LastName = "NoValue";
                privateinfo.Email = "NoValue";
                privateinfo.PrivateUsername = Request.Form["Username"].ToString();
                privateinfo.Password = Request.Form["Password"].ToString();
                string cookieObject = JsonSerializer.Serialize(privateinfo);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now;
                string SecretCookie = EncryptionHelper.Encrypt(cookieObject);
                HttpContext.Response.Cookies.Append("fastLogin", SecretCookie, options);
            }
            else
            {

            }
        }
        
    }
}
