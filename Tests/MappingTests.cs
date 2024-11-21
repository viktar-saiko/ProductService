using AutoMapper;
using Bogus;
using Common.Models;
using MongoDB.Bson;
using Tests.Faker;

namespace Tests
{
    public class MappingTests
    {
        private Faker<Product> _productWithMongoDbMock;
        private Faker<Product> _productWithSqlMock;

        public MappingTests()
        {
            _productWithMongoDbMock = ProductFaker.Create(useMongoDb: true);
            _productWithSqlMock = ProductFaker.Create(useMongoDb: false);
        }

        [Test]
        public void DataMongo_MappingToDto_CorrectMapping()
        {
            var profile = new Data.MongoDb.Mapper.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var product = GetProductWithMongo();

            var result = mapper.Map<Data.MongoDb.Entities.ProductDto>(product);

            Assert.That(result?.Id, Is.Not.Null);
            Assert.That(result.Id.ToString(), Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.Price, Is.EqualTo(product.Price));
            Assert.That(result.Quantity, Is.EqualTo(product.Quantity));
            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags, Is.EquivalentTo(product.Tags));
            Assert.That(result.CategoryNames, Is.Not.Null);
            Assert.That(result.CategoryNames, Is.EquivalentTo(product.Categories));
        }

        [Test]
        public void DataMongo_MappingFromDto_CorrectMapping()
        {
            var profile = new Data.MongoDb.Mapper.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var product = GetProductDtoWithMongo();

            var result = mapper.Map<Product>(product);

            Assert.That(result?.Id, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(product.Id.ToString()));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.Price, Is.EqualTo(product.Price));
            Assert.That(result.Quantity, Is.EqualTo(product.Quantity));
            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags, Is.EquivalentTo(product.Tags!));
            Assert.That(result.Categories, Is.Not.Null);
            Assert.That(result.Categories, Is.EquivalentTo(product.CategoryNames));
        }

        [Test]
        public void DataSql_MappingToDto_CorrectMapping()
        {
            var profile = new Data.Sql.Mapper.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var product = GetProductWithSql();

            var result = mapper.Map<Data.Sql.Entities.ProductDto>(product);

            Assert.That(result?.Id, Is.Not.Null);
            Assert.That(result.Id.ToString(), Is.EqualTo(product.Id));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.Price, Is.EqualTo(product.Price));
            Assert.That(result.Quantity, Is.EqualTo(product.Quantity));
            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags.Split(',').ToList(), Is.EquivalentTo(product.Tags));
            Assert.That(result.Categories, Is.Not.Null);
            Assert.That(result.Categories.Select(t => t.Name), Is.EquivalentTo(product.Categories));
        }

        [Test]
        public void DataSql_MappingFromDto_CorrectMapping()
        {
            var profile = new Data.Sql.Mapper.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var product = GetProductDtoWithSql();

            var result = mapper.Map<Product>(product);

            Assert.That(result?.Id, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(product.Id.ToString()));
            Assert.That(result.Name, Is.EqualTo(product.Name));
            Assert.That(result.Price, Is.EqualTo(product.Price));
            Assert.That(result.Quantity, Is.EqualTo(product.Quantity));
            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags, Is.EquivalentTo(product.Tags!.Split(',')));
            Assert.That(result.Categories, Is.Not.Null);
            Assert.That(result.Categories, Is.EquivalentTo(product.Categories.Select(t => t.Name)));
        }


        private Product GetProductWithMongo()
        {
            var product = _productWithMongoDbMock.Generate();
            return product;
        }

        private Product GetProductWithSql()
        {
            var product = _productWithSqlMock.Generate();
            return product;
        }

        private Data.MongoDb.Entities.ProductDto GetProductDtoWithMongo()
        {
            return
                new Data.MongoDb.Entities.ProductDto
                {
                    Id = new ObjectId(),
                    Name = "nameProduct",
                    Price = 111.45m,
                    Quantity = 4,
                    Tags = new[]
                    {
                        "tag1",
                        "tag2"
                    },
                    CategoryNames = new[]
                    {
                        "Category1",
                        "Category2"
                    }
                };
        }

        private Data.Sql.Entities.ProductDto GetProductDtoWithSql()
        {
            return
                new Data.Sql.Entities.ProductDto
                {
                    Id = new Guid(),
                    Name = "nameProduct",
                    Price = 111.45m,
                    Quantity = 4,
                    Tags = "tag1,tag2",
                    Categories = new List<Data.Sql.Entities.CategoryDto>
                    {
                        new() { Id = 1, Name = "Category1" },
                        new() { Id = 2, Name = "Category2" }
                    }
                };
        }
    }
}
