using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers
    .UserControllerTests.CreateUser
{
    internal class WhenThereIsAPrimaryOrUniqueConflict
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private UserController userController;
        private ActionResult<User> result;

        [SetUp]
        public async Task Setup()
        {
            var newUser = new CreateUserDto
            {
                Name = "John Doe",
                DiscordId = 1000
            };
            
            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(
                usm => usm.CreateUser(It.IsAny<User>())
            ).ThrowsAsync(new StatusExceptions.ConflictException());
            userController = new UserController(userServiceMock.Object);
            result = await userController.CreateUser(newUser);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.IsNotNull(result);
        }

        [Test]
        public void  AssertConflictResponseIsReturned()
        {
            Assert.IsInstanceOf<ConflictResult>(result.Result);
        }

        [Test]
        public void AssertCondition()
        {
            userServiceMock.Verify(
                _ => _.CreateUser(It.IsAny<User>()),
                Times.Exactly(1));
        }
    }
}