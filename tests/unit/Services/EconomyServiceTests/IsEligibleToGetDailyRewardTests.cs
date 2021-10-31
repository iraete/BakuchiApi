using System;
using BakuchiApi.Models;
using BakuchiApi.Services;
using BakuchiApi.Services.Interfaces;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Services.EconomyServiceTests
{
    internal class IsEligibleToGetDailyReward
    {
        private IEconomyService _economyService;

        [SetUp]
        public void Setup()
        {
            _economyService = new EconomyService();
        }

        [Test]
        public void WhenItIsTooEarly()
        {
            var userObj = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                LastRewardTime = DateTime.Now.AddHours(-1)
            };

            var cond = _economyService.IsEligibleToGetDailyReward(
                userObj.LastRewardTime);
            Assert.That(cond, Is.False);
        }

        [Test]
        public void WhenAllGoesWell()
        {
            var userObj = new User
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                LastRewardTime = DateTime.Now.AddDays(-2)
            };

            var cond = _economyService.IsEligibleToGetDailyReward(
                userObj.LastRewardTime);
            Assert.That(cond, Is.True);
        }
    }
}