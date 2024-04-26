using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingSiteCoreAPI.Controllers
{
    [Route("api/MatchUp")]
    [ApiController]
    public class MatchUpController : ControllerBase
    {
        [HttpGet()]
        public string Get()
        {
            return "MatchUp API is working";
        }
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

        [HttpPost()]
        [HttpPost("AddNewDateRequest")]

        public bool SendDateRequest([FromBody] DateRequest daterequest)
        {
            bool result;

            DateRequest Date = daterequest;

            int successfullike = Date.SendRequestSuccessfully(daterequest.RequesterUsername, daterequest.RequesteeId);


            if (successfullike > 0)
            {
                result = true;
            }
            else { result = false; }


            return result;

        }

        [HttpGet()]
        [HttpGet("GetMatches")]

        public List<Card> GetMatchesProfile([FromBody] Match match)
        {
            Match populateProfile = match;
            List<Card> result = populateProfile.GetMatches(match.MatchUsername);
           
            return result;

        }


        [HttpGet()]
        [HttpGet("GetProfiles")]


        public List<Card> GetProfiles([FromBody] Profile p)
        {
            Profile PopulateProfiles = p;

            List<Card> cardlist = PopulateProfiles.PopulateProfiles(p.ProfilePrivateId);




            return cardlist;    

        }

        [HttpPut()]
        [HttpPut("UpdateMatch")]

        public void UpdateMatchesCards()
        {
            UpdateMatches update = new UpdateMatches();

            update.UpdateMatch();



        }



        private static List<User> users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" }
    };

        // PUT api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            try
            {
                var existingUser = users.Find(x => x.Id == id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
