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
        public async Task Setup()
        {
            userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(_ => _.RetrieveUser(id))
                .ReturnsAsync(new User());
                
            userController = new UserController(userServiceMock.Object);
            result = await userController.DeleteUser(id);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.IsNotNull(result);
        }

        [Test]
        public void AssertNoContentIsReturned()
        {
            Assert.IsInstanceOf<NoContentResult>(result);
        }
        
        [Test]
        public void AssertDeleteUserIsCalled()
        {
            userServiceMock.Verify(
                us => us.DeleteUser(It.IsAny<User>()),
                Times.Exactly(1)
            );
        }
    }
}