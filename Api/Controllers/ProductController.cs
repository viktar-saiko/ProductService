using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _repository;

        public ProductController(ILogger<ProductController> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            try
            {
                var list = await _repository.GetAllAsync(cancellationToken);
                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _repository.GetByIdAsync(id, cancellationToken);
                return Ok(list);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]Product product, CancellationToken cancellationToken)
        {
            try
            {
                Product item = await _repository.AddAsync(product, cancellationToken);
                return Ok(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody]Product product, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.UpdateAsync(product.Id!, product, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteAsync(id, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}
