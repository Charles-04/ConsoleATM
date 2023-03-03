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
        private Transaction _transaction = new();
        private const int airtimeLimit = 100;
        public Transactions(DbService dbService, AccountService accountService)
        {
            _dbService = dbService;
            _accountService = accountService;
        }


        public async Task<Transaction> BuyAirtimeAsync(Account account, long beneficiary, decimal amount)
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
                    _transaction.Sender = user.Id;
                    _transaction.Balance = bal;
                    _transaction.Type = TransactionType.Debit;

                    return _transaction;
                  
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task CheckBalanceAsync(Account account)
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

        public async Task<Transaction> TransferAsync(Account account, long recepientAccNo, decimal amount)
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
                    string commandString = $"UPDATE AccountUser SET accountBalance = {userBal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    commandString += $";UPDATE AccountUser SET accountBalance = {recepientBal} WHERE AccountUser.accountNumber = {recepient.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();

                    _transaction.Sender = user.Id;
                    _transaction.Receiver = recepient.Id;
                    _transaction.Balance = userBal;
                    _transaction.Type = TransactionType.Debit;
                    
                    return _transaction;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else if(amount <= 0)
            {
                Console.WriteLine($"Amount {amount} is lower than zero");
                return null;
            }
            else
            {
                Console.WriteLine($"Transfer failed try again");
                return null;
            }
        }

        public async Task<Transaction> WithdrawAsync(Account account, decimal amount)
        {
            var user = await _accountService.GetUserAsync(account.AccountNumber);
            if (account.isLoggedIn && amount >= 0 && amount < user.AccountBalance)
            {
                try
                {

                    var bal = user.AccountBalance - amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET accountBalance = {bal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0) {
                        Console.WriteLine("Withdrawal Successful");
                        _transaction.Sender = user.Id;
                        _transaction.Balance = bal;
                        _transaction.Type = TransactionType.Debit;
                        _transaction.Remarks = $"Withdrew {amount}";

                        return _transaction;
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal Failed");
                        return null;
                    }


               
                }
                catch (Exception ex)
                {
                    Console.WriteLine (ex.Message);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Insufficient Funds");
                return null;
            }
        }




        public async Task<Transaction> Deposit(Account account, decimal amount)
        {
            var user = await _accountService.GetUserAsync(account.AccountNumber);
            if (account.isLoggedIn && amount >= 0 && amount < user.AccountBalance)
            {
                try
                {

                    var bal = user.AccountBalance + amount;

                    SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                    string commandString = $"UPDATE AccountUser SET accountBalance = {bal} WHERE AccountUser.accountNumber = {user.AccountNumber}";
                    await using SqlCommand command = new SqlCommand(commandString, sqlConnection);
                    command.CommandType = CommandType.Text;

                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 0)
                    {
                        Console.WriteLine("Deposit Successful");
                        _transaction.Sender = user.Id;
                        _transaction.Balance = bal;
                        _transaction.Type = TransactionType.Credit;
                        _transaction.Remarks = $"Withdrew {amount}";

                        return _transaction;
                    }
                    else
                    {
                        Console.WriteLine("Deposit Failed");
                        return null;
                    }

                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                   
                }
            }
            else
            {
                return null;
            }
        }
    }
}
