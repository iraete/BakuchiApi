using System;
using System.Collections.Generic;
using System.Linq;
using BakuchiApi.Models;
using BakuchiApi.Services.Interfaces;
using BakuchiApi.StatusExceptions;

namespace BakuchiApi.Services
{
    public class EconomyService : IEconomyService
    {
        public bool IsEnoughFunds(double userFunds, double amount)
        {
            if (amount < 0)
            {
                throw new InvalidFundsException();
            }
            return (userFunds - amount >= 0);
        }

        public User AddFunds(User toUser, double amount)
        {
            toUser.Balance = toUser.Balance + amount;
            return toUser;
        }

        public User DeductFunds(User fromUser, double amount)
        {
            fromUser.Balance = fromUser.Balance - amount;
            return fromUser;
        }

        public List<User> TransferFundsBetweenUsers(User toUser, User fromUser,
             double amount)
        {
            if (IsEnoughFunds(fromUser.Balance, amount))
            {
                fromUser.Balance = fromUser.Balance - amount;
                toUser.Balance = toUser.Balance + amount;
            }
            else
            {
                throw new InvalidFundsException();
            }

            return new List<User> { toUser, fromUser };
        }

        public bool IsEligibleToGetDailyReward(DateTime lastRewardTime)
        {
            return lastRewardTime.AddHours(24) <= DateTime.Now;
        }

        public User GetDailyReward(User userDto)
        {
            Random random = new Random();
            double dailyReward = random.Next(1000);
            userDto.Balance += dailyReward;
            userDto.LastRewardTime = DateTime.Now;
            return userDto;
        }

        public List<User> DistributePoolFunds(Pool pool, Guid winningOutcomeId)
        {
            if (pool == null || pool.Wagers == null)
            {
                throw new NullReferenceException();
            }

            var wagers = pool.Wagers.Where
                (w => w.OutcomeId == winningOutcomeId);

            if (wagers.Count() <= 0)
            {
                throw new DivideByZeroException();
            }

            var payout = pool.TotalWagers / wagers.Sum(w => w.Amount);
            var updatedUsers = new List<User>();

            foreach (var w in wagers)
            {
                var funds = Math.Ceiling(payout * w.Amount);
                updatedUsers.Add(AddFunds(w.User, funds));
            }

            return updatedUsers;
        }

    }
}