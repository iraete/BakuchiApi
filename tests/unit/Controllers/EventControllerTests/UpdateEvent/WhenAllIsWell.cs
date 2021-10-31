using System;
using System.Threading.Tasks;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .UpdateEvent
{
    internal class WhenAllIsWell
    {
        private EventController _controller;
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private IActionResult result;

        [SetUp]
        public async Task Setup()
        {
            var updateEvent = new UpdateEventDto
            {
                Id = Guid.NewGuid(),
                Description = "A sample description",
                Start = DateTime.Now,
                End = new DateTime(2099, 1, 1)
            };

            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();

            _eventService
                .Setup(_ => _.RetrieveEvent(It.IsAny<Guid>()))
                .ReturnsAsync(new Event());

            _controller = new EventController(_eventService.Object, _userService.Object);
            result = await _controller.UpdateEvent(updateEvent.Id, updateEvent);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.IsNotNull(result);
        }

        [Test]
        public void AssertNoContentIsReturned()
        {
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void AssertEventServiceUpdatesItem()
        {
            _eventService
                .Verify(
                    _ => _.UpdateEvent(It.IsAny<Event>()),
                    Times.Exactly(1));
        }
    }
}