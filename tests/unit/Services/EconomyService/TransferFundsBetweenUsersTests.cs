using System;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.Services;
using BakuchiApi.Models;
using NUnit.Framework;
using Moq;

namespace BakuchiApi.Tests.UnitTests.Services.EconomyServiceTests
{
    internal class TransferFundsBetweenUsersTests
    {   
        private IEconomyService _economyService;
        
        [SetUp]
        public void Setup()
        {
            _economyService = new EconomyService();
        }

        [Test]
        public void WhenAllGoesWell()
        {
            var users = GenerateUsers();
            var toUser = users[0];
            var fromUser = users[1];

            toUser.Balance = fromUser.Balance = 500;

            var new_obj = _economyService.TransferFundsBetweenUsers(
                toUser, fromUser, 500);
            
            var cond = (fromUser.Balance == 0) &&( toUser.Balance == 1000);

            Assert.IsTrue(cond);
        }

        [Test]
        public void WhenUserIsNull()
        {
            var users = GenerateUsers();
            var fromUser = users[1];

            fromUser.Balance = 500;

            Assert.Throws<NullReferenceException>(
                () => _economyService.TransferFundsBetweenUsers(
                    null, fromUser, 500
                )
            );
        }

        private User[] GenerateUsers(int amt = 2)
        {
            var users = new User[amt];
            for (int i=0; i < amt; i++)
            {
                users[i] = new User {
                    Id = Guid.NewGuid()
                };
            }
            return users;
        }

    }
}