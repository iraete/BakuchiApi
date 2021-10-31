using System;
using System.Threading.Tasks;
using BakuchiApi.Controllers;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Controllers.UserControllerTests
    .DeleteUser
{
    internal class WhenAllIsWell
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
                .ReturnsAsync(new User());

            userController = new UserController(userServiceMock.Object);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.That(
                async () => await userController.DeleteUser(id),
                Is.Not.Null
            );
        }

        [Test]
        public void AssertNoContentIsReturned()
        {
            Assert.That(
                async () => await userController.DeleteUser(id),
                Is.InstanceOf<NoContentResult>()
            );
        }

        [Test]
        public async Task AssertDeleteUserIsCalled()
        {
            await userController.DeleteUser(id);
            userServiceMock.Verify(
                us => us.DeleteUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}