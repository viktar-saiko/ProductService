using AutoMapper;
using Common.Models;
using Data.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            var categories = _context.Categories.Select(t => _mapper.Map<ProductCategory>(t));
            return await categories.ToListAsync();
        }

        public async Task CreateAsync(ProductCategory category)
        {
            await _context.Categories.AddAsync(_mapper.Map<CategoryDto>(category));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductCategory category)
        {
            _context.Categories.Remove(_mapper.Map<CategoryDto>(category));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategory category)
        {
            _context.Categories.Update(_mapper.Map<CategoryDto>(category));
            await _context.SaveChangesAsync();
        }
    }
}
