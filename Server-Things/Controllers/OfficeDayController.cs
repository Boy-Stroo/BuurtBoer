using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;
using Server_Things.Factories;

namespace Server_Things.Controllers
{

    [Route("api/officedays")]
    [ApiController]
    public class OfficeDayController : ControllerBase
    {

        private readonly BuurtboerContext db = DbContextFactory.Create();

        [HttpGet]
        public async Task<IActionResult> GetOfficeDays()
        {
            try
            {
                // zoekt op all officedays in de database en maakt er een list van
                var officeDays = await db.OfficeDays.Select(o => o).ToListAsync();
                var serializedOfficeDays = JsonSerializer.Serialize(officeDays);
                return Ok(serializedOfficeDays);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public record temp(Guid userID, DateOnly date);

        [HttpPost]
        public async Task<IActionResult> CreateOfficeDay([FromBody] temp officeDay)
        {
            try
            {
                // voegt een officeday toe met userid en datum
                db.OfficeDays.Add(new(officeDay.userID, officeDay.date));
                await db.SaveChangesAsync();
                return Ok("OfficeDay created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{officeDayId}")]
        public async Task<IActionResult> DeleteOfficeDay(Guid officeDayId)
        {
            try
            {
                // zoekt voor een office day met gegeven ID
                var officeDay = await db.OfficeDays.FindAsync(officeDayId);
                if (officeDay == null)
                {
                    return NotFound("OfficeDay not found");
                }

                db.OfficeDays.Remove(officeDay);
                await db.SaveChangesAsync();

                return Ok("OfficeDay deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("bydate")]
        public async Task<IActionResult> GetOfficeDaysByDate([FromQuery] DateOnly date)
        {
            try
            {
                // zoekt voor office days met gegeven datum en maakt er een list van
                var officeDaysForDate = await db.OfficeDays
                  .Where(o => o.Date == date)
                  .ToListAsync();

                return Ok(officeDaysForDate);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
