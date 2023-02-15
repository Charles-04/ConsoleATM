using ATM.DAL.DBConnection;

namespace ATM.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DbService dbService = new();
            IDBScript dbScript = new DBScript(dbService);
            await dbScript.CreateDatabaseTablesAsync();
           // await dbScript.CreateDBAsync(@"Server=CHARLES\MYDB;Integrated security = True; Initial Catalog = master; Encrypt=False");
        }
    }
    
}