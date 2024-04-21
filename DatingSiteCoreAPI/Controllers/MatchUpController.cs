using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingSiteCoreAPI.Controllers
{
    [Route("api/MatchUp")]
    [ApiController]
    public class MatchUpController : ControllerBase
    {

        [HttpPost()]
        [HttpPost("AddLikes")]
        public bool LikeUser([FromBody] LikeRequest likerequest)
        {

            bool result;

            LikeRequest like = likerequest;

            int successfullike = like.AddLikeSuccessfully(likerequest.LikerUsername, likerequest.LIkeeId);


            if (successfullike > 0)
            {
                result = true;
            }
            else { result = false; }


            return result;
        }

        [HttpDelete()]
        [HttpDelete("DeleteLikes")]
        public bool Hater(DislikeRequest hater)
        
        {

            bool result;

            DislikeRequest dislike = hater;

            int successfullike = dislike.DeleteLikeSuccessfully(hater.LikerUsername, hater.LikeeId);


            if (successfullike > 0)
            {
                result = true;
            }
            else { result = false; }


            return result;

        }

        [HttpDelete()]
        [HttpDelete("DeleteMatch")]
        public bool UnMatch(UnmatchRequest unmatchUser)
        {
            bool result;
            int success = unmatchUser.UnMatch(unmatchUser.MatcherUsername, unmatchUser.MatcherID);
            if (success >= 0)
            {
               result = true;
            }
            else { result = false; }
            return result;
        }
    }
}
