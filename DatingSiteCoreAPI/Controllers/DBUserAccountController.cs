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

        public bool AddPrivateInfo(int PrivateId, string FirstName, string LastName, string Email, string PrivateUsername, string Password) 
        
        {

            // then this one will private info properties 
            bool result = false;
            PrivateUserInfo privateinfo = new PrivateUserInfo();

           int successfuladd = privateinfo.CreateAccount(PrivateId,FirstName,LastName,Email,PrivateUsername,Password); 

            if(successfuladd > 0)
            
            {


                result = true;
            
            }

            else { result = false; }


            return result;







             
        
        
        
        
        
        }





    }











}
