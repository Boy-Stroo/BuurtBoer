using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Things.Models;

namespace Server_Things.Controllers
{
    [Route("api/officedays")]
    [ApiController]
    public class OfficeDayController : ControllerBase
    {
        private readonly BuurtboerContext db = new BuurtboerContext();

        [HttpGet]
        public async Task<IActionResult> GetOfficeDays()
        {
            try
            {
                var officeDays = await db.OfficeDays.Select(o => o).ToListAsync();
                var serializedOfficeDays = JsonSerializer.Serialize(officeDays);
                return Ok(serializedOfficeDays);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfficeDay([FromHeader] Guid userID, DateOnly officeDay)
        {
            try
            {
                db.OfficeDays.Add(new(userID, officeDay));
                await db.SaveChangesAsync();
                return Ok("OfficeDay created successfully");
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            }
        }
    }
}
