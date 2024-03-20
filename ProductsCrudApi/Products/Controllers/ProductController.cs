using Microsoft.AspNetCore.Mvc;
using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;

namespace ProductsCrudApi.Products.Controllers
{
    [ApiController]
    [Route("product/api/v1")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        private IProductRepository _productRepository;


        public ProductController(ILogger<ProductController> logger,IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {

            var products = await _productRepository.GetAllAsync();
            return Ok(products);

        }

    }
}
