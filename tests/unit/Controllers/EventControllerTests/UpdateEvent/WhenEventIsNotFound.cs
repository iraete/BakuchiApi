using System;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .UpdateEvent
{
    internal class WhenEventIsNotFound
    {
        private EventController _controller;
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private IActionResult result;
        private UpdateEventDto updateEvent;

        [SetUp]
        public void Setup()
        {
            updateEvent = new UpdateEventDto
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
                .ReturnsAsync((Event) null);

            _controller = new EventController(_eventService.Object, _userService.Object);
        }

        [Test]
        public void AssertControllerThrowsException()
        {
            Assert.That(
                async () => await _controller.UpdateEvent(updateEvent.Id, updateEvent),
                Throws.Exception
            );
        }

        [Test]
        public void AssertNotFoundExceptionIsThrown()
        {
            Assert.That(
                async () => await _controller.UpdateEvent(updateEvent.Id, updateEvent),
                Throws.InstanceOf<NotFoundException>()
            );
        }

        [Test]
        public void AssertEventServiceDoesNotUpdateItem()
        {
            _eventService
                .Verify(
                    _ => _.UpdateEvent(It.IsAny<Event>()),
                    Times.Exactly(0));
        }
    }
}