using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly BuurtboerContext db = new BuurtboerContext();


        [HttpGet("all")]
        // [Produces(MediaTypeNames.Application.Json)]
        [Consumes("application/json")]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var query = await db.Companies.Select(c => c).ToListAsync();
                var Companies = JsonSerializer.Serialize(query);
                return Ok(Companies);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("delete/{ID}")]
        public async Task DeleteCompaniesDatabase(Guid ID)
        {
            try
            {
                var companytodelete = db.Companies.Where(c => c.Id == ID);

                if (companytodelete != null)
                {
                    db.Companies.RemoveRange(companytodelete);
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
        public async Task AddCompany(Company company)
        {
            try
            {
                db.Companies.Add(company);

                var response = await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(BadRequest(ex.Message));
            }
        }
    }
}