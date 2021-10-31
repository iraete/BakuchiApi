using System;
using System.Threading.Tasks;
using BakuchiApi.Controllers;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.UserControllerTests
    .UpdateUser
{
    internal class WhenRouteIdDoesNotMatchPayloadId
    {
        private readonly Guid id = Guid.NewGuid();
        private UpdateUserDto updatedUser;
        private UserController userController;
        private Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
            updatedUser = new UpdateUserDto
            {
                Id = Guid.NewGuid(),
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
        public void AssertControllerThrowsException()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Throws.Exception);
        }

        [Test]
        public void AssertBadRequestExceptionIsThrown()
        {
            Assert.That(
                async () => await userController.UpdateUser(id, updatedUser),
                Throws.InstanceOf<BadRequestException>()
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
            {
                userServiceMock.Verify(
                    us => us.UpdateUser(It.IsAny<User>()),
                    Times.Exactly(0)
                );
            }
        }
    }
}