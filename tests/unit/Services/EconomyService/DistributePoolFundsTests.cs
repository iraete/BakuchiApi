using System;
using System.Collections.Generic;
using System.Linq;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;
using BakuchiApi.Services;
using BakuchiApi.Models;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Services.EconomyServiceTests
{
    internal class DistributePoolFundsTests
    {   
        private IEconomyService _economyService;
        public Pool pool;
        private List<Wager> wagers;
        private List<User> users;

        [SetUp]
        public void Setup()
        {
            users = new List<User>();
            wagers = new List<Wager>();
            _economyService = new EconomyService();
        }

        [Test]
        public void WhenAllIsWell()
        {
            var rand = new Random();

            var outcomeBets = new List<double> 
            { 
                60, 140, 24, 110, 220, 94, 300, 80 
            };

            var sum = outcomeBets.Sum();
            var payouts = outcomeBets.Select(e => sum / e).ToList();

            var outcomeId = Guid.NewGuid();
            var idx = rand.Next(outcomeBets.Count);

            pool = new Pool {
                Id = Guid.NewGuid(),
                TotalWagers = outcomeBets.Sum(),
                Wagers = new List<Wager>()
            };

            var user = new User 
            {
                Id = Guid.NewGuid(),
                Balance = 0
            }; 

            var wager = new Wager 
            {
                PoolId = pool.Id,
                UserId = user.Id,
                Amount = outcomeBets[idx],
                OutcomeId = outcomeId,
                User = user
            };

            pool.Wagers.Add(wager);

            var payout = payouts[idx];
            var result = _economyService.DistributePoolFunds(pool, outcomeId);
            Assert.AreEqual(
                0, result[0].Balance - Math.Ceiling(payout * wager.Amount)); 
        }

        [Test]
        public void WhenPoolWagersIsNull()
        {
            pool = new Pool { };
            Assert.Throws<BadRequestException>(
                () => _economyService.DistributePoolFunds(
                    pool, Guid.NewGuid()));
        }

        [Test]
        public void WhenThereAreNoWagers()
        {
            pool = new Pool {
                TotalWagers = 1000,
                Wagers = new List<Wager>()
            };

            Assert.That(
                () => _economyService.DistributePoolFunds(
                    pool, Guid.NewGuid()),
                Is.Empty
            );
        }

        [Test]
        public void WhenPoolIsNull()
        {
            pool = null;
            Assert.Throws<BadRequestException>(
                () => _economyService.DistributePoolFunds(
                    pool, Guid.NewGuid()));
        }

    }
}