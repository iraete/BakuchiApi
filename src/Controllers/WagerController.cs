using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagerController : ControllerBase
    {
        private readonly IWagerService _service;

        public WagerController(IWagerService service)
        {
            _service = service;
        }

        // GET: api/Wager/5
        [HttpGet]
        public async Task<ActionResult<Wager>> RetrieveWager(Guid userId, Guid poolId)
        {
            var wager = await _service.RetrieveWager(userId, poolId);

            if (wager == null)
            {
                return NotFound();
            }

            return wager;
        }

        // PUT: api/Wager/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateWager(Guid userId, Guid poolId,
            [FromBody] Wager wager)
        {
            if (userId != wager.UserId)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateWager(wager);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error updating wager");
            }

            return NoContent();
        }

        // POST: api/Wager
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wager>> CreateWager(Wager wager)
        {
            try
            {
                await _service.CreateWager(wager);
            }
            catch (status.ConflictException)
            {
                return Conflict();
            }
            catch
            {
                throw new Exception("Error adding wager");
            }

            return CreatedAtAction("RetrieveWager", new { userId = wager.UserId, poolId = wager.PoolId}, wager);
        }

        // DELETE: api/Wager/5
        [HttpDelete]
        public async Task<IActionResult> DeleteWager(Guid userId, Guid poolId)
        {
            var wager = await _service.RetrieveWager(userId, poolId);

            if (wager == null)
            {
                return NotFound();
            }

            await _service.DeleteWager(wager);

            return NoContent();
        }
    }
}
