using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_Things.Models;

namespace Server_Things.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BuurtboerContext db = new BuurtboerContext();

        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var query = db.Users.Select(u => u).ToList();
            var Users = JsonSerializer.Serialize(query);
            return Ok(Users);
        }

        [HttpGet("login")]
        public async Task<IActionResult> GetLogin(string email, string password)
        {
            var query = db.Users.Select(user => user)
                .Where(u => u.Email.ToLower() == email.ToLower() && u.Password == password).ToList();
            
            if (query.Count == 0)
            {
                BadRequest("User Not found");
            }
            var Users = JsonSerializer.Serialize(query);

            return Ok(Users);
        }

    }
}
