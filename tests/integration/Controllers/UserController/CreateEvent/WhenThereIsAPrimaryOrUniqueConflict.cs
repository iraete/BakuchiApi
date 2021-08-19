using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.IntegrationTests.Controllers.UserControllerTests
{
    internal class WhenThereIsAPrimaryOrUniqueConflict
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
            userServiceMock.Setup(
                usm => usm.CreateUser(It.IsAny<User>())
            ).ThrowsAsync(new StatusExceptions.ConflictException());

            userController = new UserController(userServiceMock.Object);
        }

        [Test]
        public async Task AssertConflictResponseIsReturned()
        {
            var result = await userController.CreateUser(newUser);
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }
    }
}