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
 
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            var query = from u in db.Users
                select u;
            return query.ToList();
        }

    }
}
