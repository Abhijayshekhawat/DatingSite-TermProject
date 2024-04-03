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

        [HttpGet()]
        [HttpGet("Login")]

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


        }

}
