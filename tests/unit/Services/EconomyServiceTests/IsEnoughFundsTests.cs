using System;
using BakuchiApi.Models;
using BakuchiApi.Services;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using NUnit.Framework;

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
            var userObj = new User
            {
                Id = Guid.NewGuid(),
                Balance = 1000
            };
            var cond = _economyService.IsEnoughFunds(userObj.Balance,
                userObj.Balance + 1);
            Assert.That(cond, Is.False);
        }

        [Test]
        public void WhenAmountIsNegative()
        {
            var userObj = new User
            {
                Id = Guid.NewGuid(),
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
            var userObj = new User
            {
                Id = Guid.NewGuid(),
                Balance = 1000
            };

            var cond = _economyService.IsEnoughFunds(userObj.Balance,
                userObj.Balance - 1);
            Assert.That(cond, Is.True);
        }
    }
}