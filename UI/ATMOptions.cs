using ATM.BLL.Services;
using ATM.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.UI
{
    internal class ATMOptions
    {
        private AccountService _accountService;
        private Account currentAccount;
        private const int DelayTime = 3000;

        public ATMOptions(AccountService accountService) {
            _accountService= accountService;
        }
        internal async Task Validate()
        {
            Console.WriteLine("Enter your account Number");
            var accNumber = long.TryParse(Console.ReadLine(), out long accountNumber);
            Console.WriteLine("Enter your pin");
            var pin = Console.ReadLine();

         
                Console.Clear();
                currentAccount = await _accountService.LogInAsync(accountNumber, pin);

                if (!string.IsNullOrEmpty(currentAccount.Name))
                {
                    Console.WriteLine($"Welcome {currentAccount.Name} \n");

                    OperationOptions();
                } 
            else
            {
                Console.WriteLine("Incorrect Details! Try again");
                await Task.Delay(DelayTime);

                Console.Clear();
                await Validate();
            }
        }
        internal void OperationOptions()
        {
            Console.WriteLine("Choose banking operation \n\n1 : Withdrawal \n2 : Transfers \n3 : Balance Check \n4 : exit  \n0 : Previous Menu");
            int.TryParse((Console.ReadLine()), out int Option);

            switch (Option)
            {
                case 0:
                    Console.Clear();
                   // Atm.Init();
                    break;

                case 1:
                    Console.Clear();
                   // Withdraw();


                    break;

                case 2:
                    Console.Clear();
                    //Transfer();
                    break;

                case 3:
                    Console.Clear();
                    //GetBalance();
                    break;
                case 4:
                    Console.WriteLine("Thanks For Using our services");
                    return;

                default:
                    Console.WriteLine("Incorrect Option");
                    Task.Delay(DelayTime).Wait();
                    Console.Clear();
                    OperationOptions();
                    break;
            }
        }
    }
}
