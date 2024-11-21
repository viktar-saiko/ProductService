using Common.Models;

namespace Data.Sql.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task CreateAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeleteAsync(ProductCategory category);
    }
}
