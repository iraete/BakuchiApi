using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Contracts.Requests;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IResultService _resultService;

        public ResultController(
            IResultService resultService,
            IEventService eventService)
        {
            _resultService = resultService;
            _eventService = eventService;
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>>
            RetrieveResultsByEvent(Guid eventId)
        {
            return await _resultService.RetrieveResultsByEvent(eventId);
        }

        // PUT: api/Result/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateResults(Guid eventId,
            UpdateResultDto[] resultDtos)
        {
            foreach (var r in resultDtos)
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }

            await ProcessDtos(resultDtos,
                _resultService.UpdateResult);
            return NoContent();
        }

        // POST: api/Result
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Result>> CreateResults(Guid eventId,
            CreateResultDto[] resultDtos)
        {
            foreach (var r in resultDtos)
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }

            var results = await ProcessDtos(resultDtos,
                _resultService.CreateResult);

            return CreatedAtAction("RetrieveResultsByEvent", results);
        }

        // DELETE: api/Result/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResults(Guid eventId)
        {
            var eventExists = await _eventService.EventExists(eventId);

            if (!eventExists)
            {
                return NotFound();
            }

            var results = await _resultService.RetrieveResultsByEvent(eventId);
            var tasks = new List<Task>();

            foreach (var r in results) 
                tasks.Add(_resultService.DeleteResult(eventId, r.Alias));

            await Task.WhenAll(tasks);
            return NoContent();
        }

        private async Task<List<ResultDto>> ProcessDtos<TSource>(
            IEnumerable<TSource> resultDtos,
            Func<TSource, Task<ResultDto>> process)
        {
            var tasks = new List<Task<ResultDto>>();
            foreach (var r in resultDtos) tasks.Add(process(r));

            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
    }
}