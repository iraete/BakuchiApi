using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers.UserControllerTests
    .UpdateUser
{
    internal class WhenAllIsWell
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private UpdateUserDto updatedUser;
        private UserController userController;
        private IActionResult result;

        [SetUp]
        public void Setup()
        {
            updatedUser = new UpdateUserDto
            {
                Id = id,
                Name = "Test name",
                DiscordId = 1001
            };

            userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(_ => _.RetrieveUser(id))
                .ReturnsAsync(new User());
                
            userController = new UserController(userServiceMock.Object);
        }

        [Test]
        public void AssertControllerDoesNotThrowException()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Throws.Nothing);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Is.Not.EqualTo(null)
            );
        }

        [Test]
        public void AssertNoContentIsReturned()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Is.InstanceOf<NoContentResult>()
            );
        }
        
        [Test]
        public async Task AssertUpdateUserIsCalled()
        {
            await userController.UpdateUser(id, updatedUser);
            userServiceMock.Verify(
                us => us.UpdateUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}