using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.IntegrationTests.Controllers.UserControllerTests
{
    internal class WhenAllIsWell
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private CreateUserDto newUser;
        private UserController userController;

        [SetUp]
        public void Setup()
        {
            newUser = new CreateUserDto
            {
                Name = "Example",
                DiscordId = 1000,
                Email = "nothing@nothing.com"
            };
            userServiceMock = new Mock<IUserService>();
            userController = new UserController(userServiceMock.Object);
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