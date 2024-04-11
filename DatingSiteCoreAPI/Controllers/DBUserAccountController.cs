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
        [HttpPost("ForgotPassword")]

        public bool ForgotPassword([FromBody] PrivateUserInfo user)
        {
            bool result;
            PrivateUserInfo privateinfo = user;

            int Reset = privateinfo.Forgot(user.Email);

            if (Reset > 0)
            {
                result = true;
            }
            else { result = false; }
            return result;



        }

        [HttpPost()]
        [HttpPost("AddUserInfo")]
       
        public bool AddUserInfo([FromBody] UserProfile acc )
        { 

            // combine the two classes and combine the table into one
            bool result = true;

            UserProfile User = acc;
              
            int AddUserSuccess = User.AddUserInfo(acc.PrivateId, acc.Age, acc.Height, acc.Weight, acc.ProfilePhotoURL, acc.City, acc.State, acc.Description, acc.Occupation, acc.Interests, acc.FavoriteCuisine, acc.FavouriteQuote, acc.Goals, acc.CommitmentType, acc.FavoriteMovieGenre, acc.FavoriteBookGenre, acc.Address, acc.PhoneNumber, acc.FavoriteMovie, acc.FavoriteBook, acc.FavoriteRestaurant, acc.Dislikes, acc.IsVisible);
        
            if (AddUserSuccess > 0)
            {
                result = true;
            }
            else { result = false; }

            return result;

        }

        [HttpPost()]
        [HttpPost("AddUserImages")]
        public bool AddUserImages([FromBody] ImageGallery img)
        {

            // combine the two classes and combine the table into one
            bool result = true;

            ImageGallery Userimgs = img;

            int AddUserImageSuccess = Userimgs.AddImages(img.PrivateId ,img.Image1, img.Image2, img.Image3, img.Image4, img.Image5);

            if (AddUserImageSuccess > 0)
            {
                result = true;
            }
            else { result = false; }

            return result;

        }

        [HttpPost()]
        [HttpPost("AddUserSecurityQuestions")]
        public bool AddUserSecurityQuestions([FromBody] SecurityQuestion secincoming)
        {

            // combine the two classes and combine the table into one
            bool result = true;

            SecurityQuestion secq = secincoming;

            int AddUserSecuritySuccess = secq.AddSecurityQuestions(secincoming.PrivateId, secincoming.Question_One, secincoming.Question_Two, secincoming.Question_Three, secincoming.Answer_One, secincoming.Answer_Two, secincoming.Answer_Three);

            if (AddUserSecuritySuccess > 0)
            {
                result = true;
            }
            else { result = false; }

            return result;

        }


    }

}
