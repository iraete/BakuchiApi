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
    internal class WhenNameIsEmpty
    {
        private readonly Guid id = Guid.NewGuid();
        private Mock<IUserService> userServiceMock;
        private UserController userController;
        private ActionResult<User> result;

        [SetUp]
        public async Task Setup()
        {   
            CreateUserDto userDto = new CreateUserDto();
            userServiceMock = new Mock<IUserService>();
            userController = new UserController(userServiceMock.Object);
            result = await userController.CreateUser(userDto);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.IsNotNull(result);
        }

        [Test]
        public void AssertBadRequestResponseIsReturned()
        {
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void AssertCondition()
        {
            userServiceMock.Verify(
                _ => _.CreateUser(It.IsAny<User>()),
                Times.Exactly(0));
        }
    }
}