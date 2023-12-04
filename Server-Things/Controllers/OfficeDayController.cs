﻿using System;
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

        public record temp(Guid userID, DateOnly date);

        [HttpPost]
        public async Task<IActionResult> CreateOfficeDay([FromBody] temp officeDay)
        {
            try
            {
                db.OfficeDays.Add(new(officeDay.userID, officeDay.date));
                await db.SaveChangesAsync();
                return Ok("OfficeDay created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOfficeDay(Guid id)
        {
            try
            {
                var officeDay = await db.OfficeDays.FindAsync(id);
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
    }
}
