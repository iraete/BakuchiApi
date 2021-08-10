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
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _service.GetUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _service.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            var updateDto = new User() {
                Id = user.Id,
                Name = user.Name
            };

            if (id != user.Id)
            {
                return BadRequest();
            }

            try 
            {
                await _service.PutUser(updateDto);
            }
            catch (status.NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                throw new Exception("Error updating user");
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var createUser = new User() {
                Id = user.Id,
                Balance = 0,
                Name = user.Name
            };

            await _service.PostUser(user);
            return CreatedAtAction("GetUser", new { id = createUser.Id }, createUser);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _service.DeleteUser(user);

            return NoContent();
        }

        // GET: api/User/5/events
        [HttpGet("{id}/events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserEvents(Guid id)
        {
            var user = await _service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return _service.GetEvents(user);
        }

        // GET: api/User/5/userwagers
        [HttpGet("{id}/wagers")]
        public async Task<ActionResult<IEnumerable<Wager>>> GetWagers(Guid id)
        {
            var user = await _service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return _service.GetWagers(user);
        }
    }
}
