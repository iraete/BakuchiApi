using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using status = BakuchiApi.StatusExceptions;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeService _service;
        private readonly OutcomeDtoMapper _outcomeMapper;

        public OutcomeController(IOutcomeService service)
        {
            _service = service;
            _outcomeMapper = new OutcomeDtoMapper();
        }

        // GET: api/Outcome
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Outcome>>> RetrieveOutcomesByEvent(
            Guid eventId)
        {
            return await _service.RetrieveOutcomesByEvent(eventId);
        }

        // GET: api/Outcome/5
        [HttpGet]
        public async Task<ActionResult<OutcomeDto>> RetrieveOutcome(Guid eventId,
            string alias)
        {
            var outcome = await _service.RetrieveOutcome(eventId, alias);

            if (outcome == null)
            {
                return NotFound();
            }

            return _outcomeMapper.MapEntityToDto(outcome);
        }

        // PUT: api/Outcome/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateOutcome(Guid eventId,
            string alias,
            [FromBody] UpdateOutcomeDto outcomeDto)
        {
            if (eventId != outcomeDto.EventId || alias != outcomeDto.Alias)
            {
                return BadRequest();
            }

            var outcome = _outcomeMapper.MapUpdateDtoToEntity(outcomeDto);

            try
            {
                var entity = await _service.RetrieveOutcome(eventId, alias);
                entity.Alias = outcome.Alias;
                entity.Name = outcome.Name;
                await _service.UpdateOutcome(outcome);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error updating event outcome");
            }

            return NoContent();
        }

        // POST: api/Outcome
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Outcome>> CreateOutcome(
            Guid eventId, CreateOutcomeDto outcomeDto)
        {
            if (eventId != outcomeDto.EventId)
            {
                return BadRequest();
            }

            var outcome = _outcomeMapper.MapCreateDtoToEntity(outcomeDto);
            await _service.CreateOutcome(outcome);
            return CreatedAtAction("RetrieveOutcome", new
                {
                    eventId,
                    alias = outcomeDto.Alias
                },
                _outcomeMapper.MapEntityToDto(outcome));
        }

        // DELETE: api/Outcome/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOutcome(Guid eventId,
            string alias)
        {
            var outcome = await _service.RetrieveOutcome(eventId, alias);

            if (outcome == null)
            {
                return NotFound();
            }

            await _service.DeleteOutcome(outcome);

            return NoContent();
        }
    }
}