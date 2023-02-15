using ATM.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.BLL.Interfaces
{
    public interface IAccountService
    {

        Task<Account> LogIn(long accountNumber, string pin);
        Task<bool> LogOut(long accountNumber);
        Task<Account> GetUser(long accountNumber);

    }
}
