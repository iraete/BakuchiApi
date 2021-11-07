using System;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .CreateEvent
{
    internal class WhenUserDoesNotExist
    {
        private EventController _controller;
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private CreateEventDto newEvent;
        private ActionResult<EventDto> result;

        [SetUp]
        public void Setup()
        {
            newEvent = new CreateEventDto
            {
                Name = "Example Event #1",
                UserName = "User",
                Alias = "alias",
                UserId = Guid.NewGuid(),
                DiscordId = 1,
                ServerId = 1,
                Description = "A sample description",
                Start = DateTime.Now,
                End = new DateTime(2099, 1, 1)
            };

            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();

            _userService
                .Setup(_ => _.UserExists(It.IsAny<Guid>()))
                .Returns(false);

            _userService
                .Setup(_ => _.DiscordIdExists(It.IsAny<long>()))
                .Returns(false);

            _controller = new EventController(_eventService.Object, _userService.Object);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.That(
                async () => await _controller.CreateEvent(newEvent),
                Is.Not.Null
            );
        }

        [Test]
        public async Task AssertEventServiceCreatesItem()
        {
            await _controller.CreateEvent(newEvent);
            _eventService
                .Verify(
                    _ => _.CreateEvent(It.IsAny<Event>()),
                    Times.Exactly(1));
        }

        [Test]
        public async Task AssertUserServiceCreatesNewUser()
        {
            await _controller.CreateEvent(newEvent);
            _userService
                .Verify(
                    _ => _.CreateUser(It.IsAny<User>()),
                    Times.Exactly(1));
        }
    }
}