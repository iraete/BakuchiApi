using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Services.Interfaces;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagerController : ControllerBase
    {
        private readonly IWagerService _service;
        private WagerDtoMapper _wagerMapper;

        public WagerController(IWagerService service)
        {
            _service = service;
            _wagerMapper = new WagerDtoMapper();
        }

        // GET: api/Wager/5
        [HttpGet]
        public async Task<ActionResult<WagerDto>> RetrieveWager(Guid userId, Guid poolId)
        {
            var wager = await _service.RetrieveWager(userId, poolId);

            if (wager == null)
            {
                return NotFound();
            }

            return _wagerMapper.MapEntityToDto(wager);
        }

        // PUT: api/Wager/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateWager(Guid userId, Guid poolId,
            [FromBody] UpdateWagerDto wagerDto)
        {
            if (userId != wagerDto.UserId)
            {
                return BadRequest();
            }

            var wager = _wagerMapper.MapUpdateDtoToEntity(wagerDto);

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
        public async Task<ActionResult<WagerDto>> CreateWager(
            CreateWagerDto wagerDto)
        {
            var wager = _wagerMapper.MapCreateDtoToEntity(wagerDto);

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

            return CreatedAtAction("RetrieveWager", new 
                { 
                    userId = wager.UserId, 
                    poolId = wager.PoolId
                },
                _wagerMapper.MapEntityToDto(wager));
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
