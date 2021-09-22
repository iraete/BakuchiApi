using System;
using System.Threading.Tasks;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Models;
using BakuchiApi.Controllers.Dtos;
using BakuchiApi.Controllers;
using BakuchiApi.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Controllers
    .PoolControllerTests.CreatePool
{
    internal class WhenAllIsWell
    {
        private Mock<IPoolService> poolServiceMock;
        private PoolController poolController;
        private CreatePoolDto poolDto;

        [SetUp]
        public void Setup()
        {
            poolDto = new CreatePoolDto()
            {
                EventId = Guid.NewGuid(),
                Alias = "Alias",
                BetType = BetType.Place,
                Description = "Sample description"
            };
            poolServiceMock = new Mock<IPoolService>();
            poolController = new PoolController(poolServiceMock.Object);
        }

        [Test]
        public void AssertControllerDoesNotThrowException()
        {
            Assert.That(
                async () => await poolController.CreatePool(poolDto),
                Throws.Nothing);
        }

        [Test]
        public void AssertResponseIsNotNull()
        {
            Assert.That(
                async () => await poolController.CreatePool(poolDto),
                Is.Not.EqualTo(null)
            );
        }
        
        [Test]
        public async Task AssertCreatePoolIsCalled()
        {
            await poolController.CreatePool(poolDto);
            poolServiceMock.Verify(
                p => p.CreatePool(It.IsAny<Pool>()),
                Times.Exactly(1)
            );
        }
    }
}