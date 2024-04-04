using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Utilities;
using System.Data;
using System.Data.SqlClient;


namespace DatingSiteCoreAPI.Controllers
{
    [Route("api/CreateAccount")]
    [ApiController]
    public class DBUserAccountController : ControllerBase
    {


        [HttpPost()]
        [HttpPost("AddPrivateInfo")]

        public bool AddPrivateInfo([FromBody] PrivateUserInfo acc)

        {

            // then this one will private info properties 
            bool result;
            PrivateUserInfo privateinfo = acc;

            int successfuladd = privateinfo.CreateAccount(acc.FirstName, acc.LastName, acc.Email, acc.PrivateUsername, acc.Password);

            if (successfuladd > 0)
            {
                result = true;
            }
            else { result = false; }


            return result;


        }

        [HttpPost()]
        [HttpPost("Login")]

        public bool Login([FromBody] PrivateUserInfo user)
        {
            bool result;
            PrivateUserInfo privateinfo = user;

            int Login = privateinfo.Login(user.PrivateUsername, user.Password);

            if (Login > 0)
            {
                result = true;
            }
            else { result = false; }
            return result;



        }

        [HttpPost()]
        [HttpPost("AddUserInfo")]

        public bool AddUserInfo([FromBody] UserProfile acc, [FromBody] SecurityQuestion ques )
        {
            bool result = true;

            UserProfile User = acc;
            SecurityQuestion question = ques;   


            int AddUserSuccess = User.AddUserInfo(acc.PrivateId, acc.Age, acc.Height, acc.Weight, acc.ProfilePhotoURL, acc.City, acc.State, acc.Description, acc.Occupation, acc.Interests, acc.FavoriteCuisine, acc.FavouriteQuote, acc.Goals, acc.CommitmentType, acc.FavoriteMovieGenre, acc.FavoriteBookGenre, acc.Address, acc.PhoneNumber, acc.FavoriteMovie, acc.FavoriteBook, acc.FavoriteRestaurant, acc.Dislikes);
            int AddQuestionSuccess = question.AddSecurityQuestions(ques.PrivateId, ques.Question_One, ques.Question_Two, ques.Question_Three, ques.Answer_One, ques.Answer_Two, ques.Answer_Three);
            if (AddUserSuccess > 0)
            {
                result = true;
            }
            else { result = false; }


            return result;







        }


    }

}
