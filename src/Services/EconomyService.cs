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
                throw new BadRequestException("Negative amount supplied");
            }

            return (userFunds - amount >= 0);
        }

        public void AddFunds(User toUser, double amount)
        {
            toUser.Balance = toUser.Balance + amount;
        }

        public void DeductFunds(User fromUser, double amount)
        {
            fromUser.Balance = fromUser.Balance - amount;
        }

        public void TransferFundsBetweenUsers(User toUser, User fromUser,
             double amount)
        {
            if (toUser == null || fromUser == null)
            {
                throw new BadRequestException("One or more supplied "
                    + "users is null");
            }

            if (IsEnoughFunds(fromUser.Balance, amount))
            {
                fromUser.Balance = fromUser.Balance - amount;
                toUser.Balance = toUser.Balance + amount;
            }
            else
            {
                throw new BadRequestException("User does not have "
                    + "enough funds.");
            }
        }

        public bool IsEligibleToGetDailyReward(DateTime lastRewardTime)
        {
            return lastRewardTime.AddHours(24) <= DateTime.Now;
        }

        public void GetDailyReward(User user)
        {
            if (user == null)
            {
                throw new BadRequestException("Supplied user is null");
            }

            Random random = new Random();
            double dailyReward = random.Next(1000);
            user.Balance += dailyReward;
            user.LastRewardTime = DateTime.Now;
        }

        public List<User> DistributePoolFunds(Pool pool, Guid winningOutcomeId)
        {
            if (pool == null || pool.Wagers == null)
            {
                throw new 
                    BadRequestException(
                        "Pool not supplied or pool has no wagers");
            }

            var wagers = pool.Wagers.Where
                (w => w.OutcomeId == winningOutcomeId);

            var updatedUsers = new List<User>();

            if (wagers.Count() > 0)
            {
                var payout = pool.TotalWagers / wagers.Sum(w => w.Amount);

                foreach (var w in wagers)
                {
                    var funds = Math.Ceiling(payout * w.Amount);
                    AddFunds(w.User, funds);
                    updatedUsers.Add(w.User);
                }
            }

            return updatedUsers;
        }

    }
}