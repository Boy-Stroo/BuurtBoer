using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;
using Server_Things.Factories;
using System.Text.Json;
using System.ComponentModel.Design;

namespace Server_Things.Controllers
{
    [Route("api/grocery")]
    [ApiController]
    public class GroceryController : ControllerBase
    {
        private readonly BuurtboerContext db = DbContextFactory.Create();

        [HttpGet("all/{CompanyID}")]
        // [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetGroceries(Guid CompanyID)
        {
            try
            {
                var query = await db.GroceryList.Where(c => c.CompanyID == CompanyID).ToListAsync();
                var Groceries = JsonSerializer.Serialize(query);
                return Ok(Groceries);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public async Task AddGrocery(Grocery grocery)
        {
            try
            {
                db.GroceryList.Add(grocery);
                var response = await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(BadRequest(ex.Message));
            }
        }

        [HttpDelete("delete/{ID}")]
        public async Task DeleteGrocery(Guid ID)
        {
            try
            {
                var query = db.GroceryList.Where(c => c.Id == ID);
                if (query != null)
                {
                    db.GroceryList.RemoveRange(query);
                    await db.SaveChangesAsync();
                    Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(BadRequest(ex.Message));
            }
        }
    }
}