using DatingSite_TermProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace DatingSite_TermProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        public IActionResult ForgotPassword()
        {
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();
            privateinfo.FirstName = "NoValue";
            privateinfo.LastName = "NoValue";
            privateinfo.Email = privateinfo.PrivateUsername = Request.Form["resetEmail"].ToString();
            privateinfo.PrivateUsername = "NoValue";
            privateinfo.Password = "NoValue";
            //email
            string UserEmail = "";
            // for email
            string FirstName = "";
            string LastName;
            string Username;
            int UserID;
            // Serialize an Account object into a JSON string.
            var jsonPayload = JsonSerializer.Serialize(privateinfo);
            try
            {
                // Send the account object to the Web API that will be used to store a new account record in the database.
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/ResetPassword");
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

                    //Create a random number for the verification code
                    Random Verfication = new Random();
                    string link = "";
                    link = Verfication.Next(100000, 1000000).ToString();

                    DataSet mydata = privateinfo.GetUserInfo("",privateinfo.Email);
                    DataTable dt = mydata.Tables[0];
                    PrivateUserInfoModel resetUser = new PrivateUserInfoModel();

                    //Get the email and first name of the user and send the email
                    foreach (DataRow dr in dt.Rows)
                    {

                        resetUser.PrivateId = int.Parse(dr["PrivateId"].ToString());
                        resetUser.Email = dr["Email"].ToString();
                        resetUser.FirstName = dr["FirstName"].ToString();
                        resetUser.LastName = dr["LastName"].ToString();
                        resetUser.PrivateUsername = dr["PrivateUsername"].ToString();
                        resetUser.Password = "";
                    }
                    //Serialize the object to string
                    string cookieObject = JsonSerializer.Serialize(resetUser);
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddSeconds(100);
                    string SecretCookie = EncryptionHelper.Encrypt(cookieObject);
                    HttpContext.Response.Cookies.Append("userDetails", SecretCookie, options);
                    EmailModel objEmail = new EmailModel();
                    String strTO = UserEmail;
                    String strFROM = "PasswordReset-Matchup@gmail.com";
                    // String strFROM = "johnson@gmail.com";
                    String strSubject = "Reset password link for MatchUp";
                    String strMessage = "Hi " + FirstName + "! Here is the link to reset your password: " + link;
                    //**** Uncomment everything before return view  when you want to send the email 

                    //try
                    //{
                    //    objEmail.SendMail(strTO, strFROM, strSubject, strMessage);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($"Failed to send verification email: {ex.Message}");
                    //    ViewBag.ErrorMessage = "The email wasn't sent because: " + ex.Message;
                    //}
                    //ViewBag.ErrorMessage = "The customer was successfully loggedin.";
                    return View("~/Views/Home/ResetPassword.cshtml");

                }
                // **
                // need to change this to going into the view with two step verification
                //**

                else
                {
                    ViewBag.ErrorMessage = "A problem occurred while logging in.";
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            return View("~/Views/Home/Login.cshtml");

        }
    }
}
