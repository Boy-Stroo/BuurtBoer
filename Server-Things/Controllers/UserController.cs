using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BuurtboerContext db = new BuurtboerContext();
        public record UserCredentials(string Email, string Password);

        [HttpGet("all")]
        // [Produces(MediaTypeNames.Application.Json)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var query = await db.Users.Select(u => u).ToListAsync();
                var Users = JsonSerializer.Serialize(query);
                return Ok(Users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetLoggedInUser([FromBody] UserCredentials credentials)
        {

            try
            {
                var query = await db.Users.Select(user => user)
                    .Where(u => u.Email.ToLower() == credentials.Email.ToLower() &&
                                u.Password == credentials.Password).ToListAsync();

                if (!query.Any())
                    return NotFound("User Not found");

                var Users = JsonSerializer.Serialize(query);

                return Ok(Users);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("all/{companyId}")]
        public IActionResult GetUsersByCompany(Guid companyId)
        {
            try
            {
                var query = db.Users.Where(u => u.CompanyId == companyId);

                var Users = JsonSerializer.Serialize(query);

                return Ok(Users);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
