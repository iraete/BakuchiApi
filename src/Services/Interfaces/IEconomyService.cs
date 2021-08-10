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
        List<User> TransferFundsBetweenUsers(User toUser, User fromUser, double amount);
        User AddFunds(User toUser, double amount);
        User DeductFunds(User fromUser, double amount);
        List<User> DistributePoolFunds(Pool pool, Guid winningOutcomeId);
        User GetDailyReward(User user);
    }
}