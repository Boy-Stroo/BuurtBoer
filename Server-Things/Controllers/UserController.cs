using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_Things.Models;

namespace Server_Things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BuurtboerContext db = new BuurtboerContext();

        [HttpGet("All")]
        public IEnumerable<User> GetUsers()
        {
            var query = db.Users.Select(u => u);
            return query.ToList();
        }

        [HttpGet("Login")]
        public IEnumerable<User> GetLogin(string email, string password)
        {
            var query = db.Users.Select(u => u)
                .Where(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            return query.ToList();
        }

    }
}
