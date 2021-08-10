using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly IEventService _eventService;

        public ResultController(
            IResultService resultService,
            IEventService eventService)
        {
            _resultService = resultService;
            _eventService = eventService;
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>>
            GetResultsByEvent(Guid eventId)
        {
            return await _resultService.GetResultsByEvent(eventId);
        }

        // PUT: api/Result/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutResults(Guid eventId, Result[] resultDtos)
        {
            var tasks = new List<Task>();

            foreach (var r in resultDtos)
            {
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }
            }

            await ProcessResults(resultDtos, _resultService.PutResult);

            return NoContent();
        }

        // POST: api/Result
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> PostResults(Guid eventId, Result[] resultDtos)
        {
            foreach (var r in resultDtos)
            {
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }
            }

            await ProcessResults(resultDtos, _resultService.PostResult);

            return CreatedAtAction("GetResultByEvent", new { eventId = eventId }, eventId);
        }

        // DELETE: api/Result/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResults(Guid eventId)
        {
            var eventExists = _eventService.EventExists(eventId);

            if (!eventExists)
            {
                return NotFound();
            }

            var results = await _resultService.GetResultsByEvent(eventId);
            await ProcessResults(results, _resultService.DeleteResult);

            return NoContent();
        }

        private async Task ProcessResults(IEnumerable<Result> resultDtos,
            Func<Result, Task> func)
        {
            var tasks = new List<Task>();

            foreach (var r in resultDtos)
            {
                tasks.Add(func(r));
            }

            await Task.WhenAll(tasks);
        }
    }
}
