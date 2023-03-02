using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DAL.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public Account Receiver { get; set; }
        public Account Sender { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
    public enum TransactionType
    {
        Debit,
        Credit,

    }
}
