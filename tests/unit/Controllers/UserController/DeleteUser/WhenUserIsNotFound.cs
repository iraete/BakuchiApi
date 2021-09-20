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
    .DeleteUser
{
    internal class WhenUserIsNotFound
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private UserController userController;
        private IActionResult result;

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
        public void AssertDeleteUserIsCalled()
        {
            userServiceMock.Verify(
                us => us.DeleteUser(It.IsAny<User>()),
                Times.Exactly(0)
            );
        }
    }
}