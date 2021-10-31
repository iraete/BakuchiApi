using System;
using System.Threading.Tasks;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers
    .UserControllerTests.CreateUser
{
    internal class WhenAllIsWell
    {
        private readonly Guid id = Guid.NewGuid();
        private CreateUserDto newUser;
        private ActionResult<User> result;
        private UserController userController;
        private Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
            newUser = new CreateUserDto
            {
                Name = "Example",
                DiscordId = 1000
            };
            userServiceMock = new Mock<IUserService>();
            userController = new UserController(userServiceMock.Object);
        }

        [Test]
        public void AssertControllerDoesNotThrowException()
        {
            Assert.That(
                async () => await userController.CreateUser(newUser),
                Throws.Nothing);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.That(
                async () => await userController.CreateUser(newUser),
                Is.Not.EqualTo(null)
            );
        }

        [Test]
        public void AssertUserIsReturned()
        {
            Assert.That(
                async () => await userController.CreateUser(newUser),
                Is.InstanceOf<ActionResult<User>>()
            );
        }

        [Test]
        public async Task AssertCreateUserIsCalled()
        {
            await userController.CreateUser(newUser);
            userServiceMock.Verify(
                us => us.CreateUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}