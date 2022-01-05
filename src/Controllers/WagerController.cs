using System;
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
    public class WagerController : ControllerBase
    {
        private readonly IWagerService _service;

        public WagerController(IWagerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<WagerDto>> RetrieveWager(long userId, Guid poolId)
        {
            var wager = await _service.RetrieveWager(userId, poolId);

            if (wager == null)
            {
                return NotFound();
            }

            return wager;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWager(long userId, Guid poolId,
            [FromBody] UpdateWagerDto wagerDto)
        {
            if (userId != wagerDto.UserId || poolId != wagerDto.PoolId)
            {
                return BadRequest();
            }

            await _service.UpdateWager(wagerDto);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<WagerDto>> CreateWager(
            CreateWagerDto wagerDto)
        {
            await _service.CreateWager(wagerDto);
            return CreatedAtAction("RetrieveWager", new
            {
                userId = wagerDto.UserId,
                poolId = wagerDto.PoolId
            });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWager(long userId, Guid poolId)
        {
            await _service.DeleteWager(userId, poolId);
            return NoContent();
        }
    }
}