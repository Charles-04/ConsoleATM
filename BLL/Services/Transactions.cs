using ATM.BLL.Interfaces;
using ATM.DAL.DBConnection;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.BLL.Services
{
    internal class Transactions : ITransactions
    {
        private readonly DbService _dbService;
        public Transactions(DbService dbService)
        {
            _dbService = dbService  ;
        }


        public async Task<bool> BuyAirtime(Account user,long beneficiary,decimal amount)
        {
            if (user.isLoggedIn && amount >=100 && amount < user.AccountBalance)
            {
                try
                {
                    SqlConnection sqlConnection = await _dbService.OpenConnection();

                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return false;
            }
        }

        public Task<long> CheckBalance()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Transfer()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Withdraw()
        {
            throw new NotImplementedException();
        }
    }
}
