using Api.Controllers;
using AutoMapper;
using Data.MongoDb.Configurations;
using Data.MongoDb.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
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
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var logger = Mock.Of<ILogger<ProductController>>();
            var dbSettings = new Mock<DatabaseSettings>();
            //dbSettings.Setup(t => t.)
            //var repository = new ProductRepository();

            // TODO: implement test
        }
    }
}