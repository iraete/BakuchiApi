using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly IPoolService _service;

        public PoolController(IPoolService service)
        {
            _service = service;
        }

        // GET: api/Pool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pool>>> RetrievePools()
        {
            return await _service.RetrievePools();
        }

        // GET: api/Pool/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pool>> RetrievePool(Guid id)
        {
            var pool = await _service.RetrievePool(id);

            if (pool == null)
            {
                return NotFound();
            }

            return pool;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pool>>>
            RetrievePoolsByEvent(Guid eventId)
        {
            return await _service.RetrievePoolsByEvent(eventId);
        }

        // PUT: api/Pool/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePool(Guid id,
            Pool poolDto)
        {
            if (id != poolDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdatePool(poolDto);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error adding event pool");
            }

            return NoContent();
        }

        // POST: api/Pool
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pool>> CreatePool(
            Guid eventId, Pool poolDto)
        {
            if (eventId != poolDto.EventId)
            {
                return BadRequest();
            }

            await _service.CreatePool(poolDto);
            return CreatedAtAction("RetrievePool", new { id = poolDto.Id },
                poolDto);
        }

        // DELETE: api/Pool/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool(Guid id)
        {
            var pool = await _service.RetrievePool(id);

            if (pool == null)
            {
                return NotFound();
            }

            await _service.DeletePool(pool);

            return NoContent();
        }
    }
}
