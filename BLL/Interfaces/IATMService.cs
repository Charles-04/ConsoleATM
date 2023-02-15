using ATM.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
   
    internal interface IATMService
    {
        double WithdrawalLimit { get; set; }
        Account CreateAccount();
        Task<bool> ChangePin();
        

    }
}
