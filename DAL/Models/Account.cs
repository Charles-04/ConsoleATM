using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DAL.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public string Pin { get; set; }
        public bool isActive { get; set; }
        public bool isLoggedIn { get; set; }

    }
   
}
