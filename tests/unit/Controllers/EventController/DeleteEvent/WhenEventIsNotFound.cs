using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.StatusExceptions;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers.EventControllerTests
    .DeleteEvent
{
    internal class WhenEventIsNotFound
    {
        private Mock<IEventService> _eventService;
        private Mock<IUserService> _userService;
        private EventController _controller;
        private IActionResult result;
        private Guid eventId;

        [SetUp]
        public void Setup()
        {
            eventId = Guid.NewGuid();

            _eventService = new Mock<IEventService>();
            _userService = new Mock<IUserService>();

            _eventService
                .Setup(_ => _.RetrieveEvent(It.IsAny<Guid>()))
                .ReturnsAsync((Event)null);

            _controller = new EventController(_eventService.Object, _userService.Object);
        }

        [Test]
        public void AssertControllerThrowsException()
        {
            Assert.That(
                async () => await _controller.DeleteEvent(eventId),
                Throws.Exception
            );
        }

        [Test]
        public void AssertNotFoundIsReturned()
        {
            Assert.That(
                async () => await _controller.DeleteEvent(eventId),
                Throws.InstanceOf<NotFoundException>()
            );
        }
        
        [Test]
        public void AssertEventServiceDoesNotDeleteItem()
        {
            _eventService
                .Verify(
                    _ => _.DeleteEvent(It.IsAny<Event>()),
                    Times.Exactly(0));
        }
    }
}