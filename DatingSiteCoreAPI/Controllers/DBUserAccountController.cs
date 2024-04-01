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

        public bool AddPrivateInfo(PrivateUserInfo privateinfo) 
        
        {





            
            
            return true; 
        
        
        
        
        
        }





    }











}
