using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Models.Dtos;
using BakuchiApi.Controllers;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers.UserControllerTests
    .UpdateUser
{
    internal class WhenUserIsNotFound
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
                .ReturnsAsync((User) null);
            
            userController = new UserController(userServiceMock.Object);
        }

        [Test]
        public void AssertControllerThrowsException()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Throws.Exception);
        }

        [Test]
        public void AssertNotFoundExceptionIsThrown()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Throws.InstanceOf<NotFoundException>()
            );
        }

        [Test]
        public async Task AssertUpdateUserIsNotCalled()
        {
            try
            {
                await userController.UpdateUser(id, updatedUser);
            }
            catch
            {    userServiceMock.Verify(
                    us => us.UpdateUser(It.IsAny<User>()),
                    Times.Exactly(0)
                );
            }
        }
    }
}