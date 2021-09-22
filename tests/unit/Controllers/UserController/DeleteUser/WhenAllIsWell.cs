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
    .DeleteUser
{
    internal class WhenAllIsWell
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