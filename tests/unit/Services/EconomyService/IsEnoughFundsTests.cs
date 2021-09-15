using BakuchiApi.Services.Interfaces;
using BakuchiApi.Services;
using BakuchiApi.StatusExceptions;
using BakuchiApi.Models;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Services.EconomyServiceTests
{
    internal class IsEnoughFundsTests
    {   
        private IEconomyService _economyService;
        
        [SetUp]
        public void Setup()
        {
            _economyService = new EconomyService();
        }

        [Test]
        public void WhenAmountExceedsUserFunds()
        {
            User userObj = new User {
                Id = System.Guid.NewGuid(),
                Balance = 1000
            };
            var cond = _economyService.IsEnoughFunds(userObj.Balance,
                 userObj.Balance + 1);
            Assert.That(cond, Is.False);
        }

        [Test]
        public void WhenAmountIsNegative()
        {
            User userObj = new User {
                Id = System.Guid.NewGuid(),
                Balance = 1000
            };

            Assert.That(
                () => _economyService.IsEnoughFunds(userObj.Balance, -999),
                Throws.InstanceOf<BadRequestException>()
            );
        }

        [Test]
        public void WhenAllGoesWell()
        {
            User userObj = new User {
                Id = System.Guid.NewGuid(),
                Balance = 1000
            };

            var cond = _economyService.IsEnoughFunds(userObj.Balance,
                 userObj.Balance - 1);
            Assert.That(cond, Is.True);
        }

    }
}