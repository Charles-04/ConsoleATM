using ATM.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    internal interface ITransactions
    {
      
        Task<bool> Transfer();
        Task<bool> BuyAirtimeAsync(Account user, long beneficiary, decimal amount);
        Task<bool> CheckBalance();
        Task<bool> Withdraw();
    }
    public enum TransactionType
    {
        Debit,
        Credit,

    }
}
