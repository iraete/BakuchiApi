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
        public async Task Setup()
        {
            newUser = new CreateUserDto
            {
                Name = "Example",
                DiscordId = 1000
            };
            userServiceMock = new Mock<IUserService>();
            userController = new UserController(userServiceMock.Object);
            result = await userController.CreateUser(newUser);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.IsNotNull(result);
        }
        
        [Test]
        public void AssertCreateUserIsCalled()
        {
            userServiceMock.Verify(
                us => us.CreateUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}