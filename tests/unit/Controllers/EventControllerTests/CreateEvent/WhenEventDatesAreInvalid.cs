using System;
using System.Threading.Tasks;
using BakuchiApi.Contracts;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .CreateEvent
{
    internal class WhenEventDatesAreInvalid
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
                Start = new DateTime(2021, 09, 01),
                End = new DateTime(2021, 08, 31)
            };

            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();

            _userService
                .Setup(_ => _.UserExists(It.IsAny<Guid>()))
                .Returns(true);

            _userService
                .Setup(_ => _.DiscordIdExists(It.IsAny<long>()))
                .Returns(true);

            _controller = new EventController(_eventService.Object, _userService.Object);
        }

        [Test]
        public void AssertControllerThrowsException()
        {
            Assert.That(
                async () => await _controller.CreateEvent(newEvent),
                Throws.Exception);
        }

        [Test]
        public void AssertBadRequestExceptionIsThrown()
        {
            Assert.That(
                async () => await _controller.CreateEvent(newEvent),
                Throws.InstanceOf<BadRequestException>()
            );
        }

        [Test]
        public async Task AssertEventServiceDoesNotCreateItem()
        {
            try
            {
                await _controller.CreateEvent(newEvent);
            }
            catch
            {
                _eventService
                    .Verify(
                        _ => _.CreateEvent(It.IsAny<Event>()),
                        Times.Exactly(0));
            }
        }

        [Test]
        public async Task AssertUserServiceDoesNotCreateNewUser()
        {
            try
            {
                await _controller.CreateEvent(newEvent);
            }
            catch
            {
                _eventService
                    .Verify(
                        _ => _.CreateEvent(It.IsAny<Event>()),
                        Times.Exactly(0));
            }
        }
    }
}