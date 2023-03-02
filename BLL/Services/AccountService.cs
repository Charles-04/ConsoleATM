using ATM.BLL.Interfaces;
using ATM.DAL.DBConnection;
using ATM.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.BLL.Services
{
    internal class AccountService : IAccountService
    {
        private DbService _dbService;
        public AccountService(DbService dbService)
        {
            _dbService = dbService ;
        }
        
        public async Task<Account> GetUserAsync(long accountNumber)
        {
            SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();

            string UserInfo = $"SELECT * FROM Users WHERE acccountNumber = @AccountNo";
            await using SqlCommand command = new SqlCommand(UserInfo, sqlConnection);
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@AccountNo",
                    Value = accountNumber,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
            });
            Account user = new Account();
            using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {
                while (dataReader.Read())
                {
                    user.AccountBalance = (decimal)dataReader["accountBalance"];
                    user.Id = (Guid)dataReader["userId"];
                    user.isActive = (bool)dataReader["isActive"];
                    user.Name = (string)dataReader["userName"];
                    user.Pin = (string)dataReader["pin"];
                    user.AccountNumber = (long)dataReader[""];
                }
            }
            return user;

           
        }



        public async Task<Account> LogInAsync(long accountNumber, string pin)
        {
            var user = await GetUserAsync(accountNumber);
            if (user.Pin == pin)
            {
                user.isLoggedIn = true;

                return user;
            }
            else
            {
                user.isLoggedIn = false;
                Console.WriteLine("Invalid Credentials");
                return user;
            }
        }

        public async Task<bool> LogOutAsync(long accountNumber)
        {
            var user = await GetUserAsync(accountNumber);
            if (user.Name != null)
            {
                return true;
            }
            else
            {
                Console.WriteLine("User not found");
                return false;
            }
        }

      
    }
}
