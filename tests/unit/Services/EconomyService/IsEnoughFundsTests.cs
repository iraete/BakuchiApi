using BakuchiApi.Services.Interfaces;
using BakuchiApi.Services;
using BakuchiApi.Models;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Services
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
            Assert.IsFalse(cond);
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
            Assert.IsTrue(cond);
        }

    }
}