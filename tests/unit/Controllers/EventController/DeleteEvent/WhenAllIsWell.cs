using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .DeleteEvent
{
    internal class WhenAllIsWell
    {
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private EventController _controller;
        private IActionResult result;

        [SetUp]
        public async Task Setup()
        {
            var eventId = Guid.NewGuid();

            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();

            _eventService
                .Setup(_ => _.RetrieveEvent(It.IsAny<Guid>()))
                .ReturnsAsync(new Event());

            _controller = new EventController(_eventService.Object, _userService.Object);
            result = await _controller.DeleteEvent(eventId);
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
        public void AssertEventServiceDeletesItem()
        {
            _eventService
                .Verify(
                    _ => _.DeleteEvent(It.IsAny<Event>()),
                    Times.Exactly(1));
        }
    }
}