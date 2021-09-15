using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers
    .UserControllerTests.CreateUser
{
    internal class WhenAllIsWell
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private CreateUserDto newUser;
        private UserController userController;
        private ActionResult<User> result;

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
        public void AssertCreateUserIsCalled()
        {
            Assert.That(
                async () => await userController.CreateUser(newUser),
                Throws.Nothing
            );

            userServiceMock.Verify(
                us => us.CreateUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}