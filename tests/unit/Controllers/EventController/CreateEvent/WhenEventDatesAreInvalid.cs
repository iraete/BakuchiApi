using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Controllers;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .CreateEvent
{
    internal class WhenEventDatesAreInvalid
    {
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private EventController _controller;
        private ActionResult<EventDto> result;
        private CreateEventDto newEvent;

        [SetUp]
        public void Setup()
        {
            newEvent = new CreateEventDto()
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