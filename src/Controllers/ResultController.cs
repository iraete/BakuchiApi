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

namespace BakuchiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly IEventService _eventService;
        private ResultDtoMapper _resultMapper;

        public ResultController(
            IResultService resultService,
            IEventService eventService)
        {
            _resultService = resultService;
            _eventService = eventService;
            _resultMapper = new ResultDtoMapper();
        }

        // GET: api/Result
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultDto>>>
            RetrieveResultsByEvent(Guid eventId)
        {
            var results = await _resultService.RetrieveResultsByEvent(eventId);
            return _resultMapper.MapEntitiesToDtos(results);
        }

        // PUT: api/Result/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> UpdateResults(Guid eventId, 
            UpdateResultDto[] resultDtos)
        {
            foreach (var r in resultDtos)
            {
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }
            }

            await ProcessDtos(resultDtos,
                _resultMapper.MapUpdateDtoToEntity,
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
            {
                if (eventId != r.EventId)
                {
                    return BadRequest();
                }
            }

            await ProcessDtos(resultDtos, 
                _resultMapper.MapCreateDtoToEntity, 
                _resultService.CreateResult);

            return CreatedAtAction("RetrieveResultsByEvent", 
                new { eventId = eventId }, eventId);
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

            var results = await _resultService.RetrieveResultsByEvent(eventId);
            var tasks = new List<Task>();

            foreach (var r in results)
            {
                tasks.Add(_resultService.DeleteResult(r));
            }

            await Task.WhenAll(tasks);
            return NoContent();
        }

        private async Task ProcessDtos<TSource>(
                IEnumerable<TSource> resultDtos,
                Func<TSource, Result> convert,
                Func<Result, Task> process)
        {
            var results = new List<Result>();
            var tasks = new List<Task>();
            foreach(var dto in resultDtos)
            {
                results.Add(convert(dto));
            }
            foreach (var r in results)
            {
                tasks.Add(process(r));
            }

            await Task.WhenAll(tasks);
        }
    }
}
