using Api.Controllers;
using AutoMapper;
using Data.MongoDb.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Tests
{
    public class DataMongoRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Ignore("Under construction")]
        [Test]
        public void Test1()
        {
            var profile = new Data.MongoDb.Mapper.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile), NullLoggerFactory.Instance);
            var mapper = new Mapper(configuration);

            var logger = Mock.Of<ILogger<ProductController>>();
            var dbSettings = new Mock<DatabaseSettings>();
            //dbSettings.Setup(t => t.)
            //var repository = new ProductRepository();

            // TODO: implement test
        }
    }
}