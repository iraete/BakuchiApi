using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoolDto>>> RetrievePools()
        {
            return await _service.RetrievePools();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PoolDto>> RetrievePool(Guid id)
        {
            var pool = await _service.RetrievePool(id);

            if (pool == null)
            {
                return NotFound();
            }

            return pool;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PoolDto>> UpdatePool(Guid id,
            UpdatePoolDto poolDto)
        {
            if (id != poolDto.Id)
            {
                return BadRequest();
            }

            return await _service.UpdatePool(poolDto);
        }

        [HttpPost]
        public async Task<ActionResult<PoolDto>> CreatePool(CreatePoolDto poolDto)
        {
            var pool = await _service.CreatePool(poolDto);
            return CreatedAtAction("RetrievePool", new {id = pool.Id}, pool);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePool(Guid id)
        {
            await _service.DeletePool(id);
            return NoContent();
        }
    }
}