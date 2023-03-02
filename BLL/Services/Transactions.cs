﻿using ATM.BLL.Interfaces;
using ATM.DAL.DBConnection;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;

namespace ATM.BLL.Services
{
    internal class Transactions : ITransactions
    {
        private readonly DbService _dbService;
        private readonly AccountService _accountService;
        const int airtimeLimit = 100;
        public Transactions(DbService dbService, AccountService accountService)
        {
            _dbService = dbService;
            _accountService = accountService;
        }


        public async Task<bool> BuyAirtimeAsync(Account account, long beneficiary, decimal amount)
        {
            var user = await _accountService.GetUserAsync(account.AccountNumber);
            if (account.isLoggedIn && amount >= airtimeLimit && amount < user.AccountBalance)
            {
                try
                {

                    var bal = user.AccountBalance - amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET balance = {bal} WHERE AccountUser.accountNumber = {user.AccountNumber}";

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

        public async Task CheckBalance(Account account)
        {
           
            try
            {
                var user = await _accountService.GetUserAsync(account.AccountNumber);
                Console.WriteLine($"Your Account Balance is {user.AccountBalance}");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
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
