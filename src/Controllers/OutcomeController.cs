using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
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

        public OutcomeController(IOutcomeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutcomeDto>>> RetrieveOutcomesByEvent(
            Guid eventId)
        {
            return await _service.RetrieveOutcomesByEvent(eventId);
        }

        [HttpGet]
        public async Task<ActionResult<OutcomeDto>> RetrieveOutcome(Guid eventId,
            string alias)
        {
            var outcome = await _service.RetrieveOutcome(eventId, alias);

            if (outcome == null)
            {
                return NotFound();
            }

            return outcome;
        }

        [HttpPut]
        public async Task<ActionResult<OutcomeDto>> UpdateOutcome(Guid eventId,
            string alias,
            [FromBody] UpdateOutcomeDto outcomeDto)
        {
            if (string.IsNullOrEmpty(alias) || eventId != outcomeDto.EventId
                                            || alias != outcomeDto.Alias)
            {
                return BadRequest();
            }

            return await _service.UpdateOutcome(outcomeDto);
        }

        [HttpPost]
        public async Task<ActionResult<Outcome>> CreateOutcome(
            Guid eventId, CreateOutcomeDto outcomeDto)
        {
            if (eventId != outcomeDto.EventId)
            {
                return BadRequest();
            }

            await _service.CreateOutcome(outcomeDto);
            return CreatedAtAction("RetrieveOutcome", new
                {
                    eventId,
                    alias = outcomeDto.Alias
                },
                outcomeDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOutcome(Guid eventId,
            string alias)
        {
            await _service.DeleteOutcome(eventId, alias);
            return NoContent();
        }
    }
}