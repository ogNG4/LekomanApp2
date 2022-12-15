using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LekomanApp.Models;
using LekomanApp;
using SQLite;



namespace LekomanApp.Data
{
    public class LekomanItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<LekomanItemDatabase> Instance =
            new AsyncLazy<LekomanItemDatabase>(async () =>
            {
                var instance = new LekomanItemDatabase();
                try
                {
                    CreateTableResult result = await Database.CreateTableAsync<LekomanItem>();
                }
                catch (Exception ex)
                {

                    throw;
                }
                return instance;


            });


        public LekomanItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

     

        public Task<List<LekomanItem>> GetItemsAsync()
        {
            return Database.Table<LekomanItem>().ToListAsync();
        }

        public Task<List<LekomanItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<LekomanItem>("SELECT * FROM [LekomanItem] WHERE [Zrobione] = 0");
        }


      


        public Task<LekomanItem> GetItemAsync(int id)
        {
            return Database.Table<LekomanItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(LekomanItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {

              
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(LekomanItem item)
        {
            return Database.DeleteAsync(item);
        }


    }
}