using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;
using Server_Things.Factories;

namespace Server_Things.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly BuurtboerContext db = DbContextFactory.Create();
        


        [HttpGet("all")]
        // [Produces(MediaTypeNames.Application.Json)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                // zoekt alle users en maakt er een list van
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
            string email = credentials.Email;
            string password = credentials.Password;
            try
            {
                var query = await db.Users.Select(user => user)
                    .Where(u => u.Email.ToLower() == email.ToLower() &&
                                u.Password == password).ToListAsync();

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
                // zoekt all users met gegeven companyID
                var query = db.Users.Where(u => u.CompanyId == companyId);

                var Users = JsonSerializer.Serialize(query);

                return Ok(Users);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{ID}")]
        public async Task DeleteUsersDatabase(Guid ID)
        {
            try
            {
                //delete user met gegeven ID
                var usertodelete = db.Users.Where(u => u.Id == ID);

                if (usertodelete != null)
                {
                    db.Users.RemoveRange(usertodelete);
                    await db.SaveChangesAsync();
                    Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task AddUser(User user)
        {
            try
            {
                db.Users.Add(user);

                var response = await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(BadRequest(ex.Message));
            }
        }
    }
}
