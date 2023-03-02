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
      
        Task<bool> Transfer(Account account, long recepientAccNo, decimal amount);
        Task<bool> BuyAirtimeAsync(Account user, long beneficiary, decimal amount);
        Task CheckBalance(Account account);
        Task<bool> Withdraw(Account account,decimal amount);
    }
    public enum TransactionType
    {
        Debit,
        Credit,

    }
}
