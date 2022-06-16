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
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TesFibo.db3");
            database = new SQLiteAsyncConnection(dbPath);
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (!initialized)
                {

                    database.CreateTableAsync<Animals>().Wait();
                    database.CreateTableAsync<Charles>().Wait();
                    initialized = true;
                }
            }
            catch (Exception)
            {
                initialized = false;
            }
        }
        #region Animals methods
        public Task<List<Animals>> GetItemsAsync()=> database.Table<Animals>().ToListAsync();
        

        public Task<int> InsertItemAsync(Animals item)=>database.InsertAsync(item);
        

        public Task<int> UpdateItemAsync(Animals item)=>database.UpdateAsync(item);
        

        public Task<int> DeleteItemAsync(Animals item)=>database.DeleteAsync(item);
        #endregion


        #region Corrals methods
        public Task<List<Charles>> GetCorralsAsync()=> database.Table<Charles>().ToListAsync();

        /// <summary>
        /// get corrals by relationship to know maximum capacity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Charles> GetCorralAsyncById(int id)=>database.Table<Charles>().Where(i => i.ID == id).FirstOrDefaultAsync();
        

        public Task<int> InsertItemAsync(Charles item)=> database.InsertAsync(item);
        

        public Task<int> UpdateItemAsync(Charles item)=> database.UpdateAsync(item);
        

        public Task<int> DeleteItemAsync(Charles item)=>database.DeleteAsync(item);
        #endregion

    }
}
