using DatingSite_TermProject.Models;
using DatingSiteCoreAPI;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using Utilities;

namespace DatingSite_TermProject.Controllers
{
    public class ForgotPasswordController : Controller
    {
        string CreateAccountAPI_Url = "http://localhost:5046/api/CreateAccount";
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("~/Views/Home/ForgotPassword.cshtml");
        }
        [HttpPost]
        public IActionResult ForgotPassword(PrivateUserInfoModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.ErrorMessage = "Please enter your email.";
                return View("~/Views/Home/ForgotPassword.cshtml");
            }
            PrivateUserInfoModel privateinfo = new PrivateUserInfoModel();
            privateinfo.FirstName = "NoValue";
            privateinfo.LastName = "NoValue";
            privateinfo.Email = privateinfo.PrivateUsername = Request.Form["Email"].ToString();
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
                WebRequest request = WebRequest.Create(CreateAccountAPI_Url + "/ForgotPassword");
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
                    Random Verfication = new Random();
                    string code = "";
                    code = Verfication.Next(100000, 1000000).ToString();
                    string cookieToken = code;
                    string secretToken = EncryptionHelper.Encrypt(cookieToken);
                    CookieOptions tokenOptions = new CookieOptions();
                    tokenOptions.Expires = DateTime.Now.AddMinutes(2);
                    HttpContext.Response.Cookies.Append("Token", secretToken, tokenOptions);
                    string link = Url.Action("AnswerSecQuestion", "AnswerSecQuestion", new { token = secretToken }, protocol: HttpContext.Request.Scheme);


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
                    String strTO = resetUser.Email;
                    String strFROM = "PasswordReset-Matchup@gmail.com";
                    String strSubject = "Reset password link for MatchUp";
                    String strMessage = "Hi " + FirstName + "! Here is the link to reset your password: " + link;
                    //**** Uncomment everything before return view  when you want to send the email 

                    try
                    {
                        objEmail.SendMail(strTO, strFROM, strSubject, strMessage);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send verification email: {ex.Message}");
                        ViewBag.ErrorMessage = "The email wasn't sent because: " + ex.Message;
                    }
                    ViewBag.ErrorMessage = "Check your email for the reset link!";
                    PopulateQuestion(strTO);
                    return View("~/Views/Home/Login.cshtml");

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

        private void PopulateQuestion(string email)
        {
            ProfileSecQuestionModel secQuestion = new ProfileSecQuestionModel();
            DBConnect objDB = new DBConnect();

            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetSecurityQuestionsByEmail";

            SqlParameter inputParameter = new SqlParameter("@Email", email);
            objCommand.Parameters.Add(inputParameter);

            DataSet ds = objDB.GetDataSet(objCommand);

            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                secQuestion.Question_One = dr["Question_One"].ToString();
                secQuestion.Question_Two = dr["Question_Two"].ToString();
                secQuestion.Question_Three = dr["Question_Three"].ToString();
                secQuestion.Answer_One = dr["Answer_One"].ToString();
                secQuestion.Answer_Two = dr["Answer_Two"].ToString();
                secQuestion.Answer_Three = dr["Answer_Three"].ToString();
            }
            Random rnd = new Random();
            int questionIndex = rnd.Next(1, 4);

            // Select random question and answer
            string selectedQuestion = "";
            string selectedAnswer = "";

            switch (questionIndex)
            {
                case 1:
                    selectedQuestion = secQuestion.Question_One; 
                    selectedAnswer = secQuestion.Answer_One;
                    break;
                case 2:
                    selectedQuestion = secQuestion.Question_Two;
                    selectedAnswer = secQuestion.Answer_Two;
                    break;
                case 3:
                    selectedQuestion = secQuestion.Question_Three;
                    selectedAnswer = secQuestion.Answer_Three;
                    break;
            }
            // Package the selected question and answer
            var selectedQA = new
            {
                Question = selectedQuestion,
                Answer = selectedAnswer
            };
            ViewBag.Question = selectedQuestion;
            
            //Serialize the object to string
            string cookieObject = JsonSerializer.Serialize(selectedQA);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddSeconds(100);
            string SecretCookie = EncryptionHelper.Encrypt(cookieObject);
            HttpContext.Response.Cookies.Append("SecurityQuestion", SecretCookie, options);
        }
    }
}
