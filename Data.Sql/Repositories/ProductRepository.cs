using AutoMapper;
using Common.Interfaces;
using Common.Models;
using Data.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Sql.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppDbContext context, IMapper mapper, ILogger<ProductRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = _context.Products.Select(t => _mapper.Map<Product>(t));
            return await products.ToListAsync(cancellationToken);
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            var dtoProduct = _mapper.Map<ProductDto>(product);
            await _context.Products.AddAsync(dtoProduct, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Product>(dtoProduct);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            _context.Products.Remove(new ProductDto { Id = Guid.Parse(id) });
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(string id, Product product, CancellationToken cancellationToken)
        {
            product.Id = id;
            _context.Products.Update(_mapper.Map<ProductDto>(product));
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetListByCategoryAsync(string category, CancellationToken cancellationToken)
        {
            var categoryDto = await _context.Categories.FirstAsync(t => t.Name == category, cancellationToken);

            if (categoryDto == null)
            {
                return Enumerable.Empty<Product>();
            }

            return
                await _context.Products
                //.Where(t => t.ProductCategoriesIds.Contains(categoryDto.Id))
                .Where(t => t.Categories.Any(t => t.Name == categoryDto.Name))
                .Select(t => _mapper.Map<Product>(t))
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(Guid.Parse(id), cancellationToken);
            return _mapper.Map<Product>(product);
        }
    }
}
