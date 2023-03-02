using ATM.BLL.Interfaces;
using ATM.DAL.DBConnection;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ATM.BLL.Services
{
    internal class Transactions : ITransactions
    {
        private readonly DbService _dbService;
        private readonly AccountService _accountService;
        private const int airtimeLimit = 100;
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
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return false;
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

        public async Task<bool> Transfer(Account account, long recepientAccNo, decimal amount)
        {

            var user = await _accountService.GetUserAsync(account.AccountNumber);
            var recepient = await _accountService.GetUserAsync(recepientAccNo);
            if (account.isLoggedIn && amount > 0  && amount < user.AccountBalance && ! string.IsNullOrEmpty( recepient.Name))
            {
                try
                {

                    var userBal = user.AccountBalance - amount;
                    var recepientBal = recepient.AccountBalance + amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET balance = {userBal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    commandString += $";UPDATE AccountUser SET balance = {recepientBal} WHERE AccountUser.accountNumber = {recepient.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else if(amount <= 0)
            {
                Console.WriteLine($"Amount {amount} is lower than zero");
                return false;
            }
            else
            {
                Console.WriteLine($"Transfer failed try again");
                return false;
            }
        }

        public async Task<bool> Withdraw(Account account, decimal amount)
        {
            var user = await _accountService.GetUserAsync(account.AccountNumber);
            if (account.isLoggedIn && amount >= airtimeLimit && amount < user.AccountBalance)
            {
                try
                {

                    var bal = user.AccountBalance - amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET balance = {bal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) {
                        Console.WriteLine("Withdrawal Successful");
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal Failed");
                    }

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




        public async Task<bool> Deposit(Account account, decimal amount)
        {
            var user = await _accountService.GetUserAsync(account.AccountNumber);
            if (account.isLoggedIn && amount >= 0 && amount < user.AccountBalance)
            {
                try
                {

                    var bal = user.AccountBalance + amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET balance = {bal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0)
                    {
                        Console.WriteLine("Deposit Successful");
                    }
                    else
                    {
                        Console.WriteLine("Deposit Failed");
                    }

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
    }
}
