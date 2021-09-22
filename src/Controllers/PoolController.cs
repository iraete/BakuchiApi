using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolController : ControllerBase
    {
        private readonly IPoolService _service;
        private PoolDtoMapper _poolMapper;

        public PoolController(IPoolService service)
        {
            _service = service;
            _poolMapper = new PoolDtoMapper();
        }

        // GET: api/Pool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoolDto>>> RetrievePools()
        {
            var pools = await _service.RetrievePools();
            return _poolMapper.MapEntitiesToDtos(pools);
        }

        // GET: api/Pool/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PoolDto>> RetrievePool(Guid id)
        {
            var pool = await _service.RetrievePool(id);

            if (pool == null)
            {
                return NotFound();
            }

            return _poolMapper.MapEntityToDto(pool);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoolDto>>>
            RetrievePoolsByEvent(Guid eventId)
        {
            var pools = await _service.RetrievePoolsByEvent(eventId);
            return _poolMapper.MapEntitiesToDtos(pools);
        }

        // PUT: api/Pool/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePool(Guid id,
            UpdatePoolDto poolDto)
        {
            if (id != poolDto.Id)
            {
                return BadRequest();
            }

            var pool = _poolMapper.MapUpdateDtoToEntity(poolDto);

            try
            {
                var entity = await _service.RetrievePool(id);
                entity.Alias = pool.Alias;
                entity.BetType = pool.BetType;
                entity.Description = pool.Description;
                await _service.UpdatePool(entity);
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
        public async Task<ActionResult<Pool>> CreatePool(CreatePoolDto poolDto)
        {
            var pool = _poolMapper.MapCreateDtoToEntity(poolDto);
            await _service.CreatePool(pool);
            return CreatedAtAction("RetrievePool", new { id = pool.Id },
                _poolMapper.MapEntityToDto(pool));
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
