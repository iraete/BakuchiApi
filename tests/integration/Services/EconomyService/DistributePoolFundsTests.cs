using System;
using System.Collections.Generic;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Services;
using BakuchiApi.Models;
using NUnit.Framework;

namespace BakuchiApi.Tests.UnitTests.Services
{
    internal class DistributePoolFundsTests
    {   
        private Guid poolId;
        private IEconomyService _economyService;
        private List<Wager> wagers;

        [SetUp]
        public void Setup()
        {
            var rand = new Random();
            poolId = Guid.NewGuid();
            wagers = new List<Wager>();

            for (int i = 0; i < 2; i++)
            {
                var wager = new Wager {
                    PoolId = poolId,
                    Amount = 500
                };
                wagers.Add(wager);
            }

            _economyService = new EconomyService();
        }
    }
}