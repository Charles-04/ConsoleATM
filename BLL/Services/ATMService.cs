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
    public class ATMService
    {
        private DbService _dbService;
        private ATMService(DbService dbService)
        {
                _dbService= dbService;
        }
        public async Task<bool> CreateTransactionAsync(Transaction transaction)

        {
            try
            {
                SqlConnection sqlConnection = await _dbService.OpenConnectionAsync();
                string TransactionInfo = $"INSERT into Transactions(currentUser,receiver,amount,balance,remarks) values (@sender,@receiver,@amount,@balance,@remark)";
                await using SqlCommand command = new SqlCommand(TransactionInfo, sqlConnection);
                command.Parameters.AddRange(new SqlParameter[]
                {
                new SqlParameter
                {
                    ParameterName = "@sender",
                    Value = transaction.Receiver.Id,
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                new SqlParameter
                {
                    ParameterName = "@receiver",
                    Value = transaction.Receiver.Id,
                    SqlDbType =  SqlDbType.UniqueIdentifier,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                    new SqlParameter
                {
                    ParameterName = "@AccountNo",
                    Value = transaction.Receiver.AccountNumber,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },
                    new SqlParameter
                {
                    ParameterName = "@AccountNo",
                    Value = transaction.Receiver.AccountNumber,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input,
                    Size = 50
                },new SqlParameter
                {
                    ParameterName = "@AccountNo",
                    Value = transaction.Receiver.AccountNumber,
                    SqlDbType = SqlDbType.BigInt,
                    Direction = ParameterDirection.Input,
                    Size = 50
                }
                });
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
           
        }
    }
}
