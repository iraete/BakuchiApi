using System;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Services;
using BakuchiApi.Models;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Services
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
            User userObj = new User {
                Id = System.Guid.NewGuid(),
                Name = "Test",
                LastRewardTime = DateTime.Now.AddHours(-1)
            };

            var cond = _economyService.IsEligibleToGetDailyReward(
                userObj.LastRewardTime);
            Assert.IsFalse(cond);
        }

        [Test]
        public void WhenAllGoesWell()
        {
            User userObj = new User {
                Id = System.Guid.NewGuid(),
                Name = "Test",
                LastRewardTime = DateTime.Now.AddDays(-2)
            };

            var cond = _economyService.IsEligibleToGetDailyReward(
                userObj.LastRewardTime);
            Assert.IsTrue(cond);
        }

    }
}