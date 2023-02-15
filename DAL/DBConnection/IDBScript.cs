using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DAL.DBConnection
{
    internal interface IDBScript
    {
        Task CreateDBAsync(string serverName);
        Task CreateDatabaseTablesAsync();
        
    }
}
