using Microsoft.AspNetCore.Mvc;
using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Model;

namespace ProductsCrudApi.Products.Controllers.Interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ProductApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Product>))]
        [ProducesResponseType(statusCode:404,type:typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Product>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductRequest productRequest);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Product))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Product>> UpdateProduct([FromRoute] int id,[FromBody] UpdateProductRequest productRequest);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Product))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Product>> DeleteProduct([FromRoute] int id);

        [HttpGet("{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Product))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Product>> GetByNameRoute([FromRoute] string name);

        [HttpGet("idOrderedByPriceDescAsync")]
        [ProducesResponseType(statusCode: 202, type: typeof(List<int>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<List<int>>> GetIdOrderedByPriceDesc();

    }
}
