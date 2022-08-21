using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestFiboTechnologies.Models;

namespace TestFiboTechnologies.Services
{
    public interface IDbService
    {
        Task<List<Users>> GetUsersAsync();
        Task<int> InsertUserAsync(Users item);
        Task<int> UpdateUserAsync(Users item);
        Task<int> DeleteUserAsync(Users item);
        Task<List<ProductsDbModel>> GetProductsAsync();
        Task<int> InsertProductAsync(ProductsDbModel item);
        Task<int> UpdateProductAsync(ProductsDbModel item);
        Task<int> DeleteProductAsync(ProductsDbModel item);
        Task<RatingDbModel> GetRatingAsync(int id);
        Task<int> InsertRatingAsync(RatingDbModel item);
        Task<int> UpdateRatingAsync(RatingDbModel item);
        Task<int> DeleteRatingAsync(RatingDbModel item);
    }
}
