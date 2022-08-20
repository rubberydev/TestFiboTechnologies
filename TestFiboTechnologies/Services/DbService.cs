using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TestFiboTechnologies.Models;

namespace TestFiboTechnologies.Services
{
    public class DbService : IDbService
    {
        bool initialized;
        readonly SQLiteAsyncConnection database;

        public DbService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestOIS.db3");
            database = new SQLiteAsyncConnection(dbPath);
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (!initialized)
                {

                    database.CreateTableAsync<Users>().Wait();
                    database.CreateTableAsync<Charles>().Wait();
                    initialized = true;
                }
            }
            catch (Exception)
            {
                initialized = false;
            }
        }
        #region Users methods
        public Task<List<Users>> GetUsersAsync()=> database.Table<Users>().ToListAsync();
        

        public Task<int> InsertUserAsync(Users item)=>database.InsertAsync(item);
        

        public Task<int> UpdateUserAsync(Users item)=>database.UpdateAsync(item);
        

        public Task<int> DeleteUserAsync(Users item)=>database.DeleteAsync(item);
        #endregion


    }
}
