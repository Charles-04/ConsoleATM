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
      
        Task<Transaction> TransferAsync(Account account, long recepientAccNo, decimal amount);
        Task<Transaction> BuyAirtimeAsync(Account user, long beneficiary, decimal amount);
        Task CheckBalanceAsync(Account account);
        Task<Transaction> WithdrawAsync(Account account,decimal amount);
    }
    public enum TransactionType
    {
        Debit,
        Credit,

    }
}
