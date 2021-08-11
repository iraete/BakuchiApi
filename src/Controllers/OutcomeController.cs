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
    public class OutcomeController : ControllerBase
    {
        private readonly IOutcomeService _service;

        public OutcomeController(IOutcomeService service)
        {
            _service = service;
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
        public async Task<ActionResult<Outcome>> RetrieveOutcome(Guid eventId,
            uint outcomeId)
        {
            var outcome = await _service.RetrieveOutcome(eventId, outcomeId);

            if (outcome == null)
            {
                return NotFound();
            }

            return outcome;
        }

        // PUT: api/Outcome/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateOutcome(Guid eventId,
            uint outcomeId,
            [FromBody] Outcome outcomeDto)
        {
            if (eventId != outcomeDto.EventId || outcomeId != outcomeDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateOutcome(outcomeDto);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error adding event outcome");
            }

            return NoContent();
        }

        // POST: api/Outcome
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Outcome>> CreateOutcome(
            Guid eventId, Outcome outcomeDto)
        {
            if (eventId != outcomeDto.EventId)
            {
                return BadRequest();
            }

            await _service.CreateOutcome(outcomeDto);
            return CreatedAtAction("RetrieveOutcome", new { id = outcomeDto.Id },
                outcomeDto);
        }

        // DELETE: api/Outcome/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOutcome(Guid eventId,
            uint outcomeId)
        {
            var outcome = await _service.RetrieveOutcome(eventId, outcomeId);

            if (outcome == null)
            {
                return NotFound();
            }

            await _service.DeleteOutcome(outcome);

            return NoContent();
        }
    }
}
