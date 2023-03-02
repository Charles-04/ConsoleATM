using ATM.BLL.Services;
using ATM.DAL.DBConnection;

namespace ATM.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DbService dbService = new DbService();
            AccountService accountService = new(dbService);
            ATMOptions aTMOptions = new(accountService);
            await aTMOptions.Validate();

            /*DbService dbService = new();
            IDBScript dbScript = new DBScript(dbService);
            await dbScript.CreateDatabaseTablesAsync();*/
           // await dbScript.CreateDBAsync(@"Server=CHARLES\MYDB;Integrated security = True; Initial Catalog = master; Encrypt=False");
        }
    }
    
}