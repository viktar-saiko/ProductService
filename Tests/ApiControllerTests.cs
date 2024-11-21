using Api.Controllers;
using AutoMapper;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Bogus;
using Tests.Faker;

namespace Tests
{
    public class ApiControllerTests
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly Faker<Product> _productFaker;

        public ApiControllerTests()
        {
            _logger = Mock.Of<ILogger<ProductController>>();
            _mapper = Mock.Of<IMapper>();

            _productFaker = ProductFaker.Create(useMongoDb: true);
        }

        [Test]
        public async Task ProductController_GetAll_ReturnsOk()
        {
            var repository = GetMockOfRepositoryWithGettingItemsOf(3);

            var controller = new ProductController(_logger, repository.Object);

            var result = await controller.GetAsync(CancellationToken.None);

            repository.Verify(mock => mock.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once());

            var okResult = result as OkObjectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.TypeOf<List<Product>>());
        }

        [Test]
        public async Task ProductController_GetById_ReturnsOk()
        {
            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(t => t.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_productFaker.Generate());

            var controller = new ProductController(_logger, repositoryMock.Object);

            var result = await controller.GetAsync("2", CancellationToken.None);

            repositoryMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = result as OkObjectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.TypeOf<Product>());
        }

        [Test]
        public async Task ProductController_Add_ReturnsOk()
        {
            var product = _productFaker.Generate();

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(t => t.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var controller = new ProductController(_logger, repositoryMock.Object);

            var result = await controller.AddAsync(product, CancellationToken.None);

            repositoryMock.Verify(mock => mock.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = result as OkObjectResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.TypeOf<Product>());
        }

        [Test]
        public async Task ProductController_Update_ReturnsOk()
        {
            var product = _productFaker.Generate();

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(t => t.UpdateAsync(It.IsAny<string>(), It.IsAny<Product>(), It.IsAny<CancellationToken>()));

            var controller = new ProductController(_logger, repositoryMock.Object);

            var result = await controller.UpdateAsync(product, CancellationToken.None);

            repositoryMock.Verify(mock => mock.UpdateAsync(It.IsAny<string>(), It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = result as OkResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ProductController_Delete_ReturnsOk()
        {
            var product = _productFaker.Generate();

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock
                .Setup(t => t.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()));

            var controller = new ProductController(_logger, repositoryMock.Object);

            var result = await controller.DeleteAsync(product.Id!, CancellationToken.None);

            repositoryMock.Verify(mock => mock.DeleteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = result as OkResult;

            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        private Mock<IProductRepository> GetMockOfRepositoryWithGettingItemsOf(int count)
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(t => t.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_productFaker.Generate(count));

            return productRepositoryMock;
        }
    }
}
