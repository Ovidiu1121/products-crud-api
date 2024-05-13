using Microsoft.AspNetCore.Mvc;
using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Controllers.Interfaces;
using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;
using ProductsCrudApi.Products.Service.Interfaces;
using ProductsCrudApi.System.Exceptions;

namespace ProductsCrudApi.Products.Controllers
{
    public class ProductController : ProductApiController
    {

        private IProductCommandService _productCommandService;
        private IProductQueryService _productQueryService;

        public ProductController(IProductCommandService productCommandService, IProductQueryService productQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
        }
        public override async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductRequest request)
        {
            try
            {
                var products = await _productCommandService.CreateProduct(request);

                return Ok(products);
            }
            catch(InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public override async Task<ActionResult<Product>> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var products = await _productCommandService.UpdateProduct(id,request);

                return Ok(products);
            }catch(InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }catch(ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
              
        }
        public override async Task<ActionResult<Product>> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var product = await _productCommandService.DeleteProduct(id);

                return Accepted("", product);
            }
            catch (ItemDoesNotExist ex)
            {
               return NotFound(ex.Message);
            }

      
        }
        public override async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            try
            {
                var products = await _productQueryService.GetAllProducts();
                return Ok(products);
            }
            catch(ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }
        public override async Task<ActionResult<Product>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var products = await _productQueryService.GetByName(name);
                return Ok(products);
            }
            catch (ItemDoesNotExist ex)
            {
               return NotFound(ex.Message);
            }

        }
        public override async Task<ActionResult<List<int>>> GetIdOrderedByPriceDesc()
        {
            try
            {
                List<int> products = await _productQueryService.GetIdOrderedByPriceDesc();
                return Ok(products);
            }
            catch(ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

    }
}
