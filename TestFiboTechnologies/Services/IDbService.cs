using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestFiboTechnologies.Models;

namespace TestFiboTechnologies.Services
{
    public interface IDbService
    {
        Task<List<Animals>> GetItemsAsync();
        Task<int> InsertItemAsync(Animals item);
        Task<int> UpdateItemAsync(Animals item);
        Task<int> DeleteItemAsync(Animals item);
        Task<List<Charles>> GetCorralsAsync();
        Task<Charles> GetCorralAsyncById(int id);
        Task<int> InsertItemAsync(Charles item);
        Task<int> UpdateItemAsync(Charles item);
        Task<int> DeleteItemAsync(Charles item);
    }
}
