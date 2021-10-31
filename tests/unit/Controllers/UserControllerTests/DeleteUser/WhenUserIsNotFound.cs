using System;
using System.Threading.Tasks;
using BakuchiApi.Controllers;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.UserControllerTests
    .DeleteUser
{
    internal class WhenUserIsNotFound
    {
        private readonly Guid id = Guid.NewGuid();
        private IActionResult result;
        private UserController userController;
        private Mock<IUserService> userServiceMock;

        [SetUp]
        public void Setup()
        {
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
                async () => await userController.DeleteUser(id),
                Throws.Exception
            );
        }

        [Test]
        public void AssertNotFoundExceptionIsThrown()
        {
            Assert.That(
                async () => await userController.DeleteUser(id),
                Throws.InstanceOf<NotFoundException>()
            );
        }

        [Test]
        public async Task AssertDeleteUserIsCalled()
        {
            try
            {
                await userController.DeleteUser(id);
            }
            catch
            {
                userServiceMock.Verify(
                    us => us.DeleteUser(It.IsAny<User>()),
                    Times.Exactly(0)
                );
            }
        }
    }
}