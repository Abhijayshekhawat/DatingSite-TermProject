using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DatingSiteCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new User { Id = 2, Name = "Jane Doe", Email = "jane.doe@example.com" }
    };

        // PUT api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var existingUser = users.Find(x => x.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            // Return a response to indicate the update was successful
            return Ok(existingUser);
        }
    }
}
