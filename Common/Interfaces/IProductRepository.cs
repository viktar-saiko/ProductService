using Common.Models;

namespace Common.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(string id, Product product, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetListByCategoryAsync(string category, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(string id, CancellationToken cancellationToken);
    }
}
