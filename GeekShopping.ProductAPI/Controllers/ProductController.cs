using GeekShopping.ProductAPI.Data.DTOs;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace GeekShopping.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private static readonly Counter ProcessedJobCount = Metrics
    .CreateCounter("myapp_jobs_processed_total", "Number of processed jobs.");

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(long productId)
        {
            ProductDTO? product = null;
            ProcessedJobCount.Inc();
            try
            {
                product = await _productRepository.GetProductById(productId);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO product)
        {
            if (product == null) return BadRequest();
            return Ok(await _productRepository.CreateProduct(product));
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO product)
        {
            if (product == null) return BadRequest();
            var updatedProduct = await _productRepository.UpdateProduct(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(long productId)
        {
            await _productRepository.DeleteProduct(productId);
            return Ok();
        }
    }
}
