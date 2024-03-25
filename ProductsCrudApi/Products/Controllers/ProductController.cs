using Microsoft.AspNetCore.Mvc;
using ProductsCrudApi.Dto;
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

        //EXISTA 2 METODE

        //1.PRIN ROUTE

        [HttpGet("/find/{name}")]
        public async Task<ActionResult<Product>> GetByNameRoute([FromRoute]string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            return Ok(products);
        }

        //2.PRIN QUERY

        [HttpGet("/find")]
        public async Task<ActionResult<Product>>GetByNameQuery([FromQuery]string name)
        {
            var products = await _productRepository.GetByNameAsync(name);
            return Ok(products);
        }

        [HttpGet("/idByPrice")]
        public async Task<ActionResult<Product>> GetIdOrderedByPriceDesc()
        {
            var products = await _productRepository.GetIdOrderedByPriceDescAsync();
            return Ok(products);
        }

        [HttpPost("/createProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductRequest request)
        {
            var products = await _productRepository.CreateProduct(request);

            return Ok(products);
        }

        [HttpPut("/updateProduct")]
        public async Task<ActionResult<Product>> UpdateProduct([FromQuery]int id,UpdateProductRequest request)
        {
            var products = await _productRepository.UpdateProduct(id, request);

            return Ok(products);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Product>>DeleteProduct([FromQuery]int id)
        {
            var product = await _productRepository.DeleteProductById(id);

            return Ok(product);
        }

    }
}
