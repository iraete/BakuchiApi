using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BakuchiApi.Models;

namespace BakuchiApi.Services.Interfaces
{
    public interface IEconomyService
    {
        bool IsEnoughFunds(double userFunds, double amount);
        bool IsEligibleToGetDailyReward(DateTime lastRewardTime);
        void TransferFundsBetweenUsers(User toUser, User fromUser, double amount);
        void AddFunds(User toUser, double amount);
        void DeductFunds(User fromUser, double amount);
        List<User> DistributePoolFunds(Pool pool, Guid winningOutcomeId);
        void GetDailyReward(User user);
    }
}