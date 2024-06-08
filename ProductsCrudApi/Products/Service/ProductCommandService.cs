using ProductsCrudApi.Dto;
using ProductsCrudApi.Products.Model;
using ProductsCrudApi.Products.Repository.interfaces;
using ProductsCrudApi.Products.Service.Interfaces;
using ProductsCrudApi.System.Constant;
using ProductsCrudApi.System.Exceptions;

namespace ProductsCrudApi.Products.Service
{
    public class ProductCommandService:IProductCommandService
    {
        private IProductRepository _repository;

        public ProductCommandService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateProduct(CreateProductRequest productRequest)
        {
            if (productRequest.Price<0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            if (productRequest.Stock<0)
            {
                throw new InvalidStock(Constants.INVALID_STOCK);
            }

            Product product = await _repository.GetByNameAsync(productRequest.Name);

            if (product!=null)
            {
                throw new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXIST);
            }

            product=await _repository.CreateProduct(productRequest);
            return product;

        }

        public async Task<Product> UpdateProduct(int id, UpdateProductRequest productRequest)
        {
            if (productRequest.Price<0)
            {
                throw new InvalidPrice(Constants.INVALID_PRICE);
            }

            if (productRequest.Stock<0)
            {
                throw new InvalidStock(Constants.INVALID_STOCK);
            }

            Product product = await _repository.GetByIdAsync(id);

            if (product==null)
            {
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            product = await _repository.UpdateProduct(id,productRequest);
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product product=await _repository.GetByIdAsync(id);

            if (product==null){
                throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
            }

            await _repository.DeleteProductById(id);
            return product;
        }
    }
}
