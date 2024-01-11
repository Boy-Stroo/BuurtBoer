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
    public class UserController : BaseController
    {
        public record UserModel(string Email, string Password, bool IsSelected, Guid ID);

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

        [HttpDelete("delete/{ID}")]
        public async Task DeleteUsersDatabase(Guid ID)
        {
            try
            {
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
