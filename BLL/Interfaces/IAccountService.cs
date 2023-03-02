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

        Task<Account> LogInAsync(long accountNumber, string pin);
        Task<bool> LogOutAsync(long accountNumber);
        Task<Account> GetUserAsync(long accountNumber);

    }
}
