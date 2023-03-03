using ATM.BLL.Services;
using ATM.DAL.DBConnection;
using ATM.DAL.Models;

namespace ATM.UI
{
    internal class UILogic
    {
       
        static DbService dbService = new DbService();
        static ATMService _aTMService = new(dbService);
        static AccountService accountService = new(dbService);
        Transactions _transaction = new(dbService, accountService);
        public async Task WithdrawAsync(Account user)
        {
          WithdrawAsync:  try
            {

               // Console.Clear();
                Console.WriteLine("Withdrawal \n \n Enter amount");
                bool isAmountValid = decimal.TryParse((Console.ReadLine()), out decimal amount);
                if (isAmountValid)
                {
                   var isWithdrawn = await _transaction.WithdrawAsync(user, amount);
                    if (isWithdrawn != null) {
                        
                        
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Amount");
                    Task.Delay(3000).Wait();
                    goto WithdrawAsync;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                goto WithdrawAsync;
            }
        }
        public async Task CheckBalanceAsync(Account user)
        {
            await _transaction.CheckBalanceAsync(user);
        }
        public async Task BuyAirtimeAsync(Account user)
        {
            try
            {
                Console.WriteLine("Buy Airtime \n \n Enter amount");
                bool isAmountValid = decimal.TryParse((Console.ReadLine()), out decimal amount);
                //await _transaction.BuyAirtimeAsync(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task TransferAsync(Account user) { }
    }
}
