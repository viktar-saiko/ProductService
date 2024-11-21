using AutoMapper;
using Common.Interfaces;
using Common.Models;
using Data.MongoDb.Configurations;
using Data.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Data.MongoDb.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ProductDto> _products;
        private readonly IMapper _mapper;
        private readonly IDatabaseSettings _dbSettings;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IDatabaseSettings settings, IMongoClient client, IMapper mapper, ILogger<ProductRepository> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _dbSettings = settings;
            _database = client.GetDatabase(_dbSettings.DatabaseName);
            _products = _database.GetCollection<ProductDto>(_dbSettings.CollectionName);
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<ProductDto>(product);
            await _products.InsertOneAsync(productEntity, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
 
            return _mapper.Map<Product>(productEntity);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<ProductDto>().Eq(t => t.Id, new MongoDB.Bson.ObjectId(id));
            await _products.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task UpdateAsync(string id, Product product, CancellationToken cancellationToken)
        {
            product.Id = id;
            ProductDto productEntity = _mapper.Map<ProductDto>(product);
            await _products.ReplaceOneAsync(t => t.Id == productEntity.Id, productEntity, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _products.Find(t => true).ToListAsync(cancellationToken);
            return list.Select(t => _mapper.Map<Product>(t));
        }

        public async Task<Product> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var filter = new FilterDefinitionBuilder<ProductDto>().Eq(t => t.Id, new MongoDB.Bson.ObjectId(id));
            var item = await _products.Find(filter).SingleOrDefaultAsync(cancellationToken);
            return _mapper.Map<Product>(item);
        }

        public async Task<IEnumerable<Product>> GetListByCategoryAsync(string category, CancellationToken cancellation)
        {
            FilterDefinition<ProductDto> filter = new FilterDefinitionBuilder<ProductDto>().Where(t => t.CategoryNames.Contains(category));
            var list = await _products.Find(filter).ToListAsync();
            return list.Select(t => _mapper.Map<Product>(t));
        }
    }
}
