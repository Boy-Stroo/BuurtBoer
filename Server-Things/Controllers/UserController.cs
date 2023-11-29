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
        public IEnumerable<User> GetUsers()
        {
            var query = db.Users.Select(u => u);
            return query.ToList();
        }

        [HttpGet("login")]
        public IEnumerable<User> GetLogin(string email, string password)
        {
            var query = db.Users.Select(user => user)
                .Where(u => u.Email.ToLower() == email.ToLower() && u.Password == password);

            return query.ToList();
        }

    }
}
