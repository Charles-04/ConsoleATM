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

               
                Console.WriteLine("Withdrawal \n \n Enter amount");
                bool isAmountValid = decimal.TryParse((Console.ReadLine()), out decimal amount);
                if (isAmountValid)
                {
                   var isWithdrawn = await _transaction.WithdrawAsync(user, amount);
                  
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
                Console.WriteLine("Buy Airtime \n \n Enter amount \n");
                bool isAmountValid = decimal.TryParse((Console.ReadLine()), out decimal amount);
                Console.WriteLine("Enter beneficiary");
                bool isnumberValid = long.TryParse((Console.ReadLine()), out long beneficiary);
                if (beneficiary.ToString().Length == 11)
                {
                    await _transaction.BuyAirtimeAsync(user,beneficiary,amount);
                }
                else
                {
                    Console.WriteLine("Number must be 11 digits");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public async Task TransferAsync(Account user) {

        TransferAsync: try
            {


                Console.WriteLine("Transfers \n \n Enter amount");
                bool isAmountValid = decimal.TryParse((Console.ReadLine()), out decimal amount);
                Console.WriteLine("\nEnter beneficiary account number\n");
                bool isbeneficiaryValid = long.TryParse((Console.ReadLine()), out long beneficiary);
                Console.WriteLine("\nEnter remarks \n");
                var remark = Console.ReadLine();
                if (isAmountValid && isbeneficiaryValid)
                {
                    var isTransferred = await _transaction.TransferAsync(user,beneficiary, amount);
                    if (isTransferred != null)
                    {
                        isTransferred.Remarks = remark;
                        await _aTMService.CreateTransactionAsync(isTransferred);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Amount or account number");
                    Task.Delay(3000).Wait();
                    goto TransferAsync;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                goto TransferAsync;
            }
        }
    }
}
