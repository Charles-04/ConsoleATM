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
        private UILogic _uILogic = new();

        public ATMOptions(AccountService accountService) {
            _accountService= accountService;
        }
        internal async Task Validate()
        {
            Console.WriteLine("Welcome To Transcend Bank PLC \n \nEnter your account Number");
            var accNumber = long.TryParse(Console.ReadLine(), out long accountNumber);
            Console.WriteLine("Enter your pin");
            var pin = Console.ReadLine();

         
                Console.Clear();
                currentAccount = await _accountService.LogInAsync(accountNumber, pin);

                if (currentAccount != null)
                {
                    Console.WriteLine($"Welcome {currentAccount.Name} \n");

                    await OperationOptions();
                } 
            else
            {
                Console.WriteLine("Incorrect Details! Try again");
                await Task.Delay(DelayTime);

                Console.Clear();
                await Validate();
            }
        }
        public async Task OperationOptions()
        {
           ATMOptions: try
            {


                Console.WriteLine("Choose banking operation \n\n1 : Withdrawal \n2 : Transfers \n3 : Balance Check \n4 : Buy Airtime \n5 : exit  \n0 : Previous Menu");
                int.TryParse((Console.ReadLine()), out int Option);

                switch (Option)
                {
                    case 0:
                        Console.Clear();
                        await _uILogic.LogOutAsync(currentAccount);
                        currentAccount = null;
                        await Validate();
                        break;

                    case 1:
                        Console.Clear();

                        await _uILogic.WithdrawAsync(currentAccount);

                        goto ATMOptions;

                    case 2:
                        Console.Clear();
                        await _uILogic.TransferAsync(currentAccount);
                        goto ATMOptions;

                    case 3:
                        Console.Clear();
                        await _uILogic.CheckBalanceAsync(currentAccount);
                        goto ATMOptions;

                    case 4:
                        Console.Clear();
                        await _uILogic.BuyAirtimeAsync(currentAccount);
                        goto ATMOptions;
                    case 5:
                        Console.WriteLine("Thanks For Using our services");
                        return;

                    default:
                        Console.WriteLine("Incorrect Option");
                        Task.Delay(DelayTime).Wait();
                        Console.Clear();
                        await OperationOptions();
                        break;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            }
        }
    }

